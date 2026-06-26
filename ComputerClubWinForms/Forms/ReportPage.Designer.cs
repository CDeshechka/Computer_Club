namespace ComputerClubWinForms.Forms
{
    partial class ReportPage
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Panel topPanel = null!;
        private System.Windows.Forms.Label lblStart = null!;
        private System.Windows.Forms.DateTimePicker dateStart = null!;
        private System.Windows.Forms.Label lblEnd = null!;
        private System.Windows.Forms.DateTimePicker dateEnd = null!;
        private System.Windows.Forms.Button btnGenerate = null!;
        private System.Windows.Forms.Button btnCompare = null!;
        private System.Windows.Forms.Button btnExport = null!;
        private System.Windows.Forms.DataGridView gridReport = null!;
        private System.Windows.Forms.Label lblSummary = null!;
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.lblStart = new System.Windows.Forms.Label();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.gridReport = new System.Windows.Forms.DataGridView();
            this.lblSummary = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).BeginInit();
            this.SuspendLayout();
            this.topPanel.Controls.Add(this.lblStart);
            this.topPanel.Controls.Add(this.dateStart);
            this.topPanel.Controls.Add(this.lblEnd);
            this.topPanel.Controls.Add(this.dateEnd);
            this.topPanel.Controls.Add(this.btnGenerate);
            this.topPanel.Controls.Add(this.btnCompare);
            this.topPanel.Controls.Add(this.btnExport);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(920, 64);
            this.topPanel.TabIndex = 0;
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(16, 22);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(56, 19);
            this.lblStart.TabIndex = 0;
            this.lblStart.Text = "Начало";
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateStart.Location = new System.Drawing.Point(80, 18);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(130, 25);
            this.dateStart.TabIndex = 1;
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(230, 22);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(48, 19);
            this.lblEnd.TabIndex = 2;
            this.lblEnd.Text = "Конец";
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnd.Location = new System.Drawing.Point(288, 18);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(130, 25);
            this.dateEnd.TabIndex = 3;
            this.btnGenerate.Location = new System.Drawing.Point(440, 16);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(130, 34);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Сформировать";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.OnGenerateClick);
            this.btnCompare.Location = new System.Drawing.Point(584, 16);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(110, 34);
            this.btnCompare.TabIndex = 5;
            this.btnCompare.Text = "Сравнить";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.OnCompareClick);
            this.btnExport.Location = new System.Drawing.Point(708, 16);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 34);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Экспорт PDF";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.OnExportClick);
            this.gridReport.AllowUserToAddRows = false;
            this.gridReport.AllowUserToDeleteRows = false;
            this.gridReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridReport.Location = new System.Drawing.Point(0, 64);
            this.gridReport.Name = "gridReport";
            this.gridReport.ReadOnly = true;
            this.gridReport.Size = new System.Drawing.Size(920, 408);
            this.gridReport.TabIndex = 1;
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummary.Location = new System.Drawing.Point(0, 472);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblSummary.Size = new System.Drawing.Size(920, 34);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Location = new System.Drawing.Point(0, 506);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMessage.Size = new System.Drawing.Size(920, 34);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridReport);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.topPanel);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "ReportPage";
            this.Size = new System.Drawing.Size(920, 540);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
