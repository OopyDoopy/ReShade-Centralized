using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ReShade_Centralized
{
    public partial class CustomizePathsForm : Form
    {
        public CustomizePathsForm()
        {
            InitializeComponent();
        }

        private void CustomizePathsForm_Load(object sender, EventArgs e)
        {
            shadersLabel.Text = Program.shaders.Remove(Program.shaders.Length - 8);
            presetsLabel.Text = Program.presets;
            screenshotsLabel.Text = Program.screenshots;
            this.Select();
        }

        private void shadersButton_Click(object sender, EventArgs e)
        {
            string curdir = Program.shaders.Remove(Program.shaders.Length - 8);
            CommonOpenFileDialog getDir = new CommonOpenFileDialog();

            getDir.Title = "Select the reshade-shaders folder you'd like to use.";
            getDir.InitialDirectory = @"C:\";
            getDir.IsFolderPicker = true;
            //getDir = Functions.getUserDirectoryCommon(getDir);
            if (getDir.ShowDialog() != CommonFileDialogResult.Ok)
            {
                this.Focus();
                return;
            }
            this.Focus();
            Program.shaders = getDir.FileName + @"\shaders";
            Program.textures = getDir.FileName + @"\textures";
            Functions.overwriteIni("ReShadeCentralized.ini", new List<string> { "shaders=", "textures=" }, new List<string> { Program.shaders, Program.textures });
            Functions.readRCIni();
            DirectoryExtensions.MoveDirectoryOverwrite(curdir, getDir.FileName);
            shadersLabel.Text = getDir.FileName;
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
            Program.presets = getDir.FileName;
            Functions.overwriteIni("ReShadeCentralized.ini", new List<string> { "presets=" }, new List<string> { Program.presets });
            Functions.readRCIni();
            DirectoryExtensions.MoveDirectoryOverwrite(curdir, getDir.FileName);
            presetsLabel.Text = Program.presets;
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
            Program.screenshots = getDir.FileName;
            Functions.overwriteIni("ReShadeCentralized.ini", new List<string> { "screenshots=" }, new List<string> { Program.screenshots });
            Functions.readRCIni();
            DirectoryExtensions.MoveDirectoryOverwrite(curdir, getDir.FileName);
            screenshotsLabel.Text = Program.screenshots;
        }
    }
}
