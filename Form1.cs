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
        //paths for centralized folders
        string shaders = System.String.Empty;
        string textures = System.String.Empty;
        string presets = System.String.Empty; //also reshade config folder
        string screenshots = System.String.Empty;
        string dlls = System.String.Empty;
        string mdlls = System.String.Empty;
        List<iniEntry> games = new List<iniEntry>();

        struct iniEntry
        {
            public string value; //string on line
            public int ln; //line number
        }

        public Form1()
        {
            InitializeComponent();
            if (!File.Exists("ReShadeCentralized.ini"))
            {
                MessageBox.Show("Looks like this is your first time running ReShade Centralized, initiating setup.  Please select your desired ReShade Centralized folder.", "First Time Startup Message");
                CommonOpenFileDialog centralizedDialog = new CommonOpenFileDialog();
                centralizedDialog.Title = "Create and/or Select your ReShade Centralized folder";
                centralizedDialog.IsFolderPicker = true;
                centralizedDialog.InitialDirectory = @"C:\";
                centralizedDialog = getUserDirectoryCommon(centralizedDialog);
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
                readIni();
                Directory.CreateDirectory(shaders);
                Directory.CreateDirectory(textures);
                Directory.CreateDirectory(presets);
                Directory.CreateDirectory(screenshots);
                Directory.CreateDirectory(dlls);
                Directory.CreateDirectory(dlls + @"\Cache");
                Directory.CreateDirectory(mdlls);
                Directory.CreateDirectory(mdlls + @"\Cache");
                backgroundWorker1.RunWorkerAsync(); //download reshade files
                backgroundWorker2.RunWorkerAsync(); //download shader files
            }
            else
            {
                readIni();
            }

        }
        //Helper Functions-------------------------------------------------------------
        private void readIni()
        {
            using (StreamReader r = new StreamReader("ReShadeCentralized.ini"))
            {
                //List<string> configfile = new List<string>();
                Regex rx = new Regex(@"\[.*\]");
                //int i = 0;
                int lineNum = 0;
                while (r.EndOfStream == false)
                {
                    iniEntry line = new iniEntry();
                    line.value = r.ReadLine();
                    
                    lineNum++;
                    if (line.value == "[paths]")
                    {
                        line.value = r.ReadLine();
                        lineNum++;
                        while (!r.EndOfStream || !rx.IsMatch(line.value))
                        {
                            if (line.value.StartsWith("shaders="))
                            {
                                shaders = line.value.Substring(8);
                            }
                            else if (line.value.StartsWith("textures="))
                            {
                                textures = line.value.Substring(9);
                            }
                            else if (line.value.StartsWith("presets="))
                            {
                                presets = line.value.Substring(8);
                            }
                            else if (line.value.StartsWith("screenshots="))
                            {
                                screenshots = line.value.Substring(12);
                            }
                            else if (line.value.StartsWith("dlls="))
                            {
                                dlls = line.value.Substring(5);
                            }
                            else if (line.value.StartsWith("mdlls="))
                            {
                                mdlls = line.value.Substring(6);
                            }
                            line.value = r.ReadLine();
                            lineNum++;
                            //while (String.IsNullOrEmpty(line.value) && !r.EndOfStream)
                            //{
                            //    line.value = r.ReadLine();
                            //    lineNum++;
                            //}

                        }
                    }
                    else if (line.value == "[games]")
                    {
                        line.value = r.ReadLine();
                        lineNum++;
                        while (!r.EndOfStream || !rx.IsMatch(line.value))
                        {
                            line.ln = lineNum;
                            games.Add(line);
                            //i++;
                            line.value = r.ReadLine();
                            lineNum++;
                        }
                    }
                }
                r.Close();
            }
        }

        private void writeGameIni(string game, string path, bool append = true) //WORK IN PROGRESS
        {
            if (append == true)
            {
                using (StreamWriter w = new StreamWriter("ReShadeCentralized.ini", true))
                {
                    w.WriteLine(game + "=" + path);
                    w.Close();
                }
            }
            else
            {
                
            }

        }

        private CommonOpenFileDialog getUserDirectoryCommon(CommonOpenFileDialog dir)
        {
            bool exitLoop = false;
            while (exitLoop == false)
            {
                CommonFileDialogResult result = dir.ShowDialog();
                if (!(result == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(dir.FileName)))
                {
                    MessageBox.Show("You must select a valid directory, try again");
                }
                else
                {
                    exitLoop = true;
                }
            }
            return dir;
        }

        private OpenFileDialog getUserDirectory(OpenFileDialog dir)
        {
            bool exitLoop = false;
            while (exitLoop == false)
            {
                DialogResult result = dir.ShowDialog();
                if (!(result == DialogResult.OK && !string.IsNullOrWhiteSpace(dir.FileName)))
                {
                    MessageBox.Show("You must select a valid file, try again");
                }
                else
                {
                    exitLoop = true;
                }
            }
            return dir;
        }

        private void moveWithReplace(string source, string dest)
        {
            if (File.Exists(dest))
            {
                File.Delete(dest);
            }

            File.Move(source, dest);
        }


        private void moveFiles(string source, string dest, string[] extensions) //enumerates all files of given extensions in source directory and subdirectories and moves them to dest directory, effectively collapsing folder structure.
        {
            DirectoryInfo d = new DirectoryInfo(source);

            var files =
                d.EnumerateFiles("*", SearchOption.AllDirectories)
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();
            
            foreach (var file in files)
            {
                moveWithReplace(file.FullName, dest + @"\" + file.Name);
            }
        }

        private void copyFiles(string source, string dest, string[] extensions) //enumerates all files of given extensions in source directory and subdirectories and moves them to dest directory, effectively collapsing folder structure.
        {
            DirectoryInfo d = new DirectoryInfo(source);

            var files =
                d.EnumerateFiles("*", SearchOption.AllDirectories)
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();

            foreach (var file in files)
            {
                File.Copy(file.FullName, dest + @"\" + file.Name, true);
            }
        }

        private void extractAllZip(string source, string dest, string[] extensions)
        {
            DirectoryInfo d = new DirectoryInfo(source);

            var files =
                d.EnumerateFiles("*", SearchOption.AllDirectories)
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();

            foreach (var file in files)
            {
                ZipArchiveExtensions.ExtractToDirectory(ZipFile.OpenRead(file.FullName), dest, true);
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
            OpenFileDialog gameDialog = new OpenFileDialog();

            gameDialog.Title = "Select the game's runtime executable.";
            gameDialog.Filter = "Select EXE|*.exe";
            gameDialog.InitialDirectory = @"C:\";
            gameDialog = getUserDirectory(gameDialog);


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
                    gamedll = @"\reshade-delete-me.dll";
                    MessageBox.Show("Feature coming soon.  For now, install ReShade for Vulkan globally through the normal ReShade installer.  This program will setup the rest.");
                    break;
            }


            string gameName = Prompt.ShowDialog(@"Enter Game name. This is used for the creation of the Presets and Screenshots Folders.", "Game Name", 520, 150);
            while (string.IsNullOrEmpty(gameName))
            {
                MessageBox.Show("Game Name can't be blank, try again.");
                gameName = Prompt.ShowDialog(@"Enter Game name. This is used for the creation of the Presets and Screenshots Folders.", "Game Name", 520, 150);
            }

            string workingDLLPath = dlls; //apply working path to variable, mostly for reshade.ini generation

            if (Prompt.ShowRadioButtons(new string[] { "Official", "Modified" }, @"Select Official or Modified ReShade.", 250, 140, @"Modified ReShade files are self provided.  Place ReShade64.dll and ReShade32.dll in the reshade-files-mod folder to use.").Text == "Official")
            {
                if(File.Exists(Path.GetDirectoryName(gameDialog.FileName) + gamedll)) //placed here and duplicated in Else{} just in case the user closes the application when prompted
                {
                    File.Delete(Path.GetDirectoryName(gameDialog.FileName) + gamedll);
                }
                if (GetMachineType(gameDialog.FileName) == MachineType.x64)
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
                workingDLLPath = mdlls;
                if (GetMachineType(gameDialog.FileName) == MachineType.x64)
                {
                    SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, workingDLLPath + @"\ReShade64.dll", 0);
                }
                else
                {
                    SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + gamedll, workingDLLPath + @"\ReShade32.dll", 0);
                }
            }

            Directory.CreateDirectory(screenshots + @"\" + gameName);
            Directory.CreateDirectory(presets + @"\" + gameName);

            string nl = "\n";
            if (!File.Exists(presets + @"\" + gameName + @"\reshade.ini"))
            {
                File.WriteAllText(presets + @"\" + gameName + @"\reshade.ini",
                    @"[GENERAL]" + nl +
                    @"EffectSearchPaths=" + shaders + nl +
                    @"IntermediateCachePath=" + workingDLLPath + @"\Cache" + nl +
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
                    @"[SCREENSHOT]" + nl +
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
                SymbolicLink.CreateSymbolicLink(Path.GetDirectoryName(gameDialog.FileName) + @"\reshade.ini", presets + @"\" + gameName + @"\reshade.ini", 0);
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

        //Update Shaders Thread
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(0);

            var items = Prompt.ShowCheckBoxes(new string[] { "qUINT - Marty McFly", "Standard - Crosire", "Legacy - Crosire", "SweetFX - CeeJayDK", "Color Effects - prod80", "Depth3D - BlueSkyDefender", "AstrayFX - BlueSkyDefender", "OtisFX - Otis Inf", "Pirate Shaders - Heathen", "Shaders - Brussell1", "Shaders - Daodan317081", "CorgiFX - originalnicoder", "Fubax - Fubaxiusz", "FXShaders - luluco250", "Shaders - Radegast", "Insane Shaders - Lord of Lunacy" }, @"Select shader repos to download.", 200, 400, 178).CheckedItems;

            using (var client = new WebClient())
            {
                bool legacy = false;
                bool fxshaders = false;
                bool lunacy = false;
                var rand = new Random();
                string randTempRootFolder = @".\temp" + rand.Next().ToString();
                while (Directory.Exists(randTempRootFolder))
                {
                    randTempRootFolder = @".\temp" + rand.Next().ToString();
                }
                Directory.CreateDirectory(randTempRootFolder);
                int pbarInc = 100 / (items.Count + 1); //split progress bar increments into how many repos get downloaded + extraction step (and prevents divide by 0)
                for (int i = 0; i < items.Count; i++)
                {
                    switch (items[i])
                    {
                        case "qUINT - Marty McFly":
                            client.DownloadFile(@"https://github.com/martymcmodding/qUINT/archive/master.zip", randTempRootFolder + @"\quint.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "Standard - Crosire":
                            client.DownloadFile(@"https://github.com/crosire/reshade-shaders/archive/slim.zip", randTempRootFolder + @"\crosire.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "Legacy - Crosire":
                            client.DownloadFile(@"https://github.com/crosire/reshade-shaders/archive/master.zip", randTempRootFolder + @"\legacy.zip");
                            legacy = true;
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "SweetFX - CeeJayDK":
                            client.DownloadFile(@"https://github.com/CeeJayDK/SweetFX/archive/master.zip", randTempRootFolder + @"\sweetfx.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "Color Effects - prod80":
                            client.DownloadFile(@"https://github.com/prod80/prod80-ReShade-Repository/archive/master.zip", randTempRootFolder + @"\prod80.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "Depth3D - BlueSkyDefender":
                            client.DownloadFile(@"https://github.com/BlueSkyDefender/Depth3D/archive/master.zip", randTempRootFolder + @"\depth3d.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "AstrayFX - BlueSkyDefender":
                            client.DownloadFile(@"https://github.com/BlueSkyDefender/AstrayFX/archive/master.zip", randTempRootFolder + @"\astrayfx.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "OtisFX - Otis Inf":
                            client.DownloadFile(@"https://github.com/FransBouma/OtisFX/archive/master.zip", randTempRootFolder + @"\otisfx.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "Pirate Shaders - Heathen":
                            client.DownloadFile(@"https://github.com/Heathen/Pirate-Shaders/archive/master.zip", randTempRootFolder + @"\pirate.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "Shaders - Brussell1":
                            client.DownloadFile(@"https://github.com/brussell1/Shaders/archive/master.zip", randTempRootFolder + @"\brussell1.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "Shaders - Daodan317081":
                            client.DownloadFile(@"https://github.com/Daodan317081/reshade-shaders/archive/master.zip", randTempRootFolder + @"\daodan.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "CorgiFX - originalnicoder":
                            client.DownloadFile(@"https://github.com/originalnicodr/CorgiFX/archive/master.zip", randTempRootFolder + @"\corgifx.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;
                            

                        case "Fubax - Fubaxiusz":
                            client.DownloadFile(@"https://github.com/Fubaxiusz/fubax-shaders/archive/master.zip", randTempRootFolder + @"\fubax.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "FXShaders - luluco250":
                            client.DownloadFile(@"https://github.com/luluco250/FXShaders/archive/master.zip", randTempRootFolder + @"\fxshaders.zip");
                            fxshaders = true;
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "Shaders - Radegast":
                            client.DownloadFile(@"https://github.com/Radegast-FFXIV/reshade-shaders/archive/master.zip", randTempRootFolder + @"\radegast.zip");
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
                            break;

                        case "Insane Shaders - Lord of Lunacy":
                            client.DownloadFile(@"https://github.com/LordOfLunacy/Insane-Shaders/archive/master.zip", randTempRootFolder + @"\insane.zip");
                            lunacy = true;
                            worker.ReportProgress(pbarInc);
                            pbarInc += pbarInc;
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
                    moveFiles(randTempRootFolder + @"\legacy", shaders, shaderExtensions);
                    moveFiles(randTempRootFolder + @"\legacy", textures, textureExtensions);
                }

                //Extract all repos for processing
                extractAllZip(randTempRootFolder, randTempRootFolder, new string[] { ".zip" });

                //FXShaders requires additional files in a specific folder, move that folder now.
                if (fxshaders == true)
                {
                    Directory.CreateDirectory(shaders + @"\" + "FXShaders");
                    DirectoryExtensions.MoveDirectoryOverwrite(randTempRootFolder + @"\FXShaders-master\Shaders\FXShaders", shaders + @"\FXShaders");
                }

                //Lord of Lunacy has folders that contain in-development/deprecated shaders.  Delete those now.
                if (lunacy == true)
                {
                    Directory.Delete(randTempRootFolder + @"\Insane-Shaders-master\Shaders\DevShaders", true);
                    Directory.Delete(randTempRootFolder + @"\Insane-Shaders-master\Shaders\OldShaders", true);
                }

                //End special cases------------

                copyFiles(randTempRootFolder, shaders, shaderExtensions);
                copyFiles(randTempRootFolder, textures, textureExtensions);
                File.Copy(randTempRootFolder + @"\reshade-shaders-slim\Shaders\ReShade.fxh", shaders + @"\ReShade.fxh", true); //Some repos contain outdated ReShade.fxh/ReShadeUI.fxh files.  Copy the correct one and overwrite.
                File.Copy(randTempRootFolder + @"\reshade-shaders-slim\Shaders\ReShadeUI.fxh", shaders + @"\ReShadeUI.fxh", true); //Some repos contain outdated ReShade.fxh/ReShadeUI.fxh files.  Copy the correct one and overwrite.
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
        }
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            updateShaders.Enabled = true;
            MessageBox.Show("Shaders Updated");
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
    }

    
}