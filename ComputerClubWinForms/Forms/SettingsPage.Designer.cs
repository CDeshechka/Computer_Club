namespace ComputerClubWinForms.Forms
{
    partial class SettingsPage
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.GroupBox groupTariff = null!;
        private System.Windows.Forms.Label lblCurrentTariff = null!;
        private System.Windows.Forms.TextBox txtTariff = null!;
        private System.Windows.Forms.Button btnSaveTariff = null!;
        private System.Windows.Forms.GroupBox groupDb = null!;
        private System.Windows.Forms.TextBox txtConnection = null!;
        private System.Windows.Forms.Button btnTest = null!;
        private System.Windows.Forms.Button btnSaveConnection = null!;
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
            this.groupTariff = new System.Windows.Forms.GroupBox();
            this.lblCurrentTariff = new System.Windows.Forms.Label();
            this.txtTariff = new System.Windows.Forms.TextBox();
            this.btnSaveTariff = new System.Windows.Forms.Button();
            this.groupDb = new System.Windows.Forms.GroupBox();
            this.txtConnection = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSaveConnection = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.groupTariff.SuspendLayout();
            this.groupDb.SuspendLayout();
            this.SuspendLayout();
            this.groupTariff.Controls.Add(this.lblCurrentTariff);
            this.groupTariff.Controls.Add(this.txtTariff);
            this.groupTariff.Controls.Add(this.btnSaveTariff);
            this.groupTariff.Location = new System.Drawing.Point(20, 20);
            this.groupTariff.Name = "groupTariff";
            this.groupTariff.Size = new System.Drawing.Size(520, 150);
            this.groupTariff.TabIndex = 0;
            this.groupTariff.TabStop = false;
            this.groupTariff.Text = "Тариф";
            this.lblCurrentTariff.Location = new System.Drawing.Point(20, 32);
            this.lblCurrentTariff.Name = "lblCurrentTariff";
            this.lblCurrentTariff.Size = new System.Drawing.Size(460, 28);
            this.lblCurrentTariff.TabIndex = 0;
            this.lblCurrentTariff.Text = "Текущий тариф: 150,00 руб./час";
            this.txtTariff.Location = new System.Drawing.Point(20, 74);
            this.txtTariff.Name = "txtTariff";
            this.txtTariff.Size = new System.Drawing.Size(150, 25);
            this.txtTariff.TabIndex = 1;
            this.btnSaveTariff.Location = new System.Drawing.Point(190, 72);
            this.btnSaveTariff.Name = "btnSaveTariff";
            this.btnSaveTariff.Size = new System.Drawing.Size(150, 32);
            this.btnSaveTariff.TabIndex = 2;
            this.btnSaveTariff.Text = "Сохранить тариф";
            this.btnSaveTariff.UseVisualStyleBackColor = true;
            this.btnSaveTariff.Click += new System.EventHandler(this.OnSaveTariffClick);
            this.groupDb.Controls.Add(this.txtConnection);
            this.groupDb.Controls.Add(this.btnTest);
            this.groupDb.Controls.Add(this.btnSaveConnection);
            this.groupDb.Location = new System.Drawing.Point(20, 190);
            this.groupDb.Name = "groupDb";
            this.groupDb.Size = new System.Drawing.Size(820, 170);
            this.groupDb.TabIndex = 1;
            this.groupDb.TabStop = false;
            this.groupDb.Text = "PostgreSQL";
            this.txtConnection.Location = new System.Drawing.Point(20, 38);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.Size = new System.Drawing.Size(760, 25);
            this.txtConnection.TabIndex = 0;
            this.btnTest.Location = new System.Drawing.Point(20, 82);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(130, 32);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Проверить";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.OnTestConnectionClick);
            this.btnSaveConnection.Location = new System.Drawing.Point(164, 82);
            this.btnSaveConnection.Name = "btnSaveConnection";
            this.btnSaveConnection.Size = new System.Drawing.Size(190, 32);
            this.btnSaveConnection.TabIndex = 2;
            this.btnSaveConnection.Text = "Сохранить подключение";
            this.btnSaveConnection.UseVisualStyleBackColor = true;
            this.btnSaveConnection.Click += new System.EventHandler(this.OnSaveConnectionClick);
            this.lblMessage.Location = new System.Drawing.Point(20, 382);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(820, 80);
            this.lblMessage.TabIndex = 2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupTariff);
            this.Controls.Add(this.groupDb);
            this.Controls.Add(this.lblMessage);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "SettingsPage";
            this.Size = new System.Drawing.Size(900, 520);
            this.groupTariff.ResumeLayout(false);
            this.groupTariff.PerformLayout();
            this.groupDb.ResumeLayout(false);
            this.groupDb.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
