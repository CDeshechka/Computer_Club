using ComputerClubWinForms.Data;
using ComputerClubWinForms.Models;
using Npgsql;

namespace ComputerClubWinForms.Managers;

public class UserManager
{
    private readonly DatabaseHelper _db;
    private User? _currentUser;
    public string LastMessage { get; private set; } = string.Empty;

    public UserManager(DatabaseHelper db)
    {
        _db = db;
    }

    public User? Login(string username, string password)
    {
        LastMessage = string.Empty;
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            LastMessage = "Введите логин и пароль.";
            return null;
        }
        using var connection = _db.CreateConnection();
        connection.Open();
        using var command = new NpgsqlCommand("SELECT id, username, password, role FROM users WHERE username = @username AND password = @password", connection);
        command.Parameters.AddWithValue("username", username.Trim());
        command.Parameters.AddWithValue("password", password.Trim());
        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            LastMessage = "Неверный логин или пароль.";
            return null;
        }
        _currentUser = new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            Password = reader.GetString(2),
            Role = reader.GetString(3)
        };
        LastMessage = "Вход выполнен.";
        return _currentUser;
    }

    public User? GetCurrentUser()
    {
        return _currentUser;
    }

    public void Logout()
    {
        _currentUser = null;
        LastMessage = "Пользователь вышел из системы.";
    }
}
