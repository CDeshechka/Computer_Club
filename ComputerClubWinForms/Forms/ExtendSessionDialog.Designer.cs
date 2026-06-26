namespace ComputerClubWinForms.Forms
{
    partial class ExtendSessionDialog
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Label lblMinutes = null!;
        private System.Windows.Forms.NumericUpDown numMinutes = null!;
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
            this.lblMinutes = new System.Windows.Forms.Label();
            this.numMinutes = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).BeginInit();
            this.SuspendLayout();
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(24, 32);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(61, 19);
            this.lblMinutes.TabIndex = 0;
            this.lblMinutes.Text = "Минуты";
            this.numMinutes.Location = new System.Drawing.Point(120, 28);
            this.numMinutes.Maximum = new decimal(new int[] { 720, 0, 0, 0 });
            this.numMinutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMinutes.Name = "numMinutes";
            this.numMinutes.Size = new System.Drawing.Size(150, 25);
            this.numMinutes.TabIndex = 1;
            this.numMinutes.Value = new decimal(new int[] { 30, 0, 0, 0 });
            this.btnOk.Location = new System.Drawing.Point(120, 76);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 32);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnOkClick);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(200, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.lblMessage.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMessage.Location = new System.Drawing.Point(24, 120);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(246, 28);
            this.lblMessage.TabIndex = 4;
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(320, 170);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.numMinutes);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblMessage);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendSessionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Продление сеанса";
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
