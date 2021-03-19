using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ReShade_Centralized
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        //paths for centralized folders
        public static string shaders = System.String.Empty;
        public static string textures = System.String.Empty;
        public static string presets = System.String.Empty; //also reshade config folder
        public static string screenshots = System.String.Empty;
        public static string dlls = System.String.Empty;
        public static string mdlls = System.String.Empty;
        //public static List<iniEntry> games = new List<iniEntry>();

        public struct iniEntry //This struct is unneeded but is setup this way in case functionality is added where knowing the line number is beneficial.
        {
            public string value; //string on line
            //public int ln; //line number
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
}

