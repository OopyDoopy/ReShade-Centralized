
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
            this.installuwp = new System.Windows.Forms.Button();
            this.installwin32 = new System.Windows.Forms.Button();
            this.custsetup = new System.Windows.Forms.Button();
            this.updatereshade = new System.Windows.Forms.Button();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.updateShaders = new System.Windows.Forms.Button();
            this.pbar2 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // installuwp
            // 
            this.installuwp.Location = new System.Drawing.Point(206, 280);
            this.installuwp.Name = "installuwp";
            this.installuwp.Size = new System.Drawing.Size(189, 49);
            this.installuwp.TabIndex = 2;
            this.installuwp.Text = "Install to UWP (Coming Soon)";
            this.installuwp.UseVisualStyleBackColor = true;
            this.installuwp.Click += new System.EventHandler(this.installuwp_Click);
            // 
            // installwin32
            // 
            this.installwin32.Location = new System.Drawing.Point(206, 225);
            this.installwin32.Name = "installwin32";
            this.installwin32.Size = new System.Drawing.Size(189, 49);
            this.installwin32.TabIndex = 3;
            this.installwin32.Text = "Install to Game";
            this.installwin32.UseVisualStyleBackColor = true;
            this.installwin32.Click += new System.EventHandler(this.installwin32_Click);
            // 
            // custsetup
            // 
            this.custsetup.BackColor = System.Drawing.Color.SteelBlue;
            this.custsetup.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.custsetup.ForeColor = System.Drawing.Color.Black;
            this.custsetup.Location = new System.Drawing.Point(206, 12);
            this.custsetup.Name = "custsetup";
            this.custsetup.Size = new System.Drawing.Size(189, 28);
            this.custsetup.TabIndex = 4;
            this.custsetup.Text = "Customize Paths (Coming Soon)";
            this.custsetup.UseVisualStyleBackColor = false;
            this.custsetup.Click += new System.EventHandler(this.custsetup_Click);
            // 
            // updatereshade
            // 
            this.updatereshade.BackColor = System.Drawing.Color.SteelBlue;
            this.updatereshade.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.updatereshade.ForeColor = System.Drawing.Color.Black;
            this.updatereshade.Location = new System.Drawing.Point(102, 46);
            this.updatereshade.Name = "updatereshade";
            this.updatereshade.Size = new System.Drawing.Size(189, 28);
            this.updatereshade.TabIndex = 5;
            this.updatereshade.Text = "Update ReShade";
            this.updatereshade.UseVisualStyleBackColor = false;
            this.updatereshade.Click += new System.EventHandler(this.updatereshade_Click);
            // 
            // pbar
            // 
            this.pbar.Location = new System.Drawing.Point(102, 80);
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
            this.updateShaders.BackColor = System.Drawing.Color.SteelBlue;
            this.updateShaders.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.updateShaders.ForeColor = System.Drawing.Color.Black;
            this.updateShaders.Location = new System.Drawing.Point(297, 46);
            this.updateShaders.Name = "updateShaders";
            this.updateShaders.Size = new System.Drawing.Size(189, 28);
            this.updateShaders.TabIndex = 7;
            this.updateShaders.Text = "Update Shaders";
            this.updateShaders.UseVisualStyleBackColor = false;
            this.updateShaders.Click += new System.EventHandler(this.updateShaders_Click);
            // 
            // pbar2
            // 
            this.pbar2.Location = new System.Drawing.Point(297, 80);
            this.pbar2.Name = "pbar2";
            this.pbar2.Size = new System.Drawing.Size(189, 23);
            this.pbar2.TabIndex = 8;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // Form1
            // 
            this.AccessibleName = "ReShade Centralized";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(598, 341);
            this.Controls.Add(this.pbar2);
            this.Controls.Add(this.updateShaders);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.updatereshade);
            this.Controls.Add(this.custsetup);
            this.Controls.Add(this.installwin32);
            this.Controls.Add(this.installuwp);
            this.Name = "Form1";
            this.Text = "ReShade Centralized";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button installuwp;
        private System.Windows.Forms.Button installwin32;
        private System.Windows.Forms.Button custsetup;
        private System.Windows.Forms.Button updatereshade;
        private System.Windows.Forms.ProgressBar pbar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button updateShaders;
        private System.Windows.Forms.ProgressBar pbar2;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
    }


}

