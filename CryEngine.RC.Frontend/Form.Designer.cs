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
			this.uxSourceTextbox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.uxEngineTextbox = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.uxLog = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
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
			// uxEngineTextbox
			// 
			this.uxEngineTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uxEngineTextbox.Location = new System.Drawing.Point(12, 38);
			this.uxEngineTextbox.Name = "uxEngineTextbox";
			this.uxEngineTextbox.Size = new System.Drawing.Size(348, 20);
			this.uxEngineTextbox.TabIndex = 4;
			this.uxEngineTextbox.TextChanged += new System.EventHandler(this.FolderChanged);
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 364);
			this.Controls.Add(this.uxLog);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.uxEngineTextbox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.uxSourceTextbox);
			this.Name = "MainForm";
			this.Text = "CryEngine FBX Converter";
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

	}
}

