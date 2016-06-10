using Xamarin.Forms;

namespace SwitchingTabs
{
    public partial class Tab1 : ContentPage
    {
        public Tab1()
        {
            InitializeComponent();
            BindingContext = new Tab1ViewModel();
        }
    }
}