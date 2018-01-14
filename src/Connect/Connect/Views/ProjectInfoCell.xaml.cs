using System;
using System.Threading.Tasks;
using Connect.Helpers;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.Views {

    public partial class ProjectInfoCell : ViewCell {

        #region Properties

        // ReSharper disable MemberCanBePrivate.Global
        public const string ShowMoreDetailLabel = "Show More Detail";
        public const string ShowLessDetailLabel = "Show Less Detail";
        // ReSharper restore MemberCanBePrivate.Global

        private bool _isSelected;
        private bool _showHideIsRunning;

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(ProjectInfoCell), false, BindingMode.TwoWay);

        public bool IsSelected {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

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

        public static readonly BindableProperty TherapeuticAreaProperty = BindableProperty.Create(nameof(TherapeuticArea), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

        public string TherapeuticArea {
            get => GetValue(TherapeuticAreaProperty).ToString();
            set => SetValue(TherapeuticAreaProperty, value);
        }

        public static readonly BindableProperty StudyPhaseProperty = BindableProperty.Create(nameof(StudyPhase), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

        public string StudyPhase {
            get => GetValue(StudyPhaseProperty).ToString();
            set => SetValue(StudyPhaseProperty, value);
        }

        public static readonly BindableProperty IndicationsProperty = BindableProperty.Create(nameof(Indications), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

        public string Indications {
            get => GetValue(IndicationsProperty).ToString();
            set => SetValue(IndicationsProperty, value);
        }

        public static readonly BindableProperty ProjDirectorProperty = BindableProperty.Create(nameof(ProjDirector), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

        public string ProjDirector {
            get => GetValue(ProjDirectorProperty).ToString();
            set => SetValue(ProjDirectorProperty, value);
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
                case nameof(IsSelected):
                    ToggleIsSelected(IsSelected);
                    break;

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

                case nameof(TherapeuticArea):
                    TherapeuticAreaCell.Description = GetValue(TherapeuticAreaProperty).ToString();
                    break;

                case nameof(StudyPhase):
                    StudyPhaseCell.Description = GetValue(StudyPhaseProperty).ToString();
                    break;

                case nameof(Indications):
                    IndicationsCell.Description = GetValue(IndicationsProperty).ToString();
                    break;

                case nameof(ProjDirector):
                    ProjDirectorCell.Description = GetValue(ProjDirectorProperty).ToString();
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

        private async void OnShowHideTapped(object sender, EventArgs e) {
            if(_showHideIsRunning) {
                return;
            }

            _showHideIsRunning = true;

            bool willBeSelected = !_isSelected;

            if(ChevronImage.AnimationIsRunning("RotateXTo")) {
                ChevronImage.AbortAnimation("RotateXTo");
            }

            if(willBeSelected) {
                Device.BeginInvokeOnMainThread(() => ShowDetailsLabel.Text = ShowLessDetailLabel);
                await ChevronImage.RotateXTo(180, 400, Easing.CubicInOut);
            } else {
                Device.BeginInvokeOnMainThread(() => ShowDetailsLabel.Text = ShowMoreDetailLabel);
                await ChevronImage.RotateXTo(0, 400, Easing.CubicInOut);
            }

            Device.BeginInvokeOnMainThread(async () => {  //This will be run almost at the same time as ForceUpdateSize() below to minimize sizing irregularities while expanding/contracting the cell
                TherapeuticAreaCell.IsVisible = willBeSelected;
                StudyPhaseCell.IsVisible      = willBeSelected;
                IndicationsCell.IsVisible     = willBeSelected;
                ProjDirectorCell.IsVisible    = willBeSelected;
                ForceUpdateSize();
                await Task.Delay(400);  //BUG: On iOS, when the ListView is not very full, animating the show more button can cause the bottom part of the ViewCell to become unclickable (seemingly being covered by something) until you scroll or expand another ViewCell. Forceing the second size update after the layout cycle is over works around this
                ForceUpdateSize();
            });

            _isSelected = willBeSelected;

            _showHideIsRunning = false;
        }

        private void ToggleIsSelected(bool isSelected) {
            if(isSelected) {
                Color color = Utility.GetResource<Color>("DarkBlue");

                ArrowButtonGrid.BackgroundColor = color;
                TopSeparator.BackgroundColor    = color;
                BottomSeparator.BackgroundColor = color;
            } else {
                ArrowButtonGrid.BackgroundColor = Utility.GetResource<Color>("OrangeYellow");
                TopSeparator.BackgroundColor    = Color.White;
                BottomSeparator.BackgroundColor = Color.White;
            }
        }
    }
}