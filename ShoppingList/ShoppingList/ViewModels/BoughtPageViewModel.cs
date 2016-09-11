using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ShoppingList
{
    public class BoughtPageViewModel
    {
        Model Model = Model.Instance;

        public async Task Initialize()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                await Model.FetchBoughtList();
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