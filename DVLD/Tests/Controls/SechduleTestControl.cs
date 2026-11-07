using DVLD.GLobal_Classes;
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
    public partial class SechduleTestControl : UserControl
    {
        enum enMode { AddNew, Update }
        enMode _mode;
        private int _localAppID;
        private clsTestType.enTestTypes _testType;
        private bool _retakeTest;
        clsApplication _reTakeTestApp;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private clsTestAppointment _testAppointment;

        public clsTestType.enTestTypes TestType
        {
            set
            {
                _testType = value;

                _ChangeTestTypeTitleAndImage();
            }

            get
            {
                return _testType;
            }
        }

        public int LocalAppID
        {
            get
            {
                return _localAppID;
            }
        }

        public bool RetakeTestEnable
        {
            set
            {
                _retakeTest = value;
            }

            get
            {
                return _retakeTest;
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


        public SechduleTestControl()
        {
            InitializeComponent();
        }

        public void LoadSechduleTest(int localAppid, clsTestType.enTestTypes testType, bool retakeTest = false)
        {
            _mode = enMode.AddNew;
            _localAppID = localAppid;
            _testType = testType;
            _retakeTest = retakeTest;


            _ChangeTestTypeTitleAndImage();


            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(_localAppID);
            
            lblLocalDrivingLicenseAppID.Text = _localAppID.ToString();
            lblDrivingClass.Text = clsLicenseClass.GetLicenseClassByID(_localDrivingLicenseApplication.LicenseClassesID).ClassName;
            lblFullName.Text = _localDrivingLicenseApplication.ApplicantFullName;
            lblTrial.Text = clsTestAppointment.GetTotelTrial(_localAppID, _testType).ToString();
            
            lblFees.Text = clsTestType.GetTestTypeByID(_testType).TestTypeFees.ToString();

            if (_retakeTest)
            {
                gbRetakeTestInfo.Enabled = true;

                float retakeAppFees = clsApplicationType.GetApplicationTypeByID((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                float TestFees = clsTestType.GetTestTypeByID(_testType).TestTypeFees;

                lblRetakeAppFees.Text = retakeAppFees.ToString();
                lblTotalFees.Text = (TestFees + retakeAppFees).ToString();

            }
        }


        public void LoadSechduleTest(int testAppointmentId)
        {
            _mode = enMode.Update;
            _testAppointment = clsTestAppointment.GetTestAppointmentByID(testAppointmentId);
            _localAppID = _testAppointment.LocalDrivingLicenseApplicationID;
            _testType = (clsTestType.enTestTypes) _testAppointment.TestTypeID;
            _retakeTest = (_testAppointment.RetakeTestApplicationID != 0);




            if (_testAppointment.IsLocked)
            {
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                lblUserMessage.Visible = true;
            }


            _ChangeTestTypeTitleAndImage();


            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(_localAppID);

            lblLocalDrivingLicenseAppID.Text = _localAppID.ToString();
            lblDrivingClass.Text = clsLicenseClass.GetLicenseClassByID(_localDrivingLicenseApplication.LicenseClassesID).ClassName;
            lblFullName.Text = _localDrivingLicenseApplication.ApplicantFullName;
            lblTrial.Text = clsTestAppointment.GetTotelTrial(_localAppID, _testType).ToString();
            dtpTestDate.Value = _testAppointment.AppointmentDate;
            lblFees.Text = _testAppointment.PaidFees.ToString();
            if (DateTime.Compare(DateTime.Now, _testAppointment.AppointmentDate) < 0)
            {
                dtpTestDate.MinDate = DateTime.Now;
            }

            if (_retakeTest)
            {
                gbRetakeTestInfo.Enabled = true;
                _reTakeTestApp = clsApplication.GetBaseApplicationInfo(_testAppointment.RetakeTestApplicationID);
                lblRetakeTestAppID.Text = _reTakeTestApp.ApplicationID.ToString();

                float retakeAppFees = clsApplicationType.GetApplicationTypeByID((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                float TestFees = clsTestType.GetTestTypeByID(_testType).TestTypeFees;

                lblRetakeAppFees.Text = retakeAppFees.ToString();
                lblTotalFees.Text = (TestFees + retakeAppFees).ToString();

            }
        }

        private void _ChangeTestTypeTitleAndImage()
        {
            switch (_testType)
            {
                case clsTestType.enTestTypes.VisionTest:
                    lblTitle.Text = "Vision Test ";
                    pbTestTypeImage.Image = Resources.Vision_512;
                    break;
                case clsTestType.enTestTypes.WrittenTest:
                    lblTitle.Text = "Written Test ";
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    break;
                case clsTestType.enTestTypes.StreetTest:
                    lblTitle.Text = "Street Test ";
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    break;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_testAppointment == null)
            {
                _testAppointment = new clsTestAppointment();
            }

            _testAppointment.TestTypeID = (int) clsTestType.GetTestTypeByID(_testType).TestTypeID;
            _testAppointment.LocalDrivingLicenseApplicationID = _localAppID;
            _testAppointment.AppointmentDate = dtpTestDate.Value;
            _testAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _testAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _testAppointment.IsLocked = false;

            if (_retakeTest)
            {
                if (_mode == enMode.AddNew)
                {
                    _reTakeTestApp = new clsApplication();
                    _reTakeTestApp.ApplicantPersonID = _localDrivingLicenseApplication.ApplicantPersonID;
                    _reTakeTestApp.ApplicationDate = DateTime.Now;
                    _reTakeTestApp.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                    _reTakeTestApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
                    _reTakeTestApp.LastStatusDate = DateTime.Now;
                    _reTakeTestApp.PaiedFees = clsApplicationType.GetApplicationTypeByID((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                    _reTakeTestApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                }


                if (_reTakeTestApp.Save())
                {
                    _testAppointment.RetakeTestApplicationID = _reTakeTestApp.ApplicationID;
                }
                else
                {
                    MessageBox.Show("Error while saving data");
                    return;
                }
            }

            

            if (_testAppointment.Save())
            {
                MessageBox.Show("Data saved successfully");
            }
            else
            {
                MessageBox.Show("Error while saving data");
            }
        }
   
    
    
    
    
    }
}
