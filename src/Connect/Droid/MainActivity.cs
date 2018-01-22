using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Chart.RadCartesianChart), typeof(Telerik.XamarinForms.ChartRenderer.Android.CartesianChartRenderer))]
[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Chart.RadPieChart), typeof(Telerik.XamarinForms.ChartRenderer.Android.PieChartRenderer))]

namespace Connect.Droid {

    [Activity(Label                = "Syneos Health",
              Icon                 = "@drawable/icon",
              Theme                = "@style/MyTheme",
              MainLauncher         = true,
              LaunchMode           = LaunchMode.SingleTop,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        #region Constructors

        // ReSharper disable UnusedMember.Global
        public MainActivity() { }

        // ReSharper disable UnusedParameter.Local
        public MainActivity(IntPtr handle, JniHandleOwnership transer) { }
        // ReSharper restore UnusedParameter.Local
        // ReSharper restore UnusedMember.Global

        #endregion

        #region Event Handler Overrides

        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource   = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

#if DEBUG
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslError) => {
                Console.WriteLine("\nAccepting self-signed certificate on Android.\n");
                return true;
            };
#endif

            Forms.Init(this, bundle);

            LoadApplication(new App());

            RequestedOrientation = ScreenOrientation.Portrait;

            if(!IsTaskRoot) {
                Intent intent = Intent;
                string action = intent.Action;
                if(intent.HasCategory(Intent.CategoryLauncher) && action != null && action.Equals(Intent.ActionMain, StringComparison.OrdinalIgnoreCase)) {
                    Finish();
                    //return;
                }
            }
        }

        public override void OnTrimMemory([GeneratedEnum] TrimMemory level) {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            base.OnTrimMemory(level);
        }

        /// <summary>
        /// Being used by the permissions plugin to allow it to handle what happens after a user is presented with a permission request and chooses an response. Only used on Android 6.0 and up.
        /// </summary>
        /// <param name="requestCode">The code corresponding to a specific permissions request.</param>
        /// <param name="permissions">The permissions asked for.</param>
        /// <param name="grantResults">What permissions the user allowed or did not allow.</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults) {
            try {
                Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            } catch(Exception e) {
                Console.WriteLine("\nIn MainActivity.OnRequestPermissionsResult() - Exception getting permissions(s): " + string.Join(", ", permissions ?? new [] { "null" }) + "\n" + e);
#if DEBUG
                throw;
#endif
            }
        }

        #endregion
    }
}