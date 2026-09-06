using ComputerClubWinForms.Data;
using ComputerClubWinForms.Forms;

namespace ComputerClubWinForms;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        var databaseHelper = new DatabaseHelper();
        try
        {
            databaseHelper.Initialize();
            Application.Run(new AuthPage(databaseHelper));
        }
        catch (Exception ex)
        {
            MessageBox.Show("Не удалось подключиться к PostgreSQL. Проверьте, что сервер PostgreSQL установлен, запущен и параметры подключения указаны в Data\\connection.txt.\n\n" + ex.Message, "Ошибка подключения к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
//._.//