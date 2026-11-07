using System;
using System.Data;
using DVLD_DataAccess;
using System.Runtime.CompilerServices;
using System.Security.Policy;


namespace DVLD_Buisness
{
    public class clsPerson
    {
        enum enMode { Add, Update }

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2} {3}", FirstName, SecondName, ThirdName, LastName);
            }
        }
        public DateTime DateOfBirth { get; set; }
        public short Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NationalityCountryID { get; set; }
        public string NationalityCountryName { get; set; }
        public string ImagePath { get; set; }

        enMode mode;

        public clsPerson()
        {
            PersonID = 0;
            NationalNo = string.Empty;
            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.MinValue;
            Gender = -1;
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            NationalityCountryID = string.Empty;
            ImagePath = string.Empty;

            mode = enMode.Add;
        }

        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime Date, short Gender, string Address, string Phone, string Email, 
            string NationalityCountryID, string NationalityCountryName, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = Date;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.NationalityCountryName = NationalityCountryName;
            this.ImagePath = ImagePath;

            mode = enMode.Update;
        }



        public static DataTable GetAllPeople()
        {
            return clsPersonData.GatAllPeople();
        }

       
        public static clsPerson GetPersonByID(int id)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "";
            DateTime Date = DateTime.MinValue;
            string Address = "", Phone = "", Email = "", NationalityCountryID = "",NationalityCountryName = "", ImagePath = "";
            short Gender = -1;

            if (clsPersonData.GetPersonByID(id, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                    ref Date, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref NationalityCountryName, ref ImagePath))
            {
                return new clsPerson(id, NationalNo, FirstName, SecondName, ThirdName, LastName,
                    Date, Gender, Address, Phone, Email, NationalityCountryID, NationalityCountryName, ImagePath);

            }
            else
            {
                return null;
            }
        }


        public static clsPerson GetPersonByNationalNo(string No)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "";
            DateTime Date = DateTime.MinValue;
            string Address = "", Phone = "", Email = "", NationalityCountryID = "", NationalityCountryName = "", ImagePath = "";
            short Gender = -1;
            int PersonID = 0;

            if (clsPersonData.GetPersonByNationalNo(ref PersonID, No, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                    ref Date, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref NationalityCountryName, ref ImagePath))
            {
                return new clsPerson(PersonID, No, FirstName, SecondName, ThirdName, LastName,
                    Date, Gender, Address, Phone, Email, NationalityCountryID, NationalityCountryName, ImagePath);

            }
            else
            {
                return null;
            }
        }


        public static bool Find(int id)
        {
           return clsPersonData.IsPersonExistByID(id);
        }

        public static bool Find(string NationalNo)
        {
            return clsPersonData.IsPersonExistByNationalNo(NationalNo);
        }


        public static bool DeletePersonByID(int id)
        {
            return clsPersonData.DeletePersonByID(id);
        }


        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                                            this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
           
            return this.PersonID > 0;
        }
        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                                            this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }


        public bool Save()
        {
            switch(mode)
            {
                case enMode.Add:
                {
                    if (_AddNewPerson())
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
                        return _UpdatePerson();
                }
            }

            return false;
        }

    
    
    
    }

}
