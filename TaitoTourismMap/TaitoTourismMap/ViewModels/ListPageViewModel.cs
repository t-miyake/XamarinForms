using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FormsPlugin.Iconize;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TaitoTourismMap
{
    public class ListPageViewModel : ViewModelBase
    {
        public ICommand InfoButton { get; set; }
        public ICommand ItemTapped { get; set; }

        Model Model = Model.Instance;

        public ListPageViewModel()
        {
            InfoButton = new Command(() => Application.Current.MainPage.Navigation.PushAsync(new InfoPage()));

            ItemTapped = new Command((itemTappedEventArgs) =>
            {
                var tappedItem = (List<ItemTappedEventArgsData>)itemTappedEventArgs;

                Model.MapRegion = MapSpan.FromCenterAndRadius(new Position(tappedItem[0].EventArgs.Latitude, tappedItem[0].EventArgs.Longitude), Distance.FromMiles(0.3));

                Model.CustomPins.Add(new TKCustomMapPin
                {
                    IsVisible = true,
                    Title = tappedItem[0].EventArgs.Name,
                    Subtitle = tappedItem[0].EventArgs.Description,
                    Position = new Position(tappedItem[0].EventArgs.Latitude, tappedItem[0].EventArgs.Longitude),
                    ShowCallout = true,
                    DefaultPinColor = Color.Blue
                });

                var page = tappedItem[0].PageName as ListPage;
                var parentPage = page.Parent as IconTabbedPage;
                parentPage.CurrentPage = parentPage.Children[0];

            });
        }

        public void Initialize()
        {
            TouristSpot = Model.TouristSpot;
        }

        public ObservableCollection<ItemProperty> TouristSpot
        {
            get
            {
                return Model.TouristSpot;
            }
            set
            {
                Model.TouristSpot = value;
            }
        }
    }
}