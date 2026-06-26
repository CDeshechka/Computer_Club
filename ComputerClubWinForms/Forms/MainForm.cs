using ComputerClubWinForms.Data;
using ComputerClubWinForms.Managers;
using ComputerClubWinForms.Models;

namespace ComputerClubWinForms.Forms;

public partial class MainForm : Form
{
    private DatabaseHelper? _db;
    public MainForm()
    {
        InitializeComponent();
    }

    public MainForm(DatabaseHelper db, User currentUser) : this()
    {
        _db = db;
        InitializeAfterLogin(currentUser);
    }

    public void InitializeAfterLogin(User user)
    {
        if (_db == null)
        {
            return;
        }

        Text = "Компьютерный клуб - " + user.Username + " (" + user.Role + ")";
        var clientManager = new ClientManager(_db);
        var tariffManager = new TariffManager(_db, clientManager);
        var sessionManager = new SessionManager(_db, clientManager, tariffManager);
        var analyticsManager = new AnalyticsManager(sessionManager);
        tabControl.TabPages.Clear();
        tabControl.TabPages.Add(CreatePage("Клиенты", new ClientPage(clientManager)));
        tabControl.TabPages.Add(CreatePage("Сеансы", new SessionPage(clientManager, sessionManager)));
        if (user.IsAdmin)
        {
            tabControl.TabPages.Add(CreatePage("Отчёты", new ReportPage(analyticsManager)));
            tabControl.TabPages.Add(CreatePage("Настройки", new SettingsPage(tariffManager, _db)));
        }
    }

    private static TabPage CreatePage(string title, Control control)
    {
        control.Dock = DockStyle.Fill;
        var page = new TabPage(title);
        page.Controls.Add(control);
        return page;
    }
}
