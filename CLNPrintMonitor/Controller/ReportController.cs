using CLNPrintMonitor.Model;
using CLNPrintMonitor.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Globalization;

namespace CLNPrintMonitor.Controller
{
    public partial class ReportController : Form
    {
        private List<Printer> printers;

        /// <summary>
        /// Construtor que recebe uma lista de impressoras
        /// </summary>
        /// <param name="printers"></param>
        public ReportController(List<Printer> printers)
        {
            InitializeComponent();
            this.printers = printers;
            CreateCheckboxMembers();
        }

        /// <summary>
        /// Cria os membros da checkbox de acordo com a lista de impressoras
        /// </summary>
        public void CreateCheckboxMembers()
        {
            foreach(var printer in this.printers)
            {
                if(printer.Status != StatusIcon.Offline)
                {
                    var item = new ListViewItem(new string[] {
                        printer.Name,
                        printer.Address.ToString()
                    })
                    {
                        Checked = true
                    };
                    this.lvwPrinters.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Abre o dialogo de relatório
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowReportDialog(object sender, System.EventArgs e)
        {
            new Task(() => CreateReport()).Start();
        }
        
        /// <summary>
        /// Cria relatório
        /// Necessita ser reformulada para dar suporte para formatação de arquivo
        /// </summary>
        private async void CreateReport()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { CreateReport(); });
                return;
            }
            if (sfdReport.ShowDialog() == DialogResult.OK)
            {
                int total = this.lvwPrinters.Items.Count;
                this.pgbLoading.Value = 0;
                List<HtmlAgilityPack.HtmlDocument> pages = await this.PrinterPageLoad(100 / total);
                this.pgbLoading.Value = 100;
                if (pages.Count != 0)
                {
                    this.SavePDFFromPages(pages);
                }
            }
        } 

        private async Task<List<HtmlAgilityPack.HtmlDocument>> PrinterPageLoad(int perPageItemPercent = 10)
        {
            var pages = new List<HtmlAgilityPack.HtmlDocument>();
            foreach (ListViewItem item in this.lvwPrinters.Items)
            {
                if (item.Checked)
                {
                    Printer printer = this.printers.Find(find => find.Name == item.Text);
                    if (printer != null)
                    {
                        string buffer = await printer.GetRawReportFromDevice();
                        buffer = buffer.Replace(
                            "<title>Estat. do dispositivo</title>",
                            "<h2>" + printer.Address.ToString() + "</h2><br/>");
                        buffer = buffer.Replace(
                            "<center><FONT STYLE=\"font-family: sans-serif; font-size: 20pt; font-weight: bold; color: #0000EE\">Estat. do dispositivo</FONT></center>",
                            "<h3>" + printer.Name + "</h3><br/>");
                        pages.Add(Helpers.CreateDocument(buffer));
                    }
                }
                this.pgbLoading.Value += perPageItemPercent;
            }
            return pages;
        }

        private void SavePDFFromPages(List<HtmlAgilityPack.HtmlDocument> pages)
        {
            byte[] file = Helpers.CreateMultiPagePDF(pages);
            if (chbEmail.Checked)
            {
                Helpers.SavePdfFile(sfdReport.FileName, file, false);
                Console.WriteLine(sfdReport.FileName);
                MapiMailMessage message = new MapiMailMessage(
                    "Relatório de impressão de " + DateTime.Now.ToString("MMMM 'de' yyyy", CultureInfo.CreateSpecificCulture("pt-br")) + " do Campus Litoral Norte", 
                    "Prezados, \n\nsegue anexo o relatório de impressão de " + 
                    DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", CultureInfo.CreateSpecificCulture("pt-br")) + 
                    " do Campus Litoral Norte. \nPor favor, confirmar recebimento.\n\nAtenciosamente, \nDivisão de Tecnologia da Informação\nCampus Litoral Norte\nUniversidade Federal do Rio Grande do Sul");
                message.Recipients.Add("Tecnoset <tecnoset@cpd.ufrgs.br>");
                message.Recipients.Add(" Suporte ufrgs <suporte.ufrgs@tecnoset.com.br>");
                message.Files.Add(@sfdReport.FileName);
                message.ShowDialog();
            }
            else
            {
                Helpers.SavePdfFile(sfdReport.FileName, file);
            }
        }
    }
}
