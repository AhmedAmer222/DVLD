using DVLD.Licenses.Detained_Licenses;
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

namespace DVLD.Applications.Release_License_Applications
{
    public partial class ManageDetainedLicenses : Form
    {
        private DataTable _detainedLicenses;

        public ManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _LoadDate()
        {
            _detainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _detainedLicenses;
            lblDetainedLcensesRecords.Text = _detainedLicenses.Rows.Count.ToString();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            Detain_License detainLicense = new Detain_License();
            detainLicense.ShowDialog();
            _LoadDate();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            ReleaseLicense releaseLicense = new ReleaseLicense();
            releaseLicense.ShowDialog();
            _LoadDate();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            _LoadDate();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = clsLicense.GetLiecnseByID((int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value).DriverInfo.PersonID;

            PersonDeatils showPerosnDetails = new PersonDeatils(personId);
            showPerosnDetails.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = clsLicense.GetLiecnseByID((int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value).DriverInfo.PersonID;

            ShowPersonLicenseHistory showPerosnHistory = new ShowPersonLicenseHistory(personId);
            showPerosnHistory.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = clsLicense.GetLiecnseByID((int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value).LicenseID;

            ShowDrivingLicense ShowLicense = new ShowDrivingLicense(LicenseID);
            ShowLicense.ShowDialog();
        }

        private void cmsDetainedLicenses_Opening(object sender, CancelEventArgs e)
        {
            int DetainID = ((int)dgvDetainedLicenses.CurrentRow.Cells[0].Value);

            releaseDetainedLicenseToolStripMenuItem.Enabled = clsDetainedLicense.IsLicenseDetainedByDetainID(DetainID);
        }

        private void ManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _LoadDate();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = clsLicense.GetLiecnseByID((int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value).LicenseID;

            ReleaseLicense releaseLicense = new ReleaseLicense(LicenseID);
            releaseLicense.ShowDialog();
            _LoadDate();
        }
    }
}

