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
            this.grbDefaultInput = new System.Windows.Forms.GroupBox();
            this.grbSecondaryInput = new System.Windows.Forms.GroupBox();
            this.gpbOutput = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.gpbInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbInformation
            // 
            this.gpbInformation.Controls.Add(this.lblName);
            this.gpbInformation.Controls.Add(this.pictureBox1);
            this.gpbInformation.Location = new System.Drawing.Point(12, 12);
            this.gpbInformation.Name = "gpbInformation";
            this.gpbInformation.Size = new System.Drawing.Size(460, 213);
            this.gpbInformation.TabIndex = 0;
            this.gpbInformation.TabStop = false;
            this.gpbInformation.Text = "Informações";
            // 
            // grbDefaultInput
            // 
            this.grbDefaultInput.Location = new System.Drawing.Point(12, 231);
            this.grbDefaultInput.Name = "grbDefaultInput";
            this.grbDefaultInput.Size = new System.Drawing.Size(460, 104);
            this.grbDefaultInput.TabIndex = 1;
            this.grbDefaultInput.TabStop = false;
            this.grbDefaultInput.Text = "Bandeja padrão";
            // 
            // grbSecondaryInput
            // 
            this.grbSecondaryInput.Location = new System.Drawing.Point(12, 341);
            this.grbSecondaryInput.Name = "grbSecondaryInput";
            this.grbSecondaryInput.Size = new System.Drawing.Size(460, 104);
            this.grbSecondaryInput.TabIndex = 2;
            this.grbSecondaryInput.TabStop = false;
            this.grbSecondaryInput.Text = "Bandeja MP";
            // 
            // gpbOutput
            // 
            this.gpbOutput.Location = new System.Drawing.Point(12, 451);
            this.gpbOutput.Name = "gpbOutput";
            this.gpbOutput.Size = new System.Drawing.Size(460, 104);
            this.gpbOutput.TabIndex = 3;
            this.gpbOutput.TabStop = false;
            this.gpbOutput.Text = "Saída de papel";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CLNPrintMonitor.Properties.Resources.offline;
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 188);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(170, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "label1";
            // 
            // PrinterController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.gpbOutput);
            this.Controls.Add(this.grbSecondaryInput);
            this.Controls.Add(this.grbDefaultInput);
            this.Controls.Add(this.gpbInformation);
            this.Name = "PrinterController";
            this.Text = "PrinterController";
            this.gpbInformation.ResumeLayout(false);
            this.gpbInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbInformation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grbDefaultInput;
        private System.Windows.Forms.GroupBox grbSecondaryInput;
        private System.Windows.Forms.GroupBox gpbOutput;
        private System.Windows.Forms.Label lblName;
    }
}