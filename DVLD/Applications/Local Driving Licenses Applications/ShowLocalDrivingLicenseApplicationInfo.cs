using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_Licenses_Applications
{
    public partial class ShowLocalDrivingLicenseApplicationInfo : Form
    {
        private int _localAppId;
        public ShowLocalDrivingLicenseApplicationInfo(int localAppId)
        {
            InitializeComponent();

            _localAppId = localAppId;
        }

        private void ShowLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            localDrivingLicenseApplicationInfo2.LoadLocalApplicationInfo(_localAppId);
        }

    }
}
