namespace ComputerClubWinForms.Forms
{
    partial class ClientEditDialog
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Label lblFullName = null!;
        private System.Windows.Forms.TextBox txtFullName = null!;
        private System.Windows.Forms.Label lblPhone = null!;
        private System.Windows.Forms.TextBox txtPhone = null!;
        private System.Windows.Forms.Button btnOk = null!;
        private System.Windows.Forms.Button btnCancel = null!;
        private System.Windows.Forms.Label lblMessage = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(24, 28);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(41, 19);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "ФИО";
            this.txtFullName.Location = new System.Drawing.Point(120, 24);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(270, 25);
            this.txtFullName.TabIndex = 1;
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(24, 72);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(66, 19);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "Телефон";
            this.txtPhone.Location = new System.Drawing.Point(120, 68);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(270, 25);
            this.txtPhone.TabIndex = 3;
            this.btnOk.Location = new System.Drawing.Point(120, 116);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(120, 34);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Сохранить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnOkClick);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(270, 116);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 34);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.lblMessage.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMessage.Location = new System.Drawing.Point(24, 162);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(366, 32);
            this.lblMessage.TabIndex = 6;
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(430, 210);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblMessage);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientEditDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Клиент";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
