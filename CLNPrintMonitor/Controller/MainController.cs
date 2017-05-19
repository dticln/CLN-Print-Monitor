using CLNPrintMonitor.Model;
using CLNPrintMonitor.Properties;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLNPrintMonitor.Controller
{
    public partial class Main : Form
    {

        private ObservableCollection<Printer> printers;
        private PrinterController childrenForm;
        
        /// <summary>
        /// Initialize component, ListView and printers ObservableCollection
        /// </summary>
        public Main()
        {
            InitializeComponent();
            this.printers = new ObservableCollection<Printer>();
            this.printers.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChangedMethod);
            this.lvwMain.LargeImageList = new ImageList();
            this.lvwMain.LargeImageList.ImageSize = new Size(150, 150);
            Image[] range = {
                (Image)Resources.ResourceManager.GetObject("ink0"),
                (Image)Resources.ResourceManager.GetObject("ink30"),
                (Image)Resources.ResourceManager.GetObject("ink60"),
                (Image)Resources.ResourceManager.GetObject("ink90"),
                (Image)Resources.ResourceManager.GetObject("ink100"),
                (Image)Resources.ResourceManager.GetObject("offline"),
                (Image)Resources.ResourceManager.GetObject("error")
            };
            this.lvwMain.LargeImageList.Images.AddRange(range);
        }

        private Printer FindPrinter(string Ip)
        {
            for (int i = 0; i < this.printers.Count; i++)
            {
                if (this.printers[i].Address.ToString() == Ip)
                {
                    return this.printers[i];
                }
            }
            return null;
        }

        #region Invoke methods for UIThread

        /// <summary>
        /// Invoke in UIThread (if necessary) the lvwMain.Items.Add() method
        /// </summary>
        /// <param name="item">Add new item in printers list</param>
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
        /// Invoke in UIThread (if necessary) the lvwMain.Items.Remove() method
        /// </summary>
        /// <param name="item"></param>
        private void InvokeRemoveItem(ListViewItem item)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeRemoveItem(item); });
                return;
            }
            lvwMain.Items.Remove(item);
        }

        /// <summary>
        /// Change an item Text in UIThread,
        /// searching for the target item by IPV4 address in printer object
        /// </summary>
        /// <param name="printer">New printer informations</param>
        private void InvokeUpdateItem(Printer printer)
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
                    return;
                }
            }
        }

        /// <summary>
        /// Change all items Text in UIThread
        /// using a printers list
        /// </summary>
        private void InvokeUpdateItems()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeUpdateItems(); });
                return;
            }
            for (int i = 0; i < this.lvwMain.Items.Count; i++)
            {
                ListViewItem item = this.lvwMain.Items[i];
                for (int j = 0; j < this.printers.Count; j++)
                {
                    Printer current = this.printers[j];
                    if (item.SubItems[1].Text == current.Address.ToString())
                    {
                        item.ImageIndex = (int)current.Status;
                        Console.WriteLine("Impressora " + current.Address.ToString() + " está sendo atualizada");
                    }
                }
            }
        }

        #endregion

        #region Event handlers 

        /// <summary>
        /// Button click action
        /// Include a new printer in the printers list
        /// </summary>
        /// <see cref="printers"/>
        /// <see cref="Printer"/>
        /// <param name="sender">Clicked button</param>
        /// <param name="e">Click event arg</param>
        private void BtnAddPrinterClickAsync(object sender, EventArgs e)
        {
            String strIp = tbxIpPrinter.Text;
            String name = tbxNamePrinter.Text;
            IPAddress ipv4;
            if (strIp != String.Empty &&
                name != String.Empty &&
                IPAddress.TryParse(strIp, out ipv4))
            {
                new Task(async () =>
                {
                    Printer printer = new Printer(name, ipv4);
                    this.printers.Add(printer);
                    if (await printer.GetInformationFromDevice())
                    {
                        InvokeUpdateItem(printer);
                    }
                }).Start();
            }
            else
            {
                MessageBox.Show("O endereço IP deve seguir os padrões de formatação.", "Endereço IP inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tbxIpPrinter.Text = "";
            tbxNamePrinter.Text = "";
        }

        /// <summary>
        /// Handler for CollectionChanged in ObservableCollection in printers list
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
                    printer = e.NewItems[0] as Printer;
                    ListViewItem item = new ListViewItem();
                    item.Text = printer.Name;
                    item.SubItems.Add(printer.Address.ToString());
                    item.ImageIndex = (int)printer.Status;
                    InvokeAddItem(item);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    printer = e.OldItems[0] as Printer;
                    for (int i = 0; i < this.lvwMain.Items.Count; i++)
                    {
                        if (this.lvwMain.Items[i].SubItems[1].Text == printer.Address.ToString())
                        {
                            InvokeRemoveItem(this.lvwMain.Items[i]);
                            return;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        /// <summary>
        /// Every 30 seconds execute printer status update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrRefreshTick(object sender, EventArgs e)
        {
            new Task(async () =>
            {
                StatusIcon old;
                for (int i = 0; i < this.printers.Count; i++)
                {
                    old = this.printers[i].Status;
                    bool response = await this.printers[i].GetInformationFromDevice();
                    this.TrySendNotification(this.printers[i], old);
                    InvokeUpdateItems();
                }
            }).Start();
        }

        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Shows a new Form with all informations about the selected printer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPrinterForm(object sender, EventArgs e)
        {
            ListView list = sender as ListView;
            ListViewItem clicked = list.SelectedItems[0];
            Printer printer = FindPrinter(clicked.SubItems[1].Text);
            if(printer != null && printer.Status != StatusIcon.Offline)
            {
                if(this.childrenForm == null || this.childrenForm.IsDisposed)
                {
                    this.childrenForm = new PrinterController(printer);
                    this.childrenForm.Show();
                } else
                {
                    this.childrenForm.UpdatePrinterReference(printer);
                    this.childrenForm.BringToFront();
                }
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewItemRenameForm(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Shows a delete form for an specific printer 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewItemDeleteForm(object sender, EventArgs e)
        {
            Printer printer = this.FindPrinter(this.lvwMain.FocusedItem.SubItems[1].Text);
            DialogResult result = MessageBox.Show(
                "Deseja excluir a impressora " + printer.Address + "?",
                printer.Name,
                MessageBoxButtons.YesNo
            );
            switch (result)
            {
                case DialogResult.Yes:
                    this.printers.Remove(printer);
                    break;
                case DialogResult.No:
                default:
                    break;
            }
        }

        /// <summary>
        /// Shows a ContextMenuStrop for list view items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewClickHandler(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right && this.lvwMain.FocusedItem.Bounds.Contains(me.Location))
            {
                cmsListViewItem.Show(Cursor.Position);
            }
        }

        #endregion

        private void MainResize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                if(this.childrenForm != null && !this.childrenForm.IsDisposed)
                {
                    this.childrenForm.Close();
                }
                this.tmrRefresh.Interval = 60000;
                this.Hide();
                this.nfiNotify.Visible = true;
            }
        }

        private void TrySendNotification(Printer printer, StatusIcon old)
        {
            if (this.WindowState == FormWindowState.Minimized && old != printer.Status)
            {
                switch (printer.Status)
                {
                    case StatusIcon.Ink0:
                        this.nfiNotify.BalloonTipIcon = ToolTipIcon.Info;
                        this.nfiNotify.BalloonTipTitle = "Aviso de toner";
                        this.nfiNotify.BalloonTipText = "A impressora " + printer.Address + " está ficando sem toner.";
                        this.nfiNotify.ShowBalloonTip(60);
                        break;
                    case StatusIcon.Error:
                        this.nfiNotify.BalloonTipIcon = ToolTipIcon.Error;
                        this.nfiNotify.BalloonTipTitle = "Problema em impressora";
                        this.nfiNotify.BalloonTipText = "A impressora " + printer.Address + " necessita de atenção.";
                        this.nfiNotify.ShowBalloonTip(60);
                        break;
                }
            }
        }

        private void NotifyClick(object sender, EventArgs e)
        {
            this.tmrRefresh.Interval = 30000;
            this.Show();
            this.nfiNotify.Visible = false;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}