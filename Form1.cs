﻿using System;
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
using System.Net;
using System.Text.RegularExpressions;
using SevenZipExtractor;
using System.Runtime.InteropServices;


namespace ReShade_Centralized
{


    public partial class Form1 : Form
    {

        string shaders = System.String.Empty;
        string textures = System.String.Empty;
        string presets = System.String.Empty;
        string screenshots = System.String.Empty;
        string dlls = System.String.Empty;
        string mdlls = System.String.Empty;

        public Form1()
        {
            InitializeComponent();
            if (!File.Exists("ReShadeCentralized.ini"))
            {

                MessageBox.Show("Looks like this is your first time running ReShade Centralized, initiating setup.  Please select your desired ReShade folder.", "First Time Startup Message");
                CommonOpenFileDialog centralizedDialog = new CommonOpenFileDialog();
                centralizedDialog.Title = "Create and/or Select your ReShade Centralized folder";
                centralizedDialog.IsFolderPicker = true;
                centralizedDialog.InitialDirectory = @"C:\";
                centralizedDialog.ShowDialog();
                using (StreamWriter w = new StreamWriter("ReShadeCentralized.ini"))
                {
                    w.WriteLine("[paths]");
                    w.WriteLine("shaders=" + centralizedDialog.FileName + "\\reshade-shaders\\shaders");
                    w.WriteLine("textures=" + centralizedDialog.FileName + "\\reshade-shaders\\textures");
                    w.WriteLine("presets=" + centralizedDialog.FileName + "\\presets");
                    w.WriteLine("screenshots=" + centralizedDialog.FileName + "\\screenshots");
                    w.WriteLine("dlls=" + centralizedDialog.FileName + "\\reshade-files");
                    w.WriteLine("mdlls=" + centralizedDialog.FileName + "\\reshade-files-mod");

                    w.Close();
                }
                readini();
                Directory.CreateDirectory(shaders);
                Directory.CreateDirectory(textures);
                Directory.CreateDirectory(presets);
                Directory.CreateDirectory(screenshots);
                Directory.CreateDirectory(dlls);
                Directory.CreateDirectory(mdlls);

            }
            else
            {
                readini();
            }

        }
        //Helper Functions-------------------------------------------------------------
        private void readini()
        {
            using (StreamReader r = new StreamReader("ReShadeCentralized.ini"))
            {
                List<string> configfile = new List<string>();
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
                    if (line.StartsWith("shaders="))
                    {
                        shaders = line.Substring(8);
                    }
                    else if (line.StartsWith("textures="))
                    {
                        textures = line.Substring(9);
                    }
                    else if (line.StartsWith("presets="))
                    {
                        presets = line.Substring(8);
                    }
                    else if (line.StartsWith("screenshots="))
                    {
                        screenshots = line.Substring(12);
                    }
                    else if (line.StartsWith("dlls="))
                    {
                        dlls = line.Substring(5);
                    }
                    else if (line.StartsWith("mdlls="))
                    {
                        mdlls = line.Substring(6);
                    }
                }
                r.Close();
            }
        }

        //Thank you Andrew Backer of stackoverflow----
        private enum MachineType
        {
            Native = 0, I386 = 0x014c, Itanium = 0x0200, x64 = 0x8664
        }

        private static MachineType GetMachineType(string fileName)
        {
            const int PE_POINTER_OFFSET = 60;
            const int MACHINE_OFFSET = 4;
            byte[] data = new byte[4096];
            using (Stream s = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                s.Read(data, 0, 4096);
            }
            // dos header is 64 bytes, last element, long (4 bytes) is the address of the PE header
            int PE_HEADER_ADDR = BitConverter.ToInt32(data, PE_POINTER_OFFSET);
            int machineUint = BitConverter.ToUInt16(data, PE_HEADER_ADDR + MACHINE_OFFSET);
            return (MachineType)machineUint;
        }
        //-----------------------------------------------

        //End Helper Functions---------------------------------------------------------------
        private void installuwp_Click(object sender, EventArgs e)
        {

        }

