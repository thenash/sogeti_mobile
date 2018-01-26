using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Connect.Models;
using Xamarin.Forms;
using Connect.ViewModels;
using Connect.Views;
using Rg.Plugins.Popup.Extensions;

namespace Connect.Pages {

    public partial class ProjectsPage : ContentPage {

        #region Properties

        private readonly FilterSearchPopup _filterSearchPopup;

        private readonly ProjectsViewModel _viewModel;

        #endregion

        #region Constructors

        public ProjectsPage() {

            BindingContext = _viewModel = new ProjectsViewModel();

            InitializeComponent();

            _filterSearchPopup = new FilterSearchPopup();
        }

        #endregion

        #region Event Handler Overrides

        protected override async void OnAppearing() {
            base.OnAppearing();

            OnSizeChanged(null, null);

            SizeChanged                 += OnSizeChanged;
            _filterSearchPopup.Filtered += OnProjectsFiltered;

            if(App.LoggedIn) {
                if(_viewModel.IsInitialized) {
                    return;
                }

                _viewModel.IsInitialized = true;
                await LoadProjectsAsync();

                _filterSearchPopup.BusinessUnits = _viewModel.BusinessUnits;

                _filterSearchPopup.Items = _filterSearchPopup.Items ?? new List<FilterSearchItem>();

                _filterSearchPopup.Items.Clear();

                _filterSearchPopup.Items.AddRange(_viewModel.FilterSearchProjectItems);
                _filterSearchPopup.Items.AddRange(_viewModel.FilterSearchProrocolItems);
            }
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();

            SizeChanged                 -= OnSizeChanged;
            _filterSearchPopup.Filtered -= OnProjectsFiltered;
        }

        #endregion

        #region Event Handlers

        private void OnSizeChanged(object sender, EventArgs eventArgs) {
            double height = (Height - Height * 0.9) / 2;

            _filterSearchPopup.Padding = new Thickness(0, height);
        }

        private void OnProjectSelected(object sender, SelectedItemChangedEventArgs e) => ProjectsList.SelectedItem = null;

        private async void OnFilterSearchTapped(object sender, EventArgs e) {
            await Navigation.PushPopupAsync(_filterSearchPopup);
        }

        private void OnProjectsFiltered(object sender, ItemTappedEventArgs itemTappedEventArgs) {
            if(_viewModel.Projects == null) {
                return;
            }

            if(itemTappedEventArgs.Item == null) {  //Clear filter if null
                _viewModel.DisplayProjects = new ObservableCollection<Project>(_viewModel.Projects);
                return;
            }

            FilterSearchItem item = (FilterSearchItem)itemTappedEventArgs.Item;

            if(!string.IsNullOrEmpty(item.ProjectId)) {
                _viewModel.DisplayProjects = new ObservableCollection<Project>(_viewModel.Projects.Where(p => p.projectId.Equals(item.ProjectId, StringComparison.OrdinalIgnoreCase)));
            } else if(!string.IsNullOrEmpty(item.ProtocolId)) {
                _viewModel.DisplayProjects = new ObservableCollection<Project>(_viewModel.Projects.Where(p => p.protocolId.Equals(item.ProtocolId, StringComparison.OrdinalIgnoreCase)));
            }
        }

        #endregion

        public async Task LoadProjectsAsync() => await _viewModel.ExecuteLoadCommand();
    }
}