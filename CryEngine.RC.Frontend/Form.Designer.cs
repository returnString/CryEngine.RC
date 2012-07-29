namespace CryEngine.RC.Frontend
{
	partial class MainForm
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.uxLog = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.uxSourceTextbox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.uxUVLayer = new System.Windows.Forms.NumericUpDown();
			this.uxScaleUpDown = new System.Windows.Forms.NumericUpDown();
			this.uxEngineTextbox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.uxUVLayer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uxScaleUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(366, 9);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(122, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Select FBX File";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.SelectFile);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(366, 329);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(122, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "Export";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Export);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Location = new System.Drawing.Point(366, 36);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(122, 23);
			this.button3.TabIndex = 5;
			this.button3.Text = "Select Engine Folder";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.SelectFolder);
			// 
			// uxLog
			// 
			this.uxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uxLog.Location = new System.Drawing.Point(12, 65);
			this.uxLog.Multiline = true;
			this.uxLog.Name = "uxLog";
			this.uxLog.ReadOnly = true;
			this.uxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.uxLog.Size = new System.Drawing.Size(476, 258);
			this.uxLog.TabIndex = 6;
			this.uxLog.Text = "Waiting for input.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 334);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Scale Factor";
			// 
			// uxSourceTextbox
			// 
			this.uxSourceTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uxSourceTextbox.Location = new System.Drawing.Point(12, 12);
			this.uxSourceTextbox.Name = "uxSourceTextbox";
			this.uxSourceTextbox.Size = new System.Drawing.Size(348, 20);
			this.uxSourceTextbox.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(164, 334);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "UV Layer";
			// 
			// uxUVLayer
			// 
			this.uxUVLayer.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CryEngine.RC.Frontend.Properties.Settings.Default, "UVLayer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.uxUVLayer.Location = new System.Drawing.Point(221, 332);
			this.uxUVLayer.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.uxUVLayer.Name = "uxUVLayer";
			this.uxUVLayer.Size = new System.Drawing.Size(73, 20);
			this.uxUVLayer.TabIndex = 9;
			this.uxUVLayer.Value = global::CryEngine.RC.Frontend.Properties.Settings.Default.UVLayer;
			// 
			// uxScaleUpDown
			// 
			this.uxScaleUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CryEngine.RC.Frontend.Properties.Settings.Default, "ScaleFactor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.uxScaleUpDown.DecimalPlaces = 2;
			this.uxScaleUpDown.Location = new System.Drawing.Point(85, 332);
			this.uxScaleUpDown.Name = "uxScaleUpDown";
			this.uxScaleUpDown.Size = new System.Drawing.Size(73, 20);
			this.uxScaleUpDown.TabIndex = 7;
			this.uxScaleUpDown.Value = global::CryEngine.RC.Frontend.Properties.Settings.Default.ScaleFactor;
			// 
			// uxEngineTextbox
			// 
			this.uxEngineTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uxEngineTextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CryEngine.RC.Frontend.Properties.Settings.Default, "ProjectPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.uxEngineTextbox.Location = new System.Drawing.Point(12, 38);
			this.uxEngineTextbox.Name = "uxEngineTextbox";
			this.uxEngineTextbox.Size = new System.Drawing.Size(348, 20);
			this.uxEngineTextbox.TabIndex = 4;
			this.uxEngineTextbox.Text = global::CryEngine.RC.Frontend.Properties.Settings.Default.ProjectPath;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 364);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.uxUVLayer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.uxScaleUpDown);
			this.Controls.Add(this.uxLog);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.uxEngineTextbox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.uxSourceTextbox);
			this.Name = "MainForm";
			this.Text = "CryEngine FBX Converter";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClose);
			((System.ComponentModel.ISupportInitialize)(this.uxUVLayer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uxScaleUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox uxSourceTextbox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox uxEngineTextbox;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox uxLog;
		private System.Windows.Forms.NumericUpDown uxScaleUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown uxUVLayer;

	}
}

