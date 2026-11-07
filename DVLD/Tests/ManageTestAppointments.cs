using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ManageTestAppointments : Form
    {
        private int _localAppID = 0;
        clsTestType.enTestTypes _TestType;
        DataTable _testAppoinetments;

        public ManageTestAppointments(int localAppID, clsTestType.enTestTypes testType)
        {
            InitializeComponent();

            _localAppID = localAppID;
            _TestType = testType;
        }

        private void _ChangeTestTypeTitleAndImage()
        {
            switch (_TestType)
            {
                case clsTestType.enTestTypes.VisionTest:
                    lblTitle.Text = "Vision Test Appointments";
                    break;
                case clsTestType.enTestTypes.WrittenTest:
                    lblTitle.Text = "Written Test Appointments";
                    break;
                case clsTestType.enTestTypes.StreetTest:
                    lblTitle.Text = "Street Test Appointments";
                    break;

            }

        }

        private void _LoadAppointments()
        {
            _testAppoinetments = clsTestAppointment.GetAllTestAppointmentsForSpecificTest(_localAppID, (int)_TestType);
            dgvLicenseTestAppointments.DataSource = _testAppoinetments;
        }

        private void ManageTestAppointments_Load(object sender, EventArgs e)
        {
            _ChangeTestTypeTitleAndImage();
            localDrivingLicenseApplicationInfo1.LoadLocalApplicationInfo(_localAppID);
            _LoadAppointments();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {

            if (_testAppoinetments.Rows.Count > 0)
            {
                clsTestAppointment lastTestAppointment = clsTestAppointment.GetLastTestAppointment(_localAppID, _TestType);

                if (lastTestAppointment.IsLocked)
                {
                    clsTest lastTest = clsTest.GetTestByTestAppointmentID(lastTestAppointment.TestAppointmentID);

                    if (!lastTest.TestResult)
                    {
                        SechduleTest sechduleTest = new SechduleTest(_localAppID, _TestType, true);
                        sechduleTest.ShowDialog();
                        _LoadAppointments();
                    }
                    else
                    {
                        MessageBox.Show("This person already passed this test");
                    }
                }
                else
                {
                    MessageBox.Show("There is an active appointment for this test");
                }
            }
            else
            {
                SechduleTest sechduleTest = new SechduleTest(_localAppID, _TestType);
                sechduleTest.ShowDialog();
                _LoadAppointments();
            }
        }

        private void updatePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            SechduleTest sechduleTest = new SechduleTest(TestAppointmentID);
            sechduleTest.ShowDialog();
            _LoadAppointments();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {         
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;
            TakeTest takeTest = new TakeTest(TestAppointmentID);
            takeTest.ShowDialog();
            _LoadAppointments() ;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (Convert.ToBoolean(dgvLicenseTestAppointments.CurrentRow.Cells["IsLocked"].Value))
            {
                takeTestToolStripMenuItem.Enabled = false;
            }
        }
    }
}
