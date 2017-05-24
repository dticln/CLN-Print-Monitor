using CLNPrintMonitor.Model;
using CLNPrintMonitor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CLNPrintMonitor.Util;

namespace CLNPrintMonitor.Controller
{
    public partial class PrinterController : Form
    {

        private Printer printer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printer"></param>
        public PrinterController(Printer printer)
        {
            InitializeComponent();
            this.UpdatePrinterReference(printer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printer"></param>
        public void UpdatePrinterReference(Printer printer)
        {
            this.printer = printer;
            this.printer.ControllerUIRelation = this;
            this.printer.UpdateUIInformation += new Printer.UpdateUIHandler(InvokeUpdateUI);
            this.printer.UpdateUIInformation(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public void InvokeUpdateUI(PrinterController target)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { target.InvokeUpdateUI(target); });
                return;
            }
            target.Text = printer.Name;
            target.lblIPV4.Text = printer.Address.ToString();
            target.lblModel.Text = printer.Model;
            target.lblType.Text = printer.DeviceType;
            target.lblSpeed.Text = printer.Speed;
            target.lblTonerCapacity.Text = printer.TonerCapacity;
            target.pgbInk.Value = printer.Ink;
            target.pgbFc.Value = printer.Fc;
            target.pgbMaintenance.Value = printer.Maintenance;
            target.pcbStatus.Image = PrinterController.GetStatusImage(printer.Status);
            target.lblFcPercent.Text = printer.Fc + "%";
            target.lblInkPercent.Text = printer.Ink + "%";
            target.lblMaintenancePercent.Text = printer.Maintenance + "%";
            target.grbDefaultInput.Text = printer.DefaultInput.Name;
            target.lblDefaultInputStatus.Text = printer.DefaultInput.Status;
            target.lblDefaultInputCapacity.Text = Resources.CapacityOf + printer.DefaultInput.Capacity.ToString() + Resources.Sheets;
            target.lblDefaultInputScale.Text = Resources.Size + printer.DefaultInput.Scale;
            target.lblDefaultInputType.Text = printer.DefaultInput.Type;
            target.lblDefaultInputStatus.Text = printer.DefaultInput.Status;
            target.grbSecondaryInput.Text = printer.SupplyMF.Name;
            target.lblSupplyMfInputCapacity.Text = Resources.CapacityOf + printer.SupplyMF.Capacity.ToString() + Resources.Sheets;
            target.lblSupplyMfInputScale.Text = Resources.Size + printer.SupplyMF.Scale;
            target.lblSupplyMfInputType.Text = printer.SupplyMF.Type;
            target.lblSupplyMfStatus.Text = printer.SupplyMF.Status;
            target.gpbOutput.Text = printer.DefaultOutput.Name;
            target.lblOuputCapacity.Text = Resources.CapacityOf + printer.DefaultOutput.Capacity.ToString() + Resources.Sheets;
            target.lblOuputStatus.Text = printer.DefaultOutput.Status;
            target.SetProgressBarColor(pgbFc);
            target.SetProgressBarColor(pgbInk);
            target.SetProgressBarColor(pgbMaintenance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static Image GetStatusImage(StatusIcon icon)
        {
            Image image = null;
            switch (icon)
            {
                case StatusIcon.Ink0:
                    image = (Image)Resources.ResourceManager.GetObject("ink0");
                    break;
                case StatusIcon.Ink30:
                    image = (Image)Resources.ResourceManager.GetObject("ink30");
                    break;
                case StatusIcon.Ink60:
                    image = (Image)Resources.ResourceManager.GetObject("ink60");
                    break;
                case StatusIcon.Ink90:
                    image = (Image)Resources.ResourceManager.GetObject("ink90");
                    break;
                case StatusIcon.Ink100:
                    image = (Image)Resources.ResourceManager.GetObject("ink100");
                    break;
                case StatusIcon.Offline:
                    image = (Image)Resources.ResourceManager.GetObject("offline");
                    break;
                case StatusIcon.Error:
                    image = (Image)Resources.ResourceManager.GetObject("error");
                    break;
            }
            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pgb"></param>
        internal void SetProgressBarColor(ProgressBar pgb)
        {
            if(pgb.Value >= 60)
            {
                Helpers.ModifyProgressBarColor(pgb, 1);
            } else if (pgb.Value <= 30)
            {
                Helpers.ModifyProgressBarColor(pgb, 2);
            } else
            {
                Helpers.ModifyProgressBarColor(pgb, 3);
            }
        }
        
    }
}