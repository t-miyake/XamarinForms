using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Google.MobileAds;
using UIKit;
using CoreGraphics;
using AddAdMobBannerSample;
using AddAdMobBannerSample.iOS.Renderers;

[assembly: ExportRenderer(typeof(AdMobBanner), typeof(AdMobBannerRenderer))]
namespace AddAdMobBannerSample.iOS.Renderers
{
    public class AdMobBannerRenderer : ViewRenderer
    {
        const string adUnitID = "Insert Your AdUnitID(for iOS)";

        BannerView adMobBanner;
        bool viewOnScreen;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {
                adMobBanner = new BannerView(AdSizeCons.Banner, new CGPoint(-10, 0))
                {
                    AdUnitID = adUnitID,
                    RootViewController = UIApplication.SharedApplication.Windows[0].RootViewController
                };
                adMobBanner.AdReceived += (sender, args) =>
                {
                    if (!viewOnScreen) AddSubview(adMobBanner);
                    viewOnScreen = true;
                };
                adMobBanner.LoadRequest(Request.GetDefaultRequest());
                SetNativeControl(adMobBanner);
            }
        }
    }
}