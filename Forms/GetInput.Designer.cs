
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
            this.game.Location = new System.Drawing.Point(11, 24);
            this.game.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.game.Name = "game";
            this.game.Size = new System.Drawing.Size(243, 20);
            this.game.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "lablel1";
            // 
            // confirmation
            // 
            this.confirmation.AutoSize = true;
            this.confirmation.Location = new System.Drawing.Point(180, 49);
            this.confirmation.Name = "confirmation";
            this.confirmation.Size = new System.Drawing.Size(75, 23);
            this.confirmation.TabIndex = 2;
            this.confirmation.Text = "OK";
            this.confirmation.UseVisualStyleBackColor = true;
            this.confirmation.Click += new System.EventHandler(this.confirmation_Click);
            // 
            // GetInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(267, 80);
            this.Controls.Add(this.confirmation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.game);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GetInput";
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