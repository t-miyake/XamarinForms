using Xamarin.Forms;

namespace TaitoTourismMap
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
            BindingContext = new ListPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var ViewModel = BindingContext as ListPageViewModel;
            ViewModel.Initialize();
        }
    }
}