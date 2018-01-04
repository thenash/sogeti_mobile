using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Connect.Droid.Renderers;
using Connect.Helpers;
using Connect.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color    = Android.Graphics.Color;
using ListView = Android.Widget.ListView;
using View     = Android.Views.View;

[assembly: ExportRenderer(typeof(MenuViewCell), typeof(MenuViewCellRenderer))]

namespace Connect.Droid.Renderers {

    /// <summary>
    /// Disabling the ViewCell selecting and selected default background color so we can handle it in the shared code.
    /// </summary>
    public class MenuViewCellRenderer : ViewCellRenderer {

        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context) {
            View cellCore = base.GetCellCore(item, convertView, parent, context);

            if(parent is ListView listView) {
                listView.SetSelector(Android.Resource.Color.Transparent);
                listView.CacheColorHint = Color.White;
            }

            cellCore.SetBackground(GetUnpressedBackground());

            return cellCore;
        }


        private StateListDrawable GetUnpressedBackground() {
            StateListDrawable states = new StateListDrawable();
            //states.AddState(new[] { Android.Resource.Attribute.StatePressed }, new ColorDrawable(Color.White));
            //states.AddState(new[] { Android.Resource.Attribute.StateFocused }, new ColorDrawable(Color.White));
            states.AddState(new int[] { }, new ColorDrawable(Utility.GetResource<Xamarin.Forms.Color>("SkyBlue").ToAndroid()));

            return states;
        }
    }
}