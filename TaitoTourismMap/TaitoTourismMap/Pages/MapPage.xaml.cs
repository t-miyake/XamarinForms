using Xamarin.Forms;

namespace TaitoTourismMap
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            BindingContext = new MapPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var ViewModel = BindingContext as MapPageViewModel;
            ViewModel.Initialize();
        }
    }
}