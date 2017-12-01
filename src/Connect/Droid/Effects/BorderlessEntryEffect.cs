using System;
using Android.Widget;
using Connect.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(BorderlessEntryEffect), "BorderlessEntryEffect")]
namespace Connect.Droid.Effects
{
	public class BorderlessEntryEffect : PlatformEffect
	{
		public BorderlessEntryEffect()
		{
		}

		protected override void OnAttached()
		{
            var editText = (EditText)Control;
            editText.Background = new Android.Graphics.Drawables.ColorDrawable(Android.Graphics.Color.Transparent);

		}

		protected override void OnDetached()
		{

		}
	}
}

