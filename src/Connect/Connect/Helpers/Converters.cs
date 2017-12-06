using System;
using System.Globalization;
using Xamarin.Forms;

namespace Connect.Helpers {


    /// <summary>
    /// Used in a XAML trigger to return true or false based on the length of the 'value' text field.
    /// </summary>
    public class BoolToColorConverter : IValueConverter {

        public static readonly BoolToColorConverter Instance = new BoolToColorConverter();

        /// <summary>
        /// Currently it is simply set to return true if the value has anything in it, or is greater than 0. This could be updated to change the condition based on the 'Converter Parameter' being passed in. Currently no parameter is being passed in.
        /// </summary>
        /// <param name="value">The text from an entry/label/etc.</param>
        /// <param name="targetType">The Type of object/control that the text/value is coming from.</param>
        /// <param name="parameter">Optional, specify what length to test against (example: for Pin Code, we would choose 3 characters, since the Pin Code needs to be over 3 characters), if not specified, defaults to 0.</param>
        /// <param name="culture">The current culture set in the device.</param>
        /// <returns>Which is a <c>bool</c> (<c>true</c> if <see cref="value"/> is greater than 0, <c>false</c> if not).</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return DoWork(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
#if DEBUG
            throw new NotImplementedException();
#else
            return false;
#endif
        }

        private static object DoWork(object value, object parameter) {
            Color parameterColor = parameter as Color? ?? Color.White;

            if(value is bool b && !b) {
                return Color.Default;
            }

            return parameterColor;
        }
    }
}
