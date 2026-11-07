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
    public partial class UserDeatails : Form
    {
        //public UserDeatails(int userID)
        //{
        //    InitializeComponent();

        //    userCard1.LoadUserInfo(userID);
        //}

        public UserDeatails(int userID, bool IsCurrentUser = false)
        {
            InitializeComponent();

            userCard1.LoadUserInfo(userID);

            if (IsCurrentUser)
            {
                label2.Text = "Current User Info";
            }
        }
    }
}
