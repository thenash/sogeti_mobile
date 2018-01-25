using Connect.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(BorderlessViewEffect), "BorderlessViewEffect")]
namespace Connect.iOS.Effects {

    public class BorderlessViewEffect : PlatformEffect {

        public BorderlessViewEffect() { }

        protected override void OnAttached() {

            if(Control is UITextField textView) {
                textView.Layer.BorderWidth = 0;
                textView.BorderStyle       = UITextBorderStyle.None;
            }
        }

        protected override void OnDetached() { }
    }
}
