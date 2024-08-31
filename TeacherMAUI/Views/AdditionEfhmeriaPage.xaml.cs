using TeacherMAUI.Models;


namespace TeacherMAUI
{
    public partial class AdditionEfhmeriaPage : ContentPage
    {
    

        public AdditionEfhmeriaPage()
        {
            InitializeComponent();
        }

        async void OnChaperonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(locationEntry.Text) && !(startchaperonEntry == null) && !(endchaperonEntry == null) && !(dayEntry == null))
            {
                TimeSpan timeEnds = endchaperonEntry.Time;
                TimeSpan timeStarts = startchaperonEntry.Time;
                string selectedDay = (string)dayEntry.SelectedItem;

                // Get the current date
                DateTime currentDate = DateTime.Now;

                await App.Database.SaveEfhmeriaAsync(new Efhmeria
                {
                    Location = locationEntry.Text,
                    //Starts = new DateTime(1, 1, 1, timeStarts.Hours, timeStarts.Minutes, timeStarts.Seconds),
                    //Ends = new DateTime(1, 1, 1, timeEnds.Hours, timeEnds.Minutes, timeEnds.Seconds),
                    Starts = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, timeStarts.Hours, timeStarts.Minutes, timeStarts.Seconds),
                    Ends = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, timeEnds.Hours, timeEnds.Minutes, timeEnds.Seconds),
                    Day = selectedDay
                });
                await DisplayAlert("Success", "Chaperon saved", "OK");

            }

        }


    }

}
