using System;
using System.Threading.Tasks;
using CoreTweet;
using System.Collections.ObjectModel;

namespace TwitterSample
{
    public sealed class Model
    {
        // Singleton instance.
        private static readonly Model _Model = new Model();

        public static Model Instance
        {
            get { return _Model; }
        }

        private Model() { }

        // Twitter API tokens. ここから入手可(https://apps.twitter.com)
        public readonly Tokens tokens = Tokens.Create("API key",
                                               "API secret",
                                               "Access token",
                                               "Access token secret");

        public string TweetText;
        public int WordCount;
        public bool IsBusy;

        public ObservableCollection<Data> TimeLine = new ObservableCollection<Data>();
        public class Data
        {
            public string ScreenName { get; set; }
            public string Name { get; set; }
            public string Tweet { get; set; }
            public string CreatedAt { get; set; }
            public string Icon { get; set; }
        }

        async public Task TweetAsync(string TweetText)
        {
            await tokens.Statuses.UpdateAsync(status => TweetText);
        }

        async public Task FetchTimeLine()
        {
            try
            {
                var status = await tokens.Statuses.HomeTimelineAsync(count => 50);

                TimeLine.Clear();
                foreach (var tweet in status)
                {
                    TimeLine.Add(new Data
                    {
                        ScreenName = tweet.User.ScreenName,
                        Name = tweet.User.Name,
                        Tweet = tweet.Text,
                        CreatedAt = tweet.CreatedAt.AddHours(9).ToString("f"),
                        Icon = tweet.User.ProfileImageUrl
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}