using System.Collections.ObjectModel;

namespace ShoppingList
{
    public class BoughtPageViewModel
    {
        Model Model = Model.Instance;

        public void Initialize()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                Model.FetchBoughtList();
            }
        }

        public ObservableCollection<ShoppingItems> BoughtList
        {
            get
            {
                return Model.BoughtList;
            }
            set
            {
                Model.BoughtList = value;
            }
        }
    }
}