using System;
using Xamarin.Forms;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo(nameof(Connect) + "Droid"),
           System.Runtime.CompilerServices.InternalsVisibleTo(nameof(Connect) + "iOS")]

namespace Connect.Views {

    public class ExtendedEntry : Entry {

        #region Properties

        /// <summary>
        /// The <see cref="NextEntry"/> property.
        /// </summary>
        public static readonly BindableProperty NextEntryProperty = BindableProperty.Create(nameof(NextEntry), typeof(Entry), typeof(ExtendedEntry));

        /// <summary>
        /// Gets or sets the <see cref="Entry"/> that will receive focus upon <see cref="Entry.Completed"/>.
        /// </summary>
        /// <value>The <see cref="Entry"/> to forward to when <see cref="Entry.Completed"/> is executed.</value>
        public Entry NextEntry {
            get => (Entry)GetValue(NextEntryProperty);
            set => SetValue(NextEntryProperty, value);
        }

        #endregion

        #region Constructors

        public ExtendedEntry() { }

        #endregion

        protected override void OnParentSet() {
            base.OnParentSet();

            if(Parent == null) {
                Completed -= OnNextEntryCompletedGoto;
            } else {
                Completed -= OnNextEntryCompletedGoto;
                Completed += OnNextEntryCompletedGoto;
            }
        }

        /// <summary>
        /// When <see cref="NextEntry"/> is not null, forces focus onto <see cref="NextEntry"/>. Currently attached to the <see cref="Entry.Completed"/> event.
        /// </summary>
        private static void OnNextEntryCompletedGoto(object sender, EventArgs e) {
            ((ExtendedEntry)sender)?.NextEntry?.Focus();
        }
    }
}