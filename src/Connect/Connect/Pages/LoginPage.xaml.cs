using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
//using Newtonsoft.Json;
//using Connect.Models;
using Xamarin.Forms;

namespace Connect.Pages {

    public partial class LoginPage : ContentPage {

        public LoginPage() {
            InitializeComponent();

#if DEBUG
            username.Text = "tpdmdev2";
            password.Text = "kL11179%";
#endif
        }

        async void login_Clicked(object sender, EventArgs e) {
            Label label = (Label)sender;

            if(!label.IsEnabled) {
                return;
            }

            label.IsEnabled = false;

            HttpClient client = new HttpClient();

            string url = "https://ecs.incresearch.com/ECS/mobile/login";

            //Login loginObj = new Login {
            //    uid = username.Text,
            //    pw = password.Text
            //};

            //string json = JsonConvert.SerializeObject(loginObj);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");

            //IEnumerable<KeyValuePair<string, string>> formLogin;


            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("uid", username.Text),
                new KeyValuePair<string, string>("pw", password.Text),
            });

            HttpResponseMessage response = await client.PostAsync(url, content);

            if(response.Headers.TryGetValues("X-Authorization", out IEnumerable<string> xAuth)) {
                App.AuthKey = xAuth.First();

                App.LoggedIn = true;

                await Navigation.PopModalAsync();
            } else {
                await DisplayAlert("Invalid Login", "Your credentials were not recognized", "Ok");
            }

            label.IsEnabled = true;
        }
    }
}