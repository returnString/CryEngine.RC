using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CryEngine.RC.Frontend.Properties;
using ManagedFbx;

namespace CryEngine.RC.Frontend
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void SelectFile(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog() { Filter = "Autodesk FBX (*.fbx)|*.fbx" };

			if(dialog.ShowDialog() == DialogResult.OK)
				uxSourceTextbox.Text = dialog.FileName;
		}

		private void SelectFolder(object sender, EventArgs e)
		{
			var dialog = new FolderBrowserDialog();

			if(dialog.ShowDialog() == DialogResult.OK)
				uxEngineTextbox.Text = dialog.SelectedPath;
		}

		private void Export(object sender, EventArgs e)
		{
			if(!File.Exists(Path.Combine(Settings.Default.ProjectPath, "Bin32", "rc", "rc.exe")))
			{
				MessageBox.Show("Invalid project folder specified, couldn't locate the resource compiler.");
				return;
			}

			if(!File.Exists(uxSourceTextbox.Text) || new FileInfo(uxSourceTextbox.Text).Extension.ToLower() != ".fbx")
			{
				MessageBox.Show("Nonexistent file specified, or file is not an FBX file.");
				return;
			}

			var dialog = new SaveFileDialog { Filter = "CryENGINE Model (*.cgf)|*.cgf" };

			if(dialog.ShowDialog() == DialogResult.OK)
			{
				var scene = Scene.Import(uxSourceTextbox.Text);
				scene.ConvertAxes(AxisConversionType.Max);
				scene.RootNode.Scale *= (float)uxScaleUpDown.Value;
				scene.BakeTransform(scene.RootNode);

				var node = scene.RootNode.ChildNodes.FirstOrDefault(n => n.Attributes.Any(a => a.Type == NodeAttributeType.Mesh));

				if(node == default(SceneNode))
				{
					MessageBox.Show("No mesh found in the FBX scene; ensure your mesh is not parented.");
					return;
				}

				var writer = new StringWriter();
				Log.Write = writer.WriteLine;

				var output = new FileInfo(dialog.FileName);
				var dae = new FileInfo(output.FullName.ToLower().Replace(".cgf", ".dae"));

				FbxConverter.ToCollada(node.Mesh, dae);
				ColladaConverter.CEPath = new DirectoryInfo(Settings.Default.ProjectPath);
				ColladaConverter.ToCgf(dae);

				dae.Delete();
				File.Delete(dae + ".rcdone");
				
				uxLog.Text = writer.ToString();
				var selected = ActiveControl;
				ActiveControl = uxLog;
				uxLog.SelectionStart = 0;
				uxLog.ScrollToCaret();
				ActiveControl = selected;
			}
		}

		private void FormClose(object sender, FormClosingEventArgs e)
		{
			Settings.Default.Save();
		}
	}
}
