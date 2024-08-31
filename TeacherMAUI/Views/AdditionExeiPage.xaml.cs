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
                DateTime currentDate = DateTime.Now;

                await App.Database.SaveExeiAsync(new Exei
                {
                    Tmima = tmimaEntry.Text,
                    Lesson = lessonEntry.Text,
                    //Starts = new DateTime(1, 1, 1, timeStarts.Hours, timeStarts.Minutes, timeStarts.Seconds),
                    //Ends = new DateTime(1, 1, 1, timeEnds.Hours, timeEnds.Minutes, timeEnds.Seconds),
                    Starts = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, timeStarts.Hours, timeStarts.Minutes, timeStarts.Seconds),
                    Ends = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, timeEnds.Hours, timeEnds.Minutes, timeEnds.Seconds),
                    Day = selectedDay
       
                });


            }

        }


    }

}
