using Connect.Models;
using Xamarin.Forms;

namespace Connect.Views {

    public partial class ProjectInfoCell : ViewCell {

        #region Properties

        public static readonly BindableProperty ProjectCodeProperty = BindableProperty.Create(nameof(ProjectCode), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

        public string ProjectCode {
            get => GetValue(ProjectCodeProperty).ToString();
            set => SetValue(ProjectCodeProperty, value);
        }

        public static readonly BindableProperty ProtocolIdProperty = BindableProperty.Create(nameof(ProtocolId), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

        public string ProtocolId {
            get => GetValue(ProtocolIdProperty).ToString();
            set => SetValue(ProtocolIdProperty, value);
        }

        public static readonly BindableProperty CustomerNameProperty = BindableProperty.Create(nameof(CustomerName), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

        public string CustomerName {
            get => GetValue(CustomerNameProperty).ToString();
            set => SetValue(CustomerNameProperty, value);
        }

        public static readonly BindableProperty BusinessUnitProperty = BindableProperty.Create(nameof(BusinessUnit), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

        public string BusinessUnit {
            get => GetValue(BusinessUnitProperty).ToString();
            set => SetValue(BusinessUnitProperty, value);
        }

        public static readonly BindableProperty ArrowTappedCommandProperty = BindableProperty.Create(nameof(ArrowTappedCommand), typeof(Command<Project>), typeof(ProjectInfoCell));

        public Command<Project> ArrowTappedCommand {
            get => (Command<Project>)GetValue(ArrowTappedCommandProperty);
            set => SetValue(ArrowTappedCommandProperty, value);
        }

        public static readonly BindableProperty ArrowTappedCommandParameterProperty = BindableProperty.Create(nameof(ArrowTappedCommandParameter), typeof(Project), typeof(ProjectInfoCell));

        public Project ArrowTappedCommandParameter {
            get => (Project)GetValue(ArrowTappedCommandParameterProperty);
            set => SetValue(ArrowTappedCommandParameterProperty, value);
        }

        #endregion

        #region Constructors

        public ProjectInfoCell() {
            InitializeComponent();
        }

        #endregion

        #region Event Handler Overrides

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if(propertyName == null) {
                return;
            }

            switch(propertyName) {
                case nameof(BusinessUnit):
                    BusinessUnitCell.Description = GetValue(BusinessUnitProperty).ToString();
                    break;

                case nameof(ProtocolId):
                    ProtocolIdCell.Description = GetValue(ProtocolIdProperty).ToString();
                    break;

                case nameof(CustomerName):
                    CustomerNameCell.Description = GetValue(CustomerNameProperty).ToString();
                    break;

                case nameof(ProjectCode):
                    ProjectCodeCell.Description = GetValue(ProjectCodeProperty).ToString();
                    break;

                case nameof(ArrowTappedCommandParameter):
                case nameof(ArrowTappedCommand):

                    if(ArrowTappedCommand == null) {
                        ArrowButtonGrid.GestureRecognizers.Clear();
                        return;
                    }

                    TapGestureRecognizer tapGesture = new TapGestureRecognizer {
                        Command          = ArrowTappedCommand,
                        CommandParameter = ArrowTappedCommandParameter
                    };

                    if(ArrowButtonGrid.GestureRecognizers.Contains(tapGesture)) {
                        ArrowButtonGrid.GestureRecognizers.Remove(tapGesture);
                    }

                    ArrowButtonGrid.GestureRecognizers.Add(tapGesture);

                    break;
            }
        }

        #endregion
    }
}