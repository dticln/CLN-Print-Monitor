using CLNPrintMonitor.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLNPrintMonitor.Controller
{
    public partial class PrinterController : Form
    {
        private Printer printer;
        
        public PrinterController(Printer printer)
        {
            InitializeComponent();
            this.printer = printer;
            this.lblName.Text = printer.Name;
        }
    }
}
