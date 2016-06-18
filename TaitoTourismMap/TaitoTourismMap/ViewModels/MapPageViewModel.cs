using System.Collections.ObjectModel;
using System.Windows.Input;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TaitoTourismMap
{
    public class MapPageViewModel : ViewModelBase
    {
        public ICommand InfoButton { get; set; }

        Model Model = Model.Instance;

        public MapPageViewModel()
        {
            InfoButton = new Command(() => Application.Current.MainPage.Navigation.PushAsync(new InfoPage()));
        }

        public void Initialize()
        {
            if (Model.TouristSpot.Count == 0)
            {
                Model.FetchTouristSpotData("動・植物園");
                Model.FetchTouristSpotData("博物館");
                Model.FetchTouristSpotData("美術館");
                Model.FetchTouristSpotData("テーマパーク・レジャーランド");
                Model.FetchTouristSpotData("公園");
                Model.FetchTouristSpotData("庭園");
                Model.FetchTouristSpotData("近代的建造物");
                Model.FetchTouristSpotData("歴史的建造物");
                Model.FetchTouristSpotData("神社・仏閣等");
                Model.FetchTouristSpotData("史跡");
                IsBusy = false;
            }
            else
            {
                //2回目以降はデータを取得しない。
            }

            CustomPins = Model.CustomPins;
            MapRegion = Model.MapRegion;
        }

        //For Anti Rome hack.
        private int Counter = 1;
        private MapSpan DefaultMapSapn = MapSpan.FromCenterAndRadius(new Position(35.7089183, 139.7742183), Distance.FromMiles(1));

        public MapSpan MapRegion
        {
            get
            {
                return Model.MapRegion;
            }
            set
            {
                //Anti Rome hack.
                Counter++;
                if (Counter <= 10) { value = DefaultMapSapn; }

                Model.MapRegion = value;
                OnPropertyChanged("MapRegion");
            }
        }

        public ObservableCollection<TKCustomMapPin> CustomPins
        {
            get
            {
                return Model.CustomPins;
            }
            set
            {
                Model.CustomPins = value;
            }
        }

        //ページの表示時にすぐにIsBusy状態にするため、初期値をtrue.
        private bool _IsBusy = true;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                _IsBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
    }
}