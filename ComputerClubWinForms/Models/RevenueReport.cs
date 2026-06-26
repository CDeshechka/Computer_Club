using ComputerClubWinForms.Utils;

namespace ComputerClubWinForms.Models;

public class RevenueReport
{
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public List<ReportDay> DailyBreakdown { get; set; } = new();
    public decimal TotalRevenue { get; set; }
    public int TotalSessions { get; set; }

    public decimal GetRevenueDifference(RevenueReport other)
    {
        return TotalRevenue - other.TotalRevenue;
    }

    public decimal GetRevenuePercentChange(RevenueReport other)
    {
        if (other.TotalRevenue == 0)
        {
            return TotalRevenue == 0 ? 0 : 100;
        }
        return Math.Round((TotalRevenue - other.TotalRevenue) / other.TotalRevenue * 100, 2);
    }

    public bool ExportToPdf(string filePath)
    {
        return SimplePdfExporter.ExportRevenueReport(this, filePath);
    }
}
