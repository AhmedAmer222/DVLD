using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsDetainedLicense
    {
        enum enMode { Add, Update }

        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public clsLicense DetainedLicenseInfo { set; get; }
        public DateTime DetainDate { set; get; }
        public float FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public clsUser ReleasedByUserInfo { set; get; }
        public int ReleaseApplicationID { set; get; }
        public clsApplication ReleaseApplicationInfo { set; get; }

        enMode mode;


        public clsDetainedLicense()
        {
            DetainID = 0;
            LicenseID = 0;
            DetainDate = DateTime.MinValue;
            FineFees = 0;
            CreatedByUserID = 0;
            IsReleased = false;
            ReleaseDate = DateTime.MinValue;
            ReleasedByUserID = 0;
            ReleaseApplicationID = 0;

            mode = enMode.Add;
        }


        private clsDetainedLicense(int DetainID, int LicenseID,
            DateTime DetainDate, float FineFees,
            int CreatedByUserID, bool IsReleased,
            DateTime ReleaseDate,
            int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;

            DetainedLicenseInfo = clsLicense.GetLiecnseByID(LicenseID);
            CreatedByUserInfo = clsUser.GetUserByID(CreatedByUserID);
            ReleasedByUserInfo = clsUser.GetUserByID(ReleasedByUserID);
            ReleaseApplicationInfo = clsApplication.GetBaseApplicationInfo(ReleaseApplicationID);

            mode = enMode.Update;
        }


        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }


        public static clsDetainedLicense GetDetainedLicenseByID(int DetainID)
        {
            int LicenseID = 0;
            DateTime DetainDate = DateTime.MinValue;
            float FineFees = 0;
            int CreatedByUserID = 0;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = 0;
            int ReleaseApplicationID = 0;


            if (clsDetainedLicenseData.GetDetainedLicenseByID(
                DetainID,
                ref LicenseID,
                ref DetainDate,
                ref FineFees,
                ref CreatedByUserID,
                ref IsReleased,
                ref ReleaseDate,
                ref ReleasedByUserID,
                ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(
                    DetainID,
                    LicenseID,
                    DetainDate,
                    FineFees,
                    CreatedByUserID,
                    IsReleased,
                    ReleaseDate,
                    ReleasedByUserID,
                    ReleaseApplicationID);
            }

            return null;
        }


        public static clsDetainedLicense GetDetainedLicenseByLicenseID(int LicenseID)
        {
            int DetainID = 0;
            DateTime DetainDate = DateTime.MinValue;
            float FineFees = 0;
            int CreatedByUserID = 0;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = 0;
            int ReleaseApplicationID = 0;


            if (clsDetainedLicenseData.GetDetainedLicenseByLicenseID(
                LicenseID,
                ref DetainID,
                ref DetainDate,
                ref FineFees,
                ref CreatedByUserID,
                ref IsReleased,
                ref ReleaseDate,
                ref ReleasedByUserID,
                ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(
                    DetainID,
                    LicenseID,
                    DetainDate,
                    FineFees,
                    CreatedByUserID,
                    IsReleased,
                    ReleaseDate,
                    ReleasedByUserID,
                    ReleaseApplicationID);
            }

            return null;
        }


        public static bool IsDetainedLicenseExist(int DetainID)
        {
            return clsDetainedLicenseData.IsDetainedLicenseExist(DetainID);
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        public static bool IsLicenseDetainedByDetainID(int DetainID)
        {
            return clsDetainedLicenseData.IsLicenseDetainedByDetainID(DetainID);
        }


        public static int GetActiveDetainedLicenseID(int LicenseID)
        {
            return clsDetainedLicenseData.GetActiveDetainedLicenseID(LicenseID);
        }


        public static bool DeleteDetainedLicense(int DetainID)
        {
            return clsDetainedLicenseData.DeleteDetainedLicense(DetainID);
        }

        public bool Delete()
        {
            return clsDetainedLicenseData.DeleteDetainedLicense(this.DetainID);
        }


        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(
                this.LicenseID,
                this.DetainDate,
                this.FineFees,
                this.CreatedByUserID);


            return this.DetainID > 0;
        }



        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(
                this.DetainID,
                this.LicenseID,
                this.DetainDate,
                this.FineFees,
                this.CreatedByUserID);
        }



        public bool Save()
        {
            switch (mode)
            {
                case enMode.Add:
                    {
                        if (_AddNewDetainedLicense())
                        {
                            mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }


                case enMode.Update:
                    {
                        return _UpdateDetainedLicense();
                    }
            }

            return false;
        }



        public bool ReleaseDetainedLicense(DateTime ReleaseDate, int ReleasedByUserID)
        {
            ReleaseApplicationInfo = new clsApplication();

            ReleaseApplicationInfo.ApplicantPersonID = this.DetainedLicenseInfo.DriverInfo.PersonInfo.PersonID;
            ReleaseApplicationInfo.ApplicationDate = DateTime.Now;
           
            int apptype = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            ReleaseApplicationInfo.ApplicationTypeID = apptype;

            ReleaseApplicationInfo.ApplicationStatus = clsApplication.enApplicationStatus.New;
            ReleaseApplicationInfo.LastStatusDate = DateTime.Now;
            ReleaseApplicationInfo.PaiedFees = clsApplicationType.GetApplicationTypeByID(apptype).ApplicationFees;
            ReleaseApplicationInfo.CreatedByUserID = ReleasedByUserID;

            if (ReleaseApplicationInfo.Save())
            {
                this.ReleaseApplicationID = ReleaseApplicationInfo.ApplicationID;
                if (clsDetainedLicenseData.ReleaseDetainedLicense(
                this.DetainID,
                ReleaseDate,
                ReleasedByUserID,
                this.ReleaseApplicationID))
                {
                    this.IsReleased = true;
                    this.ReleaseDate = ReleaseDate;
                    this.ReleasedByUserID = ReleasedByUserID;

                    if (ReleaseApplicationInfo.SetComplete())
                    {
                        return true;
                    }
                    else
                    {
                        ReleaseApplicationInfo.Delete();
                        this.Delete();
                    }
                   
                }
                else
                {
                    ReleaseApplicationInfo.Delete();
                }
            }

            return false;
        }    
    
        
    
    
    }
}