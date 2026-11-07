using DVLD.GLobal_Classes;
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

namespace DVLD.Applications.Release_License_Applications
{
    public partial class ReleaseLicense : Form
    {
        public enum enMode
        {
            Search = 0,
            SelectLicense = 1
        }

        private enMode _mode;

        private clsDetainedLicense _detainedLicense;
        private int _releasedLicenseId = 0;
        public ReleaseLicense()
        {
            InitializeComponent();
            _mode = enMode.Search;
        }

        public ReleaseLicense(int releasedLicenseId)
        {
            InitializeComponent();
            _releasedLicenseId = releasedLicenseId;
            _mode = enMode.SelectLicense;
        }

        private void driverLicenseControlWithFilter1_Load(object sender, EventArgs e)
        {
            

            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;
            lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByID(
                (int) clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();

            if (_mode == enMode.SelectLicense)
            {
                driverLicenseControlWithFilter1.FilterEnable = false;
                driverLicenseControlWithFilter1.LoadLicense(_releasedLicenseId);
            }
        }

        private void driverLicenseControlWithFilter1_OnLicenseSelected(int obj)
        {
            if (!clsDetainedLicense.IsLicenseDetained(obj))
            {
                btnRelease.Enabled = false;
                llShowLicensesHistory.Enabled = true;
                llLicenseInfo.Enabled = false;
                MessageBox.Show($"This License not detained");
                return;
            }

            _detainedLicense = clsDetainedLicense.GetDetainedLicenseByLicenseID(driverLicenseControlWithFilter1.LicenseId);

            lblDetainID.Text = _detainedLicense.DetainID.ToString();
            lblDetainDate.Text = _detainedLicense.DetainDate.ToShortDateString();
            lblFineFees.Text = _detainedLicense.FineFees.ToString();

            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + _detainedLicense.FineFees).ToString();

            lblLicenseID.Text = _detainedLicense.LicenseID.ToString();

            llShowLicensesHistory.Enabled = true;
            btnRelease.Enabled = true;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (!clsDetainedLicense.IsLicenseDetained(driverLicenseControlWithFilter1.LicenseId))
            {
                btnRelease.Enabled = false;
                llShowLicensesHistory.Enabled = true;
                llLicenseInfo.Enabled = false;
                MessageBox.Show($"This License not detained");
                return;
            }

            if (_detainedLicense.ReleaseDetainedLicense(DateTime.Now, clsGlobal.CurrentUser.UserID))
            {
                MessageBox.Show("License released successfully");
                lblApplicationID.Text = _detainedLicense.ReleaseApplicationID.ToString();
                llLicenseInfo.Enabled = true;
                btnRelease.Enabled = false;
            }
            else
            {
                MessageBox.Show("Data not saved successfully");
                llLicenseInfo.Enabled = false;
                btnRelease.Enabled = true;
            }
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDrivingLicense showLicnse = new ShowDrivingLicense(driverLicenseControlWithFilter1.LicenseId);
            showLicnse.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int personid = driverLicenseControlWithFilter1.SelectedLicense.DriverInfo.PersonInfo.PersonID;
            ShowPersonLicenseHistory showPersonLicenseHistory = new ShowPersonLicenseHistory(personid);
            showPersonLicenseHistory.ShowDialog();
        }
    }
}
