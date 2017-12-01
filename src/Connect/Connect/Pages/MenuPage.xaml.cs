using System;
using System.Collections.Generic;
using Connect.Models;
using Connect.Views;
using Xamarin.Forms;

namespace Connect.Pages
{
    public delegate void PageNavigatedHandler(object sender, PageNavigationEventArgs e);

    public partial class MenuPage : ContentPage
	{
        public event PageNavigatedHandler PageNavigated;

        public MenuPage()
        {
			InitializeComponent();

            var masterPageItems = new List<MasterPageItem>() {
				new MasterPageItem() { Title = "Project Selection", TargetType = typeof(ProjectsPage) },
				new MasterPageItem() { Title = "Project Information", TargetType = typeof(ProjectInfoPage) },
                new MasterPageItem() { Title = "Sites", TargetType = typeof(SitesPage) },
                new MasterPageItem() { Title = "Subjects", TargetType = typeof(SubjectsPage) },
                new MasterPageItem() { Title = "Monitoring", TargetType = typeof(MonitoringPage) }
            };

			listView = new ListView
			{
				ItemsSource = masterPageItems,
				ItemTemplate = new DataTemplate(() =>
				{
					var imageCell = new ImageCell();
					imageCell.SetBinding(TextCell.TextProperty, "Title");
					imageCell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None
			};

            listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
                PageNavigationEventArgs args = new PageNavigationEventArgs((MasterPageItem)e.SelectedItem);
                PageNavigated(this, args);
            };

			Padding = new Thickness(0, 40, 0, 0);
			Icon = "ic_hamburger.png";
			Title = "Menu";
			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					listView
				}
			};
        }
    }

	public class PageNavigationEventArgs : EventArgs
	{
		public PageNavigationEventArgs(MasterPageItem page)
		{
            TargetType = page.TargetType;
            Title = page.Title;
		}

        public Type TargetType { get; set; }

        public string Title { get; set; }
	}
}
