using CLNPrintMonitor.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLNPrintMonitor.Util
{
    
    class Helpers
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Normalize(string value)
        {
            string[] replace = new String[] { @"\t", @"\n", @"\r", ":", "&nbsp;" };
            foreach(string s in replace)
            {
                value = value.Replace(s, string.Empty);
            }
            return value.Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetInteger(string value)
        {
            Match match = new Regex(@"\d+").Match(value);
            if(match.Success)
            {
                return Int32.Parse(match.Value);
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBar"></param>
        /// <param name="state"></param>
        public static void ModifyProgressBarColor(ProgressBar pBar, int state)
        {
            if (!pBar.IsDisposed)
            {
                NativeMethods.SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Send a asynchronous request and retrives the response data
        /// </summary>
        /// <param name="url">Target url</param>
        /// <returns>Page data</returns>
        public static async Task<string> SendHttpRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "CLNPrinterMonitor Agent " + Application.ProductVersion; 
            request.Method = WebRequestMethods.Http.Get;
            //request.ContentType = "text/plain;charset=utf-8";
            request.Timeout = 20000;
            request.Proxy = null;
            WebResponse response = await request.GetResponseAsync();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printers"></param>
        /// <param name="ipv4"></param>
        /// <returns></returns>
        public static Printer FindPrinter(Collection<Printer> printers, string ipv4)
        {
            foreach (Printer item in printers)
            {
                if (item.Address.ToString() == ipv4)
                {
                    return item;
                }
            }
            return null;
        }

    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    }
}

