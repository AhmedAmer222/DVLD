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

namespace DVLD.Applications.Local_Driving_Licenses_Applications
{
    public partial class LocalDrivingLicenseApplicationInfo : UserControl
    {
        private int _LocalAppId = 0;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private bool _EnableLicenseInfo = true;

        public bool EnableLicenseInfo
        { 
            set
            {
                _EnableLicenseInfo = value;
            }
            get
            {
                return _EnableLicenseInfo;
            }
        }
        public int SelectedAppId
        {
            get
            {
                return _LocalAppId;
            }
        }

        public clsLocalDrivingLicenseApplication SelectedLocalocalDrivingLicenseApplication
        {
            get
            {
                return _LocalDrivingLicenseApplication;
            }
        }

        public LocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
            
        }

        public void LoadLocalApplicationInfo(int localAppId)
        {
            _LocalAppId = localAppId;

            if (_LocalAppId > 0)
            {
                _LoadData();
            }
            else
            {
                MessageBox.Show("Error while loading data");
            }
        }

        private void _LoadData()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(_LocalAppId);

            applicationBasicInfo1.LoadBaseApplicationData(_LocalDrivingLicenseApplication.ApplicationID);

            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = clsTest.GetTestPassed(_LocalAppId).ToString();
            llShowLicenceInfo.Enabled = _EnableLicenseInfo;
        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDeatils person = new PersonDeatils(_LocalDrivingLicenseApplication.ApplicantPersonID);
            person.ShowDialog();
        }
    }
}
