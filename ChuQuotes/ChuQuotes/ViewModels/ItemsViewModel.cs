using ChuQuotes.Models;
using ChuQuotes.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ChuQuotes.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Quote> Items            { get; set; }
        public Command                     LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title            = "View Favorites";
            Items            = new ObservableCollection<Quote>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<RandomPage, Quote>(this, "AddQuote", async (obj, item) =>
            {
                if (Items.Any(i => i.Id == item.Id)) return;

                var newItem = item;
                newItem.IsSelected = false;

                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        public async Task RemoveSelectedItems()
        {
            if (IsBusy) return;

            var toDelete = Items.Where(i => i.IsSelected).ToList();
            foreach (var quote in toDelete)
                await DataStore.DeleteItemAsync(quote.Id);
            
            Items.Clear();
            (await DataStore.GetItemsAsync()).ForEach(i => Items.Add(i));
        }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                (await DataStore.GetItemsAsync()).ForEach(i => Items.Add(i));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}