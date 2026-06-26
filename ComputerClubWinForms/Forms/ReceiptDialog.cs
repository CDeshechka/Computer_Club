using ComputerClubWinForms.Models;

namespace ComputerClubWinForms.Forms;

public partial class ReceiptDialog : Form
{
    private Session? _session;

    public ReceiptDialog()
    {
        InitializeComponent();
    }

    public ReceiptDialog(Session session) : this()
    {
        _session = session;
        ShowSession();
    }

    private void ShowSession()
    {
        if (_session == null)
        {
            return;
        }

        txtReceipt.Text =
            $"Клиент: {_session.ClientName}{Environment.NewLine}" +
            $"Компьютер: ПК {_session.ComputerNumber}{Environment.NewLine}" +
            $"Начало: {_session.StartTime:dd.MM.yyyy HH:mm}{Environment.NewLine}" +
            $"Окончание: {_session.EndTime:dd.MM.yyyy HH:mm}{Environment.NewLine}" +
            $"Длительность: {Math.Ceiling(_session.Duration.TotalMinutes)} мин.{Environment.NewLine}" +
            $"Тариф: {_session.TariffPerHour:0.00} руб./час{Environment.NewLine}" +
            $"Разовая скидка: {_session.OneTimeDiscountPercent:0}%{Environment.NewLine}" +
            $"Накопительная скидка: {_session.PersonalDiscountPercent:0}%{Environment.NewLine}" +
            $"Итого к оплате: {_session.TotalCost:0.00} руб.";
    }

    private void OnConfirmClick(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }
}
