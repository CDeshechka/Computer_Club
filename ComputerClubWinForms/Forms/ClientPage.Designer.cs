namespace ComputerClubWinForms.Forms
{
    partial class ClientPage
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Panel topPanel = null!;
        private System.Windows.Forms.Label lblSearch = null!;
        private System.Windows.Forms.TextBox txtSearch = null!;
        private System.Windows.Forms.Button btnAdd = null!;
        private System.Windows.Forms.Button btnEdit = null!;
        private System.Windows.Forms.Button btnDelete = null!;
        private System.Windows.Forms.DataGridView gridClients = null!;
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
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gridClients = new System.Windows.Forms.DataGridView();
            this.lblMessage = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClients)).BeginInit();
            this.SuspendLayout();
            this.topPanel.Controls.Add(this.lblSearch);
            this.topPanel.Controls.Add(this.txtSearch);
            this.topPanel.Controls.Add(this.btnAdd);
            this.topPanel.Controls.Add(this.btnEdit);
            this.topPanel.Controls.Add(this.btnDelete);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(900, 58);
            this.topPanel.TabIndex = 0;
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(16, 19);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(49, 19);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Поиск";
            this.txtSearch.Location = new System.Drawing.Point(75, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(260, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.OnSearchTextChanged);
            this.btnAdd.Location = new System.Drawing.Point(360, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 32);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.OnAddClick);
            this.btnEdit.Location = new System.Drawing.Point(480, 13);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 32);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Изменить";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.OnEditClick);
            this.btnDelete.Location = new System.Drawing.Point(600, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 32);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.OnDeleteClick);
            this.gridClients.AllowUserToAddRows = false;
            this.gridClients.AllowUserToDeleteRows = false;
            this.gridClients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClients.Location = new System.Drawing.Point(0, 58);
            this.gridClients.MultiSelect = false;
            this.gridClients.Name = "gridClients";
            this.gridClients.ReadOnly = true;
            this.gridClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridClients.Size = new System.Drawing.Size(900, 427);
            this.gridClients.TabIndex = 1;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Location = new System.Drawing.Point(0, 485);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lblMessage.Size = new System.Drawing.Size(900, 35);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridClients);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.topPanel);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "ClientPage";
            this.Size = new System.Drawing.Size(900, 520);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClients)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
