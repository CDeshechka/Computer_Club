namespace ComputerClubWinForms.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.TabControl tabControl = null!;
        private System.Windows.Forms.TabPage tabClients = null!;
        private System.Windows.Forms.TabPage tabSessions = null!;
        private System.Windows.Forms.TabPage tabReports = null!;
        private System.Windows.Forms.TabPage tabSettings = null!;
        private System.Windows.Forms.Label lblDesignInfoClients = null!;
        private System.Windows.Forms.Label lblDesignInfoSessions = null!;
        private System.Windows.Forms.Label lblDesignInfoReports = null!;
        private System.Windows.Forms.Label lblDesignInfoSettings = null!;

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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabClients = new System.Windows.Forms.TabPage();
            this.tabSessions = new System.Windows.Forms.TabPage();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.lblDesignInfoClients = new System.Windows.Forms.Label();
            this.lblDesignInfoSessions = new System.Windows.Forms.Label();
            this.lblDesignInfoReports = new System.Windows.Forms.Label();
            this.lblDesignInfoSettings = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabClients.SuspendLayout();
            this.tabSessions.SuspendLayout();
            this.tabReports.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.SuspendLayout();
            this.tabControl.Controls.Add(this.tabClients);
            this.tabControl.Controls.Add(this.tabSessions);
            this.tabControl.Controls.Add(this.tabReports);
            this.tabControl.Controls.Add(this.tabSettings);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 650);
            this.tabControl.TabIndex = 0;
            this.tabClients.Controls.Add(this.lblDesignInfoClients);
            this.tabClients.Location = new System.Drawing.Point(4, 26);
            this.tabClients.Name = "tabClients";
            this.tabClients.Padding = new System.Windows.Forms.Padding(3);
            this.tabClients.Size = new System.Drawing.Size(992, 620);
            this.tabClients.TabIndex = 0;
            this.tabClients.Text = "Клиенты";
            this.tabClients.UseVisualStyleBackColor = true;
            this.lblDesignInfoClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDesignInfoClients.Location = new System.Drawing.Point(3, 3);
            this.lblDesignInfoClients.Name = "lblDesignInfoClients";
            this.lblDesignInfoClients.Size = new System.Drawing.Size(986, 614);
            this.lblDesignInfoClients.TabIndex = 0;
            this.lblDesignInfoClients.Text = "Вкладка клиентов создаётся после входа. Отдельный интерфейс редактируется в ClientPage.";
            this.lblDesignInfoClients.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tabSessions.Controls.Add(this.lblDesignInfoSessions);
            this.tabSessions.Location = new System.Drawing.Point(4, 26);
            this.tabSessions.Name = "tabSessions";
            this.tabSessions.Padding = new System.Windows.Forms.Padding(3);
            this.tabSessions.Size = new System.Drawing.Size(992, 620);
            this.tabSessions.TabIndex = 1;
            this.tabSessions.Text = "Сеансы";
            this.tabSessions.UseVisualStyleBackColor = true;
            this.lblDesignInfoSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDesignInfoSessions.Location = new System.Drawing.Point(3, 3);
            this.lblDesignInfoSessions.Name = "lblDesignInfoSessions";
            this.lblDesignInfoSessions.Size = new System.Drawing.Size(986, 614);
            this.lblDesignInfoSessions.TabIndex = 0;
            this.lblDesignInfoSessions.Text = "Вкладка сеансов создаётся после входа. Отдельный интерфейс редактируется в SessionPage.";
            this.lblDesignInfoSessions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tabReports.Controls.Add(this.lblDesignInfoReports);
            this.tabReports.Location = new System.Drawing.Point(4, 26);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabReports.Size = new System.Drawing.Size(992, 620);
            this.tabReports.TabIndex = 2;
            this.tabReports.Text = "Отчёты";
            this.tabReports.UseVisualStyleBackColor = true;
            this.lblDesignInfoReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDesignInfoReports.Location = new System.Drawing.Point(3, 3);
            this.lblDesignInfoReports.Name = "lblDesignInfoReports";
            this.lblDesignInfoReports.Size = new System.Drawing.Size(986, 614);
            this.lblDesignInfoReports.TabIndex = 0;
            this.lblDesignInfoReports.Text = "Вкладка отчётов создаётся после входа. Отдельный интерфейс редактируется в ReportPage.";
            this.lblDesignInfoReports.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tabSettings.Controls.Add(this.lblDesignInfoSettings);
            this.tabSettings.Location = new System.Drawing.Point(4, 26);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(992, 620);
            this.tabSettings.TabIndex = 3;
            this.tabSettings.Text = "Настройки";
            this.tabSettings.UseVisualStyleBackColor = true;
            this.lblDesignInfoSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDesignInfoSettings.Location = new System.Drawing.Point(3, 3);
            this.lblDesignInfoSettings.Name = "lblDesignInfoSettings";
            this.lblDesignInfoSettings.Size = new System.Drawing.Size(986, 614);
            this.lblDesignInfoSettings.TabIndex = 0;
            this.lblDesignInfoSettings.Text = "Вкладка настроек создаётся после входа. Отдельный интерфейс редактируется в SettingsPage.";
            this.lblDesignInfoSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Компьютерный клуб";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl.ResumeLayout(false);
            this.tabClients.ResumeLayout(false);
            this.tabSessions.ResumeLayout(false);
            this.tabReports.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