        private void installwin32_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog gameDialog = new OpenFileDialog())
            {
                gameDialog.Title = "Select the game's runtime executable.";
                gameDialog.Filter = "Select EXE|*.exe";
                gameDialog.InitialDirectory = @"C:\";
                gameDialog.ShowDialog();
                //File.OpenRead(gameDialog.FileName);

                string gamedll = System.String.Empty;

                switch ((Prompt.ShowRadioButtons(new string[] { "DirectX 9", "DirectX 10/11/12", "OpenGL", "Vulkan" }, "Select the Rendering API the game utilizes.")).Text)
                {
                    case "DirectX 9":
                        gamedll = @"\d3d9.dll";
                        break;
                    case "DirectX 10/11/12":
                        gamedll = @"\dxgi.dll";
                        break;
                    case "OpenGL":
                        gamedll = @"\opengl32.dll";
                        break;
                    case "Vulkan":
                        MessageBox.Show("Feature coming soon.  For now, install ReShade for Vulkan globally through the normal ReShade installer.  This program will setup the rest.");
                        break;
                }


                string gameName = Prompt.ShowDialog(@"Enter Game name.  This is used for the creation of the Presets and Screenshots Folders.", "Game Name");

                if (Prompt.ShowRadioButtons(new string[] { "Official", "Modded" }, @"Select Official or Modified ReShade.").Text == "Official")
                {
                    string temp = Path.GetDirectoryName(gameDialog.FileName) + @"\ReShade64.dll";
                    string temp2 = dlls + @"\ReShade64.dll";
                    if (GetMachineType(gameDialog.FileName) == MachineType.x64)
                    {
                        SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, dlls + @"\ReShade64.dll", 0);
                    }
                    else
                    {
                        SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, dlls + @"\ReShade32.dll", 0);
                    }
                }
                else
                {
                    if (GetMachineType(gameDialog.FileName) == MachineType.x64)
                    {
                        SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, mdlls + @"\ReShade64.dll", 0);
                    }
                    else
                    {
                        SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, mdlls + @"\ReShade32.dll", 0);
                    }
                }

                Directory.CreateDirectory(screenshots + @"\" + gameName);
                Directory.CreateDirectory(presets + @"\" + gameName);

                string nl = "\n";
                if (!File.Exists(Path.GetDirectoryName(gameDialog.FileName) + @"\reshade.ini"))
                {
                    File.WriteAllText(Path.GetDirectoryName(gameDialog.FileName) + @"\reshade.ini",
                        @"[GENERAL]" + nl +
                        @"EffectSearchPaths=" + shaders + nl +
                        @"PerformanceMode=0" + nl +
                        @"PreprocessorDefinitions=RESHADE_DEPTH_INPUT_IS_REVERSED=0,RESHADE_DEPTH_INPUT_IS_LOGARITHMIC=0,RESHADE_DEPTH_INPUT_IS_UPSIDE_DOWN=0,RESHADE_DEPTH_LINEARIZATION_FAR_PLANE=1000" + nl +
                        @"PresetPath=" + presets + @"\" + gameName + @"\ReshadePreset.ini" + nl +
                        @"PresetTransitionDelay=1000" + nl +
                        @"SkipLoadingDisabledEffects=0" + nl +
                        @"TextureSearchPaths=" + textures + nl + nl +
                        @"[INPUT]" + nl +
                        @"ForceShortcutModifiers=1" + nl +
                        @"InputProcessing=2" + nl +
                        @"KeyEffects=145,0,0,0" + nl +
                        @"KeyNextPreset=0,0,0,0" + nl +
                        @"KeyOverlay=36,0,0,0" + nl +
                        @"KeyPerformanceMode=0,0,0,0" + nl +
                        @"KeyPreviousPreset=0,0,0,0" + nl +
                        @"KeyReload=0,0,0,0" + nl +
                        @"KeyScreenshot=44,0,0,0" + nl + nl +
                        @"[OVERLAY]" + nl +
                        @"ClockFormat=0" + nl +
                        @"FPSPosition=1" + nl +
                        @"NoFontScaling=1" + nl +
                        @"SaveWindowState=0" + nl +
                        @"ShowClock=0" + nl +
                        @"ShowForceLoadEffectsButton=1" + nl +
                        @"ShowFPS=0" + nl +
                        @"ShowFrameTime=0" + nl +
                        @"ShowScreenshotMessage=1" + nl +
                        @"TutorialProgress=4" + nl +
                        @"VariableListHeight=300.000000" + nl +
                        @"VariableListUseTabs=0" + nl + nl +
                        @"[SCREENSHOTS]" + nl +
                        @"ClearAlpha=1" + nl +
                        @"FileFormat=1" + nl +
                        @"FileNamingFormat=0" + nl +
                        @"JPEGQuality=90" + nl +
                        @"SaveBeforeShot=1" + nl +
                        @"SaveOverlayShot=0" + nl +
                        @"SavePath=" + screenshots + @"\" + gameName + nl +
                        @"SavePresetFile=0" + nl + nl +
                        @"[STYLE]" + nl +
                        @"Alpha=1.000000" + nl +
                        @"ChildRounding=0.000000" + nl +
                        @"ColFPSText=1.000000,1.000000,0.784314,1.000000" + nl +
                        @"EditorFont=ProggyClean.ttf" + nl +
                        @"EditorFontSize=13" + nl +
                        @"EditorStyleIndex=0" + nl +
                        @"Font=ProggyClean.ttf" + nl +
                        @"FontSize=13" + nl +
                        @"FPSScale=1.000000" + nl +
                        @"FrameRounding=0.000000" + nl +
                        @"GrabRounding=0.000000" + nl +
                        @"PopupRounding=0.000000" + nl +
                        @"ScrollbarRounding=0.000000" + nl +
                        @"StyleIndex=2" + nl +
                        @"TabRounding=4.000000" + nl +
                        @"WindowRounding=0.000000"
                     );
                }
                else
                {
                    MessageBox.Show("reshade.ini detected.  File has not been overwritten.");
                }


                if (!File.Exists(presets + @"\" + gameName + @"\ReshadePreset.ini"))
                {
                    File.WriteAllText(presets + @"\" + gameName + @"\ReshadePreset.ini",
                        @"PreprocessorDefinitions=" + nl +
                        @"Techniques=" + nl +
                        @"TechniqueSorting=DisplayDepth"
                    );
                 }

                MessageBox.Show("ReShade Successfully Installed!");
            }
        }

        private void custsetup_Click(object sender, EventArgs e)
        {

        }

        private void updatereshade_Click(object sender, EventArgs e)
        {
            try
            {
                updatereshade.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
            catch
            {
                MessageBox.Show("Update already in progress");
            }
        }

       

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;


            using (var client = new WebClient())
            {
                worker.ReportProgress(0);
                string download = client.DownloadString("https://reshade.me");
                Regex rg = new Regex(@"/downloads/\S*.exe");
                download = "https://reshade.me" + rg.Match(download).ToString();
                client.DownloadFile(download, "reshade.exe");
                worker.ReportProgress(20);

                using (ArchiveFile archiveFile = new ArchiveFile(@"ReShade.exe"))
                {
                    foreach (Entry entry in archiveFile.Entries)
                    {
                        //MessageBox.Show(entry.FileName);
                        if (entry.FileName == "[0]")
                        {
                            entry.Extract(entry.FileName);
                            worker.ReportProgress(40);

                            using (ArchiveFile archiveFile2 = new ArchiveFile(@"[0]"))
                            {
                                int prog = 40;
                                foreach (Entry entry2 in archiveFile2.Entries)
                                {
                                    if (entry2.FileName == "ReShade32.dll" || entry2.FileName == "ReShade64.dll")
                                    {
                                        entry2.Extract(entry2.FileName);
                                        if (File.Exists(dlls + @"\" + entry2.FileName))
                                        {
                                            File.Delete(dlls + @"\" + entry2.FileName);
                                        }
                                        File.Move(entry2.FileName, dlls + @"\" + entry2.FileName);
                                        worker.ReportProgress(prog += 20);
                                    }
                                }

                            }
                        }

                    }
                }
            }
            File.Delete(@"reshade.exe");
            File.Delete(@"[0]");
            worker.ReportProgress(100);
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            pbar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            updatereshade.Enabled = true;
            MessageBox.Show("ReShade Updated");
        }

        private void pbar_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

        }
        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void updateShaders_Click(object sender, EventArgs e)
        {

        }
    }

    
}