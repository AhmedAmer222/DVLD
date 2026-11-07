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

namespace DVLD.Applications.International_License_Applications
{
    public partial class InternationalLicenseControl : UserControl
    {
        private int _internationalLicenseId;
        private clsInternationalDrivingLicense InternationalDrivingLicense;
        
        public InternationalLicenseControl()
        {
            InitializeComponent();
        }


        public void LoadLicense(int InternationalLicenseId)
        {
            _internationalLicenseId = InternationalLicenseId;

            InternationalDrivingLicense = clsInternationalDrivingLicense.GetInternationalLicenseByID(_internationalLicenseId);

            lblFullName.Text = InternationalDrivingLicense.DriverInfo.PersonInfo.FullName;
            lblInternationalLicenseID.Text = InternationalDrivingLicense.InternationalDrivingLicenseID.ToString();
            lblLocalLicenseID.Text = InternationalDrivingLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = InternationalDrivingLicense.DriverInfo.PersonInfo.NationalNo;

            if (InternationalDrivingLicense.DriverInfo.PersonInfo.Gender == 0)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";
            }

            lblIssueDate.Text = InternationalDrivingLicense.IssueDate.ToShortDateString();

            lblApplicationID.Text = InternationalDrivingLicense.ApplicationID.ToString();

            if (InternationalDrivingLicense.IsActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }

            lblDateOfBirth.Text = InternationalDrivingLicense.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();

            lblDriverID.Text = InternationalDrivingLicense.DriverID.ToString();
            lblExpirationDate.Text = InternationalDrivingLicense.ExpirationDate.ToShortDateString();


            if (InternationalDrivingLicense.DriverInfo.PersonInfo.ImagePath == "")
            {
                if (InternationalDrivingLicense.DriverInfo.PersonInfo.Gender == 0)
                {
                    pbPersonImage.Image = Resources.Male_512;
                }
                else
                {
                    pbPersonImage.Image = Resources.Female_512;
                }
                
            }
            else
            {
                pbPersonImage.ImageLocation = InternationalDrivingLicense.DriverInfo.PersonInfo.ImagePath;
            }
        }
        
    }
}
