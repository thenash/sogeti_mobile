using System;
using Android.Content;
using Connect.Droid.Implementations;
using Connect.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(NativeCommunication))]

namespace Connect.Droid.Implementations {

    public class NativeCommunication : INativeCommunication {

        /// <inheritdoc />
        public string ShowEmailDraft(string to) {

            if(!string.IsNullOrEmpty(to) && !to.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase)) {
                to = "mailto:" + to;
            } else {
                to = "mailto:";
            }

            try {
                Intent intent = new Intent(Intent.ActionSendto, Android.Net.Uri.Parse(to));
                intent.SetFlags(ActivityFlags.ClearTop);
                intent.AddFlags(ActivityFlags.NewTask);

                if(!TryIntent(intent)) {
                    return "Your device does not seem to be able to send emails at this time.";
                }
            } catch(Exception ex) {
                return "Error occurred while trying to display the email helper.\n" + ex;
            }

            return null;
        }

        private static bool TryIntent(Intent intent) {
            try {
                if(intent.ResolveActivity(Android.App.Application.Context.PackageManager) == null) {
                    return false;
                }

                intent.SetFlags(ActivityFlags.ClearTop);
                intent.SetFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(intent);
                return true;
            } catch(ActivityNotFoundException activitEx) {
                Console.WriteLine(activitEx);
                return false;
            } catch(Exception ex) {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}