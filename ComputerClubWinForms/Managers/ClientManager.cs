using ComputerClubWinForms.Data;
using ComputerClubWinForms.Models;
using Npgsql;

namespace ComputerClubWinForms.Managers;

public class ClientManager
{
    private readonly DatabaseHelper _db;
    public string LastMessage { get; private set; } = string.Empty;

    public ClientManager(DatabaseHelper db)
    {
        _db = db;
    }

    public bool AddClient(Client client)
    {
        LastMessage = string.Empty;
        if (!ValidateClient(client))
        {
            return false;
        }
        try
        {
            using var connection = _db.CreateConnection();
            connection.Open();
            using var command = new NpgsqlCommand("INSERT INTO clients(full_name, phone, total_hours, total_spent, discount_percent) VALUES (@fullName, @phone, 0, 0, 0)", connection);
            command.Parameters.AddWithValue("fullName", client.FullName.Trim());
            command.Parameters.AddWithValue("phone", client.Phone.Trim());
            command.ExecuteNonQuery();
            LastMessage = "Клиент добавлен.";
            return true;
        }
        catch (PostgresException ex) when (ex.SqlState == "23505")
        {
            LastMessage = "Клиент с таким телефоном уже существует.";
            return false;
        }
    }

    public bool EditClient(Client client)
    {
        LastMessage = string.Empty;
        if (client.Id <= 0)
        {
            LastMessage = "Клиент не выбран.";
            return false;
        }
        if (!ValidateClient(client))
        {
            return false;
        }
        try
        {
            using var connection = _db.CreateConnection();
            connection.Open();
            using var command = new NpgsqlCommand("UPDATE clients SET full_name = @fullName, phone = @phone WHERE id = @id", connection);
            command.Parameters.AddWithValue("id", client.Id);
            command.Parameters.AddWithValue("fullName", client.FullName.Trim());
            command.Parameters.AddWithValue("phone", client.Phone.Trim());
            var rows = command.ExecuteNonQuery();
            LastMessage = rows > 0 ? "Данные клиента изменены." : "Клиент не найден.";
            return rows > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == "23505")
        {
            LastMessage = "Клиент с таким телефоном уже существует.";
            return false;
        }
    }

    public bool DeleteClient(int clientId)
    {
        LastMessage = string.Empty;
        if (clientId <= 0)
        {
            LastMessage = "Клиент не выбран.";
            return false;
        }
        using var connection = _db.CreateConnection();
        connection.Open();
        using var activeCommand = new NpgsqlCommand("SELECT COUNT(*) FROM sessions WHERE client_id = @id AND is_completed = FALSE", connection);
        activeCommand.Parameters.AddWithValue("id", clientId);
        var activeCount = Convert.ToInt32(activeCommand.ExecuteScalar());
        if (activeCount > 0)
        {
            LastMessage = "Нельзя удалить клиента с активным сеансом.";
            return false;
        }
        using var bookingCommand = new NpgsqlCommand("SELECT COUNT(*) FROM bookings WHERE client_id = @id AND status = 'active'", connection);
        bookingCommand.Parameters.AddWithValue("id", clientId);
        var bookingCount = Convert.ToInt32(bookingCommand.ExecuteScalar());
        if (bookingCount > 0)
        {
            LastMessage = "Нельзя удалить клиента с активной бронью.";
            return false;
        }
        using var command = new NpgsqlCommand("DELETE FROM clients WHERE id = @id", connection);
        command.Parameters.AddWithValue("id", clientId);
        var rows = command.ExecuteNonQuery();
        LastMessage = rows > 0 ? "Клиент удалён." : "Клиент не найден.";
        return rows > 0;
    }

    public List<Client> GetAllClients(string? searchText = null)
    {
        var result = new List<Client>();
        using var connection = _db.CreateConnection();
        connection.Open();
        var sql = "SELECT id, full_name, phone, total_hours, total_spent, discount_percent FROM clients";
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            sql += " WHERE LOWER(full_name) LIKE @search OR LOWER(phone) LIKE @search";
        }
        sql += " ORDER BY full_name";
        using var command = new NpgsqlCommand(sql, connection);
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            command.Parameters.AddWithValue("search", "%" + searchText.Trim().ToLower() + "%");
        }
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(ReadClient(reader));
        }
        return result;
    }

    public Client? GetClientById(int clientId)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        using var command = new NpgsqlCommand("SELECT id, full_name, phone, total_hours, total_spent, discount_percent FROM clients WHERE id = @id", connection);
        command.Parameters.AddWithValue("id", clientId);
        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadClient(reader) : null;
    }

    public double GetClientTotalHours(int clientId)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        using var command = new NpgsqlCommand("SELECT total_hours FROM clients WHERE id = @id", connection);
        command.Parameters.AddWithValue("id", clientId);
        var value = command.ExecuteScalar();
        return value == null ? 0 : Convert.ToDouble(value);
    }

    public bool UpdateClientStats(int clientId, double hours, decimal spent, NpgsqlConnection connection, NpgsqlTransaction transaction)
    {
        var newDiscount = GetPersonalDiscountByAddedHours(clientId, hours, connection, transaction);
        using var command = new NpgsqlCommand("UPDATE clients SET total_hours = total_hours + @hours, total_spent = total_spent + @spent, discount_percent = @discount WHERE id = @id", connection, transaction);
        command.Parameters.AddWithValue("id", clientId);
        command.Parameters.AddWithValue("hours", hours);
        command.Parameters.AddWithValue("spent", spent);
        command.Parameters.AddWithValue("discount", newDiscount);
        return command.ExecuteNonQuery() > 0;
    }

    private int GetPersonalDiscountByAddedHours(int clientId, double addedHours, NpgsqlConnection connection, NpgsqlTransaction transaction)
    {
        using var command = new NpgsqlCommand("SELECT total_hours FROM clients WHERE id = @id FOR UPDATE", connection, transaction);
        command.Parameters.AddWithValue("id", clientId);
        var value = command.ExecuteScalar();
        var totalHours = (value == null ? 0 : Convert.ToDouble(value)) + addedHours;
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

    private static Client ReadClient(NpgsqlDataReader reader)
    {
        return new Client
        {
            Id = reader.GetInt32(0),
            FullName = reader.GetString(1),
            Phone = reader.GetString(2),
            TotalHours = Convert.ToDouble(reader.GetDecimal(3)),
            TotalSpent = reader.GetDecimal(4),
            DiscountPercent = reader.GetInt32(5)
        };
    }

    private bool ValidateClient(Client client)
    {
        if (string.IsNullOrWhiteSpace(client.FullName))
        {
            LastMessage = "Введите ФИО клиента.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(client.Phone))
        {
            LastMessage = "Введите телефон клиента.";
            return false;
        }
        return true;
    }
}
