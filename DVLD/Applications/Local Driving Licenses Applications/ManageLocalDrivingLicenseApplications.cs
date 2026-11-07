using DVLD.Applications.Local_Driving_Licenses_Applications;
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
    public partial class ManageLocalDrivingLicenseApplications : Form
    {
        DataTable _dtLocalLicenseApplications;
        public ManageLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            _dtLocalLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dataGridView1.DataSource = _dtLocalLicenseApplications;
            TotalNumber.Text = dataGridView1.Rows.Count.ToString();
        }

        private void _OpencancelApplicationMenu()
        {
            updateLocalLicenseAppToolStripMenuItem.Enabled = false;
            deleteLocalLicenseAppToolStripMenuItem.Enabled = false;
            cancelApplicationToolStripMenuItem.Enabled = false;
            sechduleTestToolStripMenuItem1.Enabled = false;
            issueLicensetoolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;
        }

        private void _OpenCompleteApplicationMenu()
        {
            updateLocalLicenseAppToolStripMenuItem.Enabled = false;
            deleteLocalLicenseAppToolStripMenuItem.Enabled = false;
            cancelApplicationToolStripMenuItem.Enabled = false;
            sechduleTestToolStripMenuItem1.Enabled = false;

            int localAppId = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication localApp = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(localAppId);

            bool DoesHasLicense = clsLocalDrivingLicenseApplication.DoesPersonHaveLicenseForLicenseClass(localApp.ApplicantPersonID, localApp.LicenseClassesID); ;
            issueLicensetoolStripMenuItem.Enabled = !DoesHasLicense;
            showLicenseToolStripMenuItem.Enabled = DoesHasLicense;
        }

        private void _OpenNewApplicationMenu()
        {
            updateLocalLicenseAppToolStripMenuItem.Enabled = true;
            deleteLocalLicenseAppToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem.Enabled = true;
            sechduleTestToolStripMenuItem1.Enabled = true;
            issueLicensetoolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;
        }

        private void _OpenCurrentTest()
        {
            int PassedTest = (int) dataGridView1.CurrentRow.Cells["PassedTests"].Value;

            switch(PassedTest)
            {
                case 0:
                    sechduleVisionTestToolStripMenuItem.Enabled = true;
                    sechduleWrittenTestToolStripMenuItem.Enabled = false;
                    sechduleStreetTestToolStripMenuItem.Enabled = false;
                    break;
                case 1:
                    sechduleVisionTestToolStripMenuItem.Enabled = false;
                    sechduleWrittenTestToolStripMenuItem.Enabled = true;
                    sechduleStreetTestToolStripMenuItem.Enabled = false;
                    break;
                case 2:
                    sechduleVisionTestToolStripMenuItem.Enabled = false;
                    sechduleWrittenTestToolStripMenuItem.Enabled = false;
                    sechduleStreetTestToolStripMenuItem.Enabled = true;
                    break;
                case 3:
                    sechduleVisionTestToolStripMenuItem.Enabled = false;
                    sechduleWrittenTestToolStripMenuItem.Enabled = false;
                    sechduleStreetTestToolStripMenuItem.Enabled = false;
                    break;
                default:
                    sechduleVisionTestToolStripMenuItem.Enabled = false;
                    sechduleWrittenTestToolStripMenuItem.Enabled = false;
                    sechduleStreetTestToolStripMenuItem.Enabled = false;
                    break;
            }
        }

        private void RefreshData(int localAppId)
        {
            if (localAppId > 0)
            {
                _LoadData();
            }
        }
        private void FilterList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadData();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "LDLA ID";
                dataGridView1.Columns[0].Width = 90;

                dataGridView1.Columns[1].HeaderText = "Application ID";
                dataGridView1.Columns[1].Width = 110;

                dataGridView1.Columns[2].HeaderText = "National No.";
                dataGridView1.Columns[2].Width = 110;


                dataGridView1.Columns[3].HeaderText = "Full Name";
                dataGridView1.Columns[3].Width = 250;

                dataGridView1.Columns[4].HeaderText = "Application Date";
                dataGridView1.Columns[4].Width = 190;


                dataGridView1.Columns[5].HeaderText = "Passed Test";
                dataGridView1.Columns[5].Width = 90;

                dataGridView1.Columns[6].HeaderText = "Application Status";
                dataGridView1.Columns[6].Width = 120;
            }

            _OpencancelApplicationMenu();
        }

       

        private void updateLocalLicenseAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localAppId = (int) dataGridView1.CurrentRow.Cells[0].Value;
            Add_UpdateLocalDrivingLicenseApplication newApp = new Add_UpdateLocalDrivingLicenseApplication(localAppId);
            newApp.DataBack += RefreshData;
            newApp.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add_UpdateLocalDrivingLicenseApplication newApp = new Add_UpdateLocalDrivingLicenseApplication();
            newApp.DataBack += RefreshData;
            newApp.ShowDialog();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppStatus = dataGridView1.CurrentRow.Cells["ApplicationStatus"].Value.ToString();

            if (AppStatus == "New")
            {
                int appid = (int)dataGridView1.CurrentRow.Cells[0].Value;

                if (clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(appid).Cancel())
                {
                    MessageBox.Show("Data saved sucessfully");
                    _LoadData();
                }
                else
                {
                    MessageBox.Show("Error while saving data");
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
            string AppStatus = dataGridView1.CurrentRow.Cells["ApplicationStatus"].Value.ToString();

            if (AppStatus == "Cancelled")
            {              
                _OpencancelApplicationMenu();
            }
            else if(AppStatus == "Completed")
            {
                _OpenCompleteApplicationMenu();
            }
            else
            {
                _OpenCurrentTest();
                _OpenNewApplicationMenu();
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localAppid = (int) dataGridView1.CurrentRow.Cells[0].Value;

            ShowLocalDrivingLicenseApplicationInfo show = new ShowLocalDrivingLicenseApplicationInfo(localAppid);
            show.ShowDialog();
        }

        private void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localAppid = (int)dataGridView1.CurrentRow.Cells[0].Value;

            ManageTestAppointments testAppointments = new ManageTestAppointments(localAppid, clsTestType.enTestTypes.VisionTest);
            testAppointments.ShowDialog();
            _LoadData();

        }

        private void sechduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localAppid = (int)dataGridView1.CurrentRow.Cells[0].Value;

            ManageTestAppointments testAppointments = new ManageTestAppointments(localAppid, clsTestType.enTestTypes.WrittenTest);
            testAppointments.ShowDialog();
            _LoadData();
        }

        private void sechduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localAppid = (int)dataGridView1.CurrentRow.Cells[0].Value;

            ManageTestAppointments testAppointments = new ManageTestAppointments(localAppid, clsTestType.enTestTypes.StreetTest);
            testAppointments.ShowDialog();
            _LoadData();
        }

        private void issueLicensetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localAppId = (int) dataGridView1.CurrentRow.Cells[0].Value;
            IssueDriverLicenseFirstTime issueFirstTime = new IssueDriverLicenseFirstTime(localAppId);
            issueFirstTime.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int appId = (int)dataGridView1.CurrentRow.Cells[1].Value;
            clsLicense license = clsLicense.GetLiecnseByApplicatioID(appId);
            ShowDrivingLicense showLicense = new ShowDrivingLicense(license.LicenseID);
            showLicense.ShowDialog();
        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppId = (int)dataGridView1.CurrentRow.Cells[1].Value;

            clsApplication app = clsApplication.GetBaseApplicationInfo(AppId);
            ShowPersonLicenseHistory showLicenseHistory = new ShowPersonLicenseHistory(app.ApplicantPersonID);
            showLicenseHistory.ShowDialog();
        }
    }
}
