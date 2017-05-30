namespace CLNPrintMonitor.Controller
{
    partial class ReportController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvwPrinters = new System.Windows.Forms.ListView();
            this.ColumnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderIpv4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.sfdReport = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvwPrinters);
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 400);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Impressoras";
            // 
            // lvwPrinters
            // 
            this.lvwPrinters.CheckBoxes = true;
            this.lvwPrinters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeaderName,
            this.ColumnHeaderIpv4});
            this.lvwPrinters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwPrinters.Location = new System.Drawing.Point(6, 19);
            this.lvwPrinters.Name = "lvwPrinters";
            this.lvwPrinters.Size = new System.Drawing.Size(398, 375);
            this.lvwPrinters.TabIndex = 0;
            this.lvwPrinters.UseCompatibleStateImageBehavior = false;
            this.lvwPrinters.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeaderName
            // 
            this.ColumnHeaderName.Text = "Nome";
            this.ColumnHeaderName.Width = 240;
            // 
            // ColumnHeaderIpv4
            // 
            this.ColumnHeaderIpv4.Text = "Endereço Ip";
            this.ColumnHeaderIpv4.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Escolha as impressoras que farão parte do relatório:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(347, 431);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Gerar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ShowReportDialog);
            // 
            // sfdReport
            // 
            this.sfdReport.FileName = "relatorio";
            this.sfdReport.Filter = "Arquivo PDF|*.pdf";
            this.sfdReport.Title = "Relatório de impressão";
            // 
            // ReportController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 466);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportController";
            this.Text = "Relatórios";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView lvwPrinters;
        private System.Windows.Forms.ColumnHeader ColumnHeaderName;
        private System.Windows.Forms.ColumnHeader ColumnHeaderIpv4;
        private System.Windows.Forms.SaveFileDialog sfdReport;
    }
}