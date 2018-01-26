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

//#if DEBUG
//            BusinessUnits = new List<BusinessUnitFilterItem> {
//                new BusinessUnitFilterItem {
//                    BusinessUnitId   = -1,
//                    BusinessUnitName = "All"
//                }, new BusinessUnitFilterItem {
//                    BusinessUnitId   = 1000,
//                    BusinessUnitName = "Central Nervous System (CNS)"
//                }, new BusinessUnitFilterItem {
//                    BusinessUnitId   = 2000,
//                    BusinessUnitName = "Oncology"
//                }, new BusinessUnitFilterItem {
//                    BusinessUnitId   = 4000,
//                    BusinessUnitName = "General Medicine"
//                }, new BusinessUnitFilterItem {
//                    BusinessUnitId   = 9000,
//                    BusinessUnitName = "Study Start Up"
//                }, new BusinessUnitFilterItem {
//                    BusinessUnitId   = 9500,
//                    BusinessUnitName = "Biometrics"
//                }
//            };

//            Items = new List<FilterSearchItem> {
//                new FilterSearchItem {
//                    ItemText  = "Project 1 - Customer Name",
//                    ProjectId = "Project 1"
//                }, new FilterSearchItem {
//                    ItemText  = "Project 2 - Customer Name",
//                    ProjectId = "Project 2"
//                }, new FilterSearchItem {
//                    ItemText  = "Project 3 - Customer Name",
//                    ProjectId = "Project 3"
//                }, new FilterSearchItem {
//                    ItemText   = "9083-E1-ES3 - Customer Name",
//                    ProtocolId = "9083-E1-ES3"
//                }, new FilterSearchItem {
//                    ItemText   = "7710-TM89-Y0 - Customer Name",
//                    ProtocolId = "7710-TM89-Y0"
//                }, new FilterSearchItem {
//                    ItemText   = "5404-Y5TT-U1 - Customer Name",
//                    ProtocolId = "5404-Y5TT-U1"
//                }
//            };

//            BusinessUnitPicker.SelectedItem = BusinessUnits[0];
//#endif
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
                    if(BusinessUnits != null && BusinessUnits.Count > 0) {
                        List<BusinessUnitFilterItem> items = BusinessUnits.OrderBy(bu => bu.BusinessUnitId).ToList();

                        items.Insert(0, new BusinessUnitFilterItem {
                            BusinessUnitId   = -1,
                            BusinessUnitName = "All"
                        });

                        BusinessUnitPicker.ItemsSource  = items;
                        BusinessUnitPicker.SelectedItem = items[0];
                    } else {
                        BusinessUnitPicker.ItemsSource = null;
                    }
                    break;
            }

            switch(propertyName) {
                case nameof(Items):
                    if(BusinessUnits != null && BusinessUnits.Count > 0) {
                        List<BusinessUnitFilterItem> items = BusinessUnits.OrderBy(bu => bu.BusinessUnitId).ToList();

                        items.Insert(0, new BusinessUnitFilterItem {
                            BusinessUnitId   = -1,
                            BusinessUnitName = "All"
                        });

                        BusinessUnitPicker.ItemsSource  = items;
                        BusinessUnitPicker.SelectedItem = items[0];
                    } else {
                        BusinessUnitPicker.ItemsSource = null;
                    }
                    break;
            }
        }

        #endregion

        #region Event Handlers

        private void OnClose(object sender, EventArgs e) => PopupNavigation.PopAsync();

        private void OnSelectedBusinessUnitChanged(object sender, EventArgs e) {

            BusinessUnitFilterItem bu = (BusinessUnitFilterItem)BusinessUnitPicker.SelectedItem;

            List<FilterSearchItem> items;

            if(bu == null || bu.BusinessUnitId == -1) {
                items = Items;
            } else {
                items = Items.Where(itm => itm.BusinessUnitId == ((BusinessUnitFilterItem)BusinessUnitPicker.SelectedItem).BusinessUnitId).ToList();
            }

//#if DEBUG
//            List<FilterSearchItem> items = Items;
//#endif

            FilterViewItems(SearchEntry.Text, items);
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e) => FilterViewItems(e.NewTextValue, Items);

        private async void OnItemTapped(object sender, ItemTappedEventArgs e) {
            Filtered?.Invoke(sender, new ItemTappedEventArgs(null, e.Item));

            await Navigation.PopPopupAsync();
        }

        private async void OnClearFilterTapped(object sender, EventArgs e) {
            ItemsListView.SelectedItem = null;

            Filtered?.Invoke(sender, new ItemTappedEventArgs(null, null));

            await Navigation.PopPopupAsync();
        }

        #endregion

        private void SetItemListViewItems(ICollection<FilterSearchItem> items) {
            ItemsListView.ItemsSource = items;
            ItemsListView.IsVisible   = items != null && items.Count > 0;
        }

        private void FilterViewItems(string filterTerm, List<FilterSearchItem> items) {
            List<FilterSearchItem> filteredItems;

            if(string.IsNullOrWhiteSpace(filterTerm)) {
                filteredItems = items;
            } else {
                filteredItems = items.Where(itm => itm.ItemText.ToLowerInvariant().Contains(filterTerm.ToLowerInvariant())).ToList();
            }

            SetItemListViewItems(filteredItems);
        }
    }
}