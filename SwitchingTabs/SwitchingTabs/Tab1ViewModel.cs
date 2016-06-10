using System.Windows.Input;
using Xamarin.Forms;

namespace SwitchingTabs
{
    public class Tab1ViewModel
    {
        public ICommand GoToTab2 { get; set; } 

        public Tab1ViewModel()
        {
            GoToTab2 = new Command((PageName) =>
            {
                var Page = PageName as Tab1;
                var ParentPage = Page.Parent as TabbedPage;
                ParentPage.CurrentPage = ParentPage.Children[1];
            });
        }
    }
}