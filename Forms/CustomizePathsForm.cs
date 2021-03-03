using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace ReShade_Centralized
{
    public partial class CustomizePathsForm : Form
    {
        string oldReshadeShaders;
        string oldPresets;
        string oldScreenshots;

        public CustomizePathsForm()
        {
            InitializeComponent();
        }

        private void CustomizePathsForm_Load(object sender, EventArgs e)
        {
            //Load current paths into global variables for file relocation
            oldReshadeShaders = Program.shaders.Remove(Program.shaders.Length - 8);
            oldPresets = Program.presets;
            oldScreenshots = Program.screenshots;

            //Load current paths into text boxes
            shadersTB.Text = Program.shaders.Remove(Program.shaders.Length - 8);
            presetsTB.Text = Program.presets;
            screenshotsTB.Text = Program.screenshots;
            this.Select();
        }

        private void shadersButton_Click(object sender, EventArgs e)
        {
            string curdir = Program.shaders.Remove(Program.shaders.Length - 8);
            CommonOpenFileDialog getDir = new CommonOpenFileDialog();

            getDir.Title = "Select the reshade-shaders folder you'd like to use.";
            getDir.InitialDirectory = @"C:\";
            getDir.IsFolderPicker = true;
            if (getDir.ShowDialog() != CommonFileDialogResult.Ok)
            {
                this.Focus();
                return;
            }
            this.Focus();
            shadersTB.Text = getDir.FileName;
        }

        private void presetsButton_Click(object sender, EventArgs e)
        {
            string curdir = Program.presets;
            CommonOpenFileDialog getDir = new CommonOpenFileDialog();

            getDir.Title = "Select the Presets folder you'd like to use.";
            getDir.InitialDirectory = @"C:\";
            getDir.IsFolderPicker = true;
            if (getDir.ShowDialog() != CommonFileDialogResult.Ok)
            {
                this.Focus();
                return;
            }
            this.Focus();
            presetsTB.Text = getDir.FileName;
        }

        private void screenshotsButton_Click(object sender, EventArgs e)
        {
            string curdir = Program.screenshots;
            CommonOpenFileDialog getDir = new CommonOpenFileDialog();

            getDir.Title = "Select the Screenshots folder you'd like to use.";
            getDir.InitialDirectory = @"C:\";
            getDir.IsFolderPicker = true;
            if (getDir.ShowDialog() != CommonFileDialogResult.Ok)
            {
                this.Focus();
                return;
            }
            this.Focus();
            screenshotsTB.Text = getDir.FileName;
        }

        private void confirmBTN_Click(object sender, EventArgs e)
        {
            //Ensure valid paths.  If not valid, try again.

            try
            {
                Path.GetFullPath(shadersTB.Text + @"\shaders");
                Path.GetFullPath(shadersTB.Text + @"\textures");
                Path.GetFullPath(presetsTB.Text);
                Path.GetFullPath(screenshotsTB.Text);
            }
            catch
            {
                MessageBox.Show(@"Invalid path(s) entered.  Please double check your paths and fix any errors.");
                return;
            }

            //Update variables in memory and write variables to config file.

            Program.shaders = Path.GetFullPath(shadersTB.Text + @"\shaders");
            Program.textures = Path.GetFullPath(shadersTB.Text + @"\textures");
            Program.presets = Path.GetFullPath(presetsTB.Text);
            Program.screenshots = Path.GetFullPath(screenshotsTB.Text);
            Functions.overwriteIni("ReShadeCentralized.ini", new List<string> { "shaders=", "textures=", "presets=", "screenshots=" }, new List<string> { Program.shaders, Program.textures, Program.presets, Program.screenshots });

            //Move files from old directory to new directory
            DirectoryExtensions.MoveDirectoryOverwrite(oldReshadeShaders, Path.GetFullPath(shadersTB.Text));
            DirectoryExtensions.MoveDirectoryOverwrite(oldPresets, Program.presets);
            DirectoryExtensions.MoveDirectoryOverwrite(oldScreenshots, Program.screenshots);
            this.Close();
        }
    }
}
