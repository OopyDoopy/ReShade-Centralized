
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
            this.SuspendLayout();
            // 
            // presetsLabel
            // 
            this.presetsLabel.AutoSize = true;
            this.presetsLabel.Location = new System.Drawing.Point(9, 60);
            this.presetsLabel.Name = "presetsLabel";
            this.presetsLabel.Size = new System.Drawing.Size(67, 13);
            this.presetsLabel.TabIndex = 2;
            this.presetsLabel.Text = "Presets Path";
            // 
            // screenshotsLabel
            // 
            this.screenshotsLabel.AutoSize = true;
            this.screenshotsLabel.Location = new System.Drawing.Point(9, 109);
            this.screenshotsLabel.Name = "screenshotsLabel";
            this.screenshotsLabel.Size = new System.Drawing.Size(91, 13);
            this.screenshotsLabel.TabIndex = 3;
            this.screenshotsLabel.Text = "Screenshots Path";
            // 
            // presetsButton
            // 
            this.presetsButton.Location = new System.Drawing.Point(9, 76);
            this.presetsButton.Name = "presetsButton";
            this.presetsButton.Size = new System.Drawing.Size(145, 23);
            this.presetsButton.TabIndex = 6;
            this.presetsButton.Text = "Change Presets";
            this.presetsButton.UseVisualStyleBackColor = true;
            this.presetsButton.Click += new System.EventHandler(this.presetsButton_Click);
            // 
            // screenshotsButton
            // 
            this.screenshotsButton.Location = new System.Drawing.Point(9, 125);
            this.screenshotsButton.Name = "screenshotsButton";
            this.screenshotsButton.Size = new System.Drawing.Size(145, 23);
            this.screenshotsButton.TabIndex = 7;
            this.screenshotsButton.Text = "Change Screenshots";
            this.screenshotsButton.UseVisualStyleBackColor = true;
            this.screenshotsButton.Click += new System.EventHandler(this.screenshotsButton_Click);
            // 
            // shadersLabel
            // 
            this.shadersLabel.AutoSize = true;
            this.shadersLabel.Location = new System.Drawing.Point(9, 12);
            this.shadersLabel.Name = "shadersLabel";
            this.shadersLabel.Size = new System.Drawing.Size(115, 13);
            this.shadersLabel.TabIndex = 8;
            this.shadersLabel.Text = "Reshade-shaders Path";
            // 
            // shadersButton
            // 
            this.shadersButton.Location = new System.Drawing.Point(9, 28);
            this.shadersButton.Name = "shadersButton";
            this.shadersButton.Size = new System.Drawing.Size(145, 23);
            this.shadersButton.TabIndex = 9;
            this.shadersButton.Text = "Change Reshade-shaders";
            this.shadersButton.UseVisualStyleBackColor = true;
            this.shadersButton.Click += new System.EventHandler(this.shadersButton_Click);
            // 
            // CustomizePathsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(375, 186);
            this.Controls.Add(this.shadersButton);
            this.Controls.Add(this.shadersLabel);
            this.Controls.Add(this.screenshotsButton);
            this.Controls.Add(this.presetsButton);
            this.Controls.Add(this.screenshotsLabel);
            this.Controls.Add(this.presetsLabel);
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
    }
}