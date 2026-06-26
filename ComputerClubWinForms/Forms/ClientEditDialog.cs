namespace ComputerClubWinForms.Forms;

public partial class ClientEditDialog : Form
{
    public string ClientFullName => txtFullName.Text.Trim();
    public string ClientPhone => txtPhone.Text.Trim();

    public ClientEditDialog(string fullName = "", string phone = "")
    {
        InitializeComponent();
        txtFullName.Text = fullName;
        txtPhone.Text = phone;
    }

    private void OnOkClick(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
        {
            lblMessage.Text = "Заполните ФИО и телефон.";
            return;
        }
        DialogResult = DialogResult.OK;
        Close();
    }
}
