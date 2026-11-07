using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsLicenseClass
    {
        enum enMode { Add, Update };
        enMode mode;
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        public clsLicenseClass()
        {
            LicenseClassID = 0;
            ClassName = string.Empty;
            ClassDescription = string.Empty;
            MinimumAllowedAge = 0;
            DefaultValidityLength = 0;
            ClassFees = 0;
            mode = enMode.Add;
        }

        private clsLicenseClass(int licenseClassID, string className, string classDescription, int minimumAllowedAge, int defaultValidityLength, float classFees)
        {
            this.LicenseClassID = licenseClassID;
            this.ClassName = className;
            this.ClassDescription = classDescription;
            this.MinimumAllowedAge = minimumAllowedAge;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = classFees;
            mode = enMode.Update;
        }


        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }

        public static clsLicenseClass GetLicenseClassByID(int id)
        {
            string ClassName = string.Empty, ClassDescription = string.Empty;
            int MinimumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassByID(id, ref ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(id, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }

        public static clsLicenseClass GetLicenseClassByName(string ClassName)
        {
            string ClassDescription = string.Empty;
            int id = 0, MinimumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassByName(ref id, ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(id, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }

        private bool _AddLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassData.AddLicenseClasse(this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);

            return (this.LicenseClassID > 0);
        }


        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassData.UpdataLicenseClasse(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.Add:
                    if (_AddLicenseClass())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicenseClass();

            }

            return false;
        }
    
    
    
    
    
    
    
    
    }
}
