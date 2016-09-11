using Xamarin.Forms;

namespace ShoppingList
{
    public partial class BuyingPage : ContentPage
    {
        public BuyingPage()
        {
            InitializeComponent();
            BindingContext = new BuyingPageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var ViewModel = BindingContext as BuyingPageViewModel;
            await ViewModel.Initialize();
        }
    }
}