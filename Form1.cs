using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Net;
using System.Text.RegularExpressions;
using SevenZipExtractor;
using System.Runtime.InteropServices;
using System.IO.Compression;


namespace ReShade_Centralized
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("ReShadeCentralized.ini"))
            {
                MessageBox.Show("Looks like this is your first time running ReShade Centralized, initiating setup.  Please select your desired ReShade Centralized folder.", "First Time Startup Message");
                FirstTimeSetup startup = new FirstTimeSetup();
                startup.ShowDialog();
                //MessageBox.Show(startup.Text);
                Directory.CreateDirectory(startup.Text);

                //MessageBox.Show("Looks like this is your first time running ReShade Centralized, initiating setup.  Please select your desired ReShade Centralized folder.", "First Time Startup Message");
                //CommonOpenFileDialog centralizedDialog = new CommonOpenFileDialog();
                //centralizedDialog.Title = "Create and/or Select your ReShade Centralized folder";
                //centralizedDialog.IsFolderPicker = true;
                //centralizedDialog.InitialDirectory = @"C:\";
                //centralizedDialog = Functions.getUserDirectoryCommon(centralizedDialog);
                using (StreamWriter w = new StreamWriter("ReShadeCentralized.ini"))
                {
                    w.WriteLine("[paths]");
                    w.WriteLine("shaders=" + startup.Text + "\\reshade-shaders\\shaders");
                    w.WriteLine("textures=" + startup.Text + "\\reshade-shaders\\textures");
                    w.WriteLine("presets=" + startup.Text + "\\presets");
                    w.WriteLine("screenshots=" + startup.Text + "\\screenshots");
                    w.WriteLine("dlls=" + startup.Text + "\\reshade-files");
                    w.WriteLine("mdlls=" + startup.Text + "\\reshade-files-mod");

                    w.Close();
                }
                Functions.readRCIni();
                Directory.CreateDirectory(Program.shaders + @"\development");
                Directory.CreateDirectory(Program.textures + @"\development");
                Directory.CreateDirectory(Program.presets);
                Directory.CreateDirectory(Program.screenshots);
                Directory.CreateDirectory(Program.dlls);
                Directory.CreateDirectory(Program.dlls + @"\Cache");
                Directory.CreateDirectory(Program.mdlls);
                Directory.CreateDirectory(Program.mdlls + @"\Cache");
                backgroundWorker1.RunWorkerAsync(); //download reshade files
                backgroundWorker2.RunWorkerAsync(); //download shader files
            }
            else
            {
                Functions.readRCIni();
            }
        }


        private void installuwp_Click(object sender, EventArgs e)
        {

        }

        private void installwin32_Click(object sender, EventArgs e)
        {
            OpenFileDialog gameDialog = new OpenFileDialog();

            gameDialog.Title = "Select the game's runtime executable.";
            gameDialog.Filter = "Select EXE|*.exe";
            gameDialog.InitialDirectory = @"C:\";
            if (gameDialog.ShowDialog() != DialogResult.OK) 
            { 
                return; 
            }


            string gamedll = System.String.Empty;

            switch ((Prompt.ShowRadioButtons(new string[] { @"DirectX 9", @"DirectX 10+", @"OpenGL", @"Vulkan" }, "Select the Rendering API.", 200, 200)).Text)
            {
                case "DirectX 9":
                    gamedll = @"\d3d9.dll";
                    break;
                case "DirectX 10+":
                    gamedll = @"\dxgi.dll";
                    break;
                case "OpenGL":
                    gamedll = @"\opengl32.dll";
                    break;
                case "Vulkan":
                    MessageBox.Show("Feature coming soon.  For now, install ReShade for Vulkan globally through the normal ReShade installer.  This program will setup the rest.");
                    //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Khronos\Vulkan\ImplicitLayers");
                    //key = key.CreateSubKey(@"SOFTWARE\Khronos\Vulkan\ImplicitLayers");
                    //key.SetValue(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"ReShade\ReShade64.json"), 0, Microsoft.Win32.RegistryValueKind.DWord);
                    //key.SetValue(@"C:\ProgramData\ReShade\ReShade64.json", 0, Microsoft.Win32.RegistryValueKind.DWord);
                    //key.Close();
                    break;
            }


            string gameName = Prompt.ShowDialog(@"Enter Game name. This is used for the creation of the Presets and Screenshots Folders.", "Game Name", 520, 150);
            while (string.IsNullOrEmpty(gameName))
            {
                MessageBox.Show("Game Name can't be blank, try again.");
                gameName = Prompt.ShowDialog(@"Enter Game name. This is used for the creation of the Presets and Screenshots Folders.", "Game Name", 520, 150);
            }

            string workingDLLPath = Program.dlls; //apply working path to variable, mostly for reshade.ini generation

            if (Prompt.ShowRadioButtons(new string[] { "Official", "Modified" }, @"Select Official or Modified ReShade.", 250, 140, @"Modified ReShade files are self provided.  Place ReShade64.dll and ReShade32.dll in the reshade-files-mod folder to use.").Text == "Official")
            {
                if (File.Exists(Path.GetDirectoryName(gameDialog.FileName) + gamedll)) //placed here and duplicated in Else{} just in case the user closes the application when prompted
                {
                    File.Delete(Path.GetDirectoryName(gameDialog.FileName) + gamedll);
                }
                if (Functions.GetMachineType(gameDialog.FileName) == Functions.MachineType.x64)
                {
                    SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, workingDLLPath + @"\ReShade64.dll", 0);
                }
                else
                {
                    SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, workingDLLPath + @"\ReShade32.dll", 0);
                }
            }
            else
            {
                if (File.Exists(Path.GetDirectoryName(gameDialog.FileName) + gamedll))
                {
                    File.Delete(Path.GetDirectoryName(gameDialog.FileName) + gamedll);
                }
                workingDLLPath = Program.mdlls;
                if (Functions.GetMachineType(gameDialog.FileName) == Functions.MachineType.x64)
                {
                    SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, workingDLLPath + @"\ReShade64.dll", 0);
                }
                else
                {
                    SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, workingDLLPath + @"\ReShade32.dll", 0);
                }
            }

            Directory.CreateDirectory(Program.screenshots + @"\" + gameName);
            Directory.CreateDirectory(Program.presets + @"\" + gameName);

            DialogResult overwrite;
            if (File.Exists(Program.presets + @"\" + gameName + @"\reshade.ini") || File.Exists(Program.presets + @"\" + gameName + @"\ReShade.ini"))
            {
                overwrite = MessageBox.Show("ReShade.ini Detected.  Would you like to overwrite?", "Warning", MessageBoxButtons.YesNo);
            }
            else
            {
                overwrite = DialogResult.Yes;
            }
            if (overwrite == DialogResult.Yes)
            {
                if (File.Exists(Path.GetDirectoryName(gameDialog.FileName) + @"\reshade.ini"))
                {
                    File.Delete(Path.GetDirectoryName(gameDialog.FileName) + @"\reshade.ini");
                }
                Functions.writeReshadeini(Program.presets + @"\" + gameName, workingDLLPath, gameName, Path.GetDirectoryName(gameDialog.FileName));
            }


            if (!File.Exists(Program.presets + @"\" + gameName + @"\ReshadePreset.ini"))
            {
                string nl = "\n"; //makes typing up that multiline write a bit easier
                File.WriteAllText(Program.presets + @"\" + gameName + @"\ReshadePreset.ini",
                    @"PreprocessorDefinitions=" + nl +
                    @"Techniques=" + nl +
                    @"TechniqueSorting=DisplayDepth"
                );
            }
            else
            {
                MessageBox.Show("ReshadePreset.ini detected.  File has not been overwritten.");
            }

            MessageBox.Show("ReShade Successfully Installed!");

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


        //Update ReShade Thread
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
                                        if (File.Exists(Program.dlls + @"\" + entry2.FileName))
                                        {
                                            File.Delete(Program.dlls + @"\" + entry2.FileName);
                                        }
                                        File.Move(entry2.FileName, Program.dlls + @"\" + entry2.FileName);
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
            labelReshade.Text = "Update Progress: " + e.ProgressPercentage.ToString() + "%";
            labelReshade.Visible = true;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            updatereshade.Enabled = true;
            labelReshade.Text = "Update Complete";
        }

        private void pbar_Click(object sender, EventArgs e)
        {

        }

        //Update Shaders Thread
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(0);

            var items = Prompt.ShowCheckBoxes(new string[] { "qUINT - Marty McFly", "Standard - Crosire", "Legacy - Crosire", "SweetFX - CeeJayDK", "Color Effects - prod80", "Depth3D - BlueSkyDefender", "AstrayFX - BlueSkyDefender", "OtisFX - Otis Inf", "Pirate Shaders - Heathen", "Shaders - Brussell1", "Shaders - Daodan317081", "CorgiFX - originalnicoder", "Fubax - Fubaxiusz", "FXShaders - luluco250", "Shaders - Radegast", "Insane Shaders - Lord of Lunacy", "MShaders - TreyM" }, @"Select shader repos to download.", 200, 400, 178).CheckedItems;
            using (var client = new WebClient())
            {
                bool legacy = false;
                bool fxshaders = false;
                bool lunacy = false;
                bool treym = false;
                var rand = new Random();
                string randTempRootFolder = @".\temp" + rand.Next().ToString();
                while (Directory.Exists(randTempRootFolder))
                {
                    randTempRootFolder = @".\temp" + rand.Next().ToString();
                }
                Directory.CreateDirectory(randTempRootFolder);
                int pbarInc = 100 / (items.Count + 1); //split progress bar increments into how many repos get downloaded + extraction step (and prevents divide by 0)
                int pbarValue = 0;
                for (int i = 0; i < items.Count; i++)
                {
                    switch (items[i])
                    {
                        case "qUINT - Marty McFly":
                            client.DownloadFile(@"https://github.com/martymcmodding/qUINT/archive/master.zip", randTempRootFolder + @"\quint.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "Standard - Crosire":
                            client.DownloadFile(@"https://github.com/crosire/reshade-shaders/archive/slim.zip", randTempRootFolder + @"\crosire.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "Legacy - Crosire":
                            client.DownloadFile(@"https://github.com/crosire/reshade-shaders/archive/master.zip", randTempRootFolder + @"\legacy.zip");
                            legacy = true;
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "SweetFX - CeeJayDK":
                            client.DownloadFile(@"https://github.com/CeeJayDK/SweetFX/archive/master.zip", randTempRootFolder + @"\sweetfx.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "Color Effects - prod80":
                            client.DownloadFile(@"https://github.com/prod80/prod80-ReShade-Repository/archive/master.zip", randTempRootFolder + @"\prod80.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "Depth3D - BlueSkyDefender":
                            client.DownloadFile(@"https://github.com/BlueSkyDefender/Depth3D/archive/master.zip", randTempRootFolder + @"\depth3d.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "AstrayFX - BlueSkyDefender":
                            client.DownloadFile(@"https://github.com/BlueSkyDefender/AstrayFX/archive/master.zip", randTempRootFolder + @"\astrayfx.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "OtisFX - Otis Inf":
                            client.DownloadFile(@"https://github.com/FransBouma/OtisFX/archive/master.zip", randTempRootFolder + @"\otisfx.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "Pirate Shaders - Heathen":
                            client.DownloadFile(@"https://github.com/Heathen/Pirate-Shaders/archive/master.zip", randTempRootFolder + @"\pirate.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "Shaders - Brussell1":
                            client.DownloadFile(@"https://github.com/brussell1/Shaders/archive/master.zip", randTempRootFolder + @"\brussell1.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "Shaders - Daodan317081":
                            client.DownloadFile(@"https://github.com/Daodan317081/reshade-shaders/archive/master.zip", randTempRootFolder + @"\daodan.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "CorgiFX - originalnicoder":
                            client.DownloadFile(@"https://github.com/originalnicodr/CorgiFX/archive/master.zip", randTempRootFolder + @"\corgifx.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;


                        case "Fubax - Fubaxiusz":
                            client.DownloadFile(@"https://github.com/Fubaxiusz/fubax-shaders/archive/master.zip", randTempRootFolder + @"\fubax.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "FXShaders - luluco250":
                            client.DownloadFile(@"https://github.com/luluco250/FXShaders/archive/master.zip", randTempRootFolder + @"\fxshaders.zip");
                            fxshaders = true;
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "Shaders - Radegast":
                            client.DownloadFile(@"https://github.com/Radegast-FFXIV/reshade-shaders/archive/master.zip", randTempRootFolder + @"\radegast.zip");
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "Insane Shaders - Lord of Lunacy":
                            client.DownloadFile(@"https://github.com/LordOfLunacy/Insane-Shaders/archive/master.zip", randTempRootFolder + @"\insane.zip");
                            lunacy = true;
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;

                        case "MShaders - TreyM":
                            client.DownloadFile(@"https://github.com/TreyM/MShaders/archive/main.zip", randTempRootFolder + @"\mshaders.zip");
                            treym = true;
                            pbarValue += pbarInc;
                            worker.ReportProgress(pbarValue);
                            break;
                    }
                }
                client.Dispose();
                string[] shaderExtensions = { ".fx", ".cfg", ".fxh" };
                string[] textureExtensions = { ".png", ".dds", ".bmp", ".jpg", ".jpeg" };

                //Zip Extraction + Special cases----------------

                //Legacy repo, contains old shaders that have been updated in other repos, but also many shaders that are abandoned.
                //Move this repo first so that newer repos can overwrite when necessary.
                if (legacy == true)
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(randTempRootFolder + @"\legacy.zip", randTempRootFolder + @"\legacy");
                    }
                    catch
                    {
                        Directory.Delete(randTempRootFolder + @"\legacy", true);
                        ZipFile.ExtractToDirectory(randTempRootFolder + @"\legacy.zip", randTempRootFolder + @"\legacy");
                    }
                    File.Delete(randTempRootFolder + @"\legacy.zip");
                    File.Delete(randTempRootFolder + @"\legacy\reshade-shaders-master\Shaders\MXAO.fx"); //outdated mxao, causes issues with quint so delete
                    Functions.moveFiles(randTempRootFolder + @"\legacy", Program.shaders, shaderExtensions);
                    Functions.moveFiles(randTempRootFolder + @"\legacy", Program.textures, textureExtensions);
                }

                //Extract all repos for processing
                Functions.extractAllZip(randTempRootFolder, randTempRootFolder, new string[] { ".zip" });

                //FXShaders requires additional files in a specific folder, move that folder now.
                if (fxshaders == true)
                {
                    Directory.CreateDirectory(Program.shaders + @"\" + "FXShaders");
                    DirectoryExtensions.MoveDirectoryOverwrite(randTempRootFolder + @"\FXShaders-master\Shaders\FXShaders", Program.shaders + @"\FXShaders");
                }

                if (treym == true)
                {
                    Directory.CreateDirectory(Program.shaders + @"\" + "Include");
                    DirectoryExtensions.MoveDirectoryOverwrite(randTempRootFolder + @"\MShaders-main\Shaders\MShaders\Include", Program.shaders + @"\Include");
                }

                //Lord of Lunacy has folders that contain in-development/deprecated shaders.  Delete those now.
                if (lunacy == true)
                {
                    Directory.Delete(randTempRootFolder + @"\Insane-Shaders-master\Shaders\DevShaders", true);
                    Directory.Delete(randTempRootFolder + @"\Insane-Shaders-master\Shaders\OldShaders", true);
                }

                //End special cases------------

                Functions.copyFiles(randTempRootFolder, Program.shaders, shaderExtensions);
                Functions.copyFiles(randTempRootFolder, Program.textures, textureExtensions);
                if (File.Exists(randTempRootFolder + @"\reshade-shaders-slim\Shaders\ReShade.fxh"))
                {
                    File.Copy(randTempRootFolder + @"\reshade-shaders-slim\Shaders\ReShade.fxh", Program.shaders + @"\ReShade.fxh", true); //Some repos contain outdated ReShade.fxh/ReShadeUI.fxh files.  Copy the correct one and overwrite.
                }

                if (File.Exists(randTempRootFolder + @"\reshade-shaders-slim\Shaders\ReShadeUI.fxh"))
                {
                    File.Copy(randTempRootFolder + @"\reshade-shaders-slim\Shaders\ReShadeUI.fxh", Program.shaders + @"\ReShadeUI.fxh", true); //Some repos contain outdated ReShade.fxh/ReShadeUI.fxh files.  Copy the correct one and overwrite.
                }

                Directory.Delete(randTempRootFolder, true);

                worker.ReportProgress(100);
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                pbar2.Value = e.ProgressPercentage;
            }
            catch
            {
                pbar2.Value = 100;
            }
            labelShaders.Text = "Update Progress: " + e.ProgressPercentage.ToString() + "%";
            labelShaders.Visible = true;
        }
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            updateShaders.Enabled = true;
            labelShaders.Text = "Update Complete";
        }

        private void updateShaders_Click(object sender, EventArgs e)
        {
            try
            {
                updateShaders.Enabled = false;
                backgroundWorker2.RunWorkerAsync();
            }
            catch
            {
                MessageBox.Show("Update already in progress");
            }
        }

        private void reinstallAllReshadeiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                reinstallAllReshadeiniToolStripMenuItem.Enabled = false;
                backgroundWorker3.RunWorkerAsync();
            }
            catch
            {
                MessageBox.Show("reshade.ini update already in progress");
            }
        }

        //reinstall reshade.ini files recursively
        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            var allFiles = Directory.GetFiles(Program.presets, @"reshade.ini", SearchOption.AllDirectories);
            foreach (var file in allFiles)
            {
                string workingDLLPath = String.Empty; //needed for the shader cache regeneration

                //Separate game name from path
                string gameName = file;
                gameName = gameName.Remove(0, Program.presets.Length + 1);
                gameName = gameName.Remove(gameName.Length - 12);

                //Open file, search for relevant line to determine which dll was installed previously so that the proper shader cache can be applied
                StreamReader r = new StreamReader(file);
                string line = r.ReadLine();
                bool found = false;
                while (!r.EndOfStream && found == false)
                {
                    if (line.Length > 22 && line.StartsWith("IntermediateCachePath=")) //prevent being out of string bounds for remove
                    {
                        string IntermediateCachePath = line.Substring(22);
                        IntermediateCachePath = IntermediateCachePath.Remove(IntermediateCachePath.Length - 6);
                        if (IntermediateCachePath == Program.dlls)
                        {
                            workingDLLPath = Program.dlls;
                        }
                        else
                        {
                            workingDLLPath = Program.mdlls;
                        }
                        found = true;
                    }
                    line = r.ReadLine();
                }
                if (r.EndOfStream == true)
                {
                    r.Close();
                    MessageBox.Show(@"Error, something went wrong detecting IntermediateCachePath in reshade.ini for " + gameName + @". File has not been updated.");
                }
                else
                {
                    r.Close();
                    Functions.overwriteIni(
                        file,
                        new List<string>() { "EffectSearchPaths=", "IntermediateCachePath=", "PresetPath=", "TextureSearchPaths=", "SavePath=" },
                        new List<string>() { Program.shaders, workingDLLPath + @"\Cache", Program.presets, Program.textures, Program.screenshots }
                        );
                    //File.Delete(file);
                    //Functions.writeReshadeini(file.Remove(file.Length - 12), workingDLLPath, gameName);
                }
            }
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            reinstallAllReshadeiniToolStripMenuItem.Enabled = true;
            MessageBox.Show("All reshade.ini files have been updated.");
        }

        private void customizePathsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomizePathsForm f = new CustomizePathsForm();
            f.Show();
        }

        
    }
}
