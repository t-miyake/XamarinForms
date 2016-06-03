using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TwitterSample
{
    public class MainPageViewModel : ViewModelBase
    {
        Model Model = Model.Instance;

        public ICommand FetchTimeLine { get; set; }

        public MainPageViewModel()
        {
            FetchTimeLine = new Command(async () =>
            {
                //ListViewのIsRefreshingプロパティを手動で更新。
                //本来、自動で更新されるため不要だが、自動にすると更新されない場合がある為追加。(原因不明)
                IsBusy = true;
                try
                {
                    await Model.FetchTimeLine();
                }
                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert("エラー", e.ToString(), "OK");
                }
                IsBusy = false;
            });
        }

        async public Task Initialize()
        {
            try
            {
                await Model.FetchTimeLine();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("エラー", e.ToString(), "OK");
            }
        }

        public ObservableCollection<Model.Data> TimeLine
        {
            get
            {
                return Model.TimeLine;
            }
            set
            {
                Model.TimeLine = value;
            }
        }

        public bool IsBusy
        {
            get { return Model.IsBusy; }
            set
            {
                Model.IsBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
    }
}