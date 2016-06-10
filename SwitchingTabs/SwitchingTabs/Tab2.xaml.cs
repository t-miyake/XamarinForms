using Xamarin.Forms;

namespace SwitchingTabs
{
    public partial class Tab2 : ContentPage
    {
        public Tab2()
        {
            InitializeComponent();
            BindingContext = new Tab2ViewModel();
        }
    }
}

