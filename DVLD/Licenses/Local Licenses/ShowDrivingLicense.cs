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
    public partial class ShowDrivingLicense : Form
    {
        private int _licenseId;
        public ShowDrivingLicense(int licenseID)
        {
            InitializeComponent();
            _licenseId = licenseID;
        }


        private void ShowDrivingLicense_Load(object sender, EventArgs e)
        {
            driverLicenseControl1.LoadLicenseByID(_licenseId);
        }
    }
}
