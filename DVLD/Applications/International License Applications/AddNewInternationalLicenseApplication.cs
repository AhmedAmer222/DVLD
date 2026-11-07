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

namespace DVLD.Applications.International_License_Applications
{
    public partial class AddNewInternationalLicenseApplication : Form
    {
        private clsInternationalDrivingLicense _NewInternationalLicense;
        private clsApplication _InternationalLicenseApp;
        public AddNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void _SetbuttonsToDefault()
        {
            llNewLicenseInfo.Enabled = false;
            llShowLicensesHistory.Enabled = false;
            btnIssue.Enabled = false;
        }

        private void driverLicenseControlWithFilter1_OnLicenseSelected(int obj)
        {
            if (driverLicenseControlWithFilter1.SelectedLicense.IsLicenseExpired())
            {
                _SetbuttonsToDefault();
                MessageBox.Show($"This license with ID {driverLicenseControlWithFilter1.LicenseId} expired renew it first");
                return;
            }

            if (!driverLicenseControlWithFilter1.SelectedLicense.IsActive)
            {
                _SetbuttonsToDefault();
                MessageBox.Show($"This license with ID {driverLicenseControlWithFilter1.LicenseId} not active please contact your admin");
                return;
            }

            if (clsInternationalDrivingLicense.DoesDriverHasActiveInternationalLicense(driverLicenseControlWithFilter1.SelectedLicense.DriverID)) 
            {
                _SetbuttonsToDefault();
                MessageBox.Show($"This driver already has active international license");
                llShowLicensesHistory.Enabled = true;
                return;
            }

            if (!clsDriver.DoesHasActiveLicenseForLicenseClass(driverLicenseControlWithFilter1.SelectedLicense.DriverID, 3))
            {
                _SetbuttonsToDefault();
                MessageBox.Show($"You must have acvtive Class 3 license to get an international license");
                llShowLicensesHistory.Enabled = true;
                return;
            }

            lblLocalLicenseID.Text = driverLicenseControlWithFilter1.SelectedLicense.LicenseID.ToString();

            llShowLicensesHistory.Enabled = true;
            btnIssue.Enabled = true;
        }

        private void driverLicenseControlWithFilter1_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();

            float AppFees = clsApplicationType
               .GetApplicationTypeByID(
                   Convert.ToInt32(clsApplication.enApplicationType.NewInternationalLicense)
               ).ApplicationFees;

            lblApplicationFees.Text = AppFees.ToString();

            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (clsInternationalDrivingLicense.DoesDriverHasActiveInternationalLicense(driverLicenseControlWithFilter1.SelectedLicense.DriverID))
            {
                MessageBox.Show($"This driver already has active international license");
                return;
            }

            _InternationalLicenseApp  = new clsApplication();

            _InternationalLicenseApp.ApplicantPersonID = driverLicenseControlWithFilter1.Driver.PersonID;
            _InternationalLicenseApp.ApplicationDate = DateTime.Now;
            _InternationalLicenseApp.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicenseService;
            _InternationalLicenseApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _InternationalLicenseApp.LastStatusDate = DateTime.Now;

            if (float.TryParse(lblApplicationFees.Text, out float applicationFees))
            {
                _InternationalLicenseApp.PaiedFees = applicationFees;
            }

            _InternationalLicenseApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_InternationalLicenseApp.Save())
            {
                _NewInternationalLicense = new clsInternationalDrivingLicense();

                _NewInternationalLicense.ApplicationID = _InternationalLicenseApp.ApplicationID;
                _NewInternationalLicense.DriverID = driverLicenseControlWithFilter1.SelectedLicense.DriverID;
                _NewInternationalLicense.IssuedUsingLocalLicenseID = driverLicenseControlWithFilter1.LicenseId;
                _NewInternationalLicense.IssueDate = DateTime.Now;
                _NewInternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
                _NewInternationalLicense.IsActive = true;
                _NewInternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (_NewInternationalLicense.Save())
                {
                    if (_InternationalLicenseApp.SetComplete())
                    {
                        llNewLicenseInfo.Enabled = true;
                        btnIssue.Enabled = false;
                        MessageBox.Show("Internationl License issued successfully with ID:" + _NewInternationalLicense.InternationalDrivingLicenseID);
                    }
                    else
                    {
                        _InternationalLicenseApp.Delete();
                        _NewInternationalLicense.Delete();
                        MessageBox.Show("Error while saving data");
                    }
                }
                else
                {
                    _InternationalLicenseApp.Delete();
                    MessageBox.Show("Error while saving data");
                }
            }
            else
            {
                MessageBox.Show("Error while saving data");
            }
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int personid = driverLicenseControlWithFilter1.SelectedLicense.DriverInfo.PersonInfo.PersonID;
            ShowPersonLicenseHistory showPersonLicenseHistory = new ShowPersonLicenseHistory(personid);
            showPersonLicenseHistory.ShowDialog();
        }

        private void llNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInternationalLicense showLicnse = new ShowInternationalLicense(_NewInternationalLicense.InternationalDrivingLicenseID);
            showLicnse.ShowDialog();
        }
    }
}
