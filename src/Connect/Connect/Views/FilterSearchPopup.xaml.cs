using System;
using System.Collections.Generic;
using System.Linq;
using Connect.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Connect.Views {

    public partial class FilterSearchPopup : PopupPage {

        #region Properties

        /// <summary>
        /// Used to skip the initial selection of the All Business Unit category which would normally trigger a picker selection and load all items.
        /// </summary>
        private bool _skipBusinessUnitSelected;

        public event EventHandler<ItemTappedEventArgs> Filtered;

        public static readonly BindableProperty BusinessUnitsProperty = BindableProperty.Create(nameof(BusinessUnits), typeof(List<BusinessUnitFilterItem>), typeof(FilterSearchPopup));

        public List<BusinessUnitFilterItem> BusinessUnits {
            get => (List<BusinessUnitFilterItem>)GetValue(BusinessUnitsProperty);
            set => SetValue(BusinessUnitsProperty, value);
        }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<FilterSearchItem>), typeof(FilterSearchPopup));

        public List<FilterSearchItem> Items {
            get => (List<FilterSearchItem>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        #endregion

        #region Constructors

        public FilterSearchPopup() {
            BindingContext = this;

            InitializeComponent();

#if DEBUG
            BusinessUnits = new List<BusinessUnitFilterItem> {
                new BusinessUnitFilterItem {
                    BusinessUnitId   = -1,
                    BusinessUnitName = "All"
                }, new BusinessUnitFilterItem {
                    BusinessUnitId   = 1000,
                    BusinessUnitName = "Central Nervous System (CNS)"
                }, new BusinessUnitFilterItem {
                    BusinessUnitId   = 2000,
                    BusinessUnitName = "Oncology"
                }, new BusinessUnitFilterItem {
                    BusinessUnitId   = 4000,
                    BusinessUnitName = "General Medicine"
                }, new BusinessUnitFilterItem {
                    BusinessUnitId   = 9000,
                    BusinessUnitName = "Study Start Up"
                }, new BusinessUnitFilterItem {
                    BusinessUnitId   = 9500,
                    BusinessUnitName = "Biometrics"
                }
            };

            Items = new List<FilterSearchItem> {
                new FilterSearchItem {
                    ItemText  = "Project 1 - Customer Name",
                    ProjectId = "Project 1"
                }, new FilterSearchItem {
                    ItemText  = "Project 2 - Customer Name",
                    ProjectId = "Project 2"
                }, new FilterSearchItem {
                    ItemText  = "Project 3 - Customer Name",
                    ProjectId = "Project 3"
                }, new FilterSearchItem {
                    ItemText   = "9083-E1-ES3 - Customer Name",
                    ProtocolId = "9083-E1-ES3"
                }, new FilterSearchItem {
                    ItemText   = "7710-TM89-Y0 - Customer Name",
                    ProtocolId = "7710-TM89-Y0"
                }, new FilterSearchItem {
                    ItemText   = "5404-Y5TT-U1 - Customer Name",
                    ProtocolId = "5404-Y5TT-U1"
                }
            };
#endif
            _skipBusinessUnitSelected = true;

            BusinessUnitPicker.SelectedItem = BusinessUnits[0];
        }

        #endregion

        #region Event Handler Overrides

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if(propertyName == null) {
                return;
            }

            switch(propertyName) {
                case nameof(BusinessUnits):
                    BusinessUnitPicker.ItemsSource = BusinessUnits?.OrderBy(bu => bu.BusinessUnitId).ToList();
                    break;
            }
        }

        #endregion

        #region Event Handlers

        private void OnClose(object sender, EventArgs e) => PopupNavigation.PopAsync();

        private void OnSelectedBusinessUnitChanged(object sender, EventArgs e) {
            if(_skipBusinessUnitSelected) {
                _skipBusinessUnitSelected = false;
                return;
            }

            //TODO: Do something

            //var items = Items.Where(itm => itm.BusinessUnitId == ((BusinessUnitFilterItem)BusinessUnitPicker.SelectedItem).BusinessUnitId).ToList();
#if DEBUG
            List<FilterSearchItem> items = Items;
#endif
            SetItemListViewItems(items);
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e) {

            string searchTerm = e.NewTextValue;

            List<FilterSearchItem> items;

            if(string.IsNullOrWhiteSpace(searchTerm)) {
                items = Items;
            } else {
                items = Items.Where(itm => itm.ItemText.ToLowerInvariant().Contains(e.NewTextValue.TrimStart().ToLowerInvariant())).ToList();
            }

            SetItemListViewItems(items);
        }

        private async void OnApplyFilterTapped(object sender, EventArgs e) {
            if(ItemsListView.SelectedItem == null) {
                await DisplayAlert("Error", "You forgot to select an item from the list.", "OK");
                return;
            }

            Filtered?.Invoke(sender, new ItemTappedEventArgs(null, ItemsListView.SelectedItem));

            await Navigation.PopPopupAsync();
        }

        #endregion

        private void SetItemListViewItems(ICollection<FilterSearchItem> items) {
            ItemsListView.ItemsSource = items;
            ItemsListView.IsVisible   = items != null && items.Count > 1;
        }
    }
}