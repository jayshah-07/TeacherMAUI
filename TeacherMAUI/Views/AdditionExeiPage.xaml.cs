using TeacherMAUI.Models;
using TeacherMAUI.Services;


namespace TeacherMAUI
{
    public partial class AdditionExeiPage : ContentPage
    {
          public AdditionExeiPage()
        {
            InitializeComponent();
        }

        async void OnLessonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tmimaEntry.Text) && !(startchaperonEntry == null) && !(endchaperonEntry == null) && !(dayEntry == null))
            {
                TimeSpan timeEnds = endchaperonEntry.Time;
                TimeSpan timeStarts = startchaperonEntry.Time;
                string selectedDay = (string)dayEntry.SelectedItem;

                // Get the current date
                DateTime defaultDate = new DateTime(); // returns 1/1/0001 12:00:00 AM

                await App.Database.SaveExeiAsync(new Exei
                {
                    Tmima = tmimaEntry.Text,
                    Lesson = lessonEntry.Text,
                    //Starts = new DateTime(1, 1, 1, timeStarts.Hours, timeStarts.Minutes, timeStarts.Seconds),
                    //Ends = new DateTime(1, 1, 1, timeEnds.Hours, timeEnds.Minutes, timeEnds.Seconds),
                    Starts = new DateTime(defaultDate.Year, defaultDate.Month, defaultDate.Day, timeStarts.Hours, timeStarts.Minutes, timeStarts.Seconds),
                    Ends = new DateTime(defaultDate.Year, defaultDate.Month, defaultDate.Day, timeEnds.Hours, timeEnds.Minutes, timeEnds.Seconds),
                    Day = selectedDay
                });

                // After successful submission, reset the form
                ResetForm();

                // Display alert
                await DisplayAlert("Success", "Lesson saved", "OK");
            }
        }

        private void ResetForm()
        {
            // Clear the location entry
            lessonEntry.Text = string.Empty;
            tmimaEntry.Text = string.Empty;

            // Reset the day picker
            dayEntry.SelectedIndex = -1;

            // Reset the time pickers
            startchaperonEntry.Time = new TimeSpan(0, 0, 0);
            endchaperonEntry.Time = new TimeSpan(0, 0, 0);

            // Reset scroll position
            ExeiformScrollView.ScrollToAsync(0, 0, true);
        }
    }

}
