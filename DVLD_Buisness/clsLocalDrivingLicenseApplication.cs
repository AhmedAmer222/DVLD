using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        private enMode _mode;
        public int LocalDrivingLicenseApplicationID { get; set; }

        public int LicenseClassesID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }


        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = 0;
            LicenseClassesID = 0;
            LicenseClassInfo = null;
        }

        public clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, 
            int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)
           
            :base(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, 
                 ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassesID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.GetLicenseClassByID(LicenseClassesID);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }



        public static bool IslLocalDrivingLicenseApplicationExistByID(int LocalID)
        {
            return clsLocalDrivingLicenseApplicationData.IslLocalDrivingLicenseApplicationExistByID(LocalID);
        }
        public static bool IslLocalDrivingLicenseApplicationExistByApplicationID(int AppID)
        {
            return clsLocalDrivingLicenseApplicationData.IslLocalDrivingLicenseApplicationExistByApplicationID(AppID);
        }



        public static clsLocalDrivingLicenseApplication GetLocalDrivingLicenseApplicationByID(int LocalID)
        {
            int ApplicationID = 0, LicenseClassId = 0;

            if (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByID(LocalID, ref ApplicationID, ref LicenseClassId))
            {
                clsApplication BaseApplication = clsApplication.GetBaseApplicationInfo(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalID, ApplicationID, BaseApplication.ApplicantPersonID,
                    BaseApplication.ApplicationDate, BaseApplication.ApplicationTypeID, BaseApplication.ApplicationStatus,
                    BaseApplication.LastStatusDate, BaseApplication.PaiedFees, BaseApplication.CreatedByUserID, LicenseClassId);
            }
            else
            {
                return null;
            }
        }
        public static clsLocalDrivingLicenseApplication GetLocalDrivingLicenseApplicationByApplicationID(int AppID)
        {
            int LocalID = 0, LicenseClassId = 0;

            if (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByApplicationID(ref LocalID, AppID, ref LicenseClassId))
            {
                clsApplication BaseApplication = clsApplication.GetBaseApplicationInfo(AppID);

                return new clsLocalDrivingLicenseApplication(LocalID, AppID, BaseApplication.ApplicantPersonID,
                    BaseApplication.ApplicationDate, BaseApplication.ApplicationTypeID, BaseApplication.ApplicationStatus,
                    BaseApplication.LastStatusDate, BaseApplication.PaiedFees, BaseApplication.CreatedByUserID, LicenseClassId);
            }
            else
            {
                return null;
            }
        }


        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = 
                clsLocalDrivingLicenseApplicationData.AddLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassesID);

            return this.LocalDrivingLicenseApplicationID > 0;
        }       
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.
                UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, 
                this.ApplicationID, this.LicenseClassesID);
        }

        public override bool Save()
        {
            _mode = base._mode;

            if (!base.Save())
            {
                return false;
            }
            switch (_mode)
            {
                case enMode.Add:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        base._mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        base.Delete();
                        return false;
                    }
                case enMode.Update:
                    if(_UpdateLocalDrivingLicenseApplication())
                    {
                        return true;
                    }
                    else
                    {
                        base.Delete();
                        return false;
                    }

            }
            return false;
        }
        
        
        public bool Delete(int LocalAppId)
        {
            bool IsLocalDrivingLicenseApplicationDeleted = false;
            //bool IsBaseApplicationDeleted = false;

            IsLocalDrivingLicenseApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplicationByID(LocalAppId);

            if (!IsLocalDrivingLicenseApplicationDeleted)
                return false;
            else
                return base.Delete();
        }


        public static int DoesHasActiveApplicationForLicenseClass(int PersonId, int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesHasActiveApplicationForLicenseClass(PersonId, LicenseClassID);
        }

        public static bool DoesPersonHaveLicenseForLicenseClass(int personID, int licenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPersonHaveLicenseForLicenseClass(personID, licenseClassID);
        }

    }
}
