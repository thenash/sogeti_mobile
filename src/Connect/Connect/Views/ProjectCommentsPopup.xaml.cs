using System;
using Connect.Helpers;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Connect.Views {

    public partial class ProjectCommentsPopup : PopupPage {

        private bool _isEditing;

        public ProjectCommentsPopup() {
            InitializeComponent();

            //CommentLabel.Text  = "Blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah.\n\nBlah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah.";
            CommentEditor.Text = "Blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah.\n\nBlah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah.";
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            CommentEditor.WidthRequest  = Width  * 0.8;
            CommentEditor.HeightRequest = Height * 0.5;
        }

        private void OnClose(object sender, EventArgs e) {
            PopupNavigation.PopAsync();
        }

        private void OnCommentEditButtonClicked(object sender, EventArgs e) {
            if(_isEditing) {
                _isEditing = false;

                //TODO: Save notes somewhere?
                //CommentLabel.Text = CommentEditor.Text;
                CommentEditor.TextColor = Utility.GetResource<Color>("DarkGray");
                CommentEditor.IsEnabled = false;
                //CommentLabel.IsVisible  = true;

                CommentEditButton.Text = "Edit";
            } else {
                _isEditing = true;
                //CommentEditor.Text = CommentLabel.Text;

                //CommentLabel.IsVisible  = false;
                CommentEditor.IsEnabled = true;
                CommentEditor.TextColor = Color.Black;

                CommentEditButton.Text = "Done";
            }
        }
    }
}
