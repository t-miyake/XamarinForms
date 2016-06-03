using Xamarin.Forms;

namespace TwitterSample
{
    public partial class FixedPhrasePage : ContentPage
    {
        public FixedPhrasePage()
        {
            InitializeComponent();

            BindingContext = new FixedPhrasePageViewModel();
        }
    }
}