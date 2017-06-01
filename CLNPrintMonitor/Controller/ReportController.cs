using CLNPrintMonitor.Model;
using CLNPrintMonitor.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                string buffer = "";
                foreach(ListViewItem item in this.lvwPrinters.Items)
                {
                    if (item.Checked)
                    {
                        Printer printer = this.printers.Find(find => find.Name == item.Text);
                        if(printer != null)
                        {
                            buffer += await printer.GetRawReportFromDevice();
                        }
                    }
                }
                if(buffer != "")
                {
                    byte[] file = Helpers.CreatePDF(Helpers.CreateDocument(buffer));
                    Helpers.SavePdfFile(sfdReport.FileName,file);
                }
            }
        } 
    }
}
