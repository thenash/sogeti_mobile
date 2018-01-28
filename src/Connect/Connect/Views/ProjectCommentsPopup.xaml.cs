using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Connect.Views {

    public partial class ProjectCommentsPopup : PopupPage {

        //private bool _isEditing;

        public static readonly BindableProperty ProjectCodeProperty = BindableProperty.Create(nameof(ProjectCode), typeof(string), typeof(ProjectCommentsPopup));

        public string ProjectCode {
            get => (string)GetValue(ProjectCodeProperty);
            set => SetValue(ProjectCodeProperty, value);
        }

        public static readonly BindableProperty CustomerNameProperty = BindableProperty.Create(nameof(CustomerName), typeof(string), typeof(ProjectCommentsPopup));

        public string CustomerName {
            get => (string)GetValue(CustomerNameProperty);
            set => SetValue(CustomerNameProperty, value);
        }

        public static readonly BindableProperty ProtocolIdProperty = BindableProperty.Create(nameof(ProtocolId), typeof(string), typeof(ProjectCommentsPopup));

        public string ProtocolId {
            get => (string)GetValue(ProtocolIdProperty);
            set => SetValue(ProtocolIdProperty, value);
        }

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

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if(propertyName == null) {
                return;
            }

            switch(propertyName) {
                case nameof(ProjectCode):
                    ProjectCodeCardCellView.Description = ProjectCode;
                    break;

                case nameof(CustomerName):
                    CustomerNameCardCellView.Description = CustomerName;
                    break;

                case nameof(ProtocolId):
                    ProtocolIdCardCellView.Description = ProtocolId;
                    break;
            }
        }

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
