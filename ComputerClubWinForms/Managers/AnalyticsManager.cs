using ComputerClubWinForms.Models;

namespace ComputerClubWinForms.Managers;

public class AnalyticsManager
{
    private readonly SessionManager _sessionManager;
    public string LastMessage { get; private set; } = string.Empty;

    public AnalyticsManager(SessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public RevenueReport? GenerateReport(DateTime start, DateTime end)
    {
        LastMessage = string.Empty;
        if (start.Date > end.Date)
        {
            LastMessage = "Начальная дата не может быть больше конечной.";
            return null;
        }
        var periodStart = start.Date;
        var periodEnd = end.Date.AddDays(1);
        var sessions = _sessionManager.GetCompletedSessions(periodStart, periodEnd);
        var report = new RevenueReport
        {
            PeriodStart = periodStart,
            PeriodEnd = end.Date,
            DailyBreakdown = sessions
                .Where(s => s.EndTime.HasValue)
                .GroupBy(s => s.EndTime!.Value.Date)
                .OrderBy(g => g.Key)
                .Select(g => new ReportDay
                {
                    Date = g.Key,
                    SessionsCount = g.Count(),
                    Revenue = g.Sum(s => s.TotalCost)
                })
                .ToList()
        };
        report.TotalSessions = report.DailyBreakdown.Sum(x => x.SessionsCount);
        report.TotalRevenue = report.DailyBreakdown.Sum(x => x.Revenue);
        LastMessage = "Отчёт сформирован.";
        return report;
    }
}
