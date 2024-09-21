using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeacherMAUI.Models;

namespace TeacherMAUI.ViewModels
{
    public class TestTabViewModel : BindableObject
    {
        private ObservableCollection<GroupedScheduleItems> _groupedScheduleItems;
        public ObservableCollection<GroupedScheduleItems> GroupedScheduleItems
        {
            get => _groupedScheduleItems;
            set
            {
                _groupedScheduleItems = value;
                OnPropertyChanged();
            }
        }

        public ICommand EditItemCommand { get; private set; }
        public ICommand DeleteItemCommand { get; private set; }

        public TestTabViewModel()
        {
            EditItemCommand = new Command<CombinedScheduleItem>(EditItem);
            DeleteItemCommand = new Command<CombinedScheduleItem>(DeleteItem);
        }

        public async Task LoadScheduleItems()
        {
            var efhmeriaItems = await App.Database.GetEfhmeriasAsync();
            var exeiItems = await App.Database.GetExeisAsync();

            var orderedDays = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            var combinedItems = new List<CombinedScheduleItem>();

            combinedItems.AddRange(efhmeriaItems.Select(e => new CombinedScheduleItem
            {
                Day = e.Day,
                Starts = e.Starts,
                Ends = e.Ends,
                Type = "Efhmeria",
                Details = e.Location,
                OriginalItem = e
            }));

            combinedItems.AddRange(exeiItems.Select(e => new CombinedScheduleItem
            {
                Day = e.Day,
                Starts = e.Starts,
                Ends = e.Ends,
                Type = "Exei",
                Details = $"{e.Lesson} - {e.Tmima}",
                OriginalItem = e
            }));

            var groupedItems = combinedItems
                .GroupBy(item => item.Day)
                .Select(g => new GroupedScheduleItems(g.Key, g.OrderBy(item => item.Starts).ToList()))
                .OrderBy(g => Array.IndexOf(orderedDays, g.DayOfWeek))
                .ToList();

            GroupedScheduleItems = new ObservableCollection<GroupedScheduleItems>(groupedItems);

            // Approach 2 : time was sorting won't be there
            //var groupedItems = new ObservableCollection<GroupedScheduleItems>();

            //foreach (var day in orderedDays)
            //{
            //    var dayItems = new List<CombinedScheduleItem>();

            //    dayItems.AddRange(efhmeriaItems
            //        .Where(e => e.Day == day)
            //        .Select(e => new CombinedScheduleItem
            //        {
            //            Day = e.Day,
            //            Starts = e.Starts,
            //            Ends = e.Ends,
            //            Type = "Efhmeria",
            //            Details = e.Location,
            //            OriginalItem = e
            //        }));

            //    dayItems.AddRange(exeiItems
            //        .Where(e => e.Day == day)
            //        .Select(e => new CombinedScheduleItem
            //        {
            //            Day = e.Day,
            //            Starts = e.Starts,
            //            Ends = e.Ends,
            //            Type = "Exei",
            //            Details = $"{e.Lesson} - {e.Tmima}",
            //            OriginalItem = e
            //        }));

            //    if (dayItems.Any())
            //    {
            //        groupedItems.Add(new GroupedScheduleItems(day, dayItems.OrderBy(i => i.Starts).ToList()));
            //    }
            //}

            //GroupedScheduleItems = groupedItems;
        }

        //private async void EditItem(CombinedScheduleItem item)
        //{
        //    // Implement edit logic here
        //    // For example, navigate to an edit page or show an edit popup
        //    await Application.Current.MainPage.DisplayAlert("Edit", $"Editing {item.Type} item", "OK");
        //}
        private async void EditItem(CombinedScheduleItem item)
        {
            Page editPage;
            if (item.Type == "Efhmeria")
            {
                editPage = new EfhmeriaEditPage((Efhmeria)item.OriginalItem);
            }
            else // Exei
            {
                editPage = new ExeiEditPage((Exei)item.OriginalItem);
            }

            await Application.Current.MainPage.Navigation.PushAsync(editPage);

            // Refresh the list after returning from the edit page
            await LoadScheduleItems();
        }

        private async void DeleteItem(CombinedScheduleItem item)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", $"Are you sure you want to delete this {item.Type} item?", "Yes", "No");
            if (confirm)
            {
                try
                {
                    // Delete from database based on type
                    if (item.Type == "Efhmeria")
                    {
                        await App.Database.DeleteEfhmeriaAsync((Efhmeria)item.OriginalItem);
                    }
                    else if (item.Type == "Exei")
                    {
                        await App.Database.DeleteExeiAsync((Exei)item.OriginalItem);
                    }

                    // Refresh the list after successful deletion
                    await LoadScheduleItems();

                    await Application.Current.MainPage.DisplayAlert("Delete", $"{item.Type} item deleted", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to delete item: {ex.Message}", "OK");
                }

                // Approach 2
                //// Remove the item from the UI
                //foreach (var group in GroupedScheduleItems)
                //{
                //    if (group.Remove(item))
                //    {
                //        // If the group is now empty, remove it
                //        if (group.Count == 0)
                //        {
                //            GroupedScheduleItems.Remove(group);
                //        }
                //        break;
                //    }
                //}
                //// Refresh the list after successful deletion
                //await LoadScheduleItems();

                //// Delete from database based on type
                //if (item.Type == "Efhmeria")
                //{
                //    await App.Database.DeleteEfhmeriaAsync((Efhmeria)item.OriginalItem);
                //}
                //else if (item.Type == "Exei")
                //{
                //    await App.Database.DeleteExeiAsync((Exei)item.OriginalItem);
                //}

                //await Application.Current.MainPage.DisplayAlert("Delete", $"{item.Type} item deleted", "OK");

            }
        }
    }
}
