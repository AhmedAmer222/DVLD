using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class DriverLicenseControl : UserControl
    {
        private clsLicense _license;
        //private clsLocalDrivingLicenseApplication _localApp;

        public clsLicense SelectedLicense
        {
            get
            {
                return _license;
            }
        }
        public DriverLicenseControl()
        {
            InitializeComponent();
        }

        public clsDriver Driver
        {
            get
            {
                return clsDriver.GetByDriverID(_license.DriverID);
            }
        }

        public void LoadLicenseByAppicationID(int AppId)
        {
            _license = clsLicense.GetLiecnseByApplicatioID(AppId);
            //_localApp = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByApplicationID(AppId);

            if (_license == null)
            {
                MessageBox.Show("Error while loading data");
                return;
            }
            else
            {
                LoadLicenseData();
            }
        }

        public void LoadLicenseByID(int LicenseId)
        {
            _license = clsLicense.GetLiecnseByID(LicenseId);

            if (_license == null)
            {
                MessageBox.Show("Error while loading data");
                return;
            }
            else
            {
                LoadLicenseData();
            }
        }

        private void LoadLicenseData()
        {
            lblClass.Text = _license.LicenseClassInfo.ClassName;
            lblFullName.Text = _license.ApplicationInfo.ApplicantFullName;
            lblLicenseID.Text = _license.LicenseID.ToString();
            lblNationalNo.Text = _license.ApplicationInfo.ApplicantPersonInfo.NationalNo;

            if (_license.ApplicationInfo.ApplicantPersonInfo.Gender == 0)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";
            }

            lblIssueDate.Text = _license.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _license.ExpirationDate.ToShortDateString();

            switch (_license.IssueReason)
            {
                case clsLicense.enIssueReasons.FirstTime:
                    lblIssueReason.Text = "First Time";
                    break;
                case clsLicense.enIssueReasons.Renew:
                    lblIssueReason.Text = "Renew License";
                    break;
                case clsLicense.enIssueReasons.DamagedReplacement:
                    lblIssueReason.Text = "Replace for damage";
                    break;
                case clsLicense.enIssueReasons.LostReplacement:
                    lblIssueReason.Text = "Replcae for lost";
                    break;
                default:
                    lblIssueReason.Text = "[????]";
                    break;
            }

            lblNotes.Text = _license.Notes.ToString();

            if (_license.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

            lblDateOfBirth.Text = _license.ApplicationInfo.ApplicantPersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _license.DriverID.ToString();


            if (clsDetainedLicense.IsLicenseDetained(_license.LicenseID))
            {
                lblIsDetained.Text = "Yes";
            }
            else
            {
                lblIsDetained.Text = "No";
            }

            if (!string.IsNullOrEmpty(_license.ApplicationInfo.ApplicantPersonInfo.ImagePath))
            {
                pbPersonImage.ImageLocation = _license.ApplicationInfo.ApplicantPersonInfo.ImagePath;
            }
            else
            {
                if (_license.ApplicationInfo.ApplicantPersonInfo.Gender == 0)
                {
                    pbPersonImage.Image = Resources.Male_512;
                }
                else
                {
                    pbPersonImage.Image = Resources.Female_512;
                }
            }
        }
    }
}
