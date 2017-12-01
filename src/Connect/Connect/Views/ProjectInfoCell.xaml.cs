using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Connect.Views
{
    public partial class ProjectInfoCell : ViewCell
    {
        public static readonly BindableProperty ProjectCodeProperty = BindableProperty.Create(nameof(ProjectCode), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

        public string ProjectCode { 
            get { return GetValue(ProjectCodeProperty).ToString(); }
            set { SetValue(ProjectCodeProperty, value); }
		}

        public static readonly BindableProperty ProtocolIdProperty = BindableProperty.Create(nameof(ProtocolId), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

		public string ProtocolId
		{
			get	{ return GetValue(ProtocolIdProperty).ToString(); }
			set	{ SetValue(ProtocolIdProperty, value); }
		}

        public static readonly BindableProperty CustomerNameProperty = BindableProperty.Create(nameof(CustomerName), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

		public string CustomerName
		{
			get { return GetValue(CustomerNameProperty).ToString();	}
			set	{ SetValue(CustomerNameProperty, value); }
		}

        public static readonly BindableProperty BusinessUnitProperty = BindableProperty.Create(nameof(BusinessUnit), typeof(string), typeof(ProjectInfoCell), string.Empty, BindingMode.TwoWay);

		public string BusinessUnit
		{
			get { return GetValue(BusinessUnitProperty).ToString(); }
			set { SetValue(BusinessUnitProperty, value); }
		}

		protected override void OnPropertyChanged(string propertyName)
		{
		    base.OnPropertyChanged(propertyName);

		    switch( propertyName )
		    {
				case "BusinessUnit":
					businessUnitCell.Description = GetValue(BusinessUnitProperty).ToString();
                    break;
		        case "ProtocolId":
                    protocolIdCell.Description = GetValue(ProtocolIdProperty).ToString();
		            break;
                case "CustomerName":
                    customerNameCell.Description = GetValue(CustomerNameProperty).ToString();
					break;
				case "ProjectCode":
                    projectCodeCell.Description = GetValue(ProjectCodeProperty).ToString();
					break;
		    }
		}

        public ProjectInfoCell()
        {
            InitializeComponent();
        }
    }
}
