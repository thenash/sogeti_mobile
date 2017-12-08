using Connect.Pages;
using Xamarin.Forms;

namespace Connect.Views {

    public partial class MenuViewCell : ViewCell {

        /// <summary>
        /// The IsSelected property.
        /// </summary>
        public static readonly BindableProperty IsFirstProperty = BindableProperty.Create(nameof(IsFirst), typeof(bool), typeof(MenuViewCell), false, BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets the IsSelected.
        /// </summary>
        public bool IsFirst {
            get => (bool)GetValue(IsFirstProperty);
            set => SetValue(IsFirstProperty, value);
        }

        /// <summary>
        /// The IsSelected property.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(MenuViewCell), false, BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets the IsSelected.
        /// </summary>
        public bool IsSelected {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// The SelectedBackgroundColor property.
        /// </summary>
        public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(MenuViewCell), Color.Default);

        /// <summary>
        /// Gets or sets the SelectedBackgroundColor.
        /// </summary>
        public Color SelectedBackgroundColor {
            get => (Color)GetValue(SelectedBackgroundColorProperty);
            set => SetValue(SelectedBackgroundColorProperty, value);
        }

        public MenuViewCell() {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if(propertyName == IsFirstProperty.PropertyName) {

                if(IsFirst) {
                    MessagingCenter.Unsubscribe<MenuPage>(this, ConstantKeys.ChangeMenuBackground);
                    MessagingCenter.Subscribe<MenuPage>(this, ConstantKeys.ChangeMenuBackground, page => {
                        View.BackgroundColor = Color.Default;
                        MessagingCenter.Unsubscribe<MenuPage>(this, ConstantKeys.ChangeMenuBackground);
                    });
                }
            }
        }
    }
}
