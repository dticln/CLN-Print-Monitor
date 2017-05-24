namespace CLNPrintMonitor.Controller
{
    partial class RenameController
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
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblAddressHeader = new System.Windows.Forms.Label();
            this.txbName = new System.Windows.Forms.TextBox();
            this.lblNameHeader = new System.Windows.Forms.Label();
            this.gpbRename = new System.Windows.Forms.GroupBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gpbRename.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(73, 21);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(52, 13);
            this.lblAddress.TabIndex = 8;
            this.lblAddress.Text = "127.0.0.1";
            // 
            // lblAddressHeader
            // 
            this.lblAddressHeader.AutoSize = true;
            this.lblAddressHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddressHeader.Location = new System.Drawing.Point(6, 21);
            this.lblAddressHeader.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblAddressHeader.Name = "lblAddressHeader";
            this.lblAddressHeader.Size = new System.Drawing.Size(61, 13);
            this.lblAddressHeader.TabIndex = 7;
            this.lblAddressHeader.Text = "Endereço";
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(51, 42);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(360, 20);
            this.txbName.TabIndex = 9;
            // 
            // lblNameHeader
            // 
            this.lblNameHeader.AutoSize = true;
            this.lblNameHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameHeader.Location = new System.Drawing.Point(6, 45);
            this.lblNameHeader.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblNameHeader.Name = "lblNameHeader";
            this.lblNameHeader.Size = new System.Drawing.Size(39, 13);
            this.lblNameHeader.TabIndex = 10;
            this.lblNameHeader.Text = "Nome";
            // 
            // gpbRename
            // 
            this.gpbRename.Controls.Add(this.txbName);
            this.gpbRename.Controls.Add(this.lblAddress);
            this.gpbRename.Controls.Add(this.lblNameHeader);
            this.gpbRename.Controls.Add(this.lblAddressHeader);
            this.gpbRename.Location = new System.Drawing.Point(12, 12);
            this.gpbRename.Name = "gpbRename";
            this.gpbRename.Size = new System.Drawing.Size(420, 73);
            this.gpbRename.TabIndex = 11;
            this.gpbRename.TabStop = false;
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(357, 91);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(75, 23);
            this.btnRename.TabIndex = 11;
            this.btnRename.Text = "Renomear";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.RenameAction);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(276, 91);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ExitAction);
            // 
            // RenameController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 125);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gpbRename);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenameController";
            this.Text = "Renomear";
            this.gpbRename.ResumeLayout(false);
            this.gpbRename.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblAddressHeader;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Label lblNameHeader;
        private System.Windows.Forms.GroupBox gpbRename;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnCancel;
    }
}