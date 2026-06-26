using ComputerClubWinForms.Data;
using ComputerClubWinForms.Managers;

namespace ComputerClubWinForms.Forms;

public partial class SettingsPage : UserControl
{
    private TariffManager? _tariffManager;
    private DatabaseHelper? _db;

    public SettingsPage()
    {
        InitializeComponent();
    }

    public SettingsPage(TariffManager tariffManager, DatabaseHelper db) : this()
    {
        _tariffManager = tariffManager;
        _db = db;
        ShowCurrentTariff(_tariffManager.GetCurrentTariff());
        txtConnection.Text = _db.ConnectionString;
    }

    public void ShowCurrentTariff(decimal tariff)
    {
        lblCurrentTariff.Text = $"Текущий тариф: {tariff:0.00} руб./час";
        txtTariff.Text = tariff.ToString("0.##");
    }

    private void OnSaveTariffClick(object? sender, EventArgs e)
    {
        if (_tariffManager == null)
        {
            ShowMessage(false, "Форма открыта в режиме конструктора.");
            return;
        }

        if (!decimal.TryParse(txtTariff.Text, out var tariff))
        {
            ShowMessage(false, "Введите число.");
            return;
        }
        var success = _tariffManager.UpdateTariff(tariff);
        if (success)
        {
            ShowCurrentTariff(_tariffManager.GetCurrentTariff());
        }
        ShowMessage(success, _tariffManager.LastMessage);
    }

    private void OnSaveConnectionClick(object? sender, EventArgs e)
    {
        if (_db == null)
        {
            ShowMessage(false, "Сохранение доступно после запуска приложения.");
            return;
        }

        _db.SaveConnectionString(txtConnection.Text);
        ShowMessage(true, "Строка подключения сохранена. Перезапустите программу для применения настроек.");
    }

    private void OnTestConnectionClick(object? sender, EventArgs e)
    {
        if (_db == null)
        {
            ShowMessage(false, "Проверка подключения доступна после запуска приложения.");
            return;
        }

        _db.SaveConnectionString(txtConnection.Text);
        var success = _db.TestConnection(out var message);
        ShowMessage(success, message);
    }

    private void ShowMessage(bool success, string message)
    {
        lblMessage.ForeColor = success ? Color.DarkGreen : Color.DarkRed;
        lblMessage.Text = message;
    }
}
