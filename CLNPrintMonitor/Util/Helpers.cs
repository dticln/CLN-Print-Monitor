using CLNPrintMonitor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using CLNPrintMonitor.Properties;
using System.Collections.Specialized;

namespace CLNPrintMonitor.Util
{
    
    class Helpers
    {
        public static object HttpUtility { get; private set; }

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
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> SendHttpPostRequest(string url, Dictionary<string,string> param)
        {
            HttpClient client = new HttpClient();
            FormUrlEncodedContent content = new FormUrlEncodedContent(param);
            HttpResponseMessage response = await client.PostAsync(Resources.ApiUri, content);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> SendHttpRequestIso(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.Content.Headers.ContentType.CharSet = "ISO-8859-1";
            return await response.Content.ReadAsStringAsync();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private static string DictionaryToString(Dictionary<string, string> dict)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                builder.Append(kvp.Key + "=" + kvp.Value + "&");
            }
            return builder.ToString();
        }

    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    }

}

