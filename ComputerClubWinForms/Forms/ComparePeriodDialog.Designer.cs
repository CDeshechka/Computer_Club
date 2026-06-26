namespace ComputerClubWinForms.Forms
{
    partial class ComparePeriodDialog
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Label lblStart = null!;
        private System.Windows.Forms.DateTimePicker dateStart = null!;
        private System.Windows.Forms.Label lblEnd = null!;
        private System.Windows.Forms.DateTimePicker dateEnd = null!;
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
            this.lblStart = new System.Windows.Forms.Label();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(24, 30);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(56, 19);
            this.lblStart.TabIndex = 0;
            this.lblStart.Text = "Начало";
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateStart.Location = new System.Drawing.Point(130, 26);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(170, 25);
            this.dateStart.TabIndex = 1;
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(24, 76);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(48, 19);
            this.lblEnd.TabIndex = 2;
            this.lblEnd.Text = "Конец";
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnd.Location = new System.Drawing.Point(130, 72);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(170, 25);
            this.dateEnd.TabIndex = 3;
            this.btnOk.Location = new System.Drawing.Point(130, 116);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 32);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnOkClick);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(220, 116);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 32);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.lblMessage.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMessage.Location = new System.Drawing.Point(24, 154);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(276, 26);
            this.lblMessage.TabIndex = 6;
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(360, 190);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.dateStart);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblMessage);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComparePeriodDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Период сравнения";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
