using System;
using Android.Widget;
using Connect.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(DropShadowEffect), "DropShadowEffect")]
namespace Connect.Droid.Effects
{
    public class DropShadowEffect : PlatformEffect
    {
        public DropShadowEffect()
        {
        }

        protected override void OnAttached()
        {
            //Container
        }

        protected override void OnDetached()
        {
            
        }
    }
}