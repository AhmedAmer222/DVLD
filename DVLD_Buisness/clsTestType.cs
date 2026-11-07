using System;
using System.Data;
using DVLD_DataAccess;



namespace DVLD_Buisness
{
    public class clsTestType
    {
        enum enMode { Add, Update };
        enMode mode;

        public enum enTestTypes { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public enTestTypes TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        public clsTestType()
        {
            TestTypeID = enTestTypes.VisionTest;
            TestTypeTitle = string.Empty;
            TestTypeDescription = string.Empty;
            TestTypeFees = 0;
            mode = enMode.Add;
        }

        private clsTestType(enTestTypes id, string Title, string Description, float Fees)
        {
            TestTypeID = id;
            TestTypeTitle = Title;
            TestTypeDescription = Description;
            TestTypeFees = Fees;
            mode = enMode.Update;
        }


        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }


        public static clsTestType GetTestTypeByID(enTestTypes id)
        {
            string TestTitle = string.Empty, TestDescription = string.Empty;
            float TestFees = 0;

            if (clsTestTypeData.GetTestTypeByID((int)id, ref TestTitle, ref TestDescription, ref TestFees))
                return new clsTestType(id, TestTitle, TestDescription, TestFees);
            else
                return null;
        }
        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdataTestType((int)this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        private bool _AdTestType()
        {
            this.TestTypeID = (enTestTypes) clsTestTypeData.AddTestType(this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);

            return (this.TestTypeID > 0);
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.Update:
                    return _UpdateTestType();

                case enMode.Add:
                    if (_AdTestType())
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
