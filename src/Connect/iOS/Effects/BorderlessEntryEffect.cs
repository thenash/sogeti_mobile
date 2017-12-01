using System;
using Connect.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(BorderlessEntryEffect), "BorderlessEntryEffect")]
namespace Connect.iOS.Effects
{
    public class BorderlessEntryEffect : PlatformEffect
    {
        public BorderlessEntryEffect()
        {
        }

        protected override void OnAttached()
        {
            var textView = (UITextField)Control;
            textView.BorderStyle = UITextBorderStyle.None;
        }

        protected override void OnDetached()
        {
            
        }
    }
}
