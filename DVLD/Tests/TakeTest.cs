using DVLD.GLobal_Classes;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class TakeTest : Form
    {
        private int _testAppointmentId;
        clsTest _test;
        public TakeTest(int testAppointmentID)
        {
            InitializeComponent();
            _testAppointmentId = testAppointmentID;
        }

        private void TakeTest_Load(object sender, EventArgs e)
        {
            sechduledTestControl1.LoadSechduledTest(_testAppointmentId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _test = new clsTest();

            _test.TestAppointmentID = _testAppointmentId;
            if (rbPass.Checked)
                _test.TestResult = true;
            else
                _test.TestResult = false;
            _test.Notes = txtNotes.Text;
            _test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_test.Save())
            {
                if (clsTestAppointment.LockTestAppointment(_testAppointmentId))
                {
                    MessageBox.Show("Data Saved Successfully");

                    if (_test.TestResult)
                    {
                        if (sechduledTestControl1.TestType == clsTestType.enTestTypes.StreetTest)
                        {
                            sechduledTestControl1.SelectedLocalDrivingLicenseApplication.SetComplete();
                        }
                    }

                }
                else
                {
                    clsTest.DeleteTest(_test.TestID);
                    MessageBox.Show("Error while saving data");

                }

            }
            else
            {
            }
        }
    }
}
