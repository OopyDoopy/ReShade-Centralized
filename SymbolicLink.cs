using System.Runtime.InteropServices;

namespace ReShade_Centralized
{
    public static class SymbolicLink
    {
        [DllImport("kernel32.dll")]
        public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags); //dwFlags: 0 = file 1 = directory
    }
}
