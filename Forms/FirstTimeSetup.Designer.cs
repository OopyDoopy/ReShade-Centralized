﻿
namespace ReShade_Centralized
{
    partial class FirstTimeSetup
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
            this.textDir = new System.Windows.Forms.TextBox();
            this.buttonDir = new System.Windows.Forms.Button();
            this.buttonCont = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textDir
            // 
            this.textDir.Location = new System.Drawing.Point(18, 46);
            this.textDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textDir.Name = "textDir";
            this.textDir.Size = new System.Drawing.Size(494, 26);
            this.textDir.TabIndex = 0;
            // 
            // buttonDir
            // 
            this.buttonDir.Location = new System.Drawing.Point(524, 46);
            this.buttonDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDir.Name = "buttonDir";
            this.buttonDir.Size = new System.Drawing.Size(147, 31);
            this.buttonDir.TabIndex = 1;
            this.buttonDir.Text = "Select Directory";
            this.buttonDir.UseVisualStyleBackColor = true;
            this.buttonDir.Click += new System.EventHandler(this.buttonDir_Click);
            // 
            // buttonCont
            // 
            this.buttonCont.Location = new System.Drawing.Point(558, 112);
            this.buttonCont.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCont.Name = "buttonCont";
            this.buttonCont.Size = new System.Drawing.Size(112, 46);
            this.buttonCont.TabIndex = 2;
            this.buttonCont.Text = "Continue";
            this.buttonCont.UseVisualStyleBackColor = true;
            this.buttonCont.Click += new System.EventHandler(this.buttonCont_Click);
            // 
            // FirstTimeSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 177);
            this.Controls.Add(this.buttonCont);
            this.Controls.Add(this.buttonDir);
            this.Controls.Add(this.textDir);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FirstTimeSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select ReShade Centralized Directory";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FirstTimeSetup_FormClosed);
            this.Load += new System.EventHandler(this.FirstTimeSetup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textDir;
        private System.Windows.Forms.Button buttonDir;
        private System.Windows.Forms.Button buttonCont;
    }
}