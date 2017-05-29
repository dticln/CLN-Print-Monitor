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

        public ReportController(List<Printer> printers)
        {
            InitializeComponent();
            this.printers = printers;
            CreateCheckboxMembers();
        }

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

        private void ShowReportDialog(object sender, System.EventArgs e)
        {
            new Task(() => CreateReport()).Start();
        }
        
        /// <summary>
        /// REFAZER METODO
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
                    byte[] file = Helpers.SimplePDFReport(Helpers.CreateDocument(buffer));
                    Helpers.SavePdfFile(sfdReport.FileName,file);
                }
            }
        } 
    }
}
