using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Connect.Models;
using Xamarin.Forms;
using Connect.ViewModels;
using Connect.Views;
using Microsoft.AppCenter.Analytics;
using Rg.Plugins.Popup.Extensions;

namespace Connect.Pages {

    public partial class ProjectsPage : ContentPage {

        #region Properties

        private static string _projectIdSearch;
        private static string _protocolIdSearch;

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
                await LoadProjectsAsync();
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
            Analytics.TrackEvent("Button Clicked", new Dictionary<string, string> {
                { "Page", nameof(ProjectsPage) },
                { "Button", "FilterSearchButton"}
            });

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

                _projectIdSearch  = item.ProjectId;
                _protocolIdSearch = null;
            } else if(!string.IsNullOrEmpty(item.ProtocolId)) {
                _viewModel.DisplayProjects = new ObservableCollection<Project>(_viewModel.Projects.Where(p => p.protocolId.Equals(item.ProtocolId, StringComparison.OrdinalIgnoreCase)));

                _projectIdSearch  = null;
                _protocolIdSearch = item.ProtocolId;
            }
        }

        #endregion

        public async Task LoadProjectsAsync() {
            await _viewModel.ExecuteLoadCommand();

            FilterDisplayProjects(_projectIdSearch, _protocolIdSearch);//Filter projects if we are returning to the page from a previous page

            _filterSearchPopup.BusinessUnits = _viewModel.BusinessUnits;

            _filterSearchPopup.Items = _filterSearchPopup.Items ?? new List<FilterSearchItem>();

            _filterSearchPopup.Items.Clear();

            _filterSearchPopup.Items.AddRange(_viewModel.FilterSearchProjectItems);
            _filterSearchPopup.Items.AddRange(_viewModel.FilterSearchProrocolItems);
        }

        private void FilterDisplayProjects(string projectId, string protocolId) {
            if(!string.IsNullOrEmpty(projectId)) {
                _viewModel.DisplayProjects = new ObservableCollection<Project>(_viewModel.Projects.Where(p => p.projectId.Equals(projectId, StringComparison.OrdinalIgnoreCase)));

                _projectIdSearch  = projectId;
                _protocolIdSearch = null;
            } else if(!string.IsNullOrEmpty(protocolId)) {
                _viewModel.DisplayProjects = new ObservableCollection<Project>(_viewModel.Projects.Where(p => p.protocolId.Equals(protocolId, StringComparison.OrdinalIgnoreCase)));

                _projectIdSearch  = null;
                _protocolIdSearch = protocolId;
            }
        }
    }
}