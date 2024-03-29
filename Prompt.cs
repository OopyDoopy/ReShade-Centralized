﻿using System.Linq;
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
            Panel paneltop = new Panel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Dock = DockStyle.Top,
                Parent = prompt
            };
            Panel panelbottom = new Panel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Dock = DockStyle.Bottom,
                Parent = prompt
            };
            Label textLabel = new Label() { Left = 10, Top = 20, AutoSize = true, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = paneltop.Width - 100, AutoSize = true };
            Button confirmation = new Button() { Left = panelbottom.Width - 100, Top = 10, Text = "OK", AutoSize = true, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            paneltop.Controls.Add(textBox);
            panelbottom.Controls.Add(confirmation);
            paneltop.Controls.Add(textLabel);
            prompt.Controls.Add(paneltop);
            prompt.Controls.Add(panelbottom);
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
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown
            };

            for (int i = 0; i < options.Length; i++)
            {
                if (i == 0) { pnl.Controls.Add(new RadioButton() { Text = options[i], Checked = true, AutoSize = true }); }
                else { pnl.Controls.Add(new RadioButton() { Text = options[i], AutoSize = true }); }
            }

            Label description = new Label() { Text = label, AutoSize = true };
            Button confirmation = new Button() { Text = "OK", Anchor = AnchorStyles.Left, Dock = DockStyle.Bottom, Left = 350, Width = 100, Top = 70, AutoSize = true, DialogResult = DialogResult.OK };
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
                FlowDirection = FlowDirection.TopDown
            };
            CheckedListBox cb = new CheckedListBox();
            cb.ItemCheck += Cb_ItemCheck;
            cb.CheckOnClick = true;
            cb.Items.AddRange(options);
            cb.IntegralHeight = true;
            cb.SetItemCheckState(0, CheckState.Checked);
            for (int i = 1; i < cb.Items.Count; i++)
            {
                cb.SetItemChecked(i, false);
                cb.GetItemRectangle(i); //This is required for GetPreferredSize to function.  Very confusing.
            }


            Button toggleAllSelection = new Button() { Anchor = AnchorStyles.Right, Dock = DockStyle.Top, Text = "Select/Deselect All", Left = 350, Width = 100, Top = 70, AutoSize = true };
            toggleAllSelection.Click += (sender, e) =>
                {
                    bool isChecked = false;
                    for (int i = 1; i < cb.Items.Count && isChecked == false; i++)
                    {
                        isChecked = cb.GetItemChecked(i);
                    }

                    for (int i = 1; i < cb.Items.Count; i++)
                    {
                        cb.SetItemChecked(i, !isChecked);
                    }
                };
            pnl.Controls.Add(toggleAllSelection);

            cb.ClientSize = cb.GetPreferredSize(new System.Drawing.Size());
            pnl.Controls.Add(cb);

            Button confirmation = new Button() { Anchor = AnchorStyles.Right, Dock = DockStyle.Bottom, Text = "OK", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK, AutoSize = true };

            confirmation.Click += (sender, e) => { prompt.Close(); };

            pnl.Controls.Add(confirmation);

            prompt.AcceptButton = confirmation;
            prompt.Controls.Add(pnl);
            prompt.ShowDialog();

            return cb;
        }

        private static void Cb_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if ( e.Index == 0)
            {
                e.NewValue = CheckState.Checked;
            }
        }
    }
}
