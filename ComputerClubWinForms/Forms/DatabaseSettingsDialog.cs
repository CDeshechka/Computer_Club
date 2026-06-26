using ComputerClubWinForms.Data;

namespace ComputerClubWinForms.Forms;

public partial class DatabaseSettingsDialog : Form
{
    private DatabaseHelper? _db;

    public DatabaseSettingsDialog()
    {
        InitializeComponent();
    }

    public DatabaseSettingsDialog(DatabaseHelper db) : this()
    {
        _db = db;
        txtConnection.Text = _db.ConnectionString;
    }

    private void OnTestClick(object? sender, EventArgs e)
    {
        if (_db == null)
        {
            lblResult.ForeColor = Color.DarkRed;
            lblResult.Text = "Проверка подключения доступна после запуска приложения.";
            return;
        }

        _db.SaveConnectionString(txtConnection.Text);
        if (_db.TestConnection(out var message))
        {
            lblResult.ForeColor = Color.DarkGreen;
            lblResult.Text = message;
        }
        else
        {
            lblResult.ForeColor = Color.DarkRed;
            lblResult.Text = message;
        }
    }

    private void OnSaveClick(object? sender, EventArgs e)
    {
        if (_db == null)
        {
            lblResult.ForeColor = Color.DarkRed;
            lblResult.Text = "Сохранение доступно после запуска приложения.";
            return;
        }

        _db.SaveConnectionString(txtConnection.Text);
        DialogResult = DialogResult.OK;
        Close();
    }
}
