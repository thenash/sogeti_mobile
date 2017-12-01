using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

			username.Text = "tpdmdev2";
			password.Text = "kL11178%";
        }

		async void login_Clicked(object sender, System.EventArgs e)
		{
			HttpClient client = new HttpClient();

			var url = "https://ecs.incresearch.com/ECS/mobile/login";

			var _login = new Login();
			_login.uid = username.Text;
			_login.pw = password.Text;

			var json = JsonConvert.SerializeObject(_login);
			//var content = new StringContent(json, Encoding.UTF8, "application/json");

			//IEnumerable<KeyValuePair<string, string>> formLogin;


			var content = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("uid", username.Text),
				new KeyValuePair<string, string>("pw", password.Text),
			});

			HttpResponseMessage response = null;

			response = await client.PostAsync(url, content);

			IEnumerable<string> xAuth;

			if (response.Headers.TryGetValues("X-Authorization", out xAuth))
			{
				App.AuthKey = xAuth.First();

				App.LoggedIn = true;

				await Navigation.PopModalAsync();
			}
			else
			{
                App.LoggedIn = true;
                await Navigation.PopModalAsync();
				//await DisplayAlert("Invalid Login", "Your credentials were not recongized", "Ok");
			}

		}
	}
}
