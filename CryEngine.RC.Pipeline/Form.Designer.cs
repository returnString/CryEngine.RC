namespace CryEngine.RC.Pipeline
{
	partial class ControlForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.uxAssetTree = new System.Windows.Forms.TreeView();
			this.uxAssetContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.uxLog = new System.Windows.Forms.TextBox();
			this.uxCompileAll = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.uxUVUpDown = new System.Windows.Forms.NumericUpDown();
			this.uxScaleUpDown = new System.Windows.Forms.NumericUpDown();
			this.uxAssetContext.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uxUVUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uxScaleUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// uxAssetTree
			// 
			this.uxAssetTree.ContextMenuStrip = this.uxAssetContext;
			this.uxAssetTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxAssetTree.Location = new System.Drawing.Point(0, 0);
			this.uxAssetTree.Name = "uxAssetTree";
			this.uxAssetTree.Size = new System.Drawing.Size(269, 513);
			this.uxAssetTree.TabIndex = 0;
			// 
			// uxAssetContext
			// 
			this.uxAssetContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
			this.uxAssetContext.Name = "contextMenuStrip1";
			this.uxAssetContext.Size = new System.Drawing.Size(108, 26);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.exportToolStripMenuItem.Text = "Export";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.ContextCompile);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.uxAssetTree);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Panel2.Controls.Add(this.uxUVUpDown);
			this.splitContainer1.Panel2.Controls.Add(this.button1);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.uxScaleUpDown);
			this.splitContainer1.Panel2.Controls.Add(this.uxLog);
			this.splitContainer1.Panel2.Controls.Add(this.uxCompileAll);
			this.splitContainer1.Size = new System.Drawing.Size(772, 513);
			this.splitContainer1.SplitterDistance = 269;
			this.splitContainer1.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(312, 490);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(89, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Refresh Tree";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.RefreshTree);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 495);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Scale Factor:";
			// 
			// uxLog
			// 
			this.uxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uxLog.Location = new System.Drawing.Point(2, 0);
			this.uxLog.Multiline = true;
			this.uxLog.Name = "uxLog";
			this.uxLog.ReadOnly = true;
			this.uxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.uxLog.Size = new System.Drawing.Size(494, 481);
			this.uxLog.TabIndex = 1;
			// 
			// uxCompileAll
			// 
			this.uxCompileAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.uxCompileAll.Location = new System.Drawing.Point(407, 490);
			this.uxCompileAll.Name = "uxCompileAll";
			this.uxCompileAll.Size = new System.Drawing.Size(89, 23);
			this.uxCompileAll.TabIndex = 0;
			this.uxCompileAll.Text = "Compile Build";
			this.uxCompileAll.UseVisualStyleBackColor = true;
			this.uxCompileAll.Click += new System.EventHandler(this.CompileAll);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(151, 495);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "UV Layer:";
			// 
			// uxUVUpDown
			// 
			this.uxUVUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.uxUVUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CryEngine.RC.Pipeline.Properties.Settings.Default, "UVLayer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.uxUVUpDown.Location = new System.Drawing.Point(211, 493);
			this.uxUVUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.uxUVUpDown.Name = "uxUVUpDown";
			this.uxUVUpDown.Size = new System.Drawing.Size(66, 20);
			this.uxUVUpDown.TabIndex = 6;
			this.uxUVUpDown.Value = global::CryEngine.RC.Pipeline.Properties.Settings.Default.UVLayer;
			// 
			// uxScaleUpDown
			// 
			this.uxScaleUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.uxScaleUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CryEngine.RC.Pipeline.Properties.Settings.Default, "ScaleFactor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.uxScaleUpDown.DecimalPlaces = 2;
			this.uxScaleUpDown.Location = new System.Drawing.Point(79, 493);
			this.uxScaleUpDown.Name = "uxScaleUpDown";
			this.uxScaleUpDown.Size = new System.Drawing.Size(66, 20);
			this.uxScaleUpDown.TabIndex = 3;
			this.uxScaleUpDown.Value = global::CryEngine.RC.Pipeline.Properties.Settings.Default.ScaleFactor;
			// 
			// ControlForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(796, 537);
			this.Controls.Add(this.splitContainer1);
			this.Name = "ControlForm";
			this.Text = "CryENGINE3 Art Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClose);
			this.uxAssetContext.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uxUVUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uxScaleUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView uxAssetTree;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button uxCompileAll;
		private System.Windows.Forms.TextBox uxLog;
		private System.Windows.Forms.ContextMenuStrip uxAssetContext;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown uxScaleUpDown;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown uxUVUpDown;
	}
}

