using TeacherMAUI.Models;

namespace TeacherMAUI;

public partial class EfhmeriaEditPage : ContentPage
{
    private Efhmeria _efhmeria;

    public EfhmeriaEditPage(Efhmeria efhmeria)
	{
        InitializeComponent();
        _efhmeria = efhmeria;

        // Initialize pickers and entries with existing data
        //StartDatePicker.Date = efhmeria.Starts.Date;
        StartTimePicker.Time = efhmeria.Starts.TimeOfDay;
        //EndDatePicker.Date = efhmeria.Ends.Date;
        EndTimePicker.Time = efhmeria.Ends.TimeOfDay;

        DayPicker.ItemsSource = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        DayPicker.SelectedItem = efhmeria.Day;

        LocationEntry.Text = efhmeria.Location;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Update the Efhmeria object with new values
        DateTime defaultDate = new DateTime(); // returns 1/1/0001 12:00:00 AM
        _efhmeria.Starts = defaultDate.Date + StartTimePicker.Time;
        _efhmeria.Ends = defaultDate.Date + EndTimePicker.Time;
        _efhmeria.Day = DayPicker.SelectedItem as string;
        _efhmeria.Location = LocationEntry.Text;

        // Save changes to the database
        await App.Database.SaveEfhmeriaAsync(_efhmeria);

        // Navigate back to the previous page
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Navigate back to the previous page without saving changes
        await Navigation.PopAsync();
    }
}