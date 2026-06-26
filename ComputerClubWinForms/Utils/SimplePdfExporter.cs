using System.Drawing.Imaging;
using System.Text;
using ComputerClubWinForms.Models;

namespace ComputerClubWinForms.Utils;

public static class SimplePdfExporter
{
    public static bool ExportRevenueReport(RevenueReport report, string filePath)
    {
        try
        {
            using var bitmap = new Bitmap(1240, 1754);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                using var titleFont = new Font("Arial", 30, FontStyle.Bold);
                using var headerFont = new Font("Arial", 18, FontStyle.Bold);
                using var textFont = new Font("Arial", 17);
                var y = 70;
                graphics.DrawString("Отчёт о выручке компьютерного клуба", titleFont, Brushes.Black, 70, y);
                y += 70;
                graphics.DrawString($"Период: {report.PeriodStart:dd.MM.yyyy} - {report.PeriodEnd:dd.MM.yyyy}", textFont, Brushes.Black, 70, y);
                y += 60;
                graphics.DrawString("Дата", headerFont, Brushes.Black, 70, y);
                graphics.DrawString("Сеансов", headerFont, Brushes.Black, 330, y);
                graphics.DrawString("Выручка, руб.", headerFont, Brushes.Black, 560, y);
                y += 44;
                foreach (var day in report.DailyBreakdown)
                {
                    graphics.DrawString(day.Date.ToString("dd.MM.yyyy"), textFont, Brushes.Black, 70, y);
                    graphics.DrawString(day.SessionsCount.ToString(), textFont, Brushes.Black, 360, y);
                    graphics.DrawString(day.Revenue.ToString("0.00"), textFont, Brushes.Black, 560, y);
                    y += 38;
                    if (y > 1500)
                    {
                        break;
                    }
                }
                y += 30;
                graphics.DrawString($"Итого сеансов: {report.TotalSessions}", headerFont, Brushes.Black, 70, y);
                y += 44;
                graphics.DrawString($"Итого выручка: {report.TotalRevenue:0.00} руб.", headerFont, Brushes.Black, 70, y);
            }
            using var imageStream = new MemoryStream();
            bitmap.Save(imageStream, ImageFormat.Jpeg);
            WriteImagePdf(filePath, imageStream.ToArray(), 595, 842);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static void WriteImagePdf(string filePath, byte[] imageBytes, int width, int height)
    {
        using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        var offsets = new List<long> { 0 };
        WriteAscii(stream, "%PDF-1.4\n");
        offsets.Add(stream.Position);
        WriteAscii(stream, "1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n");
        offsets.Add(stream.Position);
        WriteAscii(stream, "2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n");
        offsets.Add(stream.Position);
        WriteAscii(stream, $"3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 {width} {height}] /Resources << /XObject << /Im0 4 0 R >> >> /Contents 5 0 R >>\nendobj\n");
        offsets.Add(stream.Position);
        WriteAscii(stream, $"4 0 obj\n<< /Type /XObject /Subtype /Image /Width 1240 /Height 1754 /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length {imageBytes.Length} >>\nstream\n");
        stream.Write(imageBytes, 0, imageBytes.Length);
        WriteAscii(stream, "\nendstream\nendobj\n");
        var content = $"q\n{width} 0 0 {height} 0 0 cm\n/Im0 Do\nQ\n";
        var contentBytes = Encoding.ASCII.GetBytes(content);
        offsets.Add(stream.Position);
        WriteAscii(stream, $"5 0 obj\n<< /Length {contentBytes.Length} >>\nstream\n");
        stream.Write(contentBytes, 0, contentBytes.Length);
        WriteAscii(stream, "endstream\nendobj\n");
        var xref = stream.Position;
        WriteAscii(stream, "xref\n0 6\n0000000000 65535 f \n");
        for (var i = 1; i < offsets.Count; i++)
        {
            WriteAscii(stream, offsets[i].ToString("0000000000") + " 00000 n \n");
        }
        WriteAscii(stream, $"trailer\n<< /Size 6 /Root 1 0 R >>\nstartxref\n{xref}\n%%EOF");
    }

    private static void WriteAscii(Stream stream, string value)
    {
        var bytes = Encoding.ASCII.GetBytes(value);
        stream.Write(bytes, 0, bytes.Length);
    }
}
