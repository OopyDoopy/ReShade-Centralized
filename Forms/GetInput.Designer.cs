
namespace ReShade_Centralized
{
    partial class GetInput
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
            this.game = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.confirmation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // game
            // 
            this.game.Location = new System.Drawing.Point(16, 37);
            this.game.Name = "game";
            this.game.Size = new System.Drawing.Size(362, 26);
            this.game.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "lablel1";
            // 
            // confirmation
            // 
            this.confirmation.AutoSize = true;
            this.confirmation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.confirmation.Location = new System.Drawing.Point(0, 88);
            this.confirmation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.confirmation.Name = "confirmation";
            this.confirmation.Size = new System.Drawing.Size(400, 35);
            this.confirmation.TabIndex = 2;
            this.confirmation.Text = "OK";
            this.confirmation.UseVisualStyleBackColor = true;
            this.confirmation.Click += new System.EventHandler(this.confirmation_Click);
            // 
            // GetInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(400, 123);
            this.Controls.Add(this.confirmation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.game);
            this.Name = "GetInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GetInput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox game;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button confirmation;
    }
}