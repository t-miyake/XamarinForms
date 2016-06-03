using System.Windows.Input;
using Xamarin.Forms;

namespace TwitterSample
{
    public class FixedPhrasePageViewModel : ViewModelBase
    {
        public ICommand SetFixedPhrase { get; set; }

        Model Model = Model.Instance;

        public FixedPhrasePageViewModel()
        {
            SetFixedPhrase = new Command<string>((arg) =>
            {
                Model.TweetText = arg;
                Model.WordCount = Model.TweetText.Length;
                Application.Current.MainPage.Navigation.PopAsync();
            });
        }
    }
}

