using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsInternationalDrivingLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalDrivingLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo { get; set; }

        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }

        public int IssuedUsingLocalLicenseID { get; set; }
        public clsLicense IssuedUsingLocalLicenseInfo { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        public clsInternationalDrivingLicense()
        {
            InternationalDrivingLicenseID = 0;
            ApplicationID = 0;
            DriverID = 0;
            IssuedUsingLocalLicenseID = 0;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = true;
            CreatedByUserID = 0;

            Mode = enMode.AddNew;
        }

        private clsInternationalDrivingLicense(
            int internationalDrivingLicenseID,
            int applicationID,
            int driverID,
            int issuedUsingLocalLicenseID,
            DateTime issueDate,
            DateTime expirationDate,
            bool isActive,
            int createdByUserID)
        {
            InternationalDrivingLicenseID = internationalDrivingLicenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserID;

            ApplicationInfo = clsApplication.GetBaseApplicationInfo(applicationID);
            DriverInfo = clsDriver.GetByDriverID(driverID);
            IssuedUsingLocalLicenseInfo = clsLicense.GetLiecnseByID(issuedUsingLocalLicenseID);
            CreatedByUserInfo = clsUser.GetUserByID(createdByUserID);

            Mode = enMode.Update;
        }

        public static clsInternationalDrivingLicense GetInternationalLicenseByID(int InternationalDrivingLicenseID)
        {
            int ApplicationID = 0;
            int DriverID = 0;
            int IssuedUsingLocalLicenseID = 0;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = 0;

            if (clsInternationalDrivingLicenseData.GetInternationalLicenseByID(
                InternationalDrivingLicenseID,
                ref ApplicationID,
                ref DriverID,
                ref IssuedUsingLocalLicenseID,
                ref IssueDate,
                ref ExpirationDate,
                ref IsActive,
                ref CreatedByUserID))
            {
                return new clsInternationalDrivingLicense(
                    InternationalDrivingLicenseID,
                    ApplicationID,
                    DriverID,
                    IssuedUsingLocalLicenseID,
                    IssueDate,
                    ExpirationDate,
                    IsActive,
                    CreatedByUserID);
            }

            return null;
        }

        public static clsInternationalDrivingLicense GetLastInternationalLicenseByDriverID(int DriverID)
        {
            int InternationalDrivingLicenseID = -1;
            int ApplicationID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsInternationalDrivingLicenseData.GetLastInternationalLicenseByDriverID(
                DriverID,
                ref InternationalDrivingLicenseID,
                ref ApplicationID,
                ref IssuedUsingLocalLicenseID,
                ref IssueDate,
                ref ExpirationDate,
                ref IsActive,
                ref CreatedByUserID))
            {
                return new clsInternationalDrivingLicense(
                    InternationalDrivingLicenseID,
                    ApplicationID,
                    DriverID,
                    IssuedUsingLocalLicenseID,
                    IssueDate,
                    ExpirationDate,
                    IsActive,
                    CreatedByUserID);
            }

            return null;
        }

        public static int GetActiveLicenseIDForDriver(int DriverID)
        {
            return clsInternationalDrivingLicenseData.GetActiveLicenseIDForDriver(DriverID);
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalDrivingLicenseData.GetAllInternationalLicenses();
        }

        public static DataTable GetAllInternationalLicensesForDriver(int DriverID)
        {
            return clsInternationalDrivingLicenseData.GetAllInternationalLicensesForDriver(DriverID);
        }

        public static DataTable GetAllInternationalLicensesForPerson(int perosnID)
        {
            return clsInternationalDrivingLicenseData.GetAllInternationalLicensesForPerons(perosnID);
        }

        public static bool IsExist(int InternationalDrivingLicenseID)
        {
            return clsInternationalDrivingLicenseData.IsInternationalLicenseExist(InternationalDrivingLicenseID);
        }

        public static bool DoesDriverHasActiveInternationalLicense(int DriverID)
        {
            return clsInternationalDrivingLicenseData.IsDriverHasActiveInternationalLicense(DriverID);
        }

        private bool _AddNew()
        {
            InternationalDrivingLicenseID =
                clsInternationalDrivingLicenseData.AddInternationalLicense(
                    ApplicationID,
                    DriverID,
                    IssuedUsingLocalLicenseID,
                    IssueDate,
                    ExpirationDate,
                    IsActive,
                    CreatedByUserID);

            return InternationalDrivingLicenseID > 0;
        }

        private bool _Update()
        {
            return clsInternationalDrivingLicenseData.UpdateInternationalLicense(
                InternationalDrivingLicenseID,
                ApplicationID,
                DriverID,
                IssuedUsingLocalLicenseID,
                IssueDate,
                ExpirationDate,
                IsActive,
                CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:

                    return _Update();
            }

            return false;
        }

        public static bool Delete(int InternationalDrivingLicenseID)
        {
            return clsInternationalDrivingLicenseData.DeleteInternationalLicense(InternationalDrivingLicenseID);
        }

        public bool Delete()
        {
            return clsInternationalDrivingLicenseData.DeleteInternationalLicense(this.InternationalDrivingLicenseID);
        }
   
    
    }
}
