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

namespace DVLD.Applications.Renew_Driving_License_Application
{
    public partial class ReNewDrivingLicense : Form
    {
        private int _licenseId = 0;
        private clsLicense _Newlicense;
        private clsApplication _renewaLicensepplication;
       
        
        public ReNewDrivingLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void driverLicenseControlWithFilter1_OnLicenseSelected(int obj)
        {
            if (driverLicenseControlWithFilter1.SelectedLicense.ExpirationDate < DateTime.Now)
            {
                _licenseId = obj;
                lblOldLicenseID.Text = _licenseId.ToString();

                float LicensFees = clsLicenseClass.GetLicenseClassByID(driverLicenseControlWithFilter1.SelectedLicense.LicenseClassID).ClassFees;
                lblLicenseFees.Text = LicensFees.ToString();
               
                if (float.TryParse(lblApplicationFees.Text, out float applicationFees))
                {
                    lblTotalFees.Text = (applicationFees + LicensFees).ToString(); 
                }

                int licenseYears = clsLicenseClass.GetLicenseClassByID(driverLicenseControlWithFilter1.SelectedLicense.LicenseClassID).DefaultValidityLength;
                lblExpirationDate.Text = DateTime.Now.AddYears(licenseYears).ToShortDateString();

                btnRenew.Enabled = true;
            }
            else
            {
                MessageBox.Show("This license not expired yet");
                btnRenew.Enabled = false;
            }

            llShowLicensesHistory.Enabled = true;
        }

        
        private void ReNewDrivingLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();

            float AppFees = clsApplicationType
               .GetApplicationTypeByID(
                   Convert.ToInt32(clsApplication.enApplicationType.RenewDrivingLicenseService)
               ).ApplicationFees;

            lblApplicationFees.Text = AppFees.ToString();

            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if(clsLicense.DeactivateLiecnse(driverLicenseControlWithFilter1.LicenseId))
            {
                _Newlicense = new clsLicense();
                _renewaLicensepplication = new clsApplication();

                _renewaLicensepplication.ApplicantPersonID = driverLicenseControlWithFilter1.Driver.PersonID;
                _renewaLicensepplication.ApplicationDate = DateTime.Now;
                _renewaLicensepplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicenseService;
                _renewaLicensepplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
                _renewaLicensepplication.LastStatusDate = DateTime.Now;

                if (float.TryParse(lblApplicationFees.Text, out float applicationFees))
                {
                    _renewaLicensepplication.PaiedFees = applicationFees;
                }

                _renewaLicensepplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (_renewaLicensepplication.Save())
                {
                    _Newlicense.ApplicationID = _renewaLicensepplication.ApplicationID;
                    _Newlicense.DriverID = driverLicenseControlWithFilter1.Driver.DriverID;
                    _Newlicense.LicenseClassID = driverLicenseControlWithFilter1.SelectedLicense.LicenseClassID;
                    _Newlicense.IssueDate = DateTime.Now;

                    int licenseYears = clsLicenseClass.GetLicenseClassByID(driverLicenseControlWithFilter1.SelectedLicense.LicenseClassID).DefaultValidityLength;
                    _Newlicense.ExpirationDate = DateTime.Now.AddYears(licenseYears);

                    if (float.TryParse(lblLicenseFees.Text, out float LicenseFees))
                    {
                        _Newlicense.PaidFees = LicenseFees;
                    }

                    _Newlicense.IsActive = true;

                    _Newlicense.IssueReason = clsLicense.enIssueReasons.Renew;
                    _Newlicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                    if(_Newlicense.Save())
                    {
                        if (_renewaLicensepplication.SetComplete())
                        {
                            MessageBox.Show("New license issued successfully with LicenseId:" + _Newlicense.LicenseID);
                            llNewLicenseInfo.Enabled = true;
                        }
                        else
                        {
                            _renewaLicensepplication.Delete();
                            clsLicense.Delete(_Newlicense.LicenseID);
                            MessageBox.Show("Error While saving data");
                        }

                    }
                    else
                    {
                        _renewaLicensepplication.Delete();
                        MessageBox.Show("Error While saving data");
                    }

                }
                else
                {
                    MessageBox.Show("Error while saving data");
                }
            }
            else
            {
                MessageBox.Show("Error while saving data");
            }


        }

        private void llNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDrivingLicense showLicense = new ShowDrivingLicense(_Newlicense.LicenseID);
            showLicense.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonLicenseHistory showLicensesHistory = new ShowPersonLicenseHistory(driverLicenseControlWithFilter1.Driver.PersonID);
            showLicensesHistory.ShowDialog();

        }
    }
}
