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
using iTextSharp.tool.xml;
using System.Xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;

namespace CLNPrintMonitor.Util
{
    
    class Helpers
    {
        public static object HttpUtility { get; private set; }

        /// <summary>
        /// Coloca a string em formato adequado para utilização
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
        /// Recupera valor inteiro de uma string
        /// </summary>
        /// <param name="value">String com um número</param>
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
        /// Modifica a cor da barra de progresso
        /// </summary>
        /// <param name="pBar">Barra de progresso</param>
        /// <param name="state">Id do status</param>
        public static void ModifyProgressBarColor(ProgressBar pBar, int state)
        {
            if (!pBar.IsDisposed)
            {
                NativeMethods.SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Envia requisição HTTP por GET
        /// </summary>
        /// <param name="url">URL desejada</param>
        /// <param name="isoCharset">A requisição é para uma página com caracteres ISO?</param>
        /// <returns></returns>
        public static async Task<string> SendHttpRequest(string url, bool isoCharset = false)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (isoCharset)
            {
                response.Content.Headers.ContentType.CharSet = "ISO-8859-1";
            }
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Envia requisição HTTP por POST
        /// </summary>
        /// <param name="url">URL desejada</param>
        /// <param name="param">Parametros POST em formato chave/valor</param>
        /// <returns>String do HTML de resposta</returns>
        public static async Task<string> SendHttpPostRequest(string url, Dictionary<string,string> param)
        {
            HttpClient client = new HttpClient();
            FormUrlEncodedContent content = new FormUrlEncodedContent(param);
            HttpResponseMessage response = await client.PostAsync(Resources.ApiUri, content);
            return await response.Content.ReadAsStringAsync();

        }
        
        /// <summary>
        /// Procura uma impressora em uma lista de impressoras por IPV4
        /// </summary>
        /// <param name="printers">Coleção de impressoras</param>
        /// <param name="ipv4">Ip da impressora</param>
        /// <returns>Impressora</returns>
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
        /// Serializa um objeto Dictionary em formato de request 
        /// </summary>
        /// <param name="dict">Dados</param>
        /// <returns>Formato de requisição web</returns>
        private static string DictionaryToString(Dictionary<string, string> dict)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                builder.Append(kvp.Key + "=" + kvp.Value + "&");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Salva um PDF em formato byte[] em arquivo
        /// </summary>
        /// <param name="path">Nome do arquivo</param>
        /// <param name="stream">PDF</param>
        /// <returns>Resposta</returns>
        public static bool SavePdfFile(string path, byte[] stream, bool openOnReader = true)
        {
            try
            {
                File.WriteAllBytes(path, stream);
                if(openOnReader)
                {
                    System.Diagnostics.Process.Start(path);
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(Resources.SaveFileErrorBody, Resources.SaveFileError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        
        /// <summary>
        /// Gera PDF a partir de um documento HtmlDocument
        /// </summary>
        /// <param name="html">HTML</param>
        /// <returns>PDF em byte[]</returns>
        public static byte[] CreatePDF(HtmlAgilityPack.HtmlDocument html)
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
        
        public static byte[] CreateMultiPagePDF(List<HtmlAgilityPack.HtmlDocument> pages)
        {
            MemoryStream msOutput = new MemoryStream();
            Document document = new Document(PageSize.A4, 40, 40, 40, 40);
            PdfWriter writer = PdfWriter.GetInstance(document, msOutput);
            HTMLWorker worker = new HTMLWorker(document);
            
            document.Open();
            worker.StartDocument();
            foreach(var item in pages)
            {
                worker.Parse(new StringReader(item.DocumentNode.InnerHtml));
                document.NewPage();
            }
            worker.EndDocument();
            worker.Close();
            document.Close();
            return msOutput.ToArray();
        }

        /// <summary>
        /// Cria um HtmlDocument a partir de uma string
        /// </summary>
        /// <param name="text">String HTML</param>
        /// <returns>Resultado HtmlDocument</returns>
        public static HtmlAgilityPack.HtmlDocument CreateDocument(string text)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(text);
            return document;
        }

        /// <summary>
        /// Envia notificação para usuário pelo Notify
        /// </summary>
        /// <param name="notify">Notify que será modificado</param>
        /// <param name="title">Título da notificação</param>
        /// <param name="body">Corpo da notificação</param>
        /// <param name="icon">Icone (opcional)</param>
        /// <param name="time">Tempo que será visualizado (opcional)</param>
        public static void Notify(NotifyIcon notify, String title, String body, ToolTipIcon icon = ToolTipIcon.None, int time = 60)
        {
            notify.BalloonTipIcon = icon;
            notify.BalloonTipTitle = title;
            notify.BalloonTipText = body;
            notify.ShowBalloonTip(time);
        }

        /// <summary>
        /// Exibe mensagem de erro de conexão
        /// </summary>
        public static void ConnectionErrorMessage()
        {
            MessageBox.Show(Resources.ConnectionErrorBody, Resources.ConnectionErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Exibe mensagem padrão de erro
        /// </summary>
        public static void DefaultErrorMessage()
        {
            MessageBox.Show(Resources.DefaultErrorBody, Resources.DefaultError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Exibe mensagem para item já existente
        /// </summary>
        public static void AlreadyExistsMessage()
        {
            MessageBox.Show(Resources.AlreadyExistsBody, Resources.AlreadyExists, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }

    internal static class NativeMethods
    {
        /// <summary>
        /// Importa DLL do sistema para modificação da cor da progressbar
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    }

}

