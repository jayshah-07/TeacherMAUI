using TeacherMAUI.Models;

namespace TeacherMAUI;

public partial class ExeiEditPage : ContentPage
{
    private Exei _exei;
    private List<string> _daysOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    public string PageTitle { get; private set; }

    //public ExeiEditPage(Exei exei)
    //{
    //    InitializeComponent();
    //    _exei = exei;



    //    // Initialize the date and time pickers
    //    if (_exei.Starts != default(DateTime))
    //    {
    //        //StartDatePicker.Date = currentDate;//_exei.Starts.Date;
    //        StartTimePicker.Time = _exei.Starts.TimeOfDay;
    //    }

    //    if (_exei.Ends != default(DateTime))
    //    {
    //        //EndDatePicker.Date = currentDate;// _exei.Ends.Date;
    //        EndTimePicker.Time = _exei.Ends.TimeOfDay;
    //    }

    //    BindingContext = _exei;
    //}

    public ExeiEditPage(Exei exei = null)
    {
        InitializeComponent();

        _exei = exei ?? new Exei();
        PageTitle = exei == null ? "Add Exei" : "Edit Exei";

        // Set up the DayPicker
        DayPicker.ItemsSource = _daysOfWeek;

        // Set the selected day if it exists
        if (!string.IsNullOrEmpty(_exei.Day))
        {
            int index = _daysOfWeek.IndexOf(_exei.Day);
            if (index != -1)
            {
                DayPicker.SelectedIndex = index;
            }
        }

        // Initialize time pickers
        if (_exei.Starts != default(DateTime))
        {
            StartTimePicker.Time = _exei.Starts.TimeOfDay;
        }

        if (_exei.Ends != default(DateTime))
        {
            EndTimePicker.Time = _exei.Ends.TimeOfDay;
        }

        BindingContext = _exei;
    }

    // only for edit mode (with out considering change of title)
    //public ExeiEditPage1(Exei exei)
    //{
    //    InitializeComponent();                                                                                                  
    //    _exei = exei;

    //    // Set up the DayPicker
    //    DayPicker.ItemsSource = _daysOfWeek;

    //    // Set the selected day if it exists
    //    if (!string.IsNullOrEmpty(_exei.Day))
    //    {
    //        int index = _daysOfWeek.IndexOf(_exei.Day);
    //        if (index != -1)
    //        {
    //            DayPicker.SelectedIndex = index;
    //        }                                                                                                       
    //    }

    //    // Initialize time pickers
    //    if (_exei.Starts != default(DateTime))
    //    {
    //        StartTimePicker.Time = _exei.Starts.TimeOfDay;
    //    }

    //    if (_exei.Ends != default(DateTime))
    //    {
    //        EndTimePicker.Time = _exei.Ends.TimeOfDay;
    //    }

    //    BindingContext = _exei;
    //}
    private void OnDayPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (DayPicker.SelectedIndex != -1)
        {
            _exei.Day = _daysOfWeek[DayPicker.SelectedIndex];
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (DayPicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(LessonEntry.Text))
        {
            await DisplayAlert("Error", "Day and Lesson are required fields", "OK");
            return;
        }

        // Update the Exei object with the entered values
        _exei.Day = _daysOfWeek[DayPicker.SelectedIndex];
        _exei.Lesson = LessonEntry.Text;
        _exei.Tmima = TmimaEntry.Text;

        //if (string.IsNullOrWhiteSpace(_exei.Day) || string.IsNullOrWhiteSpace(_exei.Lesson))
        //{
        //    await DisplayAlert("Error", "Day and Lesson are required fields", "OK");
        //    return;
        //}
        // Combine the selected day with the current date and time for Starts and Ends
        DateTime currentDate = DateTime.Now.Date;
        _exei.Starts = currentDate + StartTimePicker.Time;
        _exei.Ends = currentDate + EndTimePicker.Time;

        await App.Database.SaveExeiAsync(_exei);
        await Navigation.PopAsync();
    }

    //private async void OnSaveClicked(object sender, EventArgs e)
    //{
    //    // Combine the date and time for Starts and Ends
    //    //_exei.Starts = StartDatePicker.Date + StartTimePicker.Time;
    //    //_exei.Ends = EndDatePicker.Date + EndTimePicker.Time;

    //    // Get the current date
    //    DateTime currentDate = DateTime.Now;

    //    _exei.Starts = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, StartTimePicker.Time.Hours, StartTimePicker.Time.Minutes, StartTimePicker.Time.Seconds);
    //    _exei.Ends = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, EndTimePicker.Time.Hours, EndTimePicker.Time.Minutes, EndTimePicker.Time.Seconds); 

    //    await App.Database.SaveExeiAsync(_exei);
    //    await Navigation.PopAsync();
    //}
}