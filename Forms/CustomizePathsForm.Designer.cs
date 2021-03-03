
namespace ReShade_Centralized
{
    partial class CustomizePathsForm
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
            this.presetsLabel = new System.Windows.Forms.Label();
            this.screenshotsLabel = new System.Windows.Forms.Label();
            this.presetsButton = new System.Windows.Forms.Button();
            this.screenshotsButton = new System.Windows.Forms.Button();
            this.shadersLabel = new System.Windows.Forms.Label();
            this.shadersButton = new System.Windows.Forms.Button();
            this.shadersTB = new System.Windows.Forms.TextBox();
            this.presetsTB = new System.Windows.Forms.TextBox();
            this.screenshotsTB = new System.Windows.Forms.TextBox();
            this.confirmBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // presetsLabel
            // 
            this.presetsLabel.AutoSize = true;
            this.presetsLabel.Location = new System.Drawing.Point(9, 66);
            this.presetsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.presetsLabel.Name = "presetsLabel";
            this.presetsLabel.Size = new System.Drawing.Size(100, 20);
            this.presetsLabel.TabIndex = 20;
            this.presetsLabel.Text = "Presets Path";
            // 
            // screenshotsLabel
            // 
            this.screenshotsLabel.AutoSize = true;
            this.screenshotsLabel.Location = new System.Drawing.Point(9, 123);
            this.screenshotsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.screenshotsLabel.Name = "screenshotsLabel";
            this.screenshotsLabel.Size = new System.Drawing.Size(136, 20);
            this.screenshotsLabel.TabIndex = 30;
            this.screenshotsLabel.Text = "Screenshots Path";
            // 
            // presetsButton
            // 
            this.presetsButton.Location = new System.Drawing.Point(764, 86);
            this.presetsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.presetsButton.Name = "presetsButton";
            this.presetsButton.Size = new System.Drawing.Size(171, 35);
            this.presetsButton.TabIndex = 4;
            this.presetsButton.Text = "Select Directory";
            this.presetsButton.UseVisualStyleBackColor = true;
            this.presetsButton.Click += new System.EventHandler(this.presetsButton_Click);
            // 
            // screenshotsButton
            // 
            this.screenshotsButton.Location = new System.Drawing.Point(764, 142);
            this.screenshotsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.screenshotsButton.Name = "screenshotsButton";
            this.screenshotsButton.Size = new System.Drawing.Size(171, 35);
            this.screenshotsButton.TabIndex = 6;
            this.screenshotsButton.Text = "Select Directory";
            this.screenshotsButton.UseVisualStyleBackColor = true;
            this.screenshotsButton.Click += new System.EventHandler(this.screenshotsButton_Click);
            // 
            // shadersLabel
            // 
            this.shadersLabel.AutoSize = true;
            this.shadersLabel.Location = new System.Drawing.Point(9, 9);
            this.shadersLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.shadersLabel.Name = "shadersLabel";
            this.shadersLabel.Size = new System.Drawing.Size(173, 20);
            this.shadersLabel.TabIndex = 0;
            this.shadersLabel.Text = "Reshade-shaders Path";
            // 
            // shadersButton
            // 
            this.shadersButton.Location = new System.Drawing.Point(764, 29);
            this.shadersButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.shadersButton.Name = "shadersButton";
            this.shadersButton.Size = new System.Drawing.Size(171, 35);
            this.shadersButton.TabIndex = 2;
            this.shadersButton.Text = "Select Directory";
            this.shadersButton.UseVisualStyleBackColor = true;
            this.shadersButton.Click += new System.EventHandler(this.shadersButton_Click);
            // 
            // shadersTB
            // 
            this.shadersTB.Location = new System.Drawing.Point(14, 32);
            this.shadersTB.Name = "shadersTB";
            this.shadersTB.Size = new System.Drawing.Size(740, 26);
            this.shadersTB.TabIndex = 1;
            // 
            // presetsTB
            // 
            this.presetsTB.Location = new System.Drawing.Point(14, 89);
            this.presetsTB.Name = "presetsTB";
            this.presetsTB.Size = new System.Drawing.Size(740, 26);
            this.presetsTB.TabIndex = 3;
            // 
            // screenshotsTB
            // 
            this.screenshotsTB.Location = new System.Drawing.Point(14, 146);
            this.screenshotsTB.Name = "screenshotsTB";
            this.screenshotsTB.Size = new System.Drawing.Size(740, 26);
            this.screenshotsTB.TabIndex = 5;
            // 
            // confirmBTN
            // 
            this.confirmBTN.Location = new System.Drawing.Point(796, 223);
            this.confirmBTN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.confirmBTN.Name = "confirmBTN";
            this.confirmBTN.Size = new System.Drawing.Size(138, 35);
            this.confirmBTN.TabIndex = 7;
            this.confirmBTN.Text = "Save and Exit";
            this.confirmBTN.UseVisualStyleBackColor = true;
            this.confirmBTN.Click += new System.EventHandler(this.confirmBTN_Click);
            // 
            // CustomizePathsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(952, 274);
            this.Controls.Add(this.confirmBTN);
            this.Controls.Add(this.screenshotsTB);
            this.Controls.Add(this.presetsTB);
            this.Controls.Add(this.shadersTB);
            this.Controls.Add(this.shadersButton);
            this.Controls.Add(this.shadersLabel);
            this.Controls.Add(this.screenshotsButton);
            this.Controls.Add(this.presetsButton);
            this.Controls.Add(this.screenshotsLabel);
            this.Controls.Add(this.presetsLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CustomizePathsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customize Paths";
            this.Load += new System.EventHandler(this.CustomizePathsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label presetsLabel;
        private System.Windows.Forms.Label screenshotsLabel;
        private System.Windows.Forms.Button presetsButton;
        private System.Windows.Forms.Button screenshotsButton;
        private System.Windows.Forms.Label shadersLabel;
        private System.Windows.Forms.Button shadersButton;
        private System.Windows.Forms.TextBox shadersTB;
        private System.Windows.Forms.TextBox presetsTB;
        private System.Windows.Forms.TextBox screenshotsTB;
        private System.Windows.Forms.Button confirmBTN;
    }
}