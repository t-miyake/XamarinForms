using System.Windows.Input;
using Xamarin.Forms;

namespace AkicolleTwitterSample
{
    public class ViewModel
    {
        readonly Model Model = new Model();

        public ICommand Tweet { get; set; }

        public ViewModel()
        {
            Tweet = new Command<string>(async (TweetText) =>
            {
                var accepted = await Application.Current.MainPage.DisplayAlert("確認", "ツイートしますか？", "OK", "Cancel");
                if (accepted)
                {
                    await Model.TweetAsync(TweetText);
                    await Application.Current.MainPage.DisplayAlert("(｀・ω・´)", "ツイートしました！", "OK");
                }
            });
        }
    }
}