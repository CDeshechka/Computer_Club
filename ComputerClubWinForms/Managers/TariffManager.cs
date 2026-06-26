using ComputerClubWinForms.Data;
using ComputerClubWinForms.Models;
using Npgsql;

namespace ComputerClubWinForms.Managers;

public class TariffManager
{
    private readonly DatabaseHelper _db;
    private readonly ClientManager _clientManager;
    public string LastMessage { get; private set; } = string.Empty;

    public TariffManager(DatabaseHelper db, ClientManager clientManager)
    {
        _db = db;
        _clientManager = clientManager;
    }

    public decimal GetCurrentTariff()
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        using var command = new NpgsqlCommand("SELECT value FROM settings WHERE key = 'TariffPerHour'", connection);
        var value = command.ExecuteScalar();
        if (value == null)
        {
            return 150;
        }
        return decimal.TryParse(value.ToString(), out var tariff) ? tariff : 150;
    }

    public bool UpdateTariff(decimal newTariff)
    {
        LastMessage = string.Empty;
        if (newTariff <= 0)
        {
            LastMessage = "Тариф должен быть больше нуля.";
            return false;
        }
        using var connection = _db.CreateConnection();
        connection.Open();
        using var command = new NpgsqlCommand("INSERT INTO settings(key, value) VALUES ('TariffPerHour', @value) ON CONFLICT (key) DO UPDATE SET value = EXCLUDED.value", connection);
        command.Parameters.AddWithValue("value", newTariff.ToString("0.##"));
        command.ExecuteNonQuery();
        LastMessage = "Тариф сохранён.";
        return true;
    }

    public void CalculateSessionCost(Session session)
    {
        var hours = Math.Max(1m / 60m, (decimal)session.Duration.TotalMinutes / 60m);
        session.TariffPerHour = GetCurrentTariff();
        session.OneTimeDiscountPercent = session.Duration.TotalHours > 3 ? 5 : 0;
        session.PersonalDiscountPercent = GetPersonalDiscount(_clientManager.GetClientTotalHours(session.ClientId));
        var baseCost = hours * session.TariffPerHour;
        var totalDiscount = session.OneTimeDiscountPercent + session.PersonalDiscountPercent;
        session.TotalCost = Math.Round(baseCost * (1 - totalDiscount / 100), 2);
    }

    public int GetPersonalDiscount(double totalHours)
    {
        if (totalHours >= 100)
        {
            return 15;
        }
        if (totalHours >= 50)
        {
            return 10;
        }
        if (totalHours >= 20)
        {
            return 5;
        }
        return 0;
    }
}
