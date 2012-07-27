using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CryEngine.RC.Pipeline.Properties;
using ManagedFbx;

namespace CryEngine.RC.Pipeline
{
	public partial class ControlForm : Form
	{
		public static readonly string ExtensionFilter = "*.fbx";

		private DirectoryInfo m_assetDir;
		private DirectoryInfo m_mirrorDir;
		private DirectoryInfo m_rootDir;

		private BackgroundWorker m_worker;

		public ControlForm()
		{
			InitializeComponent();

// If you're debugging this, just set this to whatever you like
#if DEBUG
			var path = @"C:\Dev\CryENGINE3\Tools";
#else
			var path = Application.StartupPath;
#endif

			m_rootDir = new DirectoryInfo(path).Parent;
			m_assetDir = new DirectoryInfo(Path.Combine(m_rootDir.FullName, "Assets"));
			m_mirrorDir = new DirectoryInfo(Path.Combine(m_rootDir.FullName, "Game", "Objects"));

			if(!m_assetDir.Exists)
				m_assetDir.Create();

			PopulateTree();

			Shown += (s, e) =>
			{
				if(m_assetDir.GetFiles().Length == 0 && m_assetDir.GetDirectories().Length == 0)
					MessageBox.Show(string.Format("Your Assets directory, {0}, is currently empty. To get started, save your source files there.", m_assetDir.FullName), "Getting Started");
			};
		}

		private void PopulateTree()
		{
			uxAssetTree.BeginUpdate();
			uxAssetTree.Nodes.Clear();

			foreach(var dir in m_assetDir.GetDirectories())
				uxAssetTree.Nodes.Add(ProcessDirectory(dir));

			foreach(var file in m_assetDir.GetFiles(ExtensionFilter))
			{
				var fileNode = new TreeNode(file.Name) { Tag = file };
				uxAssetTree.Nodes.Add(fileNode);
			}

			uxAssetTree.EndUpdate();
		}

		private void CompileAll(object sender, EventArgs e)
		{
			CompileNonBlocking(m_assetDir.GetFiles(ExtensionFilter, SearchOption.AllDirectories));
		}

		private void CompileNonBlocking(params FileInfo[] files)
		{
			uxLog.Clear();

			m_worker = new BackgroundWorker { WorkerReportsProgress = true };

			m_worker.DoWork += (s, args) =>
			{
				var timer = Stopwatch.StartNew();

				CompileSelection(files);

				timer.Stop();
				m_worker.ReportProgress(0, string.Format("Finished build in {0}s", timer.Elapsed.Seconds));
			};

			m_worker.ProgressChanged += (s, args) =>
			{
				uxLog.AppendText(args.UserState.ToString());
				uxLog.AppendText(Environment.NewLine);
			};

			m_worker.RunWorkerAsync();
		}

		private void CompileSelection(params FileInfo[] files)
		{
			foreach(var file in files)
			{
				var scene = Scene.Import(file.FullName);
				scene.ConvertAxes(AxisConversionType.Max);
				scene.RootNode.Scale *= (float)uxScaleUpDown.Value;
				scene.BakeTransform(scene.RootNode);

				var node = scene.RootNode.ChildNodes.FirstOrDefault(n => n.Attributes.Any(a => a.Type == NodeAttributeType.Mesh));

				if(node == default(SceneNode))
					return;

				var outputFile = GetMirrorFile(file);
				var outputDir = outputFile.Directory;

				if(!outputDir.Exists)
					outputDir.Create();

				var daeFile = new FileInfo(outputFile.FullName.ToLower().Replace(".fbx", ".dae"));
				var cgfFile = new FileInfo(daeFile.FullName.Replace(".dae", ".cgf"));

				var originalTime = cgfFile.LastWriteTime;

				FbxConverter.ToCollada(node.Mesh, daeFile);
				ColladaConverter.CEPath = m_rootDir;
				var finalFile = ColladaConverter.ToCgf(daeFile);

				try
				{
					File.Delete(daeFile + ".rcdone");
				}
				catch(IOException)
				{
				}

				if(!finalFile.Exists)
				{
					m_worker.ReportProgress(0, "Failed to compile file: " + file.FullName);
				}
				else if(originalTime == finalFile.LastWriteTime)
				{
					m_worker.ReportProgress(0, "Failed to compile file but a CGF already exists: " + file.FullName);
				}
				else
				{
					m_worker.ReportProgress(0, "Successfully converted: " + file.FullName);
				}

				daeFile.Delete();
			}
		}

		private TreeNode ProcessDirectory(DirectoryInfo directoryInfo)
		{
			var directoryNode = new TreeNode(directoryInfo.Name) { Tag = directoryInfo };

			foreach(var directory in directoryInfo.GetDirectories())
			{
				var dirNode = ProcessDirectory(directory);
				directoryNode.Nodes.Add(dirNode);
			}

			foreach(var file in directoryInfo.GetFiles(ExtensionFilter))
			{
				var fileNode = new TreeNode(file.Name) { Tag = file };
				directoryNode.Nodes.Add(fileNode);
			}

			return directoryNode;
		}

		private FileInfo GetMirrorFile(FileInfo source)
		{
			return new FileInfo(source.FullName.Replace(m_assetDir.FullName, m_mirrorDir.FullName));
		}

		private void RefreshTree(object sender, EventArgs e)
		{
			PopulateTree();
		}

		private void ContextCompile(object sender, EventArgs e)
		{
			if(uxAssetTree.SelectedNode == null)
				return;

			if(m_worker != null && m_worker.IsBusy)
			{
				MessageBox.Show("Please wait for the current build to finish.", "Operation in progress");
				return;
			}

			var tag = uxAssetTree.SelectedNode.Tag;

			var file = tag as FileInfo;
			var folder = tag as DirectoryInfo;

			if(file != null)
				CompileNonBlocking(file);
			else if(folder != null)
				CompileNonBlocking(folder.GetFiles(ExtensionFilter, SearchOption.AllDirectories));
		}

		private void FormClose(object sender, FormClosingEventArgs e)
		{
			Settings.Default.Save();
		}
	}
}
