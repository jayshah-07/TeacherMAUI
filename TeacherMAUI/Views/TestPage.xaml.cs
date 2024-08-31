using System.Windows.Input;
using TeacherMAUI.Models;

namespace TeacherMAUI
{
    public partial class TestPage : ContentPage
    {
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public TestPage()
        {
            InitializeComponent();
            EditCommand = new Command<Exei>(OnEditExei);
            DeleteCommand = new Command<Exei>(OnDeleteExei);
            BindingContext = this;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshExeiList();
            // ExeiListView.ItemsSource = await App.Database.GetExeisAsync();
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    await RefreshExeiList();
        //}

        private async Task RefreshExeiList()
        {
            ExeiListView.ItemsSource = await App.Database.GetExeisAsync();
        }

        private async void OnRefreshing(object sender, EventArgs e)
        {
            await RefreshExeiList();
            ExeiRefreshView.IsRefreshing = false;
        }

        //private async void OnAddClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new ExeiEditPage(new Exei()));
        //}

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExeiEditPage());
        }

        private async void OnEditExei(Exei exei)
        {
            await Navigation.PushAsync(new ExeiEditPage(exei));
        }

        private async void OnDeleteExei(Exei exei)
        {
            bool answer = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete the Exei for {exei.Day}?", "Yes", "No");
            if (answer)
            {
                await App.Database.DeleteExeiAsync(exei);
                await RefreshExeiList();
            }
        }

    }

}
