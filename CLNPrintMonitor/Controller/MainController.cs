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
                    item.Text = printer.Name;
                    item.SubItems[1].Text = printer.Address.ToString();
                    return;
                }
            }
        }

        /// <summary>
        /// Change all items Text in UIThread
        /// using a printers list
        /// DEPRECATED
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

        /// <summary>
        /// Delete all items in UIThread
        /// </summary>
        private void InvokeClearItems()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeClearItems(); });
                return;
            }
            this.lvwMain.Clear();
        }

        #endregion

        #region Event handlers 

        /// <summary>
        /// Button click handler
        /// Include a new printer in the printers list
        /// </summary>
        /// <see cref="printers"/>
        /// <see cref="Printer"/>
        /// <param name="sender">Clicked button</param>
        /// <param name="e">Click event arg</param>
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
                    ListViewItem item = new ListViewItem(new string[] {
                        printer.Name,
                        printer.Address.ToString()
                    })
                    {
                        ImageIndex = (int)printer.Status
                    };
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
                    InvokeClearItems();
                    break;
            }
        }

        /// <summary>
        /// Every 30 seconds execute printer status update
        /// Timer handler
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
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Shows a new Form with all informations about the selected printer
        /// Click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPrinterForm(object sender, EventArgs e)
        {
            ListView list = sender as ListView;
            ListViewItem clicked = list.SelectedItems[0];
            Printer printer = Helpers.FindPrinter(this.printers, clicked.SubItems[1].Text);
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
        /// Click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewItemDeleteForm(object sender, EventArgs e)
        {
            Printer printer = Helpers.FindPrinter(this.printers, this.lvwMain.FocusedItem.SubItems[1].Text);
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

        /// <summary>
        /// Resize event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// NotifyIcon click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyClick(object sender, EventArgs e)
        {
            this.tmrRefresh.Interval = 30000;
            this.Show();
            this.nfiNotify.Visible = false;
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Menu "update printer status" item handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatePrinterStatus(object sender, EventArgs e)
        {
            RefreshTick(null, null);
        }

        /// <summary>
        /// Menu "update printer list" item handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatePrinterList(object sender, EventArgs e)
        {
            GetPrintersFromRemote();
        }

        #endregion

        /// <summary>
        /// 
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
        /// 
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
        /// Send notification in NotifyIcon
        /// </summary>
        /// <param name="printer"></param>
        /// <param name="old"></param>
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

    }
}