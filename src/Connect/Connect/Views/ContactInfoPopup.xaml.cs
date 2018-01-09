using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Connect.Views {

    public partial class ContactInfoPopup : PopupPage {

        public ContactInfoPopup() {
            InitializeComponent();
        }

        private void OnClose(object sender, EventArgs e) {
            PopupNavigation.PopAsync();
        }
    }
}
