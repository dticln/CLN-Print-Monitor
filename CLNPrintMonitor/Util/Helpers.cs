using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CLNPrintMonitor.Util
{
    
    class Helpers
    {
        public static bool InvokeRequired { get; private set; }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);

        public static string Normalize(string value)
        {
            string[] replace = new String[] { @"\t", @"\n", @"\r", ":", "&nbsp;" };
            foreach(string s in replace)
            {
                value = value.Replace(s, string.Empty);
            }
            return value.Trim();
        }

        public static int GetInteger(string value)
        {
            Match match = new Regex(@"\d+").Match(value);
            if(match.Success)
            {
                return Int32.Parse(match.Value);
            }
            return 0;
        }

        public static void ModifyProgressBarColor(ProgressBar pBar, int state)
        {
            if (!pBar.IsDisposed)
            {
                SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
            }
        }

    }
}
