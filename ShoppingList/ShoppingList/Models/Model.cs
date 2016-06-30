using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace ShoppingList
{
    public class Model
    {
        // Singleton instance.
        private static readonly Model _Model = new Model();
        public static Model Instance
        {
            get
            {
                return _Model;
            }
        }
        private Model() { }

        // Insert your application URL.
        // Sample "http://miyakeshoppinglist.azurewebsites.net"
        const string ApplicationUrl = "ApplicationUrl";
        readonly MobileServiceClient client = new MobileServiceClient(ApplicationUrl);


        public ObservableCollection<ShoppingItems> ShoppingList = new ObservableCollection<ShoppingItems>();
        public ObservableCollection<ShoppingItems> BoughtList = new ObservableCollection<ShoppingItems>();


        public async void FetchShoppingList()
        {
            var FetchedItems = await FetchItemes(false);
            ShoppingList.Clear();
            foreach (var item in FetchedItems)
            {
                ShoppingList.Add(item);
            }
        }

        public async void FetchBoughtList()
        {
            var FetchedItems = await FetchItemes(true);
            BoughtList.Clear();
            foreach (var item in FetchedItems)
            {
                BoughtList.Add(item);
            }
        }

        public async Task<List<ShoppingItems>> FetchItemes(bool bought)
        {
            var FetchedItems = await client.GetTable<ShoppingItems>().Where(item => item.Bought == bought).ToListAsync();
            FetchedItems.Reverse();
            return FetchedItems;
        }

        public async void ItemAdd(Entry item)
        {
            if (!string.IsNullOrEmpty(item.Text))
            {
                var newItem = new ShoppingItems { Bought = false, Item = item.Text };
                await client.GetTable<ShoppingItems>().InsertAsync(newItem);
                ShoppingList.Insert(0, newItem);
            }
        }

        public async void BoughtItem(ShoppingItems item)
        {
            item.Bought = true;
            await client.GetTable<ShoppingItems>().UpdateAsync(item);
            ShoppingList.Remove(item);
        }
    }
}
