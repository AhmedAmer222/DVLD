using DVLD.Applications.International_License_Applications;
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
    public partial class ShowPersonLicenseHistory : Form
    {
        private int _appID = 0;
        private int _personId = 0;
        private DataTable _localLicenses;
        private DataTable _InternationalLicenses;
       
        public ShowPersonLicenseHistory(int personId)
        {
            InitializeComponent();
            _personId = personId;
        }

        private void ShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {

            if (_personId > 0)
            {
                personCardWithFilter1.FilterEnabled = false;
                personCardWithFilter1.loadForSpecificID(_personId);

                _localLicenses = clsLicense.GetAllLicensesForPerson(_personId);
                _InternationalLicenses = clsInternationalDrivingLicense.GetAllInternationalLicensesForPerson(_personId);
                dgvLocalLicensesHistory.DataSource = _localLicenses;
                dgvInternationalLicensesHistory.DataSource = _InternationalLicenses;
            }
        }

        private void personCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDrivingLicense showLicenseInfo = new ShowDrivingLicense((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            showLicenseInfo.ShowDialog();
        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInternationalLicense showInternationlLicense = new ShowInternationalLicense((int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value);
            showInternationlLicense.ShowDialog();
        }
    }
}
