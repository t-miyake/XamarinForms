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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var ViewModel = BindingContext as BoughtPageViewModel;
            ViewModel.Initialize();
        }
    }
}