using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShoppingList
{
    public class BuyingPageViewModel : ViewModelBase
    {
        public ICommand AddButton { get; set; }
        public ICommand ItemTapped { get; set; }

        Model Model = Model.Instance;

        public BuyingPageViewModel()
        {
            AddButton = new Command((EventArgs) =>
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    Model.ItemAdd((Entry)EventArgs);
                    EntryText = "";
                }
            });

            ItemTapped = new Command(async (EventArgs) =>
            {
                if (await Application.Current.MainPage.DisplayAlert("確認", "買いましたか？", "買った！", "まだ！"))
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        Model.BoughtItem(EventArgs as ShoppingItems);
                    }
                }
            });
        }

        public void Initialize()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                Model.FetchShoppingList();
            }
        }

        public ObservableCollection<ShoppingItems> ShoppingList
        {
            get
            {
                return Model.ShoppingList;
            }
            set
            {
                Model.ShoppingList = value;
            }
        }

        private string _EntryText;
        public string EntryText
        {
            get
            {
                return _EntryText;
            }
            set
            {
                _EntryText = value;
                OnPropertyChanged("EntryText");
            }
        }
    }
}