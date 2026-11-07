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

namespace DVLD
{
    public partial class IssueDriverLicenseFirstTime : Form
    {
        private int _LocalAppId = 0;
        private clsLicense _NewLicense = new clsLicense();
        private clsDriver _Driver;
        public IssueDriverLicenseFirstTime(int localAppId)
        {
            InitializeComponent();
            _LocalAppId = localAppId;
        }

        private void localDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {
            localDrivingLicenseApplicationInfo1.EnableLicenseInfo = false;
            localDrivingLicenseApplicationInfo1.LoadLocalApplicationInfo(_LocalAppId);
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localApp = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(_LocalAppId);

            _Driver = clsDriver.GetByPersonID(localApp.ApplicantPersonID);
            bool IsFirstLicense = false;

            if (_Driver == null)
            {
                _Driver = new clsDriver();
                _Driver.PersonID = localApp.ApplicantPersonID;
                _Driver.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                _Driver.CreatedDate = DateTime.Now;
                IsFirstLicense = true;

                if (!_Driver.Save())
                {
                    MessageBox.Show("Error while saving Driver Data");
                    return;
                }
            }

            _NewLicense.ApplicationID = localApp.ApplicationID;
            _NewLicense.DriverID = _Driver.DriverID;
            _NewLicense.LicenseClassID = localApp.LicenseClassesID;
            _NewLicense.IssueDate = DateTime.Now;
            clsLicenseClass LicenseClass = clsLicenseClass.GetLicenseClassByID(localApp.LicenseClassesID);
            _NewLicense.ExpirationDate = DateTime.Now.AddYears(LicenseClass.DefaultValidityLength);
            _NewLicense.Notes = txtNotes.Text;
            _NewLicense.PaidFees = LicenseClass.ClassFees;
            _NewLicense.IsActive = true;
            _NewLicense.IssueReason = clsLicense.enIssueReasons.FirstTime;
            _NewLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_NewLicense.Save())
            {
                MessageBox.Show("License issued successfully with LicenseId = " + _NewLicense.LicenseID);
            }
            else
            {
                if (IsFirstLicense)
                {
                    clsDriver.Delete(_Driver.DriverID);
                }

                MessageBox.Show("Error while saving Data");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
