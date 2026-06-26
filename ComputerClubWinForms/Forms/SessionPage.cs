using ComputerClubWinForms.Managers;
using ComputerClubWinForms.Models;

namespace ComputerClubWinForms.Forms;

public partial class SessionPage : UserControl
{
    private ClientManager? _clientManager;
    private SessionManager? _sessionManager;
    private int _selectedComputerNumber = 1;

    public SessionPage()
    {
        InitializeComponent();
    }

    public SessionPage(ClientManager clientManager, SessionManager sessionManager) : this()
    {
        _clientManager = clientManager;
        _sessionManager = sessionManager;
        BuildHallButtons();
        LoadAllData();
        refreshTimer.Start();
    }

    public void ShowActiveSessions(List<Session> sessions)
    {
        gridSessions.DataSource = sessions.Select(s => new
        {
            s.Id,
            Клиент = s.ClientName,
            ПК = s.ComputerNumber,
            Начало = s.StartTime.ToString("dd.MM.yyyy HH:mm"),
            ПлановоеОкончание = s.PlannedEndTime.ToString("dd.MM.yyyy HH:mm"),
            Осталось = FormatTime(s.RemainingTime)
        }).ToList();
        if (gridSessions.Columns["Id"] != null)
        {
            gridSessions.Columns["Id"]!.Visible = false;
        }
    }

    public void ShowBookings(List<Booking> bookings)
    {
        gridBookings.DataSource = bookings.Select(b => new
        {
            b.Id,
            Клиент = b.ClientName,
            ПК = b.ComputerNumber,
            Начало = b.PlannedStart.ToString("dd.MM.yyyy HH:mm"),
            Длительность = FormatTime(b.Duration),
            Окончание = b.PlannedEnd.ToString("dd.MM.yyyy HH:mm")
        }).ToList();
        if (gridBookings.Columns["Id"] != null)
        {
            gridBookings.Columns["Id"]!.Visible = false;
        }
    }

    private void LoadAllData()
    {
        LoadClients();
        LoadSessionsAndBookings();
    }

    private void LoadClients()
    {
        if (_clientManager == null)
        {
            gridClients.DataSource = new List<object>();
            return;
        }

        gridClients.DataSource = _clientManager.GetAllClients(txtClientSearch.Text).Select(c => new
        {
            c.Id,
            ФИО = c.FullName,
            Телефон = c.Phone,
            Скидка = c.DiscountPercent
        }).ToList();
        if (gridClients.Columns["Id"] != null)
        {
            gridClients.Columns["Id"]!.Visible = false;
        }
    }

    private void LoadSessionsAndBookings()
    {
        if (_sessionManager == null)
        {
            ShowActiveSessions(new List<Session>());
            ShowBookings(new List<Booking>());
            RefreshHall(new List<Session>(), new List<Booking>());
            return;
        }

        var sessions = _sessionManager.GetActiveSessions();
        var bookings = _sessionManager.GetActiveBookings();
        ShowActiveSessions(sessions);
        ShowBookings(bookings);
        RefreshHall(sessions, bookings);
    }

    private void BuildHallButtons()
    {
        if (_sessionManager == null)
        {
            return;
        }

        hallPanel.Controls.Clear();
        var computers = _sessionManager.GetComputers();
        foreach (var computer in computers)
        {
            var button = new Button
            {
                Text = computer.Name,
                Width = 96,
                Height = 52,
                Margin = new Padding(10),
                Tag = computer.Number
            };
            button.Click += OnComputerClick;
            hallPanel.Controls.Add(button);
        }
    }

    private void RefreshHall(List<Session> sessions, List<Booking> bookings)
    {
        foreach (Control control in hallPanel.Controls)
        {
            if (control is not Button button || button.Tag == null)
            {
                continue;
            }
            var number = Convert.ToInt32(button.Tag);
            var busy = sessions.Any(s => s.ComputerNumber == number) || bookings.Any(b => b.ComputerNumber == number);
            button.BackColor = busy ? Color.FromArgb(255, 210, 210) : Color.FromArgb(210, 245, 215);
            button.FlatStyle = number == _selectedComputerNumber ? FlatStyle.Popup : FlatStyle.Standard;
        }
        lblSelectedComputer.Text = "Выбран ПК " + _selectedComputerNumber;
    }

    private void OnComputerClick(object? sender, EventArgs e)
    {
        if (sender is Button button && button.Tag != null)
        {
            _selectedComputerNumber = Convert.ToInt32(button.Tag);
            LoadSessionsAndBookings();
        }
    }

    private void OnClientSearchChanged(object? sender, EventArgs e)
    {
        LoadClients();
    }

    private void OnStartSessionClick(object? sender, EventArgs e)
    {
        if (_sessionManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        var client = GetSelectedClient();
        if (client == null)
        {
            ShowMessage("Клиент не выбран.", false);
            return;
        }
        using var dialog = new ExtendSessionDialog("Длительность сеанса, минут", 60);
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }
        var session = _sessionManager.StartSessionDirect(client.Id, _selectedComputerNumber, DateTime.Now, TimeSpan.FromMinutes(dialog.Minutes));
        LoadSessionsAndBookings();
        ShowMessage(_sessionManager.LastMessage, session != null);
    }

    private void OnCreateBookingClick(object? sender, EventArgs e)
    {
        if (_sessionManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        var client = GetSelectedClient();
        if (client == null)
        {
            ShowMessage("Клиент не выбран.", false);
            return;
        }
        using var dialog = new BookingDialog(_selectedComputerNumber);
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }
        var success = _sessionManager.CreateBooking(client.Id, dialog.ComputerNumber, dialog.PlannedStart, TimeSpan.FromMinutes(dialog.DurationMinutes));
        LoadSessionsAndBookings();
        ShowMessage(_sessionManager.LastMessage, success);
    }

    private void OnStartByBookingClick(object? sender, EventArgs e)
    {
        if (_sessionManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        var bookingId = GetSelectedBookingId();
        if (bookingId == null)
        {
            ShowMessage("Бронь не выбрана.", false);
            return;
        }
        var session = _sessionManager.StartSession(bookingId.Value);
        LoadSessionsAndBookings();
        ShowMessage(_sessionManager.LastMessage, session != null);
    }

    private void OnDeleteBookingClick(object? sender, EventArgs e)
    {
        if (_sessionManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        var bookingId = GetSelectedBookingId();
        if (bookingId == null)
        {
            ShowMessage("Бронь не выбрана.", false);
            return;
        }
        var success = _sessionManager.DeleteBooking(bookingId.Value);
        LoadSessionsAndBookings();
        ShowMessage(_sessionManager.LastMessage, success);
    }

    private void OnExtendClick(object? sender, EventArgs e)
    {
        if (_sessionManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        var sessionId = GetSelectedSessionId();
        if (sessionId == null)
        {
            ShowMessage("Сеанс не выбран.", false);
            return;
        }
        using var dialog = new ExtendSessionDialog("Добавить минут", 30);
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }
        var success = _sessionManager.ExtendSession(sessionId.Value, TimeSpan.FromMinutes(dialog.Minutes));
        LoadSessionsAndBookings();
        ShowMessage(_sessionManager.LastMessage, success);
    }

    private void OnCompleteClick(object? sender, EventArgs e)
    {
        if (_sessionManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        var sessionId = GetSelectedSessionId();
        if (sessionId == null)
        {
            ShowMessage("Сеанс не выбран.", false);
            return;
        }
        var preview = _sessionManager.PreviewCompletion(sessionId.Value);
        if (preview == null)
        {
            ShowMessage(_sessionManager.LastMessage, false);
            return;
        }
        using var receiptDialog = new ReceiptDialog(preview);
        if (receiptDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }
        var completed = _sessionManager.CompleteSession(sessionId.Value);
        LoadSessionsAndBookings();
        ShowMessage(_sessionManager.LastMessage, completed != null);
    }

    private void OnRefreshClick(object? sender, EventArgs e)
    {
        LoadAllData();
        ShowMessage("Данные обновлены.", true);
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        if (_sessionManager != null)
        {
            LoadSessionsAndBookings();
        }
    }

    private Client? GetSelectedClient()
    {
        if (_clientManager == null || gridClients.CurrentRow == null || gridClients.CurrentRow.Cells["Id"].Value == null)
        {
            return null;
        }
        var id = Convert.ToInt32(gridClients.CurrentRow.Cells["Id"].Value);
        return _clientManager.GetClientById(id);
    }

    private int? GetSelectedSessionId()
    {
        if (gridSessions.CurrentRow == null || gridSessions.CurrentRow.Cells["Id"].Value == null)
        {
            return null;
        }
        return Convert.ToInt32(gridSessions.CurrentRow.Cells["Id"].Value);
    }

    private int? GetSelectedBookingId()
    {
        if (gridBookings.CurrentRow == null || gridBookings.CurrentRow.Cells["Id"].Value == null)
        {
            return null;
        }
        return Convert.ToInt32(gridBookings.CurrentRow.Cells["Id"].Value);
    }

    private void ShowMessage(string message, bool success)
    {
        lblMessage.ForeColor = success ? Color.DarkGreen : Color.DarkRed;
        lblMessage.Text = message;
    }

    private static string FormatTime(TimeSpan value)
    {
        return value.TotalHours >= 1 ? $"{(int)value.TotalHours:00}:{value.Minutes:00}" : $"00:{value.Minutes:00}";
    }
}
