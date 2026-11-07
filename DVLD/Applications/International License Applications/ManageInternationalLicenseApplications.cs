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
    public partial class ManageInternationalLicenseApplications : Form
    {
        private DataTable _internationalLicenses;
        
        public ManageInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            _internationalLicenses = clsInternationalDrivingLicense.GetAllInternationalLicenses();
            dgvInternationalLicenses.DataSource = _internationalLicenses;
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void ManageInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = clsDriver.GetByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells["DriverID"].Value).PersonID;

            PersonDeatils showPerosnDetails = new PersonDeatils(personId);
            showPerosnDetails.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = clsDriver.GetByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells["DriverID"].Value).PersonID;

            ShowPersonLicenseHistory showLicenseHistoy = new ShowPersonLicenseHistory(personId);
            showLicenseHistoy.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseId = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;
            ShowInternationalLicense showLicense = new ShowInternationalLicense(LicenseId);
            showLicense.ShowDialog();
        }
    }
}
