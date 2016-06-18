using Foundation;
using TK.CustomMap.iOSUnified;
using UIKit;

namespace TaitoTourismMap.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

            FormsPlugin.Iconize.iOS.IconControls.Init();
            Xamarin.FormsMaps.Init();
            TKCustomMapRenderer.InitMapRenderer();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}

