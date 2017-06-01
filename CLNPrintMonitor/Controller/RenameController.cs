using CLNPrintMonitor.Model;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using CLNPrintMonitor.Persistence;

namespace CLNPrintMonitor.Controller
{
    public partial class RenameController : Form
    {
        private Printer reference;
        private Main context;

        /// <summary>
        /// Construtor do formulário, recebe o contexto de execução
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
        /// Executa a tarefa de modificação do nome da impressora em um thread distinta 
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
        /// Envia solicitação para API para renomear impressora
        /// </summary>
        /// <param name="context">Contexto de execução</param>
        /// <param name="printer">Impressora alvo</param>
        /// <param name="name">Novo nome</param>
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
        /// Fecha form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitAction(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Realiza o fechamento do formulário por outra um thread diferente da UIThread
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
        /// Bloqueia campos do formulário por outra UIThread
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
        /// Desbloqueia campos do formulário por outra UIThread
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
