namespace ComputerClubWinForms.Forms
{
    partial class AuthPage
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Label lblTitle = null!;
        private System.Windows.Forms.Label lblLogin = null!;
        private System.Windows.Forms.TextBox txtUsername = null!;
        private System.Windows.Forms.Label lblPassword = null!;
        private System.Windows.Forms.TextBox txtPassword = null!;
        private System.Windows.Forms.Button btnLogin = null!;
        private System.Windows.Forms.Button btnDbSettings = null!;
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnDbSettings = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(95, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(229, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Компьютерный клуб";
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(60, 92);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(49, 19);
            this.lblLogin.TabIndex = 1;
            this.lblLogin.Text = "Логин";
            this.txtUsername.Location = new System.Drawing.Point(150, 88);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 25);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.Text = "admin";
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(60, 132);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 19);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Пароль";
            this.txtPassword.Location = new System.Drawing.Point(150, 128);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 25);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.Text = "admin123";
            this.txtPassword.UseSystemPasswordChar = true;
            this.btnLogin.Location = new System.Drawing.Point(150, 170);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(200, 34);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.OnLoginClick);
            this.btnDbSettings.Location = new System.Drawing.Point(150, 212);
            this.btnDbSettings.Name = "btnDbSettings";
            this.btnDbSettings.Size = new System.Drawing.Size(200, 32);
            this.btnDbSettings.TabIndex = 6;
            this.btnDbSettings.Text = "Настройки PostgreSQL";
            this.btnDbSettings.UseVisualStyleBackColor = true;
            this.btnDbSettings.Click += new System.EventHandler(this.OnSettingsClick);
            this.lblMessage.Location = new System.Drawing.Point(40, 252);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(340, 40);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 300);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnDbSettings);
            this.Controls.Add(this.lblMessage);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AuthPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход в систему";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
