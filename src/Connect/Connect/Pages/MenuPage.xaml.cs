﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Connect.Models;
using Connect.ViewModels;
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
                Navigate(new ProjectInfoPage());
            });

            /* Subscription is going in the constructor so that the page will update values whether its displayed or not */
            MessagingCenter.Unsubscribe<MainBasePage, ContentPage>(this, ConstantKeys.SwipePage);
            MessagingCenter.Subscribe<MainBasePage, ContentPage>(this, ConstantKeys.SwipePage, (vm, page) => Navigate(page));
        }

        public void Navigate(Page page) {
            MasterPageItem selectedItem = MasterPageItems.FirstOrDefault(item => item.TargetType == page.GetType());

            if(selectedItem != null) {

                MessagingCenter.Send(this, ConstantKeys.ChangeBackground, selectedItem.Title);

                MasterPageItem previouslySelectedItem = MasterPageItems.FirstOrDefault(item => item.IsSelected);

                if(previouslySelectedItem != null) {
                    previouslySelectedItem.IsSelected = false;
                }

                selectedItem.IsSelected = true;

                OnPropertyChanged(nameof(MasterPageItems));

                PageNavigationEventArgs args = new PageNavigationEventArgs(selectedItem, page);
                PageNavigated?.Invoke(this, args);
            }
        }

        private void OnMenuItemTapped(object sender, EventArgs e) {
            MasterPageItem selectedItem = MasterPageItems.FirstOrDefault(item => item.TargetType == ((MasterPageItem)((ViewCell)sender).BindingContext).TargetType);

            if(selectedItem != null) {

                MessagingCenter.Send(this, ConstantKeys.ChangeBackground, selectedItem.Title);

                MasterPageItem previouslySelectedItem = MasterPageItems.FirstOrDefault(item => item.IsSelected);

                if(previouslySelectedItem != null) {
                    previouslySelectedItem.IsSelected = false;
                }

                selectedItem.IsSelected = true;

                OnPropertyChanged(nameof(MasterPageItems));

                PageNavigationEventArgs args = new PageNavigationEventArgs(selectedItem, (Page)Activator.CreateInstance(selectedItem.TargetType));
                PageNavigated?.Invoke(this, args);
            }
        }

        private void ToggleProjectLabelVisibility(bool display) {
            Device.BeginInvokeOnMainThread(() => {
                NoProjectLabel.IsVisible = !display;

                ProjectCodeLabel.IsVisible  = display;
                ProtocolIdLabel.IsVisible   = display;
                CustomerLabel.IsVisible     = display;
                BusinessUnitLabel.IsVisible = display;
            });
        }

        private void SetProjectLabels(Project project) {
            Device.BeginInvokeOnMainThread(() => {
                ProjectCodeLabel.Text  = "Project Code: " + project.projectId;
                ProtocolIdLabel.Text   = "Protocol ID: " + project.protocolId;
                CustomerLabel.Text     = "Customer: " + project.customerName;
                BusinessUnitLabel.Text = "BU: " + project.owningBu;
            });
        }
    }

    public class PageNavigationEventArgs : EventArgs {

        public PageNavigationEventArgs(MasterPageItem page, Page pageItem) {
            TargetType = page.TargetType;
            Title      = page.Title;
            PageItem   = pageItem;
        }

        public Page PageItem {
            get; set;
        }

        public Type TargetType {
            get; set;
        }

        public string Title {
            get; set;
        }
    }
}