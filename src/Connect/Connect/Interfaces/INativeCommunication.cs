using Xamarin.Forms;

namespace Connect.Interfaces {

    /// <summary>
    /// Handles communication on the device (email, phone, sharing, etc.).
    /// </summary>
    public interface INativeCommunication {

        /// <summary>
        /// Shows the email draft using the passed in params. Ability to send attachments has been commented out but should work if needed. Also sending BCC, CC, and multiple recipients is possible but not added here.
        /// </summary>
        /// <param name="to">The email address that the email is going to.</param>
        /// <returns><c>null</c> if no errors occurred or an error message.</returns>
        /// <remarks>
        /// iOS Source:     https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/src/Platform/XLabs.Platform.iOS/Services/Email/EmailService.cs
        /// Android Source: https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/src/Platform/XLabs.Platform.Droid/Services/Email/EmailService.cs
        /// </remarks>
        string ShowEmailDraft(string to);
    }
}
