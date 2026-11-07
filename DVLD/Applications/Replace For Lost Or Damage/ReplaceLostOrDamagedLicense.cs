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

namespace DVLD.Applications.Replace_For_Lost_Or_Damage
{
    public partial class ReplaceLostOrDamagedLicense : Form
    {
        private clsLicense.enIssueReasons _issueReason;
        private clsLicense _newLicense;
        public ReplaceLostOrDamagedLicense()
        {
            InitializeComponent();
        }

        private void driverLicenseControlWithFilter1_OnLicenseSelected(int obj)
        {
            if (!driverLicenseControlWithFilter1.SelectedLicense.IsActive)
            {
                MessageBox.Show("This license not acvtive you can not replace it");
                return;
            }

            btnIssueReplacement.Enabled = true;
            lblOldLicenseID.Text = driverLicenseControlWithFilter1.SelectedLicense.LicenseID.ToString();

        }

        private void _GetApplicationFees()
        {
            if (rbDamagedLicense.Checked)
            {
                lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByID((int)clsApplication.enApplicationType.ReplacementForADamagedDrivingLicense).ApplicationFees.ToString();
                _issueReason = clsLicense.enIssueReasons.DamagedReplacement;
            }
            else if (rbLostLicense.Checked)
            {
                lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByID((int)clsApplication.enApplicationType.ReplacementForALostDrivingLicense).ApplicationFees.ToString();
                _issueReason = clsLicense.enIssueReasons.LostReplacement;
            }
        }
        private void driverLicenseControlWithFilter1_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;
            rbDamagedLicense.Checked = true;
            _GetApplicationFees();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _GetApplicationFees();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            _GetApplicationFees();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            _newLicense = driverLicenseControlWithFilter1.SelectedLicense.Replace(_issueReason, clsGlobal.CurrentUser.UserID);

            if (_newLicense != null)
            {
                MessageBox.Show("New license issued successfullty with LicenseId: " + _newLicense.LicenseID);
                llShowLicenseInfo.Enabled = true;
                llShowLicenseHistory.Enabled = true;
                lblApplicationID.Text = _newLicense.ApplicationID.ToString();
                lblRreplacedLicenseID.Text = _newLicense.LicenseID.ToString();
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDrivingLicense show = new ShowDrivingLicense(_newLicense.LicenseID);
            show.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicenseHistory showHistory = new ShowPersonLicenseHistory(driverLicenseControlWithFilter1.SelectedLicense.DriverInfo.PersonID);
            showHistory.ShowDialog();
        }
    }
}
