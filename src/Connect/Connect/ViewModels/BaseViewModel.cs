using System.ComponentModel;
using System.Windows.Input;
using Connect.Interfaces;
using Connect.Pages;
using Connect.Services;
using Xamarin.Forms;

namespace Connect.ViewModels {

    public class BaseViewModel : INotifyPropertyChanged {

        public string Title {
            get; set;
        }

        public bool IsInitialized {
            get; set;
        }

        private bool _isBusy;
        /// <summary>
        /// Gets or sets if VM is busy working
        /// </summary>
        public bool IsBusy {
            get => _isBusy;
            set {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public ICommand LogoutCommand { get; }

        //INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected readonly INavigationService NavService;

        public BaseViewModel() : this(new NavigationService()) { }

        public BaseViewModel(INavigationService navigationService) {
            NavService = navigationService;

            LogoutCommand = new Command(async () => {
                App.LoggedIn = false;

                await ((MainPage)Application.Current.MainPage).PresentLoginPage();
            });
        }

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}