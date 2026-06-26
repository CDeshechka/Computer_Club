namespace ComputerClubWinForms.Forms;

public partial class ExtendSessionDialog : Form
{
    public int Minutes => (int)numMinutes.Value;

    public ExtendSessionDialog()
    {
        InitializeComponent();
    }

    public ExtendSessionDialog(string title, int defaultMinutes) : this()
    {
        Text = title;
        numMinutes.Value = Math.Max(numMinutes.Minimum, Math.Min(numMinutes.Maximum, defaultMinutes));
    }

    private void OnOkClick(object? sender, EventArgs e)
    {
        if (Minutes <= 0)
        {
            lblMessage.Text = "Введите положительное число минут.";
            return;
        }
        DialogResult = DialogResult.OK;
        Close();
    }
}
