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
    public partial class ManageDrivers : Form
    {
        private DataTable _Drivers;
        public ManageDrivers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageDrivers_Load(object sender, EventArgs e)
        {
            _Drivers = clsDriver.GetAll();
            dgvDrivers.DataSource = _Drivers;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = (int) dgvDrivers.CurrentRow.Cells["PersonID"].Value;
            PersonDeatils personInof = new PersonDeatils(personId);
            personInof.ShowDialog();
            ManageDrivers_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = (int)dgvDrivers.CurrentRow.Cells["PersonID"].Value;
            ShowPersonLicenseHistory personLicenseHistory = new ShowPersonLicenseHistory(personId);
            personLicenseHistory.ShowDialog();
        }
    }
}
