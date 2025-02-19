
namespace ReShade_Centralized
{
    partial class Form1
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.installuwp = new System.Windows.Forms.Button();
            this.installwin32 = new System.Windows.Forms.Button();
            this.updatereshade = new System.Windows.Forms.Button();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.updateShaders = new System.Windows.Forms.Button();
            this.pbar2 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizePathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reinstallAllReshadeiniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.labelReshade = new System.Windows.Forms.Label();
            this.labelShaders = new System.Windows.Forms.Label();
            this.updateLink = new System.Windows.Forms.LinkLabel();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // installuwp
            // 
            this.installuwp.Location = new System.Drawing.Point(151, 229);
            this.installuwp.Name = "installuwp";
            this.installuwp.Size = new System.Drawing.Size(189, 49);
            this.installuwp.TabIndex = 2;
            this.installuwp.Text = "Install to Microsoft Store";
            this.installuwp.UseVisualStyleBackColor = true;
            this.installuwp.Click += new System.EventHandler(this.installuwp_Click);
            // 
            // installwin32
            // 
            this.installwin32.Location = new System.Drawing.Point(151, 174);
            this.installwin32.Name = "installwin32";
            this.installwin32.Size = new System.Drawing.Size(189, 49);
            this.installwin32.TabIndex = 3;
            this.installwin32.Text = "Install to Game";
            this.installwin32.UseVisualStyleBackColor = true;
            this.installwin32.Click += new System.EventHandler(this.installwin32_Click);
            // 
            // updatereshade
            // 
            this.updatereshade.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.updatereshade.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.updatereshade.ForeColor = System.Drawing.Color.Black;
            this.updatereshade.Location = new System.Drawing.Point(53, 59);
            this.updatereshade.Name = "updatereshade";
            this.updatereshade.Size = new System.Drawing.Size(189, 28);
            this.updatereshade.TabIndex = 5;
            this.updatereshade.Text = "Update ReShade";
            this.updatereshade.UseVisualStyleBackColor = false;
            this.updatereshade.Click += new System.EventHandler(this.updatereshade_Click);
            // 
            // pbar
            // 
            this.pbar.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pbar.Location = new System.Drawing.Point(53, 93);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(189, 23);
            this.pbar.TabIndex = 6;
            this.pbar.Click += new System.EventHandler(this.pbar_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // updateShaders
            // 
            this.updateShaders.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.updateShaders.ForeColor = System.Drawing.Color.Black;
            this.updateShaders.Location = new System.Drawing.Point(248, 59);
            this.updateShaders.Name = "updateShaders";
            this.updateShaders.Size = new System.Drawing.Size(189, 28);
            this.updateShaders.TabIndex = 7;
            this.updateShaders.Text = "Update Shaders";
            this.updateShaders.UseVisualStyleBackColor = false;
            this.updateShaders.Click += new System.EventHandler(this.updateShaders_Click);
            // 
            // pbar2
            // 
            this.pbar2.BackColor = System.Drawing.SystemColors.InfoText;
            this.pbar2.Location = new System.Drawing.Point(248, 93);
            this.pbar2.Name = "pbar2";
            this.pbar2.Size = new System.Drawing.Size(189, 23);
            this.pbar2.TabIndex = 8;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(491, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizePathsToolStripMenuItem,
            this.reinstallAllReshadeiniToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 22);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // customizePathsToolStripMenuItem
            // 
            this.customizePathsToolStripMenuItem.Name = "customizePathsToolStripMenuItem";
            this.customizePathsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.customizePathsToolStripMenuItem.Text = "Customize Paths";
            this.customizePathsToolStripMenuItem.Click += new System.EventHandler(this.customizePathsToolStripMenuItem_Click);
            // 
            // reinstallAllReshadeiniToolStripMenuItem
            // 
            this.reinstallAllReshadeiniToolStripMenuItem.Name = "reinstallAllReshadeiniToolStripMenuItem";
            this.reinstallAllReshadeiniToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.reinstallAllReshadeiniToolStripMenuItem.Text = "Update Configuration Files";
            this.reinstallAllReshadeiniToolStripMenuItem.Click += new System.EventHandler(this.reinstallAllReshadeiniToolStripMenuItem_Click);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker3_RunWorkerCompleted);
            // 
            // labelReshade
            // 
            this.labelReshade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelReshade.Location = new System.Drawing.Point(50, 119);
            this.labelReshade.Name = "labelReshade";
            this.labelReshade.Size = new System.Drawing.Size(192, 13);
            this.labelReshade.TabIndex = 10;
            this.labelReshade.Text = "Update Progress: 100%";
            this.labelReshade.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelReshade.Visible = false;
            // 
            // labelShaders
            // 
            this.labelShaders.Location = new System.Drawing.Point(248, 119);
            this.labelShaders.Name = "labelShaders";
            this.labelShaders.Size = new System.Drawing.Size(189, 13);
            this.labelShaders.TabIndex = 11;
            this.labelShaders.Text = "Update Progress: 100%";
            this.labelShaders.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelShaders.Visible = false;
            // 
            // updateLink
            // 
            this.updateLink.AutoSize = true;
            this.updateLink.Location = new System.Drawing.Point(389, 269);
            this.updateLink.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.updateLink.Name = "updateLink";
            this.updateLink.Size = new System.Drawing.Size(91, 13);
            this.updateLink.TabIndex = 12;
            this.updateLink.TabStop = true;
            this.updateLink.Text = "Update Available!";
            this.updateLink.Visible = false;
            this.updateLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.updateLink_LinkClicked);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker4_DoWork);
            this.backgroundWorker4.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker4_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AccessibleName = "ReShade Centralized";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(491, 291);
            this.Controls.Add(this.updateLink);
            this.Controls.Add(this.labelShaders);
            this.Controls.Add(this.labelReshade);
            this.Controls.Add(this.pbar2);
            this.Controls.Add(this.updateShaders);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.updatereshade);
            this.Controls.Add(this.installwin32);
            this.Controls.Add(this.installuwp);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReShade Centralized";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button installuwp;
        private System.Windows.Forms.Button installwin32;
        private System.Windows.Forms.Button updatereshade;
        private System.Windows.Forms.ProgressBar pbar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button updateShaders;
        private System.Windows.Forms.ProgressBar pbar2;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizePathsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reinstallAllReshadeiniToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.Label labelReshade;
        private System.Windows.Forms.Label labelShaders;
        private System.Windows.Forms.LinkLabel updateLink;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
    }


}

