namespace ComputerClubWinForms.Forms;

public partial class ComparePeriodDialog : Form
{
    public DateTime PeriodStart => dateStart.Value.Date;
    public DateTime PeriodEnd => dateEnd.Value.Date;

    public ComparePeriodDialog()
    {
        InitializeComponent();
        dateStart.Value = DateTime.Now.Date.AddDays(-14);
        dateEnd.Value = DateTime.Now.Date.AddDays(-8);
    }

    private void OnOkClick(object? sender, EventArgs e)
    {
        if (PeriodStart > PeriodEnd)
        {
            lblMessage.Text = "Начальная дата больше конечной.";
            return;
        }
        DialogResult = DialogResult.OK;
        Close();
    }
}
