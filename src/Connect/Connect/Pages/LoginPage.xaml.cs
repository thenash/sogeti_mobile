using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace Connect.Pages {

    public partial class LoginPage : ContentPage {

        public LoginPage() {
            InitializeComponent();

#if DEBUG
            UsernameEntry.Text = "tpdmdev2";
            PasswordEntry.Text = "kL11179%";
#endif
        }

        async void login_Clicked(object sender, EventArgs e) {
            View loginButton = (View)sender;

            if(!loginButton.IsEnabled) {
                return;
            }

            Analytics.TrackEvent("Button Clicked", new Dictionary<string, string> {
                { "Page", nameof(LoginPage) },
                { "Button", "LoginButton"}
            });

            loginButton.IsEnabled = false;

            HttpClient client = new HttpClient();

            const string url = "https://ecs.incresearch.com/ECS/mobile/login";

            //Login loginObj = new Login {
            //    uid = UsernameEntry.Text,
            //    pw  = PasswordEntry.Text
            //};

            //string json = JsonConvert.SerializeObject(loginObj);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");

            //IEnumerable<KeyValuePair<string, string>> formLogin;

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("uid", UsernameEntry.Text),
                new KeyValuePair<string, string>("pw", PasswordEntry.Text)
            });

            HttpResponseMessage response = await client.PostAsync(url, content);

            if(response.Headers.TryGetValues("X-Authorization", out IEnumerable<string> xAuth)) {
                App.AuthKey = xAuth.First();

                App.LoggedIn = true;

                await Navigation.PopModalAsync();
            } else {
                await DisplayAlert("Invalid Login", "Your credentials were not recognized", "Ok");
            }

            loginButton.IsEnabled = true;
        }
    }
}