﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using Connect.Helpers;
using Connect.Interfaces;
using Connect.Models;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Connect.Views {

    public partial class ContactInfoPopup : PopupPage {

        #region Properties

        private readonly INativeCommunication _nativeComm;

        public ICommand PhoneTappedCommand { get; }
        public ICommand EmailTappedCommand { get; }

        public static readonly BindableProperty ProjectCodeProperty = BindableProperty.Create(nameof(ProjectCode), typeof(string), typeof(ContactInfoPopup));

        public string ProjectCode {
            get => (string)GetValue(ProjectCodeProperty);
            set => SetValue(ProjectCodeProperty, value);
        }

        public static readonly BindableProperty CustomerNameProperty = BindableProperty.Create(nameof(CustomerName), typeof(string), typeof(ContactInfoPopup));

        public string CustomerName {
            get => (string)GetValue(CustomerNameProperty);
            set => SetValue(CustomerNameProperty, value);
        }

        public static readonly BindableProperty ProtocolIdProperty = BindableProperty.Create(nameof(ProtocolId), typeof(string), typeof(ContactInfoPopup));

        public string ProtocolId {
            get => (string)GetValue(ProtocolIdProperty);
            set => SetValue(ProtocolIdProperty, value);
        }

        public static readonly BindableProperty ContactsProperty = BindableProperty.Create(nameof(Contacts), typeof(List<ContactInfo>), typeof(ContactInfoPopup));

        public List<ContactInfo> Contacts {
            get => (List<ContactInfo>)GetValue(ContactsProperty);
            set => SetValue(ContactsProperty, value);
        }

        #endregion

        #region Constructors

        public ContactInfoPopup() : this(DependencyService.Get<INativeCommunication>()) { }

        public ContactInfoPopup(INativeCommunication nativeComm) {

            _nativeComm = nativeComm;

            PhoneTappedCommand = new Command<string>(async phoneNum  => {
                if(string.IsNullOrEmpty(phoneNum)) {
                    return;
                }

                if(await PermissionsInstance.HasOrGetsPermissionAsync(Permission.Phone)) {
                    Device.OpenUri(new Uri("tel:" + phoneNum));
                } else if(await DisplayAlert("ERROR", "Unable to make phone calls at this time.", "To Device Settings", "OK")) {
                    PermissionsInstance.Instance.OpenAppSettings();
                }
            });

            EmailTappedCommand = new Command<string>(async emailAddr => {
                if(string.IsNullOrEmpty(emailAddr)) {
                    return;
                }

                string error = _nativeComm.ShowEmailDraft(emailAddr);

                if(!string.IsNullOrEmpty(error)) {
                    await DisplayAlert("ERROR", error, "OK");
                }
            });

            BindingContext = this;

            InitializeComponent();

//#if DEBUG
//            Contacts = new List<ContactInfo> {
//                new ContactInfo("Project Director", "Sally Smith", "555-555-5555", "genericemail@email.com"),
//                new ContactInfo("Project Director", "Sally Smith", "555-555-5555", "genericemail@email.com"),
//                new ContactInfo("Project Director", "Sally Smith", "555-555-5555", "genericemail@email.com")
//            };
//#endif
        }

        #endregion

        #region Event Handler Overrides

        protected override void OnParentSet() {
            base.OnParentSet();

            ContactsListView.ItemSelected -= OnContactSelected;

            if(Parent != null) {
                ContactsListView.ItemSelected += OnContactSelected;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if(propertyName == null) {
                return;
            }

            switch(propertyName) {
                case nameof(Contacts):
                    ContactsListView.ItemsSource = Contacts;
                    break;

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

        #endregion

        #region Event Handlers

        private void OnContactSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs) => ContactsListView.SelectedItem = null;

        private void OnClose(object sender, EventArgs e) => PopupNavigation.PopAsync();

        #endregion
    }
}