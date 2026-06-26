using Npgsql;

namespace ComputerClubWinForms.Data;

public class DatabaseHelper
{
    private readonly string _connectionFile;
    public string ConnectionString { get; private set; }

    public DatabaseHelper()
    {
        _connectionFile = Path.Combine(AppContext.BaseDirectory, "Data", "connection.txt");
        ConnectionString = LoadConnectionString();
    }

    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(ConnectionString);
    }

    public void SaveConnectionString(string connectionString)
    {
        var directory = Path.GetDirectoryName(_connectionFile);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }
        File.WriteAllText(_connectionFile, connectionString.Trim());
        ConnectionString = connectionString.Trim();
    }

    public bool TestConnection(out string message)
    {
        try
        {
            EnsureDatabaseExists();
            using var connection = CreateConnection();
            connection.Open();
            using var command = new NpgsqlCommand("SELECT 1", connection);
            command.ExecuteScalar();
            message = "Подключение к PostgreSQL выполнено успешно.";
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }

    public void Initialize()
    {
        EnsureDatabaseExists();
        using var connection = CreateConnection();
        connection.Open();
        ExecuteNonQuery(connection, GetSchemaSql());
        SeedDefaultData(connection);
    }

    private string LoadConnectionString()
    {
        var environmentValue = Environment.GetEnvironmentVariable("COMPUTER_CLUB_CONNECTION_STRING");
        if (!string.IsNullOrWhiteSpace(environmentValue))
        {
            return environmentValue.Trim();
        }
        if (File.Exists(_connectionFile))
        {
            var fileValue = File.ReadAllText(_connectionFile).Trim();
            if (!string.IsNullOrWhiteSpace(fileValue))
            {
                return fileValue;
            }
        }
        return "Host=localhost;Port=5432;Database=computer_club;Username=postgres;Password=postgres";
    }

    private void EnsureDatabaseExists()
    {
        var builder = new NpgsqlConnectionStringBuilder(ConnectionString);
        var databaseName = string.IsNullOrWhiteSpace(builder.Database) ? "computer_club" : builder.Database;
        builder.Database = "postgres";
        using var connection = new NpgsqlConnection(builder.ConnectionString);
        connection.Open();
        using var existsCommand = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @name", connection);
        existsCommand.Parameters.AddWithValue("name", databaseName);
        var exists = existsCommand.ExecuteScalar() != null;
        if (!exists)
        {
            using var createCommand = new NpgsqlCommand("CREATE DATABASE " + QuoteIdentifier(databaseName), connection);
            createCommand.ExecuteNonQuery();
        }
    }

    private static void ExecuteNonQuery(NpgsqlConnection connection, string sql)
    {
        using var command = new NpgsqlCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    private static void SeedDefaultData(NpgsqlConnection connection)
    {
        using var command = new NpgsqlCommand(@"
INSERT INTO users(username, password, role) VALUES
('admin', 'admin123', 'admin'),
('manager', 'manager123', 'manager')
ON CONFLICT (username) DO NOTHING;

INSERT INTO computers(number, name, is_active)
SELECT i, 'ПК ' || i, TRUE FROM generate_series(1, 12) AS s(i)
ON CONFLICT (number) DO NOTHING;

INSERT INTO settings(key, value) VALUES ('TariffPerHour', '150')
ON CONFLICT (key) DO NOTHING;", connection);
        command.ExecuteNonQuery();
    }

    private static string GetSchemaSql()
    {
        return @"
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL,
    role VARCHAR(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS clients (
    id SERIAL PRIMARY KEY,
    full_name VARCHAR(120) NOT NULL,
    phone VARCHAR(30) NOT NULL UNIQUE,
    total_hours NUMERIC(10,2) NOT NULL DEFAULT 0,
    total_spent NUMERIC(12,2) NOT NULL DEFAULT 0,
    discount_percent INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS computers (
    number INTEGER PRIMARY KEY,
    name VARCHAR(30) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS bookings (
    id SERIAL PRIMARY KEY,
    client_id INTEGER NOT NULL REFERENCES clients(id) ON DELETE RESTRICT,
    computer_number INTEGER NOT NULL REFERENCES computers(number) ON DELETE RESTRICT,
    planned_start TIMESTAMP NOT NULL,
    duration_minutes INTEGER NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'active'
);

CREATE TABLE IF NOT EXISTS sessions (
    id SERIAL PRIMARY KEY,
    client_id INTEGER NOT NULL REFERENCES clients(id) ON DELETE RESTRICT,
    computer_number INTEGER NOT NULL REFERENCES computers(number) ON DELETE RESTRICT,
    start_time TIMESTAMP NOT NULL,
    planned_end_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NULL,
    duration_minutes INTEGER NOT NULL DEFAULT 0,
    total_cost NUMERIC(12,2) NOT NULL DEFAULT 0,
    tariff_per_hour NUMERIC(12,2) NOT NULL DEFAULT 0,
    one_time_discount_percent NUMERIC(5,2) NOT NULL DEFAULT 0,
    personal_discount_percent NUMERIC(5,2) NOT NULL DEFAULT 0,
    is_completed BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE IF NOT EXISTS settings (
    key VARCHAR(60) PRIMARY KEY,
    value VARCHAR(120) NOT NULL
);

CREATE INDEX IF NOT EXISTS idx_bookings_computer_time ON bookings(computer_number, planned_start);
CREATE INDEX IF NOT EXISTS idx_sessions_computer_time ON sessions(computer_number, start_time, planned_end_time);";
    }

    private static string QuoteIdentifier(string value)
    {
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
