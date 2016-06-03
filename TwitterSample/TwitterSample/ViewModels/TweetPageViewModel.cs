using System.Windows.Input;
using Xamarin.Forms;
namespace TwitterSample
{
	public class TweetPageViewModel : ViewModelBase
    {
        public ICommand CountWord { get; set; }
        public ICommand Tweet { get; set; }
        public ICommand GoToFixedPhrasePage { get; set; }
        public ICommand Reset { get; set; }

        Model Model = Model.Instance;

        public TweetPageViewModel()
        {
            CountWord = new Command(() => WordCount = TweetText.Length);

            Tweet = new Command(async () =>
            {
                var accepted = await Application.Current.MainPage.DisplayAlert("確認", "Tweetしますか？", "OK", "Cancel");
                if (accepted)
                {
                    await Model.TweetAsync(TweetText);
                    await Application.Current.MainPage.DisplayAlert("(｀・ω・´)", "Tweetしました！", "OK");
                    TweetText = "";
                }
            });

            GoToFixedPhrasePage = new Command(() => Application.Current.MainPage.Navigation.PushAsync(new FixedPhrasePage()));

            Reset = new Command(() =>
            {
                TweetText = "";
                WordCount = Model.WordCount;
            });
        }

        public void Initialize()
        {
            TweetText = Model.TweetText;
            WordCount = Model.WordCount;
        }

        public string TweetText
        {
            get { return Model.TweetText; }
            set
            {
                Model.TweetText = value;
                OnPropertyChanged("TweetText");
            }
        }

        public int WordCount
        {
            get { return Model.WordCount; }
            set
            {
                Model.WordCount = value;
                OnPropertyChanged("WordCount");
            }
        }
    }
}