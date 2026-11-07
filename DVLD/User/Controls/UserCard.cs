using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class UserCard : UserControl
    {
        public UserCard()
        {
            InitializeComponent();
        }

        private int _userID;
        private clsUser _user;

        public int PersonID
        {
            get
            {
                return personCard1.PersonID;
            }
        }

        public clsPerson SelectedPerson
        {
            get
            {
                return personCard1.SelectedPerson;
            }
        }
        public int UserID
        {
            get
            {
                return _userID;
            }
        }

        public clsUser SelectedUser
        {
            get
            {
                return _user;
            }
        }

        public void LoadUserInfo(int userID)
        {
            _userID = userID;
            _user = clsUser.GetUserByID(userID);
            if (_user != null)
            {
                personCard1.LoadPersonInfo(_user.PersonID);

                labUserID.Text = _user.UserID.ToString();
                labUserName.Text = _user.Username;

                if (_user.IsActive)
                    labIsActive.Text = "Yes";
                else
                    labIsActive.Text = "No";
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
