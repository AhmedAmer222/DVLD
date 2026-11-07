using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsDriver
    {
        private enum enMode { AddNew, Update }
        private enMode _mode;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public DateTime CreatedDate { get; set; }


        public clsDriver()
        {
            DriverID = 0;
            PersonID = 0;
            PersonInfo = null;
            CreatedByUserID = 0;
            CreatedByUserInfo = null;
            CreatedDate = DateTime.Now;

            _mode = enMode.AddNew;
        }

        private clsDriver(int driverID, int personID,
            int createdByUserID, DateTime createdDate)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = createdDate;

            this.PersonInfo = clsPerson.GetPersonByID(personID);
            this.CreatedByUserInfo = clsUser.GetUserByID(createdByUserID);

            _mode = enMode.Update;
        }

        public static DataTable GetAll()
        {
            return clsDriverData.GetAllDrivers();
        }


        public static clsDriver GetByDriverID(int driverID)
        {
            int personID = 0, createdByUserID = 0;
            DateTime createdDate = DateTime.Now;

            if (clsDriverData.GetDriverByID(driverID, ref personID,
                ref createdByUserID, ref createdDate))
            {
                return new clsDriver(driverID, personID,
                    createdByUserID, createdDate);
            }

            return null;
        }
        public static clsDriver GetByPersonID(int personID)
        {
            int driverID = 0, createdByUserID = 0;
            DateTime createdDate = DateTime.Now;

            if (clsDriverData.GetDriverByPersonID(ref driverID, personID,
                ref createdByUserID, ref createdDate))
            {
                return new clsDriver(driverID, personID,
                    createdByUserID, createdDate);
            }

            return null;
        }

        public static bool IsDriverExist(int driverID)
        {
            return clsDriverData.IsDriverExist(driverID);
        }
        public static bool IsDriverExistByPersonID(int personID)
        {
            return clsDriverData.IsDriverExistByPersonID(personID);
        }

        public static bool Delete(int driverID)
        {
            return clsDriverData.DeleteDriver(driverID);
        }


        private bool _AddNew()
        {
            this.DriverID = clsDriverData.AddDriver(
                this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return this.DriverID != 0;
        }
        private bool _Update()
        {
            return clsDriverData.UpdateDriver(
                this.DriverID, this.PersonID,
                this.CreatedByUserID, this.CreatedDate);
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
   
        public bool DoesHasActiveLicenseForLicenseClass(int LicenseClassID)
        {
            return clsDriverData.DoesHasActiveLicenseForLicenseClass(this.DriverID, LicenseClassID);
        }

        public static bool DoesHasActiveLicenseForLicenseClass(int DriverId, int LicenseClassID)
        {
            return clsDriverData.DoesHasActiveLicenseForLicenseClass(DriverId, LicenseClassID);
        }


        public static bool DoesPersonHasActiveLicenseForLicenseClass(int personID, int LicenseClassID)
        {
            return clsDriverData.DoesPersonHaveActiveLicenseForLicenseClass(personID, LicenseClassID);
        }

    }
}
