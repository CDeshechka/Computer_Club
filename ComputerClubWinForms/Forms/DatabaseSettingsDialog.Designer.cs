namespace ComputerClubWinForms.Forms
{
    partial class DatabaseSettingsDialog
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Label lblInfo = null!;
        private System.Windows.Forms.TextBox txtConnection = null!;
        private System.Windows.Forms.Button btnTest = null!;
        private System.Windows.Forms.Button btnSave = null!;
        private System.Windows.Forms.Label lblResult = null!;

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
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtConnection = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(20, 20);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(257, 19);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Строка подключения к PostgreSQL";
            this.txtConnection.Location = new System.Drawing.Point(20, 52);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.Size = new System.Drawing.Size(630, 25);
            this.txtConnection.TabIndex = 1;
            this.btnTest.Location = new System.Drawing.Point(20, 94);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(140, 34);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "Проверить";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.OnTestClick);
            this.btnSave.Location = new System.Drawing.Point(170, 94);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 34);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.OnSaveClick);
            this.lblResult.Location = new System.Drawing.Point(20, 145);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(630, 60);
            this.lblResult.TabIndex = 4;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 230);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtConnection);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblResult);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "DatabaseSettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Параметры PostgreSQL";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
