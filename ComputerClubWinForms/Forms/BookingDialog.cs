namespace ComputerClubWinForms.Forms;

public partial class BookingDialog : Form
{
    public DateTime PlannedStart => dateStart.Value.Date.Add(timeStart.Value.TimeOfDay);
    public int DurationMinutes => (int)numDuration.Value;
    public int ComputerNumber => (int)numComputer.Value;

    public BookingDialog()
    {
        InitializeComponent();
        SetDefaultValues(1);
    }

    public BookingDialog(int selectedComputerNumber)
    {
        InitializeComponent();
        SetDefaultValues(selectedComputerNumber);
    }

    private void SetDefaultValues(int selectedComputerNumber)
    {
        dateStart.Value = DateTime.Now.Date;
        timeStart.Value = DateTime.Now.AddMinutes(30);
        numComputer.Value = Math.Max(numComputer.Minimum, Math.Min(numComputer.Maximum, selectedComputerNumber));
    }

    private void OnOkClick(object? sender, EventArgs e)
    {
        if (DurationMinutes <= 0)
        {
            lblMessage.Text = "Длительность должна быть больше нуля.";
            return;
        }
        DialogResult = DialogResult.OK;
        Close();
    }
}
