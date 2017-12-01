using System;
using Connect.iOS.Effects;
using UIKit;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(DropShadowEffect), "DropShadowEffect")]
namespace Connect.iOS.Effects
{
    public class DropShadowEffect : PlatformEffect
    {
        public DropShadowEffect()
        {
        }

        protected override void OnAttached()
        {
			//Container.Layer.MasksToBounds = false;
			Container.Layer.ShadowOffset = new CGSize(2, 2);
			Container.Layer.ShadowRadius = 2;
			Container.Layer.ShadowOpacity = 0.5f;
        }

        protected override void OnDetached()
        {
            
        }
    }
}
