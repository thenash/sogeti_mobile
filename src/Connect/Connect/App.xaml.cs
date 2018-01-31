using Connect.Models;
using Xamarin.Forms;
using Connect.Pages;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Xamarin.Forms.Xaml;
using Device = Xamarin.Forms.Device;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Connect {

    public partial class App : Application {

        public static MainPage NavPage;

        public static Project SelectedProject { get; set; }

        public static bool IsPhone {
            get; private set;
        }

        public static bool IsAndroid {
            get; private set;
        }

        public static bool LoggedIn {
            get; set;
        }

        public static string AuthKey {
            get; set;
        }

        //public static string SelectedProjectID { get; set; }

        public App() {
            InitializeComponent();

            IsPhone   = Device.Idiom           == TargetIdiom.Phone;
            IsAndroid = Device.RuntimePlatform == Device.Android;

            MainPage = NavPage = new MainPage();
        }

        protected override void OnStart() {
            AppCenter.Start("ios=b93096aa-e65b-4977-a7d0-c60a0a92d618;" + "android=041fa813-eecd-4350-8a5e-fe800b6913ed;", typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }
    }
}