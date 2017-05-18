using CLNPrintMonitor.Model;
using CLNPrintMonitor.Properties;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLNPrintMonitor
{
    public partial class Main : Form
    {

        private ObservableCollection<Printer> printers;

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
                    if(await printer.GetInformationFromDevice())
                    {
                        InvokeUpdateItem(printer);
                    }
                }).Start();
            } else
            {
                MessageBox.Show("O endereço IP deve seguir os padrões de formatação.", "Endereço IP inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tbxIpPrinter.Text = "";
            tbxNamePrinter.Text = "";
        }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Printer printer = e.NewItems[0] as Printer;
                    ListViewItem item = new ListViewItem();
                    item.Text = printer.Name;
                    item.SubItems.Add(printer.Address.ToString());
                    item.ImageIndex = (int)printer.Status;
                    InvokeAddItems(item);
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }
        
        private void InvokeAddItems(ListViewItem item)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { this.InvokeAddItems(item); });
                return;
            }
            lvwMain.Items.Add(item);
        }

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
                }
            }
        }

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
        
        private void TmrRefreshTick(object sender, EventArgs e)
        {
            new Task(async () =>
            {
                
                for (int i = 0; i < this.printers.Count; i++)
                {
                    bool response = await this.printers[i].GetInformationFromDevice();
                }
                InvokeUpdateItems();
            }).Start();
        }
    }
}