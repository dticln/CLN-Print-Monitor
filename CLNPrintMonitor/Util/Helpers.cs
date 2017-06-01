using CLNPrintMonitor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using CLNPrintMonitor.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

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

        public static bool SavePdfFile(string path, byte[] stream)
        {
            try
            {
                File.WriteAllBytes(path, stream);
                System.Diagnostics.Process.Start(path);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Falha ao salvar arquivo.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        
        public static byte[] SimplePDFReport(HtmlAgilityPack.HtmlDocument html)
        {
            MemoryStream msOutput = new MemoryStream();
            TextReader reader = new StringReader(html.DocumentNode.InnerHtml);
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, msOutput);
            HTMLWorker worker = new HTMLWorker(document);
            document.Open();
            worker.StartDocument();
            worker.Parse(reader);
            worker.EndDocument();
            worker.Close();
            document.Close();
            return msOutput.ToArray();
        }


        /// <summary>
        /// Create a HtmlDocument from string
        /// </summary>
        /// <param name="text">Data from request</param>
        /// <returns>A HtmlDocument from text</returns>
        public static HtmlAgilityPack.HtmlDocument CreateDocument(string text)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(text);
            return document;
        }

        public static void Notify(NotifyIcon notify, ToolTipIcon icon, String title, String body, int time)
        {
            notify.BalloonTipIcon = icon;
            notify.BalloonTipTitle = title;
            notify.BalloonTipText = body;
            notify.ShowBalloonTip(time);
        }


        public static void ConnectionErrorMessage()
        {
            MessageBox.Show(Resources.ConnectionErrorBody, Resources.ConnectionErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    }

}

