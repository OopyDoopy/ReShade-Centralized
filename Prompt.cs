using System.Linq;
using System.Windows.Forms;

namespace ReShade_Centralized
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, int w, int h)
        {
            Form prompt = new Form()
            {
                Width = w,
                Height = h,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                ControlBox = false,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                AutoSize = true
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

        public static RadioButton ShowRadioButtons(string[] options, string caption, int w, int h, string label = "")
        {
            Form prompt = new Form()
            {
                Width = w,
                Height = h,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                ControlBox = false,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                AutoSize = true
            };
            FlowLayoutPanel pnl = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Visible = true,
                Name = caption,
                TabIndex = 0,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                AutoSize = true
            };

            for (int i = 0; i < options.Length; i++)
            {
                if (i == 0) { pnl.Controls.Add(new RadioButton() { Text = options[i], Checked = true }); }
                else { pnl.Controls.Add(new RadioButton() { Text = options[i] }); }
            }

            Label description = new Label() { Text = label, AutoSize = true };
            Button confirmation = new Button() { Text = "OK", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            pnl.Controls.Add(description);
            pnl.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            prompt.Controls.Add(pnl);
            prompt.ShowDialog();
            RadioButton rbSelected = pnl.Controls
                         .OfType<RadioButton>()
                         .FirstOrDefault(r => r.Checked);

            return rbSelected;
        }

        public static CheckedListBox ShowCheckBoxes(string[] options, string caption, int w, int h, int wc = 110, int hc = 300)
        {
            Form prompt = new Form()
            {
                AutoSize = true,
                Width = w,
                Height = h,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                ControlBox = false,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowOnly
            };
            FlowLayoutPanel pnl = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Visible = true,
                Name = caption,
                TabIndex = 0,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                
            };

            CheckedListBox cb = new CheckedListBox();
            cb.CheckOnClick = true;
            cb.Items.AddRange(options);
            cb.IntegralHeight = true;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                cb.SetItemChecked(i, true);
                cb.GetItemRectangle(i); //This is required for GetPreferredSize to function.  Very confusing.
            }
            cb.ClientSize = cb.GetPreferredSize(new System.Drawing.Size());
            pnl.Controls.Add(cb);

            Button confirmation = new Button() { Text = "OK", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK, AutoSize = true };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            pnl.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            prompt.Controls.Add(pnl);
            prompt.ShowDialog();

            return cb;
        }
    }
}
