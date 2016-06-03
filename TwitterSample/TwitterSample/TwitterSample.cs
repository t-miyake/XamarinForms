using FormsPlugin.Iconize;
using Xamarin.Forms;

namespace TwitterSample
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application

            //IconizeライブラリのIconTabbedPageを使用する為、TabbedPageの生成のみXAMLではなくC#で記載。
            var TabbedPage = new IconTabbedPage { Title = "Twitter" };

            foreach (var module in Plugin.Iconize.Iconize.Modules)
            {
                TabbedPage.Children.Add(new MainPage
                {
                    BindingContext = new MainPageViewModel(),
                    Icon = "fa-home"
                });

                TabbedPage.Children.Add(new TweetPage
                {
                    BindingContext = new TweetPageViewModel(),
                    Icon = "fa-twitter"
                });
            }

            MainPage = new IconNavigationPage(TabbedPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

