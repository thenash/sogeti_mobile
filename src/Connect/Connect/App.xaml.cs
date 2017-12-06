using Xamarin.Forms;
using Connect.Pages;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Connect {

    public partial class App : Application {

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

            MainPage = new MainPage();
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
