using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Connect.Droid.Renderers;
using Connect.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using ListView = Android.Widget.ListView;
using View     = Android.Views.View;

[assembly: ExportRenderer(typeof(MenuViewCell), typeof(MenuViewCellRenderer))]

namespace Connect.Droid.Renderers {

    public class MenuViewCellRenderer : ViewCellRenderer {

        private View _cellCore;

        private bool _isChanging;

        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context) {
            _cellCore = base.GetCellCore(item, convertView, parent, context);

            //menuCell.PropertyChanged +=

            if(parent is ListView listView) {
                //Color color = menuCell.SelectedBackgroundColor.ToAndroid();

                // Disable native cell selection color style - set as *Transparent*
                listView.SetSelector(Android.Resource.Color.White);
                listView.CacheColorHint = Color.White;
            }

            _cellCore.SetBackground(GetUnpressedBackground());

            return _cellCore;
        }

        private StateListDrawable GetUnpressedBackground() {
            StateListDrawable states = new StateListDrawable();
            states.AddState(new[] { Android.Resource.Attribute.StatePressed }, new ColorDrawable(Color.White));
            states.AddState(new[] { Android.Resource.Attribute.StateFocused }, new ColorDrawable(Color.White));
            states.AddState(new int[] { }, new ColorDrawable(((Xamarin.Forms.Color)Application.Current.Resources["SkyBlue"]).ToAndroid()));

            return states;
        }

        private StateListDrawable GetPressedBackground() {
            StateListDrawable states = new StateListDrawable();
            states.AddState(new[] { Android.Resource.Attribute.StatePressed }, new ColorDrawable(Color.White));
            states.AddState(new[] { Android.Resource.Attribute.StateFocused }, new ColorDrawable(Color.White));
            states.AddState(new int[] { }, new ColorDrawable(Color.White));

            return states;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args) {
            base.OnCellPropertyChanged(sender, args);

            if(_isChanging) {
                _isChanging = false;
                return;
            }

            if(args.PropertyName == MenuViewCell.IsSelectedProperty.PropertyName && sender is MenuViewCell customTextCell) {
                if(!customTextCell.IsSelected) {
                    _cellCore.SetBackground(GetPressedBackground());
                } else {
                    _cellCore.SetBackground(GetUnpressedBackground());
                }

                _isChanging = true;
                customTextCell.IsSelected = !customTextCell.IsSelected;
            }
        }
    }
}