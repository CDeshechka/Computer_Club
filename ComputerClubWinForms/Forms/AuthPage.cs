using ComputerClubWinForms.Data;
using ComputerClubWinForms.Managers;

namespace ComputerClubWinForms.Forms;

public partial class AuthPage : Form
{
    private DatabaseHelper? _db;
    private UserManager? _userManager;

    public AuthPage()
    {
        InitializeComponent();
    }

    public AuthPage(DatabaseHelper db) : this()
    {
        _db = db;
        _userManager = new UserManager(db);
    }

    private void OnLoginClick(object? sender, EventArgs e)
    {
        if (_db == null || _userManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        var user = _userManager.Login(txtUsername.Text, txtPassword.Text);
        if (user == null)
        {
            ShowMessage(_userManager.LastMessage, false);
            return;
        }
        Hide();
        using var mainForm = new MainForm(_db, user);
        mainForm.ShowDialog();
        Show();
        txtPassword.Clear();
        txtPassword.Focus();
    }

    private void OnSettingsClick(object? sender, EventArgs e)
    {
        if (_db == null)
        {
            ShowMessage("Настройки доступны после запуска приложения.", false);
            return;
        }

        using var dialog = new DatabaseSettingsDialog(_db);
        dialog.ShowDialog(this);
    }

    private void ShowMessage(string message, bool success)
    {
        lblMessage.ForeColor = success ? Color.DarkGreen : Color.DarkRed;
        lblMessage.Text = message;
    }
}
