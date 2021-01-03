using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReShade_Centralized
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                AutoSize = true,
                //Width = 500,
                //Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                ControlBox = false
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "OK", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public static RadioButton ShowRadioButtons(string[] options, string caption)
        {
            Form prompt = new Form()
            {
                AutoSize = true,
                //Width = 200,
                //Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                ControlBox = false
        };
            FlowLayoutPanel pnl = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Visible = true,
                Name = caption,
                TabIndex = 0
            };

            for (int i = 0; i < options.Length; i++)
            {
                if (i == 0) { pnl.Controls.Add(new RadioButton() { Text = options[i], Checked = true }); }
                else { pnl.Controls.Add(new RadioButton() { Text = options[i] }); }
            }
            
            
            Button confirmation = new Button() { Text = "OK", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            pnl.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            prompt.Controls.Add(pnl);
            prompt.ShowDialog();
            RadioButton rbSelected = pnl.Controls
                         .OfType<RadioButton>()
                         .FirstOrDefault(r => r.Checked);

            return rbSelected;
        }
    }
}
