using System;
using Connect.iOS.Implementations;
using Connect.Interfaces;
using MessageUI;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(NativeCommunication))]

namespace Connect.iOS.Implementations {

    public class NativeCommunication : INativeCommunication {

        private MFMailComposeViewController _mailer;

        /// <inheritdoc />
        public string ShowEmailDraft(string to) {

            if(!MFMailComposeViewController.CanSendMail) {
                return "This device seems to be unable to send emails at this time.";
            }

            _mailer = new MFMailComposeViewController();

            if(!string.IsNullOrEmpty(to)) {
                _mailer.SetToRecipients(new[] { to.Replace("mailto:", string.Empty) });
            }

            _mailer.Finished -= OnMailerFinished;
            _mailer.Finished += OnMailerFinished;

            UIViewController vc = GetVisibleViewController();

            try {
                Device.BeginInvokeOnMainThread(async () => await vc.PresentViewControllerAsync(_mailer, true));
            } catch(Exception ex) {
                return "Error occurred while trying to display the email helper.\n" + ex;
            }

            return null;
        }

        private void OnMailerFinished(object s, MFComposeResultEventArgs mfComposeResultEventArgs) {
            _mailer.Finished -= OnMailerFinished;
            Device.BeginInvokeOnMainThread(async () => await ((MFMailComposeViewController)s).DismissViewControllerAsync(true));
        }

        /// <summary>
        /// Gets the visible view controller.
        /// </summary>
        /// <returns>The visible view controller.</returns>
        private static UIViewController GetVisibleViewController(UIViewController controller = null) {
            while(true) {
                controller = controller ?? UIApplication.SharedApplication.KeyWindow.RootViewController;

                if(controller.PresentedViewController == null) {
                    return controller;
                }

                if(controller.PresentedViewController is UINavigationController navViewController) {
                    return navViewController.VisibleViewController;
                }

                if(controller.PresentedViewController is UITabBarController tabBarController) {
                    return tabBarController.SelectedViewController;
                }

                controller = controller.PresentedViewController;
            }
        }
    }
}