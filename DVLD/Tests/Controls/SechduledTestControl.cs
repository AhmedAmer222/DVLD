using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class SechduledTestControl : UserControl
    {
        private int _localAppID;
        private clsTestType.enTestTypes _testType;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private clsTestAppointment _testAppointment;

        public clsTestType.enTestTypes TestType
        {
            set
            {
                _testType = value;
            }

            get
            {
                return _testType;
            }
        }

        public int TestAppointmentId
        {
            get
            {
                return _testAppointment.TestAppointmentID;
            }
        }

        public int LocalAppID
        {
            get
            {
                return _localAppID;
            }
        }

        

        public clsLocalDrivingLicenseApplication SelectedLocalDrivingLicenseApplication
        {
            get
            {
                return _localDrivingLicenseApplication;
            }
        }

        public clsTestAppointment SelectedTestAppointment
        {
            get
            {
                return _testAppointment;
            }

        }
        
        public SechduledTestControl()
        {
            InitializeComponent();
        }


        public void LoadSechduledTest(int testAppointment)
        {
            _testAppointment = clsTestAppointment.GetTestAppointmentByID(testAppointment);

            if (_testAppointment == null)
            {
                MessageBox.Show("Error while loading data");
                return;
            }

            _testType = (clsTestType.enTestTypes) _testAppointment.TestTypeID;
            _localAppID = _testAppointment.LocalDrivingLicenseApplicationID;
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(_localAppID);


            _ChangeTestTypeTitleAndImage();
            lblLocalDrivingLicenseAppID.Text = _localAppID.ToString();
            lblDrivingClass.Text = clsLicenseClass.GetLicenseClassByID(_localDrivingLicenseApplication.LicenseClassesID).ClassName;
            lblFullName.Text = _localDrivingLicenseApplication.ApplicantFullName;
            lblTrial.Text = clsTestAppointment.GetTotelTrial(_localAppID, _testType).ToString();
            lblDate.Text = _testAppointment.AppointmentDate.ToShortDateString();
            lblFees.Text = _testAppointment.PaidFees.ToString();
        }


        private void _ChangeTestTypeTitleAndImage()
        {
            switch (_testType)
            {
                case clsTestType.enTestTypes.VisionTest:
                    lblTitle.Text = "Vision Test Appointments";
                    pbTestTypeImage.Image = Resources.Vision_512;
                    break;
                case clsTestType.enTestTypes.WrittenTest:
                    lblTitle.Text = "Written Test Appointments";
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    break;
                case clsTestType.enTestTypes.StreetTest:
                    lblTitle.Text = "Street Test Appointments";
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    break;

            }
        }
    }
}
