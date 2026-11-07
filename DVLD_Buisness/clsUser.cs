using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsUser
    {
        enum enMode { Add, Update }
        enMode mode = enMode.Add;
        public int UserID { get; set; }

        public int PersonID;
        public clsPerson PersonData = null;
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }


        public clsUser()
        {
            UserID = 0;
            Username = string.Empty;
            Password = string.Empty;
            IsActive = false;
            PersonID = 0;
            PersonData = null;
            mode = enMode.Add;
        }

        private clsUser(int userID, int personID, string username, string password, bool isActive)
        {
            UserID = userID;
            Username = username;
            Password = password;
            IsActive = isActive;
            PersonID = personID;
            PersonData = clsPerson.GetPersonByID(PersonID);
            mode = enMode.Update;
        }


        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static clsUser GetUserByID(int id)
        {
            int personID = 0;
            string username = string.Empty, password = string.Empty;
            bool isActive = false;

            if (clsUserData.GetUserByID(id, ref personID, ref username, ref password, ref isActive))
            {
                return new clsUser(id, personID, username, password, isActive);
            }

            return null;
        }

        public static clsUser Login(string UserName, string Password)
        {
            int personID = 0, UserID = 0;
            bool isActive = false;

            if (clsUserData.Login(ref UserID, ref personID, UserName, Password, ref isActive))
            {
                return new clsUser(UserID, personID, UserName, Password, isActive);
            }

            return null;
        }
        public static bool IsUserExistByUserID(int Userid)
        {
            return clsUserData.IsUserExistByUserID(Userid);
        }

        public static bool IsUserExistByPersonID(int Peronsid)
        {
            return clsUserData.IsUserExistByPersonID(Peronsid);
        }


        public static bool DeleteUser(int id)
        {
            return clsUserData.DeleteUser(id);
        }

        
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.Username, this.Password, this.IsActive);
        }
        private bool _AddUser()
        {
            this.UserID = clsUserData.AddUser(this.PersonID, this.Username, this.Password, this.IsActive);

            return this.UserID > 0;
        }
        
        public bool Save()
        {
            switch(mode)
            {
                case enMode.Update:
                    return _UpdateUser();
                
                case enMode.Add:
                {
                    if (_AddUser())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }







    }
}
