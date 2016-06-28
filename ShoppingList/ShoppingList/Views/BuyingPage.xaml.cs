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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var ViewModel = BindingContext as BuyingPageViewModel;
            ViewModel.Initialize();
        }
    }
}