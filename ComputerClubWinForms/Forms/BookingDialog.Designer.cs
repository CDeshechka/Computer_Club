namespace ComputerClubWinForms.Forms
{
    partial class BookingDialog
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Label lblDate = null!;
        private System.Windows.Forms.DateTimePicker dateStart = null!;
        private System.Windows.Forms.Label lblTime = null!;
        private System.Windows.Forms.DateTimePicker timeStart = null!;
        private System.Windows.Forms.Label lblDuration = null!;
        private System.Windows.Forms.NumericUpDown numDuration = null!;
        private System.Windows.Forms.Label lblComputer = null!;
        private System.Windows.Forms.NumericUpDown numComputer = null!;
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
            this.lblDate = new System.Windows.Forms.Label();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.lblTime = new System.Windows.Forms.Label();
            this.timeStart = new System.Windows.Forms.DateTimePicker();
            this.lblDuration = new System.Windows.Forms.Label();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.lblComputer = new System.Windows.Forms.Label();
            this.numComputer = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numComputer)).BeginInit();
            this.SuspendLayout();
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(24, 28);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(40, 19);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Дата";
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateStart.Location = new System.Drawing.Point(160, 24);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(210, 25);
            this.dateStart.TabIndex = 1;
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(24, 72);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(50, 19);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "Время";
            this.timeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeStart.Location = new System.Drawing.Point(160, 68);
            this.timeStart.Name = "timeStart";
            this.timeStart.ShowUpDown = true;
            this.timeStart.Size = new System.Drawing.Size(210, 25);
            this.timeStart.TabIndex = 3;
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(24, 116);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(55, 19);
            this.lblDuration.TabIndex = 4;
            this.lblDuration.Text = "Минут";
            this.numDuration.Location = new System.Drawing.Point(160, 112);
            this.numDuration.Maximum = new decimal(new int[] { 720, 0, 0, 0 });
            this.numDuration.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(210, 25);
            this.numDuration.TabIndex = 5;
            this.numDuration.Value = new decimal(new int[] { 60, 0, 0, 0 });
            this.lblComputer.AutoSize = true;
            this.lblComputer.Location = new System.Drawing.Point(24, 160);
            this.lblComputer.Name = "lblComputer";
            this.lblComputer.Size = new System.Drawing.Size(81, 19);
            this.lblComputer.TabIndex = 6;
            this.lblComputer.Text = "Компьютер";
            this.numComputer.Location = new System.Drawing.Point(160, 156);
            this.numComputer.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numComputer.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numComputer.Name = "numComputer";
            this.numComputer.Size = new System.Drawing.Size(210, 25);
            this.numComputer.TabIndex = 7;
            this.numComputer.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.btnOk.Location = new System.Drawing.Point(160, 202);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 34);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Создать";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnOkClick);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(270, 202);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 34);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.lblMessage.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMessage.Location = new System.Drawing.Point(24, 240);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(346, 24);
            this.lblMessage.TabIndex = 10;
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(420, 270);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dateStart);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.timeStart);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.numDuration);
            this.Controls.Add(this.lblComputer);
            this.Controls.Add(this.numComputer);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblMessage);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BookingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новая бронь";
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numComputer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
