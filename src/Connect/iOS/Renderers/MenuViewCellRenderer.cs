using Connect.iOS.Renderers;
using Connect.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MenuViewCell), typeof(NoBackgroundViewCellRenderer))]
[assembly: ExportRenderer(typeof(ProjectInfoCell), typeof(NoBackgroundViewCellRenderer))]

namespace Connect.iOS.Renderers {

    public class NoBackgroundViewCellRenderer : ViewCellRenderer {

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv) {
            UITableViewCell  cell = base.GetCell(item, reusableCell, tv);

            if(cell != null) {
                // Disable native cell selection color style - set as *Transparent*
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;

                //cell.SelectedBackgroundView = new UIView(new RectangleF(0, 0, (float)cell.Bounds.Width, (float)cell.Bounds.Height)) {
                //    BackgroundColor = UIColor.Clear
                //};
            }

            return cell;
        }
    }
}
