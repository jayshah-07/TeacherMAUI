using TeacherMAUI.Models;

namespace TeacherMAUI;

public partial class ExeiEditPage : ContentPage
{
    private Exei _exei;

    public ExeiEditPage(Exei exei)
    {
        InitializeComponent();
        _exei = exei;

        // Initialize pickers and entries with existing data
        //StartDatePicker.Date = exei.Starts.Date;
        StartTimePicker.Time = exei.Starts.TimeOfDay;
        //EndDatePicker.Date = exei.Ends.Date;
        EndTimePicker.Time = exei.Ends.TimeOfDay;

        DayPicker.ItemsSource = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        DayPicker.SelectedItem = exei.Day;

        LessonEntry.Text = exei.Lesson;
        TmimaEntry.Text = exei.Tmima;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Update the Exei object with new values
        DateTime defaultDate = new DateTime(); // returns 1/1/0001 12:00:00 AM
        _exei.Starts = defaultDate.Date + StartTimePicker.Time;
        _exei.Ends = defaultDate.Date + EndTimePicker.Time;
        _exei.Day = DayPicker.SelectedItem as string;
        _exei.Lesson = LessonEntry.Text;
        _exei.Tmima = TmimaEntry.Text;

        // Save changes to the database
        await App.Database.SaveExeiAsync(_exei);

        // Navigate back to the previous page
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Navigate back to the previous page without saving changes
        await Navigation.PopAsync();
    }
}