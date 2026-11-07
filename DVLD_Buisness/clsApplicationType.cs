using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsApplicationType
    {
        enum enMode { Add, Update };
        enMode mode;

        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

        public clsApplicationType()
        {
            ApplicationTypeID = 0;
            ApplicationTypeTitle = string.Empty;
            ApplicationFees = 0;
            mode = enMode.Add;
        }

        private clsApplicationType(int id, string Title, float Fees)
        {
            ApplicationTypeID = id;
            ApplicationTypeTitle = Title;
            ApplicationFees = Fees;
            mode = enMode.Update;
        }


        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllAplicationTypes();
        }


        public static clsApplicationType GetApplicationTypeByID(int id)
        {
            string AppTitle = string.Empty;
            float AppFees = 0;

            if(clsApplicationTypeData.GetApplicationTypeByID(id, ref AppTitle, ref AppFees))
                return new clsApplicationType(id, AppTitle, AppFees);
            else
                return null;
        }
        private bool _UpdateAppType()
        {
            return clsApplicationTypeData.UpdataApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        private bool _AddAppType()
        {
            this.ApplicationTypeID = clsApplicationTypeData.AddApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);

            return (this.ApplicationTypeID > 0);
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.Update:
                    return _UpdateAppType();

                case enMode.Add:
                    if (_AddAppType())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }

            return false;
        }


    }
}
