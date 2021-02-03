﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ReShade_Centralized
{
    public static class Functions
    {
        public static void readRCIni()
        {
            using (StreamReader r = new StreamReader(@".\ReShadeCentralized.ini"))
            {
                int lineNum = 0;
                Program.iniEntry line = new Program.iniEntry();

                while (!r.EndOfStream)
                {
                    line.value = r.ReadLine();
                    lineNum++;
                    if (line.value.StartsWith("shaders="))
                    {
                        Program.shaders = line.value.Substring(8);
                    }
                    else if (line.value.StartsWith("textures="))
                    {
                        Program.textures = line.value.Substring(9);
                    }
                    else if (line.value.StartsWith("presets="))
                    {
                        Program.presets = line.value.Substring(8);
                    }
                    else if (line.value.StartsWith("screenshots="))
                    {
                        Program.screenshots = line.value.Substring(12);
                    }
                    else if (line.value.StartsWith("dlls="))
                    {
                        Program.dlls = line.value.Substring(5);
                    }
                    else if (line.value.StartsWith("mdlls="))
                    {
                        Program.mdlls = line.value.Substring(6);
                    }
                }
                r.Close();
            }

        }
    
    public static void overwriteIni(string path, List<string> var, List<string> value)
        {
            //Read file into memory
            List<string> file = new List<string>();
            using (StreamReader r = new StreamReader(path))
            {
                while(!r.EndOfStream)
                {
                    file.Add(r.ReadLine());
                }
            }
            //Write new text to file
            using (StreamWriter w = new StreamWriter(path))
            {
                int filecount = file.Count;
                int varcount = var.Count; //var and value will always be the same size
                for(int i = 0; i < filecount; i++)
                {
                    for (int j = 0; j < varcount; j++)
                    {
                        if (file[i].StartsWith(var[j]))
                        {
                            file[i] = var[j] + value[j];
                        }
                    }
                    w.WriteLine(file[i]);
                }
            }
        }

        public static CommonOpenFileDialog getUserDirectoryCommon(CommonOpenFileDialog dir)
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

        //public static OpenFileDialog getUserDirectory(OpenFileDialog dir)
        //{
        //    bool exitLoop = false;
        //    while (exitLoop == false)
        //    {
        //        DialogResult result = dir.ShowDialog();
        //        if (!(result == DialogResult.OK && !string.IsNullOrWhiteSpace(dir.FileName)))
        //        {
        //            MessageBox.Show("You must select a valid file, try again");
        //        }
        //        else
        //        {
        //            exitLoop = true;
        //        }
        //    }
        //    return dir;
        //}

        public static void writeReshadeini(string dest, string workingDLLPath, string gameName, string gameDir)
        {
            string nl = "\n"; //makes typing up that multiline write a bit easier
            File.WriteAllText(dest + @"\reshade.ini",
                    @"[GENERAL]" + nl +
                    @"EffectSearchPaths=" + Program.shaders + @"," + Program.shaders + @"\development" + nl +
                    @"IntermediateCachePath=" + workingDLLPath + @"\Cache" + nl +
                    @"PerformanceMode=0" + nl +
                    @"PreprocessorDefinitions=RESHADE_DEPTH_INPUT_IS_REVERSED=0,RESHADE_DEPTH_INPUT_IS_LOGARITHMIC=0,RESHADE_DEPTH_INPUT_IS_UPSIDE_DOWN=0,RESHADE_DEPTH_LINEARIZATION_FAR_PLANE=1000" + nl +
                    @"PresetPath=" + Program.presets + @"\" + gameName + @"\ReshadePreset.ini" + nl +
                    @"PresetTransitionDelay=1000" + nl +
                    @"SkipLoadingDisabledEffects=0" + nl +
                    @"TextureSearchPaths=" + Program.textures + @"," + Program.textures + @"\development" + nl + nl +
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
                    @"SavePath=" + Program.screenshots + @"\" + gameName + nl +
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
            SymbolicLink.CreateSymbolicLink(gameDir + @"\reshade.ini", dest + @"\reshade.ini", 0);
        }

        public static void moveWithReplace(string source, string dest)
        {
            if (File.Exists(dest))
            {
                File.Delete(dest);
            }

            File.Move(source, dest);
        }


        public static void moveFiles(string source, string dest, string[] extensions) //enumerates all files of given extensions in source directory and subdirectories and moves them to dest directory, effectively collapsing folder structure.
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

        public static void copyFiles(string source, string dest, string[] extensions) //enumerates all files of given extensions in source directory and subdirectories and moves them to dest directory, effectively collapsing folder structure.
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

        public static void extractAllZip(string source, string dest, string[] extensions)
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

        //Thank you Andrew Backer of stackoverflow------
        public enum MachineType
        {
            Native = 0, I386 = 0x014c, Itanium = 0x0200, x64 = 0x8664
        }

        public static MachineType GetMachineType(string fileName)
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

    }
}