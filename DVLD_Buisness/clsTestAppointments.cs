using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsApplication;

namespace DVLD_Buisness
{
    public class clsTestAppointment
    {
        private enum enMode { AddNew, Update };
        private enMode _mode;
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public clsTestType TestTypeInfo { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestApplicationInfo { get; set; }

        public clsTestAppointment()
        {
            TestAppointmentID = 0;
            TestTypeID = 0;
            LocalDrivingLicenseApplicationID = 0;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = 0;
            IsLocked = false;
            RetakeTestApplicationID = 0;
            _mode = enMode.AddNew;
        }

        private clsTestAppointment(int testAppointmentID, int testTypeID,
        int localDrivingLicenseApplicationID, DateTime appointmentDate,
        float paidFees, int createdByUserID, bool isLocked,
        int retakeTestApplicationID = 0)
        {
            this.TestAppointmentID = testAppointmentID;
            this.TestTypeID = testTypeID;
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.AppointmentDate = appointmentDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.IsLocked = isLocked;
            this.RetakeTestApplicationID = retakeTestApplicationID;

            this.TestTypeInfo = clsTestType.GetTestTypeByID((clsTestType.enTestTypes) testTypeID);
            this.LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(localDrivingLicenseApplicationID);
            this.CreatedByUserInfo = clsUser.GetUserByID(createdByUserID);

            if (retakeTestApplicationID != 0)
            {
                this.RetakeTestApplicationInfo = clsApplication.GetBaseApplicationInfo(retakeTestApplicationID);
            }

            _mode = enMode.Update;
        }


        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }

        public static int GetTotelTrial(int localAppId, clsTestType.enTestTypes testTypeId)
        {
            return clsTestAppointmentData.GetTotelTrial(localAppId,(int) testTypeId);
        }
        public static DataTable GetAllTestAppointmentsForSpecificTest(int localAppId, int testTypeId)
        {
            return clsTestAppointmentData.GetAllTestAppointmentsForSpecificTest(localAppId, testTypeId);
        }

        public static clsTestAppointment GetLastTestAppointment(int localAppId, clsTestType.enTestTypes testTypeID)
        {
            int testAppointmentID = 0;
            int createdByUserID = 0, retakeTestApplicationID = 0;
            DateTime appointmentDate = DateTime.Now;
            float paidFees = 0;
            bool isLocked = false;

            if (clsTestAppointmentData.GetLastTestAppointment(ref testAppointmentID,
                 (int) testTypeID, localAppId,
                ref appointmentDate, ref paidFees, ref createdByUserID,
                ref isLocked, ref retakeTestApplicationID))
            {
                return new clsTestAppointment(testAppointmentID, (int) testTypeID,
                    localAppId, appointmentDate,
                    paidFees, createdByUserID, isLocked, retakeTestApplicationID);
            }

            return null;
        }

        public static clsTestAppointment GetTestAppointmentByID(int testAppointmentID)
        {
            int testTypeID = 0, localDrivingLicenseApplicationID = 0;
            int createdByUserID = 0, retakeTestApplicationID = 0;
            DateTime appointmentDate = DateTime.Now;
            float paidFees = 0;
            bool isLocked = false;

            if (clsTestAppointmentData.GetTestAppointmentByID(testAppointmentID,
                ref testTypeID, ref localDrivingLicenseApplicationID,
                ref appointmentDate, ref paidFees, ref createdByUserID,
                ref isLocked, ref retakeTestApplicationID))
            {
                return new clsTestAppointment(testAppointmentID, testTypeID,
                    localDrivingLicenseApplicationID, appointmentDate,
                    paidFees, createdByUserID, isLocked, retakeTestApplicationID);
            }

            return null;
        }
        public static bool IsTestAppointmentExist(int testAppointmentID)
        {
            return clsTestAppointmentData.IsTestAppointmentExist(testAppointmentID);
        }

       
        public static bool Delete(int testAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(testAppointmentID);
        }

       
        public static bool LockTestAppointment(int testAppointmentID)
        {
            return clsTestAppointmentData.LockTestAppointment(testAppointmentID);
        }


        private bool _AddNew()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddTestAppointment(
                this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID,
                this.IsLocked, this.RetakeTestApplicationID);

            return this.TestAppointmentID != 0;
        }

        private bool _Update()
        {
            return clsTestAppointmentData.UpdateTestAppointment(
                this.TestAppointmentID, this.TestTypeID,
                this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.IsLocked,
                this.RetakeTestApplicationID);
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

















    }
}
