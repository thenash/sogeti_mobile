using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Connect.Models;
using Connect.ViewModels;
using Connect.Views;
using Xamarin.Forms;

namespace Connect.Pages {

    public delegate void PageNavigatedHandler(object sender, PageNavigationEventArgs e);

    public partial class MenuPage : ContentPage {

        public event PageNavigatedHandler PageNavigated;

        private ObservableCollection<MasterPageItem> _masterPageItems;
        private ObservableCollection<MasterPageItem> MasterPageItems {
            get => _masterPageItems;
            set {
                if(!Equals(_masterPageItems, value)) {
                    _masterPageItems = value;
                    OnPropertyChanged();
                }
            }
        }

        public MenuPage() {
            _masterPageItems = new ObservableCollection<MasterPageItem> {
                new MasterPageItem { Title = "Project Selection", TargetType = typeof(ProjectsPage), IsFirst = true },
                new MasterPageItem { Title = "Project Information", TargetType = typeof(ProjectInfoPage) },
                new MasterPageItem { Title = "Sites", TargetType = typeof(SitesPage) },
                new MasterPageItem { Title = "Subjects", TargetType = typeof(SubjectsPage) },
                new MasterPageItem { Title = "Monitoring", TargetType = typeof(MonitoringPage) }
            };

            BindingContext = MasterPageItems;

            InitializeComponent();

            //listView.ItemsSource  = MasterPageItems;
            //listView.SelectedItem = MasterPageItems[0];

            /* Subscription is going in the constructor so that the page will update values whether its displayed or not */
            MessagingCenter.Unsubscribe<ProjectsViewModel, Project>(this, ConstantKeys.ProjectSelected);
            MessagingCenter.Subscribe<ProjectsViewModel, Project>(this, ConstantKeys.ProjectSelected, (vm, project) => {
                if(project == null) {
                    return;
                }

                ToggleProjectLabelVisibility(true);
                SetProjectLabels(project);
            });
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e) {

            MessagingCenter.Send(this, ConstantKeys.ChangeMenuFirstBackground);

            MasterPageItem selectedItem = MasterPageItems.FirstOrDefault(item => item.TargetType == ((MasterPageItem)e.SelectedItem).TargetType);

            if(selectedItem != null) {

                MasterPageItem previouslySelectedItem = MasterPageItems.FirstOrDefault(item => item.IsSelected);

                if(previouslySelectedItem != null) {
                    previouslySelectedItem.IsSelected = false;
                }

                selectedItem.IsSelected = true;

                //MasterPageItems = new ObservableCollection<MasterPageItem>(MasterPageItems);
                OnPropertyChanged(nameof(MasterPageItems));

                PageNavigationEventArgs args = new PageNavigationEventArgs(selectedItem);
                PageNavigated?.Invoke(this, args);
            }

            //selectedViewCell.IsEnabled = true;
        }

        //private void OnItemTapped(object sender, EventArgs e) {

        //    if(sender == null) {
        //        return;
        //    }

        //    MenuViewCell selectedViewCell = (MenuViewCell)sender;

        //    if(!selectedViewCell.IsEnabled) {
        //        return;
        //    }

        //    selectedViewCell.IsEnabled = false;

        //    MasterPageItem selectedItem = MasterPageItems.FirstOrDefault(item => item.TargetType == ((MasterPageItem)selectedViewCell.BindingContext).TargetType);

        //    if(selectedItem != null) {

        //        MasterPageItem previouslySelectedItem = MasterPageItems.FirstOrDefault(item => item.IsSelected);

        //        if(previouslySelectedItem != null) {
        //            previouslySelectedItem.IsSelected = false;
        //        }

        //        selectedItem.IsSelected = true;

        //        MasterPageItems = new ObservableCollection<MasterPageItem>(MasterPageItems);
        //        //OnPropertyChanged(nameof(MasterPageItems));

        //        PageNavigationEventArgs args = new PageNavigationEventArgs(selectedItem);
        //        PageNavigated?.Invoke(this, args);
        //    }

        //    selectedViewCell.IsEnabled = true;
        //}

        private void ToggleProjectLabelVisibility(bool display) {
            NoProjectLabel.IsVisible = !display;

            ProjectCodeLabel.IsVisible  = display;
            ProtocolIdLabel.IsVisible   = display;
            CustomerLabel.IsVisible     = display;
            BusinessUnitLabel.IsVisible = display;
        }

        private void SetProjectLabels(Project project) {
            ProjectCodeLabel.Text  = "Project Code: " + project.projectId;
            ProtocolIdLabel.Text   = "Protocol ID: " + project.protocolId;
            CustomerLabel.Text     = project.customerName;
            BusinessUnitLabel.Text = project.owningBu;
        }
    }

    public class PageNavigationEventArgs : EventArgs {

        public PageNavigationEventArgs(MasterPageItem page) {
            TargetType = page.TargetType;
            Title      = page.Title;
        }

        public Type TargetType {
            get; set;
        }

        public string Title {
            get; set;
        }
    }
}