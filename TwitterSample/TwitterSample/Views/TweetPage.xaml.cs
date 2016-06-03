using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TwitterSample
{
    public partial class TweetPage : ContentPage
    {
        public TweetPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var ViewModel = BindingContext as TweetPageViewModel;
            ViewModel.Initialize();
        }
    }
}