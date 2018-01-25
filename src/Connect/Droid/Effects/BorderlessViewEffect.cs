using Connect.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportEffect(typeof(BorderlessViewEffect), "BorderlessViewEffect")]
namespace Connect.Droid.Effects {

    public class BorderlessViewEffect : PlatformEffect {

        public BorderlessViewEffect() { }

        protected override void OnAttached() {
            View view = Control;
            view.Background = new Android.Graphics.Drawables.ColorDrawable(Android.Graphics.Color.Transparent);

        }

        protected override void OnDetached() { }
    }
}