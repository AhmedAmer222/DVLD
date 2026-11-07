using DVLD_DataAccess;
using System;
using System.Data;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DVLD_Buisness
{
    public class clsApplication
    {
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }
        public enum enApplicationType 
        {
            NewLocalDrivingLicenseService = 1,
            RenewDrivingLicenseService = 2,
            ReplacementForALostDrivingLicense = 3,
            ReplacementForADamagedDrivingLicense = 4,
            ReleaseDetainedDrivingLicsense = 5,
            NewInternationalLicense = 6,
            RetakeTest = 7
        }

        public enum enMode { Add = 1, Update = 2 }
        public enMode _mode  = enMode.Add;

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.GetPersonByID(ApplicantPersonID)?.FullName ?? "Unknown Applicant";
            }
        }
        public clsPerson ApplicantPersonInfo { get; set; } 

        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeData = null;

        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown Status";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public float PaiedFees { get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo = null;


        public clsApplication()
        {
            this.ApplicationID = 0;
            this.ApplicantPersonID = 0;
            this.ApplicantPersonInfo = null;
            this.ApplicationDate = DateTime.MinValue;
            this.ApplicationTypeID = 0;
            this.ApplicationTypeData = null;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.MinValue;
            this.PaiedFees = 0;
            this.CreatedByUserID = 0;
            this.CreatedByUserInfo= null;
        }

        public clsApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicantPersonInfo = clsPerson.GetPersonByID(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeData = clsApplicationType.GetApplicationTypeByID(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaiedFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.GetUserByID(CreatedByUserID);

            _mode = enMode.Update;
        }


        public static DataTable GetAllApplication()
        {
            return clsApplicationData.GetAllApplications();
        }

        public static clsApplication GetBaseApplicationInfo(int Appid)
        {
            int ApplicantPersonID = 0, ApplicationTypeID = 0, CreatedByUserID = 0;

            DateTime ApplicationDate = DateTime.MinValue, LastStatusDate = DateTime.MinValue;

            byte ApplicationStatus = 1; 

            float PaidFees = 0.0f;

            if (clsApplicationData.GetApplicationById(Appid, ref ApplicantPersonID, ref ApplicationDate, 
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
                ref CreatedByUserID))
            {
                return new clsApplication(Appid, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                    (enApplicationStatus) ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsApplicationExist(int Appid)
        {
            return clsApplicationData.IsApplicationExist(Appid);
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public bool SetComplete()
        {
            ApplicationStatus = enApplicationStatus.Completed;
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte) enApplicationStatus.Completed);
        }

        public bool Cancel()
        {
            ApplicationStatus = enApplicationStatus.Cancelled;
            return clsApplicationData.UpdateStatus(this.ApplicationID, (byte)enApplicationStatus.Cancelled);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID,
                this.ApplicationDate, this.ApplicationTypeID,(int) this.ApplicationStatus, this.LastStatusDate,
                this.PaiedFees, this.CreatedByUserID);
        }

        private bool _AddApplication()
        {
            this.ApplicationID = clsApplicationData.AddApplication(this.ApplicantPersonID,
                this.ApplicationDate, this.ApplicationTypeID, (int)this.ApplicationStatus, this.LastStatusDate,
                this.PaiedFees, this.CreatedByUserID);

            return this.ApplicationID > 0;
        }


        public virtual bool Save()
        {
            switch(_mode)
            {
                case enMode.Add:
                    if (_AddApplication())
                    {
                        _mode = enMode.Update;    
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplication();
            }

            return false;
        }








    }
}
