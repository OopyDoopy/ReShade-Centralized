using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

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
                this.Text = textDir.Text;
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
            if(ContinueClicked == false)
            {
                Application.Exit();
            }
        }
    }
}