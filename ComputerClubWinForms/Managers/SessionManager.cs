using System.Data;
using ComputerClubWinForms.Data;
using ComputerClubWinForms.Models;
using Npgsql;

namespace ComputerClubWinForms.Managers;

public class SessionManager
{
    private readonly DatabaseHelper _db;
    private readonly ClientManager _clientManager;
    private readonly TariffManager _tariffManager;
    public string LastMessage { get; private set; } = string.Empty;

    public SessionManager(DatabaseHelper db, ClientManager clientManager, TariffManager tariffManager)
    {
        _db = db;
        _clientManager = clientManager;
        _tariffManager = tariffManager;
    }

    public List<Computer> GetComputers()
    {
        var result = new List<Computer>();
        using var connection = _db.CreateConnection();
        connection.Open();
        using var command = new NpgsqlCommand("SELECT number, name, is_active FROM computers ORDER BY number", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Computer
            {
                Number = reader.GetInt32(0),
                Name = reader.GetString(1),
                IsActive = reader.GetBoolean(2)
            });
        }
        return result;
    }

    public bool IsTimeSlotAvailable(DateTime start, TimeSpan duration, int computerNumber)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        return IsTimeSlotAvailable(start, duration, computerNumber, connection, null, 0, 0);
    }

    public bool CreateBooking(int clientId, int computerNumber, DateTime start, TimeSpan duration)
    {
        LastMessage = string.Empty;
        if (!ValidateClientComputerDuration(clientId, computerNumber, duration))
        {
            return false;
        }
        if (start <= DateTime.Now)
        {
            LastMessage = "Нельзя забронировать прошедшее время.";
            return false;
        }
        using var connection = _db.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            if (!ClientExists(clientId, connection, transaction))
            {
                LastMessage = "Клиент не найден.";
                transaction.Rollback();
                return false;
            }
            if (!ComputerExists(computerNumber, connection, transaction))
            {
                LastMessage = "Компьютер не найден.";
                transaction.Rollback();
                return false;
            }
            if (!IsTimeSlotAvailable(start, duration, computerNumber, connection, transaction, 0, 0))
            {
                LastMessage = $"Компьютер №{computerNumber} уже занят на это время.";
                transaction.Rollback();
                return false;
            }
            using var command = new NpgsqlCommand("INSERT INTO bookings(client_id, computer_number, planned_start, duration_minutes, status) VALUES (@clientId, @computerNumber, @start, @duration, 'active')", connection, transaction);
            command.Parameters.AddWithValue("clientId", clientId);
            command.Parameters.AddWithValue("computerNumber", computerNumber);
            command.Parameters.AddWithValue("start", start);
            command.Parameters.AddWithValue("duration", (int)Math.Ceiling(duration.TotalMinutes));
            command.ExecuteNonQuery();
            transaction.Commit();
            LastMessage = "Бронь создана.";
            return true;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            LastMessage = "Ошибка базы данных. Бронь не сохранена. " + ex.Message;
            return false;
        }
    }

    public bool DeleteBooking(int bookingId)
    {
        LastMessage = string.Empty;
        if (bookingId <= 0)
        {
            LastMessage = "Бронь не выбрана.";
            return false;
        }
        using var connection = _db.CreateConnection();
        connection.Open();
        using var command = new NpgsqlCommand("UPDATE bookings SET status = 'cancelled' WHERE id = @id AND status = 'active'", connection);
        command.Parameters.AddWithValue("id", bookingId);
        var rows = command.ExecuteNonQuery();
        LastMessage = rows > 0 ? "Бронь отменена." : "Активная бронь не найдена.";
        return rows > 0;
    }

    public Session? StartSession(int bookingId)
    {
        LastMessage = string.Empty;
        if (bookingId <= 0)
        {
            LastMessage = "Бронь не выбрана.";
            return null;
        }
        using var connection = _db.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            var booking = GetBookingById(bookingId, connection, transaction);
            if (booking == null || booking.Status != "active")
            {
                LastMessage = "Активная бронь не найдена.";
                transaction.Rollback();
                return null;
            }
            if (ClientHasActiveSession(booking.ClientId, connection, transaction))
            {
                LastMessage = "У клиента уже есть активный сеанс.";
                transaction.Rollback();
                return null;
            }
            var session = InsertSession(booking.ClientId, booking.ComputerNumber, DateTime.Now, booking.Duration, connection, transaction);
            using var updateCommand = new NpgsqlCommand("UPDATE bookings SET status = 'used' WHERE id = @id", connection, transaction);
            updateCommand.Parameters.AddWithValue("id", booking.Id);
            updateCommand.ExecuteNonQuery();
            transaction.Commit();
            LastMessage = "Сеанс открыт по брони.";
            return session;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            LastMessage = "Ошибка открытия сеанса. " + ex.Message;
            return null;
        }
    }

    public Session? StartSessionDirect(int clientId, int computerNumber, DateTime start, TimeSpan duration)
    {
        LastMessage = string.Empty;
        if (!ValidateClientComputerDuration(clientId, computerNumber, duration))
        {
            return null;
        }
        using var connection = _db.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            if (!ClientExists(clientId, connection, transaction))
            {
                LastMessage = "Клиент не найден.";
                transaction.Rollback();
                return null;
            }
            if (!ComputerExists(computerNumber, connection, transaction))
            {
                LastMessage = "Компьютер не найден.";
                transaction.Rollback();
                return null;
            }
            if (ClientHasActiveSession(clientId, connection, transaction))
            {
                LastMessage = "У клиента уже есть активный сеанс.";
                transaction.Rollback();
                return null;
            }
            if (!IsTimeSlotAvailable(start, duration, computerNumber, connection, transaction, 0, 0))
            {
                LastMessage = $"Компьютер №{computerNumber} уже занят на выбранный интервал.";
                transaction.Rollback();
                return null;
            }
            var session = InsertSession(clientId, computerNumber, start, duration, connection, transaction);
            transaction.Commit();
            LastMessage = "Сеанс открыт.";
            return session;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            LastMessage = "Ошибка открытия сеанса. " + ex.Message;
            return null;
        }
    }

    public bool ExtendSession(int sessionId, TimeSpan additionalDuration)
    {
        LastMessage = string.Empty;
        if (sessionId <= 0)
        {
            LastMessage = "Сеанс не выбран.";
            return false;
        }
        if (additionalDuration.TotalMinutes <= 0)
        {
            LastMessage = "Дополнительное время должно быть больше нуля.";
            return false;
        }
        using var connection = _db.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            var session = GetSessionById(sessionId, connection, transaction);
            if (session == null || session.IsCompleted)
            {
                LastMessage = "Активный сеанс не найден.";
                transaction.Rollback();
                return false;
            }
            var newDuration = session.PlannedEndTime.Add(additionalDuration) - session.StartTime;
            if (!IsTimeSlotAvailable(session.StartTime, newDuration, session.ComputerNumber, connection, transaction, session.Id, 0))
            {
                LastMessage = "Продление невозможно: на это время есть бронь или другой сеанс.";
                transaction.Rollback();
                return false;
            }
            using var command = new NpgsqlCommand("UPDATE sessions SET planned_end_time = planned_end_time + (@minutes * INTERVAL '1 minute') WHERE id = @id AND is_completed = FALSE", connection, transaction);
            command.Parameters.AddWithValue("id", sessionId);
            command.Parameters.AddWithValue("minutes", (int)Math.Ceiling(additionalDuration.TotalMinutes));
            var rows = command.ExecuteNonQuery();
            transaction.Commit();
            LastMessage = rows > 0 ? "Сеанс продлён." : "Сеанс не найден.";
            return rows > 0;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            LastMessage = "Ошибка продления сеанса. " + ex.Message;
            return false;
        }
    }

    public Session? PreviewCompletion(int sessionId)
    {
        LastMessage = string.Empty;
        using var connection = _db.CreateConnection();
        connection.Open();
        var session = GetSessionById(sessionId, connection, null);
        if (session == null || session.IsCompleted)
        {
            LastMessage = "Активный сеанс не найден.";
            return null;
        }
        session.EndTime = DateTime.Now;
        session.Duration = session.EndTime.Value - session.StartTime;
        if (session.Duration.TotalMinutes < 1)
        {
            session.Duration = TimeSpan.FromMinutes(1);
        }
        _tariffManager.CalculateSessionCost(session);
        LastMessage = "Расчёт подготовлен.";
        return session;
    }

    public Session? CompleteSession(int sessionId)
    {
        LastMessage = string.Empty;
        using var connection = _db.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            var session = GetSessionById(sessionId, connection, transaction);
            if (session == null || session.IsCompleted)
            {
                LastMessage = "Активный сеанс не найден.";
                transaction.Rollback();
                return null;
            }
            session.EndTime = DateTime.Now;
            session.Duration = session.EndTime.Value - session.StartTime;
            if (session.Duration.TotalMinutes < 1)
            {
                session.Duration = TimeSpan.FromMinutes(1);
            }
            _tariffManager.CalculateSessionCost(session);
            using var command = new NpgsqlCommand(@"
UPDATE sessions
SET end_time = @endTime,
    duration_minutes = @duration,
    total_cost = @cost,
    tariff_per_hour = @tariff,
    one_time_discount_percent = @oneTimeDiscount,
    personal_discount_percent = @personalDiscount,
    is_completed = TRUE
WHERE id = @id AND is_completed = FALSE", connection, transaction);
            command.Parameters.AddWithValue("id", session.Id);
            command.Parameters.AddWithValue("endTime", session.EndTime.Value);
            command.Parameters.AddWithValue("duration", (int)Math.Ceiling(session.Duration.TotalMinutes));
            command.Parameters.AddWithValue("cost", session.TotalCost);
            command.Parameters.AddWithValue("tariff", session.TariffPerHour);
            command.Parameters.AddWithValue("oneTimeDiscount", session.OneTimeDiscountPercent);
            command.Parameters.AddWithValue("personalDiscount", session.PersonalDiscountPercent);
            command.ExecuteNonQuery();
            _clientManager.UpdateClientStats(session.ClientId, session.Duration.TotalHours, session.TotalCost, connection, transaction);
            transaction.Commit();
            session.IsCompleted = true;
            LastMessage = "Сеанс завершён.";
            return session;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            LastMessage = "Ошибка завершения сеанса. " + ex.Message;
            return null;
        }
    }

    public List<Session> GetActiveSessions()
    {
        return GetSessions(false, null, null);
    }

    public List<Session> GetCompletedSessions(DateTime start, DateTime end)
    {
        return GetSessions(true, start, end);
    }

    public List<Booking> GetActiveBookings()
    {
        var result = new List<Booking>();
        using var connection = _db.CreateConnection();
        connection.Open();
        using var command = new NpgsqlCommand(@"
SELECT b.id, b.client_id, c.full_name, b.computer_number, b.planned_start, b.duration_minutes, b.status
FROM bookings b
JOIN clients c ON c.id = b.client_id
WHERE b.status = 'active'
ORDER BY b.planned_start", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(ReadBooking(reader));
        }
        return result;
    }

    private bool ValidateClientComputerDuration(int clientId, int computerNumber, TimeSpan duration)
    {
        if (clientId <= 0)
        {
            LastMessage = "Клиент не выбран.";
            return false;
        }
        if (computerNumber <= 0)
        {
            LastMessage = "Компьютер не выбран.";
            return false;
        }
        if (duration.TotalMinutes <= 0)
        {
            LastMessage = "Длительность должна быть больше нуля.";
            return false;
        }
        return true;
    }

    private bool IsTimeSlotAvailable(DateTime start, TimeSpan duration, int computerNumber, NpgsqlConnection connection, NpgsqlTransaction? transaction, int ignoreSessionId, int ignoreBookingId)
    {
        var end = start.Add(duration);
        using var sessionCommand = new NpgsqlCommand(@"
SELECT id, start_time, planned_end_time
FROM sessions
WHERE computer_number = @computerNumber AND is_completed = FALSE", connection, transaction);
        sessionCommand.Parameters.AddWithValue("computerNumber", computerNumber);
        using (var reader = sessionCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                if (id == ignoreSessionId)
                {
                    continue;
                }
                var existingStart = reader.GetDateTime(1);
                var existingEnd = reader.GetDateTime(2);
                if (start < existingEnd && existingStart < end)
                {
                    return false;
                }
            }
        }
        using var bookingCommand = new NpgsqlCommand(@"
SELECT id, planned_start, duration_minutes
FROM bookings
WHERE computer_number = @computerNumber AND status = 'active'", connection, transaction);
        bookingCommand.Parameters.AddWithValue("computerNumber", computerNumber);
        using (var reader = bookingCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                if (id == ignoreBookingId)
                {
                    continue;
                }
                var existingStart = reader.GetDateTime(1);
                var existingEnd = existingStart.AddMinutes(reader.GetInt32(2));
                if (start < existingEnd && existingStart < end)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool ClientExists(int clientId, NpgsqlConnection connection, NpgsqlTransaction? transaction)
    {
        using var command = new NpgsqlCommand("SELECT COUNT(*) FROM clients WHERE id = @id", connection, transaction);
        command.Parameters.AddWithValue("id", clientId);
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    private bool ComputerExists(int computerNumber, NpgsqlConnection connection, NpgsqlTransaction? transaction)
    {
        using var command = new NpgsqlCommand("SELECT COUNT(*) FROM computers WHERE number = @number AND is_active = TRUE", connection, transaction);
        command.Parameters.AddWithValue("number", computerNumber);
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    private bool ClientHasActiveSession(int clientId, NpgsqlConnection connection, NpgsqlTransaction? transaction)
    {
        using var command = new NpgsqlCommand("SELECT COUNT(*) FROM sessions WHERE client_id = @id AND is_completed = FALSE", connection, transaction);
        command.Parameters.AddWithValue("id", clientId);
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    private Session InsertSession(int clientId, int computerNumber, DateTime start, TimeSpan duration, NpgsqlConnection connection, NpgsqlTransaction transaction)
    {
        using var command = new NpgsqlCommand(@"
INSERT INTO sessions(client_id, computer_number, start_time, planned_end_time, duration_minutes, is_completed)
VALUES (@clientId, @computerNumber, @start, @plannedEnd, 0, FALSE)
RETURNING id", connection, transaction);
        command.Parameters.AddWithValue("clientId", clientId);
        command.Parameters.AddWithValue("computerNumber", computerNumber);
        command.Parameters.AddWithValue("start", start);
        command.Parameters.AddWithValue("plannedEnd", start.Add(duration));
        var id = Convert.ToInt32(command.ExecuteScalar());
        return GetSessionById(id, connection, transaction) ?? new Session();
    }

    private Booking? GetBookingById(int bookingId, NpgsqlConnection connection, NpgsqlTransaction? transaction)
    {
        using var command = new NpgsqlCommand(@"
SELECT b.id, b.client_id, c.full_name, b.computer_number, b.planned_start, b.duration_minutes, b.status
FROM bookings b
JOIN clients c ON c.id = b.client_id
WHERE b.id = @id", connection, transaction);
        command.Parameters.AddWithValue("id", bookingId);
        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadBooking(reader) : null;
    }

    private Session? GetSessionById(int sessionId, NpgsqlConnection connection, NpgsqlTransaction? transaction)
    {
        using var command = new NpgsqlCommand(@"
SELECT s.id, s.client_id, c.full_name, s.computer_number, s.start_time, s.planned_end_time,
       s.end_time, s.duration_minutes, s.total_cost, s.tariff_per_hour,
       s.one_time_discount_percent, s.personal_discount_percent, s.is_completed
FROM sessions s
JOIN clients c ON c.id = s.client_id
WHERE s.id = @id", connection, transaction);
        command.Parameters.AddWithValue("id", sessionId);
        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadSession(reader) : null;
    }

    private List<Session> GetSessions(bool completed, DateTime? start, DateTime? end)
    {
        var result = new List<Session>();
        using var connection = _db.CreateConnection();
        connection.Open();
        var sql = @"
SELECT s.id, s.client_id, c.full_name, s.computer_number, s.start_time, s.planned_end_time,
       s.end_time, s.duration_minutes, s.total_cost, s.tariff_per_hour,
       s.one_time_discount_percent, s.personal_discount_percent, s.is_completed
FROM sessions s
JOIN clients c ON c.id = s.client_id
WHERE s.is_completed = @completed";
        if (completed && start.HasValue && end.HasValue)
        {
            sql += " AND s.end_time >= @start AND s.end_time < @end";
        }
        sql += completed ? " ORDER BY s.end_time" : " ORDER BY s.start_time";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("completed", completed);
        if (completed && start.HasValue && end.HasValue)
        {
            command.Parameters.AddWithValue("start", start.Value);
            command.Parameters.AddWithValue("end", end.Value);
        }
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(ReadSession(reader));
        }
        return result;
    }

    private static Booking ReadBooking(NpgsqlDataReader reader)
    {
        return new Booking
        {
            Id = reader.GetInt32(0),
            ClientId = reader.GetInt32(1),
            ClientName = reader.GetString(2),
            ComputerNumber = reader.GetInt32(3),
            PlannedStart = reader.GetDateTime(4),
            Duration = TimeSpan.FromMinutes(reader.GetInt32(5)),
            Status = reader.GetString(6)
        };
    }

    private static Session ReadSession(NpgsqlDataReader reader)
    {
        return new Session
        {
            Id = reader.GetInt32(0),
            ClientId = reader.GetInt32(1),
            ClientName = reader.GetString(2),
            ComputerNumber = reader.GetInt32(3),
            StartTime = reader.GetDateTime(4),
            PlannedEndTime = reader.GetDateTime(5),
            EndTime = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
            Duration = TimeSpan.FromMinutes(reader.GetInt32(7)),
            TotalCost = reader.GetDecimal(8),
            TariffPerHour = reader.GetDecimal(9),
            OneTimeDiscountPercent = reader.GetDecimal(10),
            PersonalDiscountPercent = reader.GetDecimal(11),
            IsCompleted = reader.GetBoolean(12)
        };
    }
}
