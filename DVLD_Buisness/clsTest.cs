using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsTest
    {
        private enum enMode { AddNew, Update }
        private enMode _mode;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        // ✅ default constructor — for adding new test
        public clsTest()
        {
            TestID = 0;
            TestAppointmentID = 0;
            TestAppointmentInfo = null;
            TestResult = false;
            Notes = string.Empty;
            CreatedByUserID = 0;
            CreatedByUserInfo = null;

            _mode = enMode.AddNew;
        }

        // ✅ parameterized constructor — for loading from database
        private clsTest(int testID, int testAppointmentID,
            bool testResult, string notes, int createdByUserID)
        {
            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestResult = testResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;

            // ✅ load related objects using composition
            this.TestAppointmentInfo = clsTestAppointment.GetTestAppointmentByID(testAppointmentID);
            this.CreatedByUserInfo = clsUser.GetUserByID(createdByUserID);

            _mode = enMode.Update;
        }

        public static DataTable GetAll()
        {
            return clsTestData.GetAllTests();
        }


        public static clsTest GetTestByID(int testID)
        {
            int testAppointmentID = 0, createdByUserID = 0;
            bool testResult = false;
            string notes = string.Empty;

            if (clsTestData.GetTestByID(testID, ref testAppointmentID,
                ref testResult, ref notes, ref createdByUserID))
            {
                return new clsTest(testID, testAppointmentID,
                    testResult, notes, createdByUserID);
            }

            return null;
        }

        public static clsTest GetTestByTestAppointmentID(int testAppointmentID)
        {
            int testID = 0, createdByUserID = 0;
            bool testResult = false;
            string notes = string.Empty;

            if (clsTestData.GetTestByTestAppointmentID(ref testID, testAppointmentID,
                ref testResult, ref notes, ref createdByUserID))
            {
                return new clsTest(testID, testAppointmentID,
                    testResult, notes, createdByUserID);
            }

            return null;
        }
        public static bool IsExist(int testID)
        {
            return clsTestData.IsTestExist(testID);
        }

        public static bool DeleteTest(int testID)
        {
            return clsTestData.DeleteTest(testID);
        }


        private bool _AddNew()
        {
            this.TestID = clsTestData.AddTest(
                this.TestAppointmentID, this.TestResult,
                this.Notes, this.CreatedByUserID);

            return this.TestID != 0;
        }

        private bool _Update()
        {
            return clsTestData.UpdateTest(
                this.TestID, this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _Update();
            }

            return false;
        }
   
    
        public static int GetTestPassed(int localAppid)
        {
            return clsTestData.GetPassedTest(localAppid);
        }
    
    
    
    
    }
}
