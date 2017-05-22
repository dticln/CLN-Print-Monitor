namespace CLNPrintMonitor.Controller
{
    partial class PrinterController
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
            this.gpbInformation = new System.Windows.Forms.GroupBox();
            this.lblTonerCapacity = new System.Windows.Forms.Label();
            this.lblTonerCapacityHeader = new System.Windows.Forms.Label();
            this.lblSpeedHeader = new System.Windows.Forms.Label();
            this.lblTypeHeader = new System.Windows.Forms.Label();
            this.lblMaintenancePercent = new System.Windows.Forms.Label();
            this.lblFcPercent = new System.Windows.Forms.Label();
            this.lblInkPercent = new System.Windows.Forms.Label();
            this.pgbMaintenance = new System.Windows.Forms.ProgressBar();
            this.pgbFc = new System.Windows.Forms.ProgressBar();
            this.pgbInk = new System.Windows.Forms.ProgressBar();
            this.lblMaintenanceHeader = new System.Windows.Forms.Label();
            this.lblFcHeader = new System.Windows.Forms.Label();
            this.lblInkHeader = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblModelHeader = new System.Windows.Forms.Label();
            this.lblIPV4 = new System.Windows.Forms.Label();
            this.pcbStatus = new System.Windows.Forms.PictureBox();
            this.grbDefaultInput = new System.Windows.Forms.GroupBox();
            this.lblDefaultInputStatus = new System.Windows.Forms.Label();
            this.lblDefaultInputType = new System.Windows.Forms.Label();
            this.lblDefaultInputScale = new System.Windows.Forms.Label();
            this.lblDefaultInputCapacity = new System.Windows.Forms.Label();
            this.gpbOutput = new System.Windows.Forms.GroupBox();
            this.lblOuputStatus = new System.Windows.Forms.Label();
            this.lblOuputCapacity = new System.Windows.Forms.Label();
            this.grbSecondaryInput = new System.Windows.Forms.GroupBox();
            this.lblSupplyMfStatus = new System.Windows.Forms.Label();
            this.lblSupplyMfInputScale = new System.Windows.Forms.Label();
            this.lblSupplyMfInputType = new System.Windows.Forms.Label();
            this.lblSupplyMfInputCapacity = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gpbInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbStatus)).BeginInit();
            this.grbDefaultInput.SuspendLayout();
            this.gpbOutput.SuspendLayout();
            this.grbSecondaryInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbInformation
            // 
            this.gpbInformation.Controls.Add(this.lblTonerCapacity);
            this.gpbInformation.Controls.Add(this.lblTonerCapacityHeader);
            this.gpbInformation.Controls.Add(this.lblSpeedHeader);
            this.gpbInformation.Controls.Add(this.lblTypeHeader);
            this.gpbInformation.Controls.Add(this.lblMaintenancePercent);
            this.gpbInformation.Controls.Add(this.lblFcPercent);
            this.gpbInformation.Controls.Add(this.lblInkPercent);
            this.gpbInformation.Controls.Add(this.pgbMaintenance);
            this.gpbInformation.Controls.Add(this.pgbFc);
            this.gpbInformation.Controls.Add(this.pgbInk);
            this.gpbInformation.Controls.Add(this.lblMaintenanceHeader);
            this.gpbInformation.Controls.Add(this.lblFcHeader);
            this.gpbInformation.Controls.Add(this.lblInkHeader);
            this.gpbInformation.Controls.Add(this.lblSpeed);
            this.gpbInformation.Controls.Add(this.lblType);
            this.gpbInformation.Controls.Add(this.lblModel);
            this.gpbInformation.Controls.Add(this.lblModelHeader);
            this.gpbInformation.Controls.Add(this.lblIPV4);
            this.gpbInformation.Controls.Add(this.pcbStatus);
            this.gpbInformation.Location = new System.Drawing.Point(12, 12);
            this.gpbInformation.Name = "gpbInformation";
            this.gpbInformation.Size = new System.Drawing.Size(442, 280);
            this.gpbInformation.TabIndex = 0;
            this.gpbInformation.TabStop = false;
            this.gpbInformation.Text = "Informações";
            // 
            // lblTonerCapacity
            // 
            this.lblTonerCapacity.AutoSize = true;
            this.lblTonerCapacity.Location = new System.Drawing.Point(157, 166);
            this.lblTonerCapacity.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblTonerCapacity.Name = "lblTonerCapacity";
            this.lblTonerCapacity.Size = new System.Drawing.Size(135, 13);
            this.lblTonerCapacity.TabIndex = 21;
            this.lblTonerCapacity.Text = "CAPACIDADE DO TONER";
            // 
            // lblTonerCapacityHeader
            // 
            this.lblTonerCapacityHeader.AutoSize = true;
            this.lblTonerCapacityHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTonerCapacityHeader.Location = new System.Drawing.Point(150, 148);
            this.lblTonerCapacityHeader.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblTonerCapacityHeader.Name = "lblTonerCapacityHeader";
            this.lblTonerCapacityHeader.Size = new System.Drawing.Size(125, 13);
            this.lblTonerCapacityHeader.TabIndex = 20;
            this.lblTonerCapacityHeader.Text = "Capacidade do toner";
            // 
            // lblSpeedHeader
            // 
            this.lblSpeedHeader.AutoSize = true;
            this.lblSpeedHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeedHeader.Location = new System.Drawing.Point(150, 112);
            this.lblSpeedHeader.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblSpeedHeader.Name = "lblSpeedHeader";
            this.lblSpeedHeader.Size = new System.Drawing.Size(148, 13);
            this.lblSpeedHeader.TabIndex = 19;
            this.lblSpeedHeader.Text = "Velocidade de impressão";
            // 
            // lblTypeHeader
            // 
            this.lblTypeHeader.AutoSize = true;
            this.lblTypeHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeHeader.Location = new System.Drawing.Point(150, 76);
            this.lblTypeHeader.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblTypeHeader.Name = "lblTypeHeader";
            this.lblTypeHeader.Size = new System.Drawing.Size(126, 13);
            this.lblTypeHeader.TabIndex = 18;
            this.lblTypeHeader.Text = "Tipo de equipamento";
            // 
            // lblMaintenancePercent
            // 
            this.lblMaintenancePercent.AutoSize = true;
            this.lblMaintenancePercent.Location = new System.Drawing.Point(403, 251);
            this.lblMaintenancePercent.Name = "lblMaintenancePercent";
            this.lblMaintenancePercent.Size = new System.Drawing.Size(33, 13);
            this.lblMaintenancePercent.TabIndex = 17;
            this.lblMaintenancePercent.Text = "100%";
            this.lblMaintenancePercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFcPercent
            // 
            this.lblFcPercent.AutoSize = true;
            this.lblFcPercent.Location = new System.Drawing.Point(403, 222);
            this.lblFcPercent.Name = "lblFcPercent";
            this.lblFcPercent.Size = new System.Drawing.Size(33, 13);
            this.lblFcPercent.TabIndex = 16;
            this.lblFcPercent.Text = "100%";
            this.lblFcPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInkPercent
            // 
            this.lblInkPercent.AutoSize = true;
            this.lblInkPercent.Location = new System.Drawing.Point(403, 193);
            this.lblInkPercent.Name = "lblInkPercent";
            this.lblInkPercent.Size = new System.Drawing.Size(33, 13);
            this.lblInkPercent.TabIndex = 15;
            this.lblInkPercent.Text = "100%";
            this.lblInkPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgbMaintenance
            // 
            this.pgbMaintenance.Location = new System.Drawing.Point(82, 247);
            this.pgbMaintenance.MarqueeAnimationSpeed = 0;
            this.pgbMaintenance.Name = "pgbMaintenance";
            this.pgbMaintenance.Size = new System.Drawing.Size(315, 23);
            this.pgbMaintenance.TabIndex = 12;
            // 
            // pgbFc
            // 
            this.pgbFc.Location = new System.Drawing.Point(82, 218);
            this.pgbFc.MarqueeAnimationSpeed = 0;
            this.pgbFc.Name = "pgbFc";
            this.pgbFc.Size = new System.Drawing.Size(315, 23);
            this.pgbFc.TabIndex = 12;
            // 
            // pgbInk
            // 
            this.pgbInk.Location = new System.Drawing.Point(82, 189);
            this.pgbInk.MarqueeAnimationSpeed = 0;
            this.pgbInk.Name = "pgbInk";
            this.pgbInk.Size = new System.Drawing.Size(315, 23);
            this.pgbInk.TabIndex = 12;
            // 
            // lblMaintenanceHeader
            // 
            this.lblMaintenanceHeader.AutoSize = true;
            this.lblMaintenanceHeader.Location = new System.Drawing.Point(9, 251);
            this.lblMaintenanceHeader.Name = "lblMaintenanceHeader";
            this.lblMaintenanceHeader.Size = new System.Drawing.Size(67, 13);
            this.lblMaintenanceHeader.TabIndex = 11;
            this.lblMaintenanceHeader.Text = "Manutenção";
            this.lblMaintenanceHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFcHeader
            // 
            this.lblFcHeader.AutoSize = true;
            this.lblFcHeader.Location = new System.Drawing.Point(56, 222);
            this.lblFcHeader.Name = "lblFcHeader";
            this.lblFcHeader.Size = new System.Drawing.Size(20, 13);
            this.lblFcHeader.TabIndex = 10;
            this.lblFcHeader.Text = "FC";
            this.lblFcHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInkHeader
            // 
            this.lblInkHeader.AutoSize = true;
            this.lblInkHeader.Location = new System.Drawing.Point(41, 193);
            this.lblInkHeader.Name = "lblInkHeader";
            this.lblInkHeader.Size = new System.Drawing.Size(35, 13);
            this.lblInkHeader.TabIndex = 9;
            this.lblInkHeader.Text = "Toner";
            this.lblInkHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(157, 130);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(159, 13);
            this.lblSpeed.TabIndex = 8;
            this.lblSpeed.Text = "VELOCIDADE DE IMPRESSAO";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(157, 94);
            this.lblType.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(132, 13);
            this.lblType.TabIndex = 7;
            this.lblType.Text = "TIPO DE EQUIPAMENTO";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(157, 58);
            this.lblModel.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(53, 13);
            this.lblModel.TabIndex = 6;
            this.lblModel.Text = "MODELO";
            // 
            // lblModelHeader
            // 
            this.lblModelHeader.AutoSize = true;
            this.lblModelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelHeader.Location = new System.Drawing.Point(150, 40);
            this.lblModelHeader.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblModelHeader.Name = "lblModelHeader";
            this.lblModelHeader.Size = new System.Drawing.Size(48, 13);
            this.lblModelHeader.TabIndex = 5;
            this.lblModelHeader.Text = "Modelo";
            // 
            // lblIPV4
            // 
            this.lblIPV4.AutoSize = true;
            this.lblIPV4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIPV4.Location = new System.Drawing.Point(150, 19);
            this.lblIPV4.Name = "lblIPV4";
            this.lblIPV4.Size = new System.Drawing.Size(116, 16);
            this.lblIPV4.TabIndex = 4;
            this.lblIPV4.Text = "255.255.255.255";
            // 
            // pcbStatus
            // 
            this.pcbStatus.Image = global::CLNPrintMonitor.Properties.Resources.offline;
            this.pcbStatus.Location = new System.Drawing.Point(6, 19);
            this.pcbStatus.Name = "pcbStatus";
            this.pcbStatus.Size = new System.Drawing.Size(138, 160);
            this.pcbStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbStatus.TabIndex = 0;
            this.pcbStatus.TabStop = false;
            // 
            // grbDefaultInput
            // 
            this.grbDefaultInput.Controls.Add(this.lblDefaultInputStatus);
            this.grbDefaultInput.Controls.Add(this.lblDefaultInputType);
            this.grbDefaultInput.Controls.Add(this.lblDefaultInputScale);
            this.grbDefaultInput.Controls.Add(this.lblDefaultInputCapacity);
            this.grbDefaultInput.Location = new System.Drawing.Point(12, 298);
            this.grbDefaultInput.Name = "grbDefaultInput";
            this.grbDefaultInput.Size = new System.Drawing.Size(144, 111);
            this.grbDefaultInput.TabIndex = 1;
            this.grbDefaultInput.TabStop = false;
            this.grbDefaultInput.Text = "Bandeja padrão";
            // 
            // lblDefaultInputStatus
            // 
            this.lblDefaultInputStatus.AutoSize = true;
            this.lblDefaultInputStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultInputStatus.Location = new System.Drawing.Point(6, 21);
            this.lblDefaultInputStatus.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblDefaultInputStatus.Name = "lblDefaultInputStatus";
            this.lblDefaultInputStatus.Size = new System.Drawing.Size(69, 13);
            this.lblDefaultInputStatus.TabIndex = 22;
            this.lblDefaultInputStatus.Text = "SITUACAO";
            this.lblDefaultInputStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDefaultInputType
            // 
            this.lblDefaultInputType.AutoSize = true;
            this.lblDefaultInputType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultInputType.Location = new System.Drawing.Point(6, 88);
            this.lblDefaultInputType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblDefaultInputType.Name = "lblDefaultInputType";
            this.lblDefaultInputType.Size = new System.Drawing.Size(71, 13);
            this.lblDefaultInputType.TabIndex = 25;
            this.lblDefaultInputType.Text = "Papel comum";
            // 
            // lblDefaultInputScale
            // 
            this.lblDefaultInputScale.AutoSize = true;
            this.lblDefaultInputScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultInputScale.Location = new System.Drawing.Point(6, 44);
            this.lblDefaultInputScale.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblDefaultInputScale.Name = "lblDefaultInputScale";
            this.lblDefaultInputScale.Size = new System.Drawing.Size(68, 13);
            this.lblDefaultInputScale.TabIndex = 24;
            this.lblDefaultInputScale.Text = "Tamanho A4";
            // 
            // lblDefaultInputCapacity
            // 
            this.lblDefaultInputCapacity.AutoSize = true;
            this.lblDefaultInputCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultInputCapacity.Location = new System.Drawing.Point(6, 67);
            this.lblDefaultInputCapacity.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblDefaultInputCapacity.Name = "lblDefaultInputCapacity";
            this.lblDefaultInputCapacity.Size = new System.Drawing.Size(131, 13);
            this.lblDefaultInputCapacity.TabIndex = 22;
            this.lblDefaultInputCapacity.Text = "Capacidade de 250 folhas";
            // 
            // gpbOutput
            // 
            this.gpbOutput.Controls.Add(this.lblOuputStatus);
            this.gpbOutput.Controls.Add(this.lblOuputCapacity);
            this.gpbOutput.Location = new System.Drawing.Point(312, 298);
            this.gpbOutput.Name = "gpbOutput";
            this.gpbOutput.Size = new System.Drawing.Size(142, 67);
            this.gpbOutput.TabIndex = 3;
            this.gpbOutput.TabStop = false;
            this.gpbOutput.Text = "Saída de papel";
            // 
            // lblOuputStatus
            // 
            this.lblOuputStatus.AutoSize = true;
            this.lblOuputStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOuputStatus.Location = new System.Drawing.Point(6, 21);
            this.lblOuputStatus.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblOuputStatus.Name = "lblOuputStatus";
            this.lblOuputStatus.Size = new System.Drawing.Size(69, 13);
            this.lblOuputStatus.TabIndex = 22;
            this.lblOuputStatus.Text = "SITUACAO";
            this.lblOuputStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOuputCapacity
            // 
            this.lblOuputCapacity.AutoSize = true;
            this.lblOuputCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOuputCapacity.Location = new System.Drawing.Point(6, 44);
            this.lblOuputCapacity.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblOuputCapacity.Name = "lblOuputCapacity";
            this.lblOuputCapacity.Size = new System.Drawing.Size(131, 13);
            this.lblOuputCapacity.TabIndex = 22;
            this.lblOuputCapacity.Text = "Capacidade de 250 folhas";
            // 
            // grbSecondaryInput
            // 
            this.grbSecondaryInput.Controls.Add(this.lblSupplyMfStatus);
            this.grbSecondaryInput.Controls.Add(this.lblSupplyMfInputScale);
            this.grbSecondaryInput.Controls.Add(this.lblSupplyMfInputType);
            this.grbSecondaryInput.Controls.Add(this.lblSupplyMfInputCapacity);
            this.grbSecondaryInput.Location = new System.Drawing.Point(162, 298);
            this.grbSecondaryInput.Name = "grbSecondaryInput";
            this.grbSecondaryInput.Size = new System.Drawing.Size(144, 111);
            this.grbSecondaryInput.TabIndex = 2;
            this.grbSecondaryInput.TabStop = false;
            this.grbSecondaryInput.Text = "Bandeja MP";
            // 
            // lblSupplyMfStatus
            // 
            this.lblSupplyMfStatus.AutoSize = true;
            this.lblSupplyMfStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplyMfStatus.Location = new System.Drawing.Point(6, 21);
            this.lblSupplyMfStatus.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblSupplyMfStatus.Name = "lblSupplyMfStatus";
            this.lblSupplyMfStatus.Size = new System.Drawing.Size(69, 13);
            this.lblSupplyMfStatus.TabIndex = 22;
            this.lblSupplyMfStatus.Text = "SITUACAO";
            this.lblSupplyMfStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSupplyMfInputScale
            // 
            this.lblSupplyMfInputScale.AutoSize = true;
            this.lblSupplyMfInputScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplyMfInputScale.Location = new System.Drawing.Point(6, 45);
            this.lblSupplyMfInputScale.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblSupplyMfInputScale.Name = "lblSupplyMfInputScale";
            this.lblSupplyMfInputScale.Size = new System.Drawing.Size(68, 13);
            this.lblSupplyMfInputScale.TabIndex = 24;
            this.lblSupplyMfInputScale.Text = "Tamanho A4";
            // 
            // lblSupplyMfInputType
            // 
            this.lblSupplyMfInputType.AutoSize = true;
            this.lblSupplyMfInputType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplyMfInputType.Location = new System.Drawing.Point(6, 88);
            this.lblSupplyMfInputType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblSupplyMfInputType.Name = "lblSupplyMfInputType";
            this.lblSupplyMfInputType.Size = new System.Drawing.Size(71, 13);
            this.lblSupplyMfInputType.TabIndex = 25;
            this.lblSupplyMfInputType.Text = "Papel comum";
            // 
            // lblSupplyMfInputCapacity
            // 
            this.lblSupplyMfInputCapacity.AutoSize = true;
            this.lblSupplyMfInputCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplyMfInputCapacity.Location = new System.Drawing.Point(6, 67);
            this.lblSupplyMfInputCapacity.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblSupplyMfInputCapacity.Name = "lblSupplyMfInputCapacity";
            this.lblSupplyMfInputCapacity.Size = new System.Drawing.Size(131, 13);
            this.lblSupplyMfInputCapacity.TabIndex = 22;
            this.lblSupplyMfInputCapacity.Text = "Capacidade de 250 folhas";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 371);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 37);
            this.button1.TabIndex = 4;
            this.button1.Text = "Gerar relatório";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // PrinterController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 420);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gpbOutput);
            this.Controls.Add(this.grbSecondaryInput);
            this.Controls.Add(this.grbDefaultInput);
            this.Controls.Add(this.gpbInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PrinterController";
            this.Text = "PrinterController";
            this.TopMost = true;
            this.gpbInformation.ResumeLayout(false);
            this.gpbInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbStatus)).EndInit();
            this.grbDefaultInput.ResumeLayout(false);
            this.grbDefaultInput.PerformLayout();
            this.gpbOutput.ResumeLayout(false);
            this.gpbOutput.PerformLayout();
            this.grbSecondaryInput.ResumeLayout(false);
            this.grbSecondaryInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbInformation;
        private System.Windows.Forms.PictureBox pcbStatus;
        private System.Windows.Forms.GroupBox grbDefaultInput;
        private System.Windows.Forms.Label lblIPV4;
        private System.Windows.Forms.Label lblModelHeader;
        private System.Windows.Forms.Label lblMaintenancePercent;
        private System.Windows.Forms.Label lblFcPercent;
        private System.Windows.Forms.Label lblInkPercent;
        private System.Windows.Forms.ProgressBar pgbInk;
        private System.Windows.Forms.Label lblMaintenanceHeader;
        private System.Windows.Forms.Label lblFcHeader;
        private System.Windows.Forms.Label lblInkHeader;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblTonerCapacityHeader;
        private System.Windows.Forms.Label lblSpeedHeader;
        private System.Windows.Forms.Label lblTypeHeader;
        private System.Windows.Forms.Label lblTonerCapacity;
        private System.Windows.Forms.Label lblDefaultInputStatus;
        private System.Windows.Forms.Label lblDefaultInputType;
        private System.Windows.Forms.Label lblDefaultInputScale;
        private System.Windows.Forms.Label lblDefaultInputCapacity;
        private System.Windows.Forms.GroupBox gpbOutput;
        private System.Windows.Forms.GroupBox grbSecondaryInput;
        private System.Windows.Forms.Label lblSupplyMfStatus;
        private System.Windows.Forms.Label lblSupplyMfInputScale;
        private System.Windows.Forms.Label lblSupplyMfInputType;
        private System.Windows.Forms.Label lblSupplyMfInputCapacity;
        private System.Windows.Forms.Label lblOuputStatus;
        private System.Windows.Forms.Label lblOuputCapacity;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar pgbMaintenance;
        private System.Windows.Forms.ProgressBar pgbFc;
    }
}