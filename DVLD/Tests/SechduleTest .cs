using DVLD.GLobal_Classes;
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
    public partial class SechduleTest : Form
    {
        enum enMode { AddNew, Update };
        enMode _mode;
        private int _localAppID;
        private clsTestType.enTestTypes _testType;
        private bool _retakeTest;
        private int _testAppointmentId;
        public SechduleTest(int localAppid, clsTestType.enTestTypes testType, bool retakeTest = false)
        {
            InitializeComponent();
            _localAppID = localAppid;
            _testType = testType;
            _retakeTest = retakeTest;
            _mode = enMode.AddNew;
        }

        public SechduleTest(int testAppointmentId)
        {
            InitializeComponent();
            
            _testAppointmentId = testAppointmentId;
            _mode = enMode.Update;
        }

        private void sechduleTestControl1_Load(object sender, EventArgs e)
        {
            if (_mode == enMode.AddNew)
            {
                sechduleTestControl1.LoadSechduleTest(_localAppID, _testType, _retakeTest);

            }
            else
            {
                sechduleTestControl1.LoadSechduleTest(_testAppointmentId);
            }
        }
    }
}
