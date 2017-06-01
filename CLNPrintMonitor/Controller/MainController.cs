using CLNPrintMonitor.Model;
using CLNPrintMonitor.Persistence;
using CLNPrintMonitor.Properties;
using CLNPrintMonitor.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace CLNPrintMonitor.Controller
{
    public partial class Main : Form
    {

        private const int DEFAULT_NOTIFY_TIME = 60;
        private const int DEFAULT_TICK_TIME = 30000;
        private const int BACKGROUND_TICK_TIME = 60000;
        private ObservableCollection<Printer> printers;
        private PrinterController childrenForm;

        /// <summary>
        /// Inicializa componentes visuais e objetos da classe principal
        /// Instancia lista observavel de impressora que será utilizada ao longo do programa
        /// Inicia lista de imagens dos incones e atribui delegates
        /// Por fim, recupera impressoras da fonte de dados remota
        /// </summary>
        public Main()
        {
            InitializeComponent();
            this.printers = new ObservableCollection<Printer>();
            /// Adicionar o novo Handler para itens duplicados
            Repository.GetInstance.ConnectionErroHandler += new Repository.ConnectionErrorUIHandler(Helpers.ConnectionErrorMessage);
            this.printers.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChangedMethod);
            this.lvwMain.LargeImageList = new ImageList()
            {
                ImageSize = new Size(150, 150),
                Images = {
                    (Image)Resources.ResourceManager.GetObject("ink0"),
                    (Image)Resources.ResourceManager.GetObject("ink30"),
                    (Image)Resources.ResourceManager.GetObject("ink60"),
                    (Image)Resources.ResourceManager.GetObject("ink90"),
                    (Image)Resources.ResourceManager.GetObject("ink100"),
                    (Image)Resources.ResourceManager.GetObject("offline"),
                    (Image)Resources.ResourceManager.GetObject("error")
                }
            };
            this.GetPrintersFromRemote();
        }
        
        #region Invoke methods for UIThread

        /// <summary>
        /// Realiza a adição de um item na list view de impressoras
        /// É capaz de realizar essa adição mesmo que a chamada seja realizada fora da UIThread
        /// </summary>
        /// <param name="item">Novo item</param>
        private void InvokeAddItem(ListViewItem item)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeAddItem(item); });
                return;
            }
            lvwMain.Items.Add(item);
        }

        /// <summary>
        /// Realiza a remoção de um item na list view de impressoras 
        /// É capaz de realizar essa remoção mesmo que a chamada seja realizada fora da UIThread 
        /// </summary>
        /// <param name="item">Ip do item que será removido</param>
        private void InvokeRemoveItemWith(string ip)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeRemoveItemWith(ip); });
                return;
            }
            for (int i = 0; i < this.lvwMain.Items.Count; i++)
            {
                if (this.lvwMain.Items[i].SubItems[1].Text == ip)
                {
                    lvwMain.Items.Remove(this.lvwMain.Items[i]);
                    return;
                }
            }
        }

        /// <summary>
        /// Modifica atributos de um item na ListView
        /// É capaz de realizar essa atualização mesmo que a chamada seja realizada fora da UIThread 
        /// </summary>
        /// <param name="printer">Impressora </param>
        public void InvokeUpdateItem(Printer printer)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeUpdateItem(printer); });
                return;
            }
            for (int i = 0; i < this.lvwMain.Items.Count; i++)
            {
                ListViewItem item = this.lvwMain.Items[i];
                if (item.SubItems[1].Text == printer.Address.ToString())
                {
                    item.ImageIndex = (int)printer.Status;
                    item.Text = printer.Name;
                    item.SubItems[1].Text = printer.Address.ToString();
                    item.SubItems[2].Text = printer.Feedback;
                    SetSubitemColor(item.SubItems[2], printer.Feedback);
                    return;
                }
            }
        }

        /// <summary>
        /// Realiza a exclusão de todos os itens presentes o ListView
        /// É capaz de realizar essa exclusão mesmo que a chamada seja realizada fora da UIThread 
        /// </summary>
        private void InvokeClearItems()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeClearItems(); });
                return;
            }
            this.lvwMain.Items.Clear();
        }

        /// <summary>
        /// Mostra o dialogo para salvar um relatório
        /// É capaz de realizar esse procedimento mesmo que a chamada seja realizada fora da UIThread 
        /// </summary>
        private async void InvokeSaveReportDialog()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { InvokeSaveReportDialog(); });
                return;
            }
            if (sfdReport.ShowDialog() == DialogResult.OK)
            {
                Printer printer = Helpers.FindPrinter(this.printers, this.lvwMain.FocusedItem.SubItems[1].Text);
                byte[] file = await printer.GetReportFromDevice();
                Helpers.SavePdfFile(sfdReport.FileName, file);
            }
        }

        #endregion

        #region Event handlers 

        /// <summary>
        /// Evento que trata o click no botão de "Adicionar impressora"
        /// Inclui uma nova impressora na lista de impressoras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPrinter(object sender, EventArgs e)
        {
            String strIp = tbxIpPrinter.Text;
            String name = tbxNamePrinter.Text;
            if (strIp != String.Empty && name != String.Empty && IPAddress.TryParse(strIp, out IPAddress ipv4))
            {
                new Task(async () =>
                {
                    Printer printer = new Printer(name, ipv4);
                    if (await Repository.GetInstance.Add(printer))
                    {
                        this.printers.Add(printer);
                        if (await printer.GetInformationFromDevice())
                        {
                            InvokeUpdateItem(printer);
                        }
                    }
                }).Start();
            }
            else
            {
                MessageBox.Show(Resources.IpErrorBody, Resources.IpErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tbxIpPrinter.Text = String.Empty;
            tbxNamePrinter.Text = String.Empty;
        }

        /// <summary>
        /// Manipulador de eventos para a lista observavel (this.printers)
        /// Sera disparado para cada ação realizada no list view (add, remove, clear)
        /// </summary>
        /// <see cref="printers"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            Printer printer = null;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ///
                    /// Cria uma nova impressora
                    ///
                    printer = e.NewItems[0] as Printer;
                    ListViewItem item = new ListViewItem(new string[] {
                        printer.Name,
                        printer.Address.ToString(),
                        printer.Feedback
                    })
                    {
                        ImageIndex = (int)printer.Status,
                        UseItemStyleForSubItems = false
                    };
                    item.SubItems[1].ForeColor = Color.DimGray;
                    InvokeAddItem(item);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ///
                    /// Remove uma impressora
                    ///
                    printer = e.OldItems[0] as Printer;
                    InvokeRemoveItemWith(printer.Address.ToString());
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                    ///
                    /// Limpa itens do ListView
                    ///
                    InvokeClearItems();
                    break;
            }
        }

        /// <summary>
        /// A cada 30 segundos (ou 1 minutos em caso de execução em segundo plano)
        /// atualiza os status das impressoras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshTick(object sender, EventArgs e)
        {
            foreach (Printer printer in this.printers)
            {
                new Task(() => this.VerifyPrinter(printer)).Start();
            }

        }

        /// <summary>
        /// Finaliza a aplicação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        /// <summary>
        /// Mostra o formulário com os dados da impressora selecionada
        /// Caso já exista uma janela ativa, atualiza os dados presentes nela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void ShowPrinterForm(object sender, EventArgs e)
        {
            ListView list = sender as ListView;
            ListViewItem clicked = list.SelectedItems[0];
            Printer printer = Helpers.FindPrinter(this.printers, clicked.SubItems[1].Text);
            if(printer != null && printer.Status != StatusIcon.Offline)
            {
                if (this.childrenForm == null || this.childrenForm.IsDisposed)
                {
                    this.childrenForm = new PrinterController(printer);
                    this.childrenForm.Show();
                }
                else
                {
                    this.childrenForm.UpdatePrinterReference(printer);
                    this.childrenForm.BringToFront();
                }
            }
        }

        /// <summary>
        /// Motra o formulário para renomear uma impressora
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewRenameForm(object sender, EventArgs e)
        {
            Printer printer = Helpers.FindPrinter(this.printers, this.lvwMain.FocusedItem.SubItems[1].Text);
            RenameController rename = new RenameController(this,printer);
            rename.ShowDialog();
        }

        /// <summary>
        /// Mostra o formulário para deletar uma impressora
        /// Realiza a exclusão, caso seja Sim a resposta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDeleteForm(object sender, EventArgs e)
        {
            Printer printer = Helpers.FindPrinter(this.printers, this.lvwMain.FocusedItem.SubItems[1].Text);
            DialogResult result = MessageBox.Show(
                Resources.DeleteFormBody + printer.Address + "?",
                printer.Name,
                MessageBoxButtons.YesNo
            );
            switch (result)
            {
                case DialogResult.Yes:
                    new Task(async () => {
                        if (await Repository.GetInstance.Remove(printer.Address.ToString()))
                        {
                            InvokeRemoveItemWith(printer.Address.ToString());
                        }
                    }).Start();
                    break;
                case DialogResult.No:
                default:
                    break;
            }
        }

        /// <summary>
        /// Mostra o dialogo de relatório de uma única impressora
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowReportDialog(object sender, EventArgs e)
        {
            new Task(() => InvokeSaveReportDialog()).Start();
        }

        /// <summary>
        /// Mostra o menu de contexto da ListView ao clicar com o botão direito do mouse 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewClickHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewHitTestInfo info = this.lvwMain.HitTest(e.X, e.Y);
                if (info.Item != null)
                {
                    Printer current = Helpers.FindPrinter(this.printers, info.Item.SubItems[1].Text);
                    if(current.Status == StatusIcon.Offline)
                    {
                        cmsListViewItem.Items[0].Enabled = false;
                        cmsListViewItem.Show(this.lvwMain, new Point(e.X, e.Y));

                    } else
                    {
                        cmsListViewItem.Items[0].Enabled = true;
                        cmsListViewItem.Show(this.lvwMain, new Point(e.X, e.Y));
                    }
                } else
                {
                    cmsNonListViewItem.Show(this.lvwMain, new Point(e.X, e.Y));
                }
            }
        }

        /// <summary>
        /// Lida com a ação de redimencionamento de janela
        /// Quando a janela é minimizada, inicia o notificador
        /// Modifica o tempo de atualização das impressoras para 1 minuto (quando minimizado), notificando o usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainResize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.nfiNotify.Visible = true;
                Helpers.Notify(
                    this.nfiNotify,
                    ToolTipIcon.None,
                    Resources.MonitorAgent,
                    Resources.NotifyEnabled,
                    DEFAULT_NOTIFY_TIME
                );
                if (this.childrenForm != null && !this.childrenForm.IsDisposed)
                {
                    this.childrenForm.Close();
                }
                this.tmrRefresh.Interval = BACKGROUND_TICK_TIME;
                this.Hide();
            }
        }

        /// <summary>
        /// Gerenciador de click no notificador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyClick(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            if(me.Button == MouseButtons.Right)
            {
                this.nfiNotify.ContextMenuStrip.Show();
            } else
            {
                this.tmrRefresh.Interval = DEFAULT_TICK_TIME;
                this.Show();
                this.nfiNotify.Visible = false;
                this.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// Dispara a atualização de impressoras, similiar ao evento de tick do timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatePrinterStatus(object sender, EventArgs e)
        {
            RefreshTick(null, null);
        }

        /// <summary>
        /// Recupera lista de impressoras, trata evento de click de menus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatePrinterList(object sender, EventArgs e)
        {
            GetPrintersFromRemote();
        }

        /// <summary>
        /// Abre formulário para geração de relatório
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewReportForm(object sender, EventArgs e)
        {
            List<Printer> list = new List<Printer>();
            foreach(var item in this.printers)
            {
                list.Add((Printer)item.Clone());   
            }
            ReportController rename = new ReportController(list);
            rename.ShowDialog();
        }

        #endregion

        /// <summary>
        /// Verifica impressora e, caso haja alteração, notifica usuário e atualiza a lista
        /// </summary>
        /// <param name="printer"></param>
        private async void VerifyPrinter(Printer printer)
        {
            StatusIcon old = printer.Status;
            bool response = await printer.GetInformationFromDevice();
            this.TrySendNotification(printer, old);
            InvokeUpdateItem(printer);
        }

        /// <summary>
        /// Realiza a requisição de atualização de impressora
        /// Evento disparado em uma thread separada do fluxo de execução do programa
        /// </summary>
        private void GetPrintersFromRemote()
        {
            new Task(async () =>
            {
                printers.Clear();
                Repository rep = Repository.GetInstance;
                List<Printer> list = await rep.SelectAll();
                foreach (var item in list)
                {
                    printers.Add(item);
                }
            }).Start();
        }

        /// <summary>
        /// Tenta enviar notificação para o usuário
        /// Caso a janela esteja minimizada e haja alteração, envia notificação
        /// </summary>
        /// <param name="printer">Impressora modificada</param>
        /// <param name="old">Antigo status</param>
        private void TrySendNotification(Printer printer, StatusIcon old)
        {
            if (this.WindowState == FormWindowState.Minimized && old != printer.Status)
            {
                switch (printer)
                {
                    case var a when a.Ink is 0:
                        Helpers.Notify(
                            this.nfiNotify,
                            ToolTipIcon.Warning,
                            a.Address.ToString(),
                            Resources.NotifyIconText + a.Address + Resources.NotifyIconProblem + a.Feedback.ToLower(),
                            DEFAULT_NOTIFY_TIME
                        );
                        break;
                    case var b when b.DefaultInput.Status != Resources.Ok:
                        Helpers.Notify(
                            this.nfiNotify,
                            ToolTipIcon.Info,
                            b.DefaultInput.Name + Resources.OfPrinter + b.Address.ToString(),
                            b.DefaultInput.Name + Resources.OfPrinter + b.Address + Resources.NotifyIconProblem + b.DefaultInput.Status.ToLower(),
                            DEFAULT_NOTIFY_TIME
                        );
                        break;
                    case var c when c.DefaultOutput.Status != Resources.Ok:
                        Helpers.Notify(
                            this.nfiNotify,
                            ToolTipIcon.Info,
                            c.DefaultOutput.Name + Resources.OfPrinter + c.Address.ToString(),
                            c.DefaultInput.Name + Resources.OfPrinter + c.Address + Resources.NotifyIconProblem + c.DefaultInput.Status.ToLower(),
                            DEFAULT_NOTIFY_TIME
                        );
                        break;
                    case var d when d.SupplyMF.Status != Resources.Ok:
                        Helpers.Notify(
                            this.nfiNotify,
                            ToolTipIcon.Info,
                            d.SupplyMF.Name + Resources.OfPrinter + d.Address.ToString(),
                            d.DefaultInput.Name + Resources.OfPrinter + d.Address + Resources.NotifyIconProblem + d.DefaultInput.Status.ToLower(),
                            DEFAULT_NOTIFY_TIME
                        );
                        break;
                    case var e when e.Maintenance < 10:
                        Helpers.Notify(
                            this.nfiNotify,
                            ToolTipIcon.Warning,
                            Resources.NotifyLowMaintenance,
                            Resources.NotifyLowMaintenanceBody + e.Address + Resources.NotifyLow,
                            DEFAULT_NOTIFY_TIME
                        );
                        break;
                    case var f when f.Fc < 10:
                        Helpers.Notify(
                           this.nfiNotify,
                           ToolTipIcon.Warning,
                           Resources.NotifyLowFC,
                           Resources.NotifyLowFCBody + f.Address + Resources.NotifyLow,
                           DEFAULT_NOTIFY_TIME
                        );
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Define uma cor personalizada para o subitem da list view
        /// </summary>
        /// <param name="item"></param>
        /// <param name="status"></param>
        private void SetSubitemColor(ListViewSubItem item, string status)
        {
            item.Text = status;
            switch (item.Text)
            {
                case var a when a.Contains("Pronto"):
                    item.ForeColor = Color.Green;
                    break;
                case var b when b.Contains("Economiz."):
                    item.ForeColor = Color.CornflowerBlue;
                    break;
                case var b when b.Contains("Ocupada"):
                    item.ForeColor = Color.Yellow;
                    break;
                default:
                    item.ForeColor = Color.Red;
                    break;
            }
        }
        
    }
}