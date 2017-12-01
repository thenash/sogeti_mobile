using System;
using System.Collections.Generic;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.Pages
{
    public partial class MainPage : MasterDetailPage
    {
        MenuPage masterPage;

        public MainPage()
        {
            masterPage = new MenuPage();

            Master = masterPage;

            Detail = new NavigationPage(new ProjectsPage() { Title = "Project Selection" }) { BarBackgroundColor = Color.FromHex("#2C4A70"), BarTextColor = Color.White }; //new ProjectsPage() { Title = "Project Selection" }) { BackgroundColor = Color.FromHex("#2F4973") };

			masterPage.PageNavigated += (object sender, PageNavigationEventArgs e) =>
			{
                Detail = new NavigationPage((Page)Activator.CreateInstance(e.TargetType)) { Title = e.Title };
                IsPresented = false;
            };

			if (!App.LoggedIn)
			{
				Navigation.PushModalAsync(new LoginPage(), true);
			}
		}
    }
}
