using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Windows.Forms;

namespace ReShade_Centralized
{
    public partial class FirstTimeSetup : Form
    {
        bool ContinueClicked = false; //set true if the form exits correctly
        public FirstTimeSetup()
        {
            InitializeComponent();
        }
        private void buttonDir_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dir = new CommonOpenFileDialog();
            dir.DefaultDirectory = textDir.Text;
            dir.IsFolderPicker = true;
            dir.Title = @"Select the ReShade Centralized directory.";
            try
            {
                dir.ShowDialog();
                textDir.Text = dir.FileName;
            }
            catch
            {
                return;
            }
        }


        private void FirstTimeSetup_Load(object sender, EventArgs e)
        {
            textDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ReShade Centralized";
        }

        private void buttonCont_Click(object sender, EventArgs e)
        {
            try
            {
                this.Text = Path.GetFullPath(textDir.Text);
                this.DialogResult = DialogResult.OK;
                ContinueClicked = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Invalid path, enter a valid path and try again.");
            }
        }

        private void FirstTimeSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ContinueClicked == false)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}