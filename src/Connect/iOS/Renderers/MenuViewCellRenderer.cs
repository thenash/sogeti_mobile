using System.Drawing;
using Connect.iOS.Renderers;
using Connect.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MenuViewCell), typeof(MenuViewCellRenderer))]

namespace Connect.iOS.Renderers {

    public class MenuViewCellRenderer : ViewCellRenderer {

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv) {
            UITableViewCell cell = base.GetCell(item, reusableCell, tv);
            MenuViewCell menuCell = (MenuViewCell)item;

            if(cell != null) {
                // Disable native cell selection color style - set as *Transparent*
                cell.SelectionStyle = UITableViewCellSelectionStyle.Default;

                cell.SelectedBackgroundView = new UIView(new RectangleF(0, 0, (float)cell.Bounds.Width, (float)cell.Bounds.Height)) {
                    BackgroundColor = menuCell.SelectedBackgroundColor.ToUIColor()
                };

            }

            return cell;
        }
    }
}
