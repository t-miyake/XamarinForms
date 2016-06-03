using Xamarin.Forms;

namespace TwitterSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var ViewModel = BindingContext as MainPageViewModel;
            await ViewModel.Initialize();
        }
    }
}