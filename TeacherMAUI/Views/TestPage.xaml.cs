using System.Windows.Input;
using TeacherMAUI.Models;
using TeacherMAUI.ViewModels;

namespace TeacherMAUI
{
    public partial class TestPage : ContentPage
    {
        private TestTabViewModel _viewModel;

        public TestPage()
        {
            InitializeComponent();
            _viewModel = new TestTabViewModel();
            BindingContext = _viewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadScheduleItems();
        }
    }

}
