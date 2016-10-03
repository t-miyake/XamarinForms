using System;

using Xamarin.Forms;

namespace AddAdMobBannerSample
{
    public class App : Application
    {
        public App()
        {
           
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            MainPage = new NavigationPage(new MainPage());
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
