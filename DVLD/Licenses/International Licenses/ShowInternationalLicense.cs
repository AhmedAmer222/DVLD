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
    public partial class ShowInternationalLicense : Form
    {
        private int _internationalLicenseID;
        public ShowInternationalLicense(int internationalLicense)
        {
            InitializeComponent();
            _internationalLicenseID = internationalLicense;
        }

        private void ShowInternationalLicense_Load(object sender, EventArgs e)
        {
            internationalLicenseControl1.LoadLicense(_internationalLicenseID);
        }
    }
}
