using Xamarin.Forms;

namespace AkicolleTwitterSample
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new MainPage());
        }
    }
}