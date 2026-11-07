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

namespace DVLD.Licenses.Controls
{
    public partial class DriverLicenseControlWithFilter : UserControl
    {
        private int _licenseId;
        private clsLicense _license;
        private bool _filterEnable = true;

        public int LicenseId
        {
            get { return _licenseId; }
        }

        public clsLicense SelectedLicense
        {
            get
            {
                return _license;
            }
        }

        public bool FilterEnable
        {
            set
            {
                _filterEnable = value;
                gbFilters.Enabled = _filterEnable;
            }
            get
            {
                return _filterEnable;
            }
        }

        public clsDriver Driver
        {
            get
            {
                return driverLicenseControl1.Driver;
            }
        }
        public DriverLicenseControlWithFilter()
        {
            InitializeComponent();
        }


        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtLicenseID.Text == "")
            {
                MessageBox.Show("Please enter licenseId");
                return;
            }

            int LicenseId = Convert.ToInt32(txtLicenseID.Text);

            if (clsLicense.IsLiecnseExist(LicenseId))
            {
                driverLicenseControl1.LoadLicenseByID(LicenseId);
                _licenseId = LicenseId;
                _license = driverLicenseControl1.SelectedLicense;
            }
            else
            {
                MessageBox.Show("There is no license wiht LicenseId:" + LicenseId);
                _filterEnable = true;
            }

            if (OnLicenseSelected != null && _filterEnable && driverLicenseControl1.SelectedLicense != null)
            {
                LicenseSelected(_licenseId);
            }
        }

        public void LoadLicense(int LicenseId)
        {
            _license = clsLicense.GetLiecnseByID(LicenseId);
            _licenseId = LicenseId;

            if (clsLicense.IsLiecnseExist(LicenseId))
            {
                driverLicenseControl1.LoadLicenseByID(LicenseId);
            }
            else
            {
                MessageBox.Show("There is no license wiht LicenseId:" + LicenseId);
                _filterEnable = true;
            }
            _filterEnable = false;

            txtLicenseID.Text = LicenseId.ToString();

            if (OnLicenseSelected != null)
            {
                LicenseSelected(LicenseId);
            }
        }




        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        
    }
}
