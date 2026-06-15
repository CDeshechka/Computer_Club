using Microsoft.Data.Sqlite;

namespace ComputerClubWinForms.Data;

public class DatabaseHelper
{
    public string DatabasePath { get; }
    public string ConnectionString { get; }

    public DatabaseHelper()
    {
        var dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        Directory.CreateDirectory(dataDirectory);

        DatabasePath = Path.Combine(dataDirectory, "computer_club.db");
        ConnectionString = new SqliteConnectionStringBuilder
        {
            DataSource = DatabasePath,
            ForeignKeys = true
        }.ToString();
    }

    public SqliteConnection CreateConnection()
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    public void Initialize()
    {
        using var connection = CreateConnection();
        CreateTables(connection);
        SeedInitialData(connection);
    }

    private static void CreateTables(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
        PRAGMA foreign_keys = ON;

        CREATE TABLE IF NOT EXISTS Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Username TEXT NOT NULL UNIQUE,
            Password TEXT NOT NULL,
            Role TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS Clients (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            FullName TEXT NOT NULL,
            Phone TEXT NOT NULL UNIQUE,
            TotalHours REAL NOT NULL DEFAULT 0,
            TotalSpent REAL NOT NULL DEFAULT 0,
            DiscountPercent INTEGER NOT NULL DEFAULT 0
        );

        CREATE TABLE IF NOT EXISTS Computers (
            Number INTEGER PRIMARY KEY,
            Name TEXT NOT NULL,
            IsActive INTEGER NOT NULL DEFAULT 1
        );

        CREATE TABLE IF NOT EXISTS Bookings (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            ClientId INTEGER NOT NULL,
            ComputerNumber INTEGER NOT NULL,
            PlannedStart TEXT NOT NULL,
            DurationMinutes INTEGER NOT NULL,
            Status TEXT NOT NULL DEFAULT 'active',
            CreatedAt TEXT NOT NULL,
            FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
            FOREIGN KEY (ComputerNumber) REFERENCES Computers(Number)
        );

        CREATE TABLE IF NOT EXISTS Sessions (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            ClientId INTEGER NOT NULL,
            ComputerNumber INTEGER NOT NULL,
            StartTime TEXT NOT NULL,
            PlannedEndTime TEXT NOT NULL,
            EndTime TEXT NULL,
            DurationMinutes INTEGER NOT NULL DEFAULT 0,
            TotalCost REAL NOT NULL DEFAULT 0,
            TariffPerHour REAL NOT NULL DEFAULT 0,
            OneTimeDiscountPercent REAL NOT NULL DEFAULT 0,
            PersonalDiscountPercent REAL NOT NULL DEFAULT 0,
            IsCompleted INTEGER NOT NULL DEFAULT 0,
            BookingId INTEGER NULL,
            FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
            FOREIGN KEY (ComputerNumber) REFERENCES Computers(Number),
            FOREIGN KEY (BookingId) REFERENCES Bookings(Id) ON DELETE SET NULL
        );

        CREATE TABLE IF NOT EXISTS Settings (
            Key TEXT PRIMARY KEY,
            Value TEXT NOT NULL
        );
        """;
        command.ExecuteNonQuery();
    }


    private static void SeedInitialData(SqliteConnection connection)
    {
        using var transaction = connection.BeginTransaction();

        Execute(connection, transaction,
            "INSERT OR IGNORE INTO Settings(Key, Value) VALUES ('TariffPerHour', '150')");

        for (var number = 1; number <= 12; number++)
        {
            Execute(connection, transaction,
                "INSERT OR IGNORE INTO Computers(Number, Name, IsActive) VALUES ($number, $name, 1)",
                ("$number", number),
                ("$name", $"Компьютер №{number}"));
        }

        if (CountRows(connection, transaction, "Users") == 0)
        {
            Execute(connection, transaction,
                "INSERT INTO Users(Username, Password, Role) VALUES ('admin', 'admin123', 'admin')");
            Execute(connection, transaction,
                "INSERT INTO Users(Username, Password, Role) VALUES ('manager', 'manager123', 'manager')");
        }

        transaction.Commit();
    }

    private static int CountRows(SqliteConnection connection, SqliteTransaction transaction, string tableName)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = $"SELECT COUNT(*) FROM {tableName}";
        return Convert.ToInt32(command.ExecuteScalar());
    }

    private static void Execute(SqliteConnection connection, SqliteTransaction transaction, string sql, params (string Name, object? Value)[] parameters)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = sql;

        foreach (var parameter in parameters)
            command.Parameters.AddWithValue(parameter.Name, parameter.Value ?? DBNull.Value);

        command.ExecuteNonQuery();
    }
}
