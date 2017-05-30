namespace CLNPrintMonitor.Controller
{
    partial class Main
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnAddPrinter = new System.Windows.Forms.Button();
            this.lvwMain = new System.Windows.Forms.ListView();
            this.clnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnIpv4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxNamePrinter = new System.Windows.Forms.TextBox();
            this.tbxIpPrinter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.stsFooter = new System.Windows.Forms.StatusStrip();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.mnsHeader = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUpdateStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUpdateList = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmsListViewItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.relatórioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sspDeleteSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.smiRename = new System.Windows.Forms.ToolStripMenuItem();
            this.smiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.nfiNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sfdReport = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.mnsHeader.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cmsListViewItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddPrinter
            // 
            this.btnAddPrinter.Location = new System.Drawing.Point(477, 17);
            this.btnAddPrinter.Name = "btnAddPrinter";
            this.btnAddPrinter.Size = new System.Drawing.Size(74, 23);
            this.btnAddPrinter.TabIndex = 1;
            this.btnAddPrinter.Text = "Adicionar";
            this.btnAddPrinter.UseVisualStyleBackColor = true;
            this.btnAddPrinter.Click += new System.EventHandler(this.AddPrinter);
            // 
            // lvwMain
            // 
            this.lvwMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clnName,
            this.clnIpv4});
            this.lvwMain.Location = new System.Drawing.Point(6, 19);
            this.lvwMain.Name = "lvwMain";
            this.lvwMain.Size = new System.Drawing.Size(545, 230);
            this.lvwMain.TabIndex = 2;
            this.lvwMain.TileSize = new System.Drawing.Size(250, 150);
            this.lvwMain.UseCompatibleStateImageBehavior = false;
            this.lvwMain.View = System.Windows.Forms.View.Tile;
            this.lvwMain.ItemActivate += new System.EventHandler(this.ShowPrinterForm);
            this.lvwMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewClickHandler);
            // 
            // clnName
            // 
            this.clnName.Text = "Nome";
            this.clnName.Width = 390;
            // 
            // clnIpv4
            // 
            this.clnIpv4.Text = "IP";
            this.clnIpv4.Width = 126;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbxNamePrinter);
            this.groupBox1.Controls.Add(this.tbxIpPrinter);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnAddPrinter);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 48);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adicionar impressora";
            // 
            // tbxNamePrinter
            // 
            this.tbxNamePrinter.Location = new System.Drawing.Point(177, 19);
            this.tbxNamePrinter.Name = "tbxNamePrinter";
            this.tbxNamePrinter.Size = new System.Drawing.Size(294, 20);
            this.tbxNamePrinter.TabIndex = 5;
            // 
            // tbxIpPrinter
            // 
            this.tbxIpPrinter.Location = new System.Drawing.Point(29, 19);
            this.tbxIpPrinter.Name = "tbxIpPrinter";
            this.tbxIpPrinter.Size = new System.Drawing.Size(101, 20);
            this.tbxIpPrinter.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP";
            // 
            // stsFooter
            // 
            this.stsFooter.Location = new System.Drawing.Point(0, 339);
            this.stsFooter.Name = "stsFooter";
            this.stsFooter.Size = new System.Drawing.Size(584, 22);
            this.stsFooter.TabIndex = 4;
            this.stsFooter.Text = "statusStrip1";
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 5000;
            this.tmrRefresh.Tick += new System.EventHandler(this.RefreshTick);
            // 
            // mnsHeader
            // 
            this.mnsHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.tsmUpdate});
            this.mnsHeader.Location = new System.Drawing.Point(0, 0);
            this.mnsHeader.Name = "mnsHeader";
            this.mnsHeader.Size = new System.Drawing.Size(584, 24);
            this.mnsHeader.TabIndex = 5;
            this.mnsHeader.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.tsmExit});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.menuToolStripMenuItem.Text = "Arquivo";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItem1.Text = "&Relatórios";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.NewReportForm);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(126, 22);
            this.tsmExit.Text = "&Sair";
            this.tsmExit.Click += new System.EventHandler(this.ExitClick);
            // 
            // tsmUpdate
            // 
            this.tsmUpdate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUpdateStatus,
            this.tsmUpdateList});
            this.tsmUpdate.Name = "tsmUpdate";
            this.tsmUpdate.Size = new System.Drawing.Size(65, 20);
            this.tsmUpdate.Text = "Atualizar";
            // 
            // tsmUpdateStatus
            // 
            this.tsmUpdateStatus.Name = "tsmUpdateStatus";
            this.tsmUpdateStatus.Size = new System.Drawing.Size(180, 22);
            this.tsmUpdateStatus.Text = "Status";
            this.tsmUpdateStatus.Click += new System.EventHandler(this.UpdatePrinterStatus);
            // 
            // tsmUpdateList
            // 
            this.tsmUpdateList.Name = "tsmUpdateList";
            this.tsmUpdateList.Size = new System.Drawing.Size(180, 22);
            this.tsmUpdateList.Text = "Lista de impressoras";
            this.tsmUpdateList.Click += new System.EventHandler(this.UpdatePrinterList);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lvwMain);
            this.groupBox2.Location = new System.Drawing.Point(12, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 255);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Impressoras";
            // 
            // cmsListViewItem
            // 
            this.cmsListViewItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.relatórioToolStripMenuItem,
            this.sspDeleteSeparator,
            this.smiRename,
            this.smiDelete});
            this.cmsListViewItem.Name = "cmsListViewItem";
            this.cmsListViewItem.Size = new System.Drawing.Size(129, 76);
            // 
            // relatórioToolStripMenuItem
            // 
            this.relatórioToolStripMenuItem.Name = "relatórioToolStripMenuItem";
            this.relatórioToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.relatórioToolStripMenuItem.Text = "Relatório";
            this.relatórioToolStripMenuItem.Click += new System.EventHandler(this.ShowReportDialog);
            // 
            // sspDeleteSeparator
            // 
            this.sspDeleteSeparator.Name = "sspDeleteSeparator";
            this.sspDeleteSeparator.Size = new System.Drawing.Size(125, 6);
            // 
            // smiRename
            // 
            this.smiRename.Name = "smiRename";
            this.smiRename.Size = new System.Drawing.Size(128, 22);
            this.smiRename.Text = "Renomear";
            this.smiRename.Click += new System.EventHandler(this.NewRenameForm);
            // 
            // smiDelete
            // 
            this.smiDelete.Name = "smiDelete";
            this.smiDelete.Size = new System.Drawing.Size(128, 22);
            this.smiDelete.Text = "Excluir";
            this.smiDelete.Click += new System.EventHandler(this.NewDeleteForm);
            // 
            // nfiNotify
            // 
            this.nfiNotify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.nfiNotify.BalloonTipText = "Uma impressora necessita de atenção.";
            this.nfiNotify.ContextMenuStrip = this.cmsNotify;
            this.nfiNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("nfiNotify.Icon")));
            this.nfiNotify.Text = "CLN Print Monitor";
            this.nfiNotify.Visible = true;
            this.nfiNotify.Click += new System.EventHandler(this.NotifyClick);
            // 
            // cmsNotify
            // 
            this.cmsNotify.Name = "cmsNotify";
            this.cmsNotify.Size = new System.Drawing.Size(61, 4);
            // 
            // sfdReport
            // 
            this.sfdReport.FileName = "relatorio";
            this.sfdReport.Filter = "Arquivo PDF|*.pdf";
            this.sfdReport.Title = "Relatório de impressão";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stsFooter);
            this.Controls.Add(this.mnsHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnsHeader;
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "Main";
            this.Text = "Print Monitor Agent - Campus Litoral Norte";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.MainResize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mnsHeader.ResumeLayout(false);
            this.mnsHeader.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.cmsListViewItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddPrinter;
        private System.Windows.Forms.ListView lvwMain;
        private System.Windows.Forms.ColumnHeader clnName;
        private System.Windows.Forms.ColumnHeader clnIpv4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxIpPrinter;
        private System.Windows.Forms.StatusStrip stsFooter;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.TextBox tbxNamePrinter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip mnsHeader;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip cmsListViewItem;
        private System.Windows.Forms.ToolStripMenuItem smiRename;
        private System.Windows.Forms.ToolStripSeparator sspDeleteSeparator;
        private System.Windows.Forms.ToolStripMenuItem smiDelete;
        private System.Windows.Forms.NotifyIcon nfiNotify;
        private System.Windows.Forms.ContextMenuStrip cmsNotify;
        private System.Windows.Forms.ToolStripMenuItem tsmUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmUpdateList;
        private System.Windows.Forms.ToolStripMenuItem tsmUpdateStatus;
        private System.Windows.Forms.ToolStripMenuItem relatórioToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdReport;
    }
}

