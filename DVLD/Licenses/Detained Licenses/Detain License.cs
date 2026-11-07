using DVLD.Applications.International_License_Applications;
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

namespace DVLD.Licenses.Detained_Licenses
{
    public partial class Detain_License : Form
    {
        private clsDetainedLicense _detainLicense;
        public Detain_License()
        {
            InitializeComponent();
        }

        private void driverLicenseControlWithFilter1_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;
        }

       
        private void driverLicenseControlWithFilter1_OnLicenseSelected(int obj)
        {
            if (driverLicenseControlWithFilter1.SelectedLicense.IsLicenseExpired())
            {
                btnDetain.Enabled = false;
                llShowLicensesHistory.Enabled = true;
                llLicenseInfo.Enabled = true;
                MessageBox.Show($"This license with ID {driverLicenseControlWithFilter1.LicenseId} expired renew it first");
                return;
            }

            if (!driverLicenseControlWithFilter1.SelectedLicense.IsActive)
            {
                btnDetain.Enabled = false;
                llShowLicensesHistory.Enabled = true;
                llLicenseInfo.Enabled = true;
                MessageBox.Show($"This license with ID {driverLicenseControlWithFilter1.LicenseId} not active");
                return;
            }

            if (clsDetainedLicense.IsLicenseDetained(driverLicenseControlWithFilter1.LicenseId))
            {
                btnDetain.Enabled = false;
                llShowLicensesHistory.Enabled = true;
                llLicenseInfo.Enabled = true;
                MessageBox.Show($"This License already detained");
                return;
            }

            lblLicenseID.Text = driverLicenseControlWithFilter1.LicenseId.ToString();
            btnDetain.Enabled = true;
            llShowLicensesHistory.Enabled = true;
            llLicenseInfo.Enabled = true;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (clsDetainedLicense.IsLicenseDetained(driverLicenseControlWithFilter1.LicenseId))
            {
                btnDetain.Enabled = false;
                llShowLicensesHistory.Enabled = true;
                llLicenseInfo.Enabled = true;
                MessageBox.Show($"This License already detained");
                return;
            }

            this.ValidateChildren(ValidationConstraints.None);

            if (clsUtil.HasValidationErrors(errorProvider1, gpDetainLicens))
            {
                MessageBox.Show("Please enter fine fees first");
                return;
            }
            
            _detainLicense = new clsDetainedLicense();

            _detainLicense.LicenseID = driverLicenseControlWithFilter1.LicenseId;
            _detainLicense.DetainDate = DateTime.Now;

            if (txtFineFees.Text != "" && float.TryParse(txtFineFees.Text, out float fees))
            {
                _detainLicense.FineFees = fees;
            }

            _detainLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_detainLicense.Save())
            {
                MessageBox.Show("License detained successfully");
                btnDetain.Enabled = false;
            }
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text))
            {
                errorProvider1.SetError(txtFineFees, "Enter Fine Fees");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, "");
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

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void gpDetainLicens_Enter(object sender, EventArgs e)
        {

        }

        private void txtFineFees_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblLicenseID_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void lblDetainDate_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblCreatedByUser_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblDetainID_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
