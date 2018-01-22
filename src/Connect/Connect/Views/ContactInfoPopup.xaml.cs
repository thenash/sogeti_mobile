using System;
using System.Collections.Generic;
using System.Windows.Input;
using Connect.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Connect.Views {

    public partial class ContactInfoPopup : PopupPage {

        #region Properties

        public ICommand PhoneTappedCommand { get; }
        public ICommand EmailTappedCommand { get; }

        public static readonly BindableProperty ContactsProperty = BindableProperty.Create(nameof(Contacts), typeof(List<ContactInfo>), typeof(ContactInfoPopup));

        public List<ContactInfo> Contacts {
            get => (List<ContactInfo>)GetValue(ContactsProperty);
            set => SetValue(ContactsProperty, value);
        }

        #endregion

        #region Constructors

        public ContactInfoPopup() {
            PhoneTappedCommand = new Command<string>(phoneNum  => Device.OpenUri(new Uri("tel:"    + phoneNum)));
            EmailTappedCommand = new Command<string>(emailAddr => Device.OpenUri(new Uri("mailto:" + emailAddr)));

            BindingContext = this;

            InitializeComponent();

#if DEBUG
            Contacts = new List<ContactInfo> {
                new ContactInfo("Project Director", "Sally Smith", "555-555-5555", "genericemail@email.com"),
                new ContactInfo("Project Director", "Sally Smith", "555-555-5555", "genericemail@email.com"),
                new ContactInfo("Project Director", "Sally Smith", "555-555-5555", "genericemail@email.com")
            };
#endif
        }

        #endregion

        #region Event Handler Overrides

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if(propertyName == null) {
                return;
            }

            switch(propertyName) {
                case nameof(Contacts):
                    ContactsListView.ItemsSource = Contacts;
                    break;
            }
        }

        #endregion

        #region Event Handlers

        private void OnClose(object sender, EventArgs e) => PopupNavigation.PopAsync();

        #endregion
    }
}
