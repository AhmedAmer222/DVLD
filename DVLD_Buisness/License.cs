using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLicense
    {
        private enum enMode { AddNew, Update }
        private enMode _mode;

        public enum enIssueReasons
        {
            FirstTime = 1,
            Renew = 2,
            DamagedReplacement = 3,
            LostReplacement = 4
        }

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public enIssueReasons IssueReason { get; set; }

        public clsLicense()
        {
            LicenseID = 0;
            ApplicationID = 0;
            DriverID = 0;
            LicenseClassID = 0;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = string.Empty;
            PaidFees = 0;
            IsActive = true;
            IssueReason = enIssueReasons.FirstTime;
            CreatedByUserID = 0;


            _mode = enMode.AddNew;
        }

        private clsLicense(int licenseID, int applicationID, int driverID,
            int licenseClassID, DateTime issueDate, DateTime expirationDate,
            string notes, float paidFees, bool isActive, enIssueReasons issueReason,
            int createdByUserID)
        {
            this.LicenseID = licenseID;
            this.ApplicationID = applicationID;
            this.DriverID = driverID;
            this.LicenseClassID = licenseClassID;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.Notes = notes;
            this.PaidFees = paidFees;
            this.IsActive = isActive;
            this.IssueReason = issueReason;
            this.CreatedByUserID = createdByUserID;



            this.ApplicationInfo = clsApplication.GetBaseApplicationInfo(applicationID);
            this.DriverInfo = clsDriver.GetByDriverID(driverID);
            this.LicenseClassInfo = clsLicenseClass.GetLicenseClassByID(licenseClassID);
            this.CreatedByUserInfo = clsUser.GetUserByID(createdByUserID);

            _mode = enMode.Update;
        }



        public static clsLicense GetLiecnseByID(int licenseID)
        {
            int applicationID = 0, driverID = 0, licenseClassID = 0;
            int createdByUserID = 0, issueReason = 0;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            string notes = string.Empty;
            float paidFees = 0;
            bool isActive = false;

            if (clsLicenseData.GetLicenseByID(licenseID, ref applicationID,
                ref driverID, ref licenseClassID, ref issueDate,
                ref expirationDate, ref notes, ref paidFees,
                ref isActive, ref issueReason, ref createdByUserID))
            {
                return new clsLicense(licenseID, applicationID, driverID,
                    licenseClassID, issueDate, expirationDate, notes,
                    paidFees, isActive, 
                    (enIssueReasons)issueReason,createdByUserID);
            }

            return null;
        }
        public static clsLicense GetLiecnseByApplicatioID(int applicationID)
        {
            int licenseID = 0, driverID = 0, licenseClassID = 0;
            int createdByUserID = 0, issueReason = 0;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            string notes = string.Empty;
            float paidFees = 0;
            bool isActive = false;

            if (clsLicenseData.GetLicenseByApplicationID(ref licenseID, applicationID,
                ref driverID, ref licenseClassID, ref issueDate,
                ref expirationDate, ref notes, ref paidFees,
                ref isActive, ref issueReason, ref createdByUserID))
            {
                return new clsLicense(licenseID, applicationID, driverID,
                    licenseClassID, issueDate, expirationDate, notes,
                    paidFees, isActive,
                    (enIssueReasons)issueReason, createdByUserID);
            }

            return null;
        }



        public static clsLicense GetActiveLicense(int driverID, int licenseClassID)
        {
            int licenseID = 0;

            if (clsLicenseData.GetActiveLicenseByDriverIDAndClass(
                driverID, licenseClassID, ref licenseID))
            {
                return GetLiecnseByID(licenseID);
            }

            return null;
        }

        public static bool IsLiecnseExist(int licenseID)
        {
            return clsLicenseData.IsLicenseExist(licenseID);
        }

        public static bool Delete(int licenseID)
        {
            return clsLicenseData.DeleteLicense(licenseID);
        }

        public bool Delete()
        {
            return clsLicenseData.DeleteLicense(this.LicenseID);
        }


        public static bool DeactivateLiecnse(int licenseID)
        {
            return clsLicenseData.DeactivateLicense(licenseID);
        }

        public bool DeactivateLiecnse()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }


        public static DataTable GetAll()
        {
            return clsLicenseData.GetAllLicenses();
        }
        public static DataTable GetAllLicensesForPerson(int personId)
        {
            return clsLicenseData.GetAllLicensesForPerson(personId);
        }

        private bool _AddNew()
        {
            this.LicenseID = clsLicenseData.AddLicense(
                this.ApplicationID, this.DriverID, this.LicenseClassID,
                this.IssueDate, this.ExpirationDate, this.Notes,
                this.PaidFees, this.IsActive, (int)this.IssueReason,
                this.CreatedByUserID);

            return this.LicenseID != 0;
        }

        private bool _Update()
        {
            return clsLicenseData.UpdateLicense(
                this.LicenseID, this.ApplicationID, this.DriverID,
                this.LicenseClassID, this.IssueDate, this.ExpirationDate,
                this.Notes, this.PaidFees, this.IsActive,
                (int)this.IssueReason, this.CreatedByUserID);
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
   

        public clsLicense Replace(enIssueReasons IssueReason, int CreatedByUserId, string Notes = "")
        {
            clsApplication application = new clsApplication();

            application.ApplicantPersonID = this.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
           
            application.ApplicationTypeID = (IssueReason == enIssueReasons.DamagedReplacement) ?
               (int) clsApplication.enApplicationType.ReplacementForADamagedDrivingLicense :
               (int) clsApplication.enApplicationType.ReplacementForALostDrivingLicense;

            application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            application.LastStatusDate = DateTime.Now;
            application.PaiedFees = clsApplicationType.GetApplicationTypeByID(application.ApplicationTypeID).ApplicationFees;

            application.CreatedByUserID = CreatedByUserId;

            if (!application.Save())
                return null;

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserId;

            if (NewLicense.Save())
            {
                if (this.DeactivateLiecnse())
                {
                    if (!application.SetComplete())
                    {
                        NewLicense.Delete();
                        application.Delete();
                        return null;
                    }
                    return NewLicense;
                }
                else
                {
                    NewLicense.Delete();
                    application.Delete();
                    return null;
                }
            }
            else
            {
                application.Delete();
                return null;
            }



        }

        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }
    
    
    }
}
