using System.Threading.Tasks;
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
            AppCenter.Start("ios=77364ed3-f0b7-43d8-8492-fd0e97e1235b;" + "android=1321f962-11d2-4ac2-9abe-f9cb26fa580d;", typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }
    }
}