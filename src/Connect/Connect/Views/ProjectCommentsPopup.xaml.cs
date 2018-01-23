using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Connect.Views {

    public partial class ProjectCommentsPopup : PopupPage {

        //private bool _isEditing;

        public ProjectCommentsPopup() {
            InitializeComponent();

            CommentLabel.Text  = "Lorem ipsum dolor sit amet, at eum nemore admodum inciderint. Perpetua assueverit id est, ne mea errem abhorreant. Oratio labore legendos vis in. In alii saepe pri. Sint magna eum in. At quo platonem praesent torquatos, qui ex novum aliquip virtute. Ius ad suas facete ponderum, ad tibique aliquando eam.\n\nWisi sonet inimicus sed id. Reque facilisis complectitur vim eu. Eum modo utamur antiopam ad, urbanitas consequuntur has ex. Cu has graecis deleniti praesent, no adhuc ornatus nec, vis eu nobis vivendum percipitur.\n\nAd rebum temporibus sed, te ferri discere sapientem eum. Ei usu expetendis comprehensam, disputando dissentiet duo ei. Pro erroribus dignissim ea, eam te odio persecuti. Ex duo eirmod aliquam commune. Viris mnesarchum an eam, affert delectus patrioque ea qui, ex alii sapientem usu.";
            //CommentEditor.Text = "Lorem ipsum dolor sit amet, at eum nemore admodum inciderint. Perpetua assueverit id est, ne mea errem abhorreant. Oratio labore legendos vis in. In alii saepe pri. Sint magna eum in. At quo platonem praesent torquatos, qui ex novum aliquip virtute. Ius ad suas facete ponderum, ad tibique aliquando eam.\n\nWisi sonet inimicus sed id. Reque facilisis complectitur vim eu. Eum modo utamur antiopam ad, urbanitas consequuntur has ex. Cu has graecis deleniti praesent, no adhuc ornatus nec, vis eu nobis vivendum percipitur.\n\nAd rebum temporibus sed, te ferri discere sapientem eum. Ei usu expetendis comprehensam, disputando dissentiet duo ei. Pro erroribus dignissim ea, eam te odio persecuti. Ex duo eirmod aliquam commune. Viris mnesarchum an eam, affert delectus patrioque ea qui, ex alii sapientem usu.";
        }

        //protected override void OnAppearing() {
        //    base.OnAppearing();

        //    CommentEditor.WidthRequest  = Width  * 0.8;
        //    CommentEditor.HeightRequest = Height * 0.5;
        //}

        private void OnClose(object sender, EventArgs e) => PopupNavigation.PopAsync();

        //private void OnCommentEditButtonClicked(object sender, EventArgs e) {
        //    if(_isEditing) {
        //        _isEditing = false;

        //        //TODO: Save notes somewhere?

        //        CommentEditor.TextColor = Utility.GetResource<Color>("DarkGray");
        //        CommentEditor.IsEnabled = false;

        //        CommentEditButton.Text = "Edit";
        //    } else {
        //        _isEditing = true;

        //        CommentEditor.IsEnabled = true;
        //        CommentEditor.TextColor = Color.Black;

        //        CommentEditButton.Text = "Done";
        //    }
        //}
    }
}
