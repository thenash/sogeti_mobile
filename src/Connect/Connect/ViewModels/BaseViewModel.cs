using System.ComponentModel;
using Connect.Interfaces;
using Connect.Services;

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

        //INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected readonly INavigationService NavService;

        public BaseViewModel() : this(new NavigationService()) { }

        public BaseViewModel(INavigationService navigationService) {
            NavService = navigationService;
        }

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}