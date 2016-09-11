using Xamarin.Forms;

namespace ShoppingList
{
    public partial class BoughtPage : ContentPage
    {
        public BoughtPage()
        {
            InitializeComponent();
            BindingContext = new BoughtPageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var ViewModel = BindingContext as BoughtPageViewModel;
            await ViewModel.Initialize();
        }
    }
}