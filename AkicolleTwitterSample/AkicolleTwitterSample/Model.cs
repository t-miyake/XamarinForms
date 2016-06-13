using System;
using System.Threading.Tasks;
using CoreTweet;

namespace AkicolleTwitterSample
{
    public class Model
    {
        // Twitter API tokens. ここから入手可(https://apps.twitter.com)
        public readonly Tokens tokens = Tokens.Create("API key","API secret","Access token","Access token secret");

        async public Task TweetAsync(string TweetText)
        {
            TweetText = $"{TweetText} {DateTime.Now.ToString()}";
            await tokens.Statuses.UpdateAsync(status => TweetText);
        }
    }
}