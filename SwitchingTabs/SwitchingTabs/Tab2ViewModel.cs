using System.Windows.Input;
using Xamarin.Forms;

namespace SwitchingTabs
{
    public class Tab2ViewModel
    {
        public ICommand GoToTab1 { get; set; }

        public Tab2ViewModel()
        {
            GoToTab1 = new Command((PageName) =>
            {
                var Page = PageName as Tab2;
                var ParentPage = Page.Parent as TabbedPage;
                ParentPage.CurrentPage = ParentPage.Children[0];
            });
        }
    }
}