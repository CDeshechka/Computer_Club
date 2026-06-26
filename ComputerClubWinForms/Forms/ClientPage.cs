using ComputerClubWinForms.Managers;
using ComputerClubWinForms.Models;

namespace ComputerClubWinForms.Forms;

public partial class ClientPage : UserControl
{
    private ClientManager? _clientManager;

    public event EventHandler? ClientsChanged;

    public ClientPage()
    {
        InitializeComponent();
    }

    public ClientPage(ClientManager clientManager) : this()
    {
        _clientManager = clientManager;
        LoadClients();
    }

    public void LoadClients()
    {
        if (_clientManager == null)
        {
            ShowClients(new List<Client>());
            return;
        }

        ShowClients(_clientManager.GetAllClients(txtSearch.Text));
    }

    public void ShowClients(List<Client> clients)
    {
        gridClients.DataSource = clients.Select(c => new
        {
            c.Id,
            ФИО = c.FullName,
            Телефон = c.Phone,
            Часы = Math.Round(c.TotalHours, 2),
            Потрачено = c.TotalSpent,
            Скидка = c.DiscountPercent
        }).ToList();
        if (gridClients.Columns["Id"] != null)
        {
            gridClients.Columns["Id"]!.Visible = false;
        }
    }

    private void OnSearchTextChanged(object? sender, EventArgs e)
    {
        LoadClients();
    }

    private void OnAddClick(object? sender, EventArgs e)
    {
        if (_clientManager == null)
        {
            ShowMessage("Форма открыта в режиме конструктора.", false);
            return;
        }

        using var dialog = new ClientEditDialog();
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }
        var client = new Client { FullName = dialog.ClientFullName, Phone = dialog.ClientPhone };
        if (_clientManager.AddClient(client))
        {
            LoadClients();
            ClientsChanged?.Invoke(this, EventArgs.Empty);
        }
        ShowMessage(_clientManager.LastMessage, _clientManager.LastMessage.Contains("добавлен"));
    }

    private void OnEditClick(object? sender, EventArgs e)
    {
        if (_clientManager == null)
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
        using var dialog = new ClientEditDialog(client.FullName, client.Phone);
        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }
        client.FullName = dialog.ClientFullName;
        client.Phone = dialog.ClientPhone;
        if (_clientManager.EditClient(client))
        {
            LoadClients();
            ClientsChanged?.Invoke(this, EventArgs.Empty);
        }
        ShowMessage(_clientManager.LastMessage, _clientManager.LastMessage.Contains("изменены"));
    }

    private void OnDeleteClick(object? sender, EventArgs e)
    {
        if (_clientManager == null)
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
        if (MessageBox.Show("Удалить выбранного клиента?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }
        if (_clientManager.DeleteClient(client.Id))
        {
            LoadClients();
            ClientsChanged?.Invoke(this, EventArgs.Empty);
        }
        ShowMessage(_clientManager.LastMessage, _clientManager.LastMessage.Contains("удалён"));
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

    private void ShowMessage(string message, bool success)
    {
        lblMessage.ForeColor = success ? Color.DarkGreen : Color.DarkRed;
        lblMessage.Text = message;
    }
}
