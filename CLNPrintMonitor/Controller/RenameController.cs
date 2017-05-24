using CLNPrintMonitor.Model;
using System;
using System.Windows.Forms;
using CLNPrintMonitor.Controller;
using System.Threading.Tasks;
using CLNPrintMonitor.Persistence;

namespace CLNPrintMonitor.Controller
{
    public partial class RenameController : Form
    {
        private Printer reference;
        private Main context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="printer"></param>
        public RenameController(Main context, Printer printer)
        {
            InitializeComponent();
            this.txbName.Text = printer.Name;
            this.lblAddress.Text = printer.Address.ToString();
            this.gpbRename.Text = printer.Model;
            this.reference = printer;
            this.context = context;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameAction(object sender, EventArgs e)
        {
            if (txbName.Text != String.Empty)
            {
                new Task(() => ExecuteRenameTask(this.context, this.reference, txbName.Text)).Start();
            }
            else
            {
                txbName.Focus();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="printer"></param>
        /// <param name="name"></param>
        private async void ExecuteRenameTask(Main context, Printer printer, string name)
        {
            try
            {
                Repository rep = Repository.GetInstance;
                this.InvokeLock();
                int id = await rep.SelectId(printer.Address.ToString());
                if (id != 0)
                {
                    printer.Name = name;
                    await rep.Update(id, printer);
                    context.InvokeUpdateItem(printer);
                    this.InvokeClose();
                    return;
                }
                this.InvokeUnlock();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitAction(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InvokeClose()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeClose(); });
                return;
            }
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InvokeLock()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeLock(); });
                return;
            }
            this.btnCancel.Enabled = false;
            this.btnRename.Enabled = false;
            this.txbName.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InvokeUnlock()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeUnlock(); });
                return;
            }
            this.btnCancel.Enabled = true;
            this.btnRename.Enabled = true;
            this.txbName.Enabled = true;
        }
    }
}
