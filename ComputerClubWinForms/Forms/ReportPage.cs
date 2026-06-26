using ComputerClubWinForms.Managers;
using ComputerClubWinForms.Models;

namespace ComputerClubWinForms.Forms;

public partial class ReportPage : UserControl
{
    private AnalyticsManager? _analyticsManager;
    private RevenueReport? _currentReport;

    public ReportPage()
    {
        InitializeComponent();
        dateStart.Value = DateTime.Now.Date.AddDays(-7);
        dateEnd.Value = DateTime.Now.Date;
    }

    public ReportPage(AnalyticsManager analyticsManager) : this()
    {
        _analyticsManager = analyticsManager;
    }

    public void ShowReport(RevenueReport report)
    {
        gridReport.DataSource = report.DailyBreakdown.Select(d => new
        {
            Дата = d.Date.ToString("dd.MM.yyyy"),
            Сеансов = d.SessionsCount,
            Выручка = d.Revenue
        }).ToList();
        lblSummary.Text = $"Итого: {report.TotalSessions} сеансов, {report.TotalRevenue:0.00} руб.";
    }

    private void OnGenerateClick(object? sender, EventArgs e)
    {
        if (_analyticsManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        _currentReport = _analyticsManager.GenerateReport(dateStart.Value, dateEnd.Value);
        if (_currentReport == null)
        {
            ShowMessage(_analyticsManager.LastMessage, false);
            return;
        }
        ShowReport(_currentReport);
        ShowMessage(_analyticsManager.LastMessage, true);
    }

    private void OnCompareClick(object? sender, EventArgs e)
    {
        if (_analyticsManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        if (_currentReport == null)
        {
            ShowMessage("Сначала сформируйте основной отчёт.", false);
            return;
        }
        using var dialog = new ComparePeriodDialog();
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }
        var otherReport = _analyticsManager.GenerateReport(dialog.PeriodStart, dialog.PeriodEnd);
        if (otherReport == null)
        {
            ShowMessage(_analyticsManager.LastMessage, false);
            return;
        }
        var difference = _currentReport.GetRevenueDifference(otherReport);
        var percent = _currentReport.GetRevenuePercentChange(otherReport);
        MessageBox.Show($"Выручка текущего периода: {_currentReport.TotalRevenue:0.00} руб.\nВыручка второго периода: {otherReport.TotalRevenue:0.00} руб.\nРазница: {difference:0.00} руб.\nИзменение: {percent:0.##}%", "Сравнение периодов", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void OnExportClick(object? sender, EventArgs e)
    {
        if (_currentReport == null)
        {
            ShowMessage("Сначала сформируйте отчёт.", false);
            return;
        }
        using var dialog = new SaveFileDialog
        {
            Filter = "PDF|*.pdf",
            FileName = "revenue_report.pdf"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }
        var success = _currentReport.ExportToPdf(dialog.FileName);
        ShowMessage(success ? "Отчёт экспортирован." : "Не удалось экспортировать отчёт.", success);
    }

    private void ShowMessage(string message, bool success)
    {
        lblMessage.ForeColor = success ? Color.DarkGreen : Color.DarkRed;
        lblMessage.Text = message;
    }
}
