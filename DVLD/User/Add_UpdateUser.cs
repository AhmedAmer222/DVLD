using DVLD.GLobal_Classes;
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
    public partial class Add_UpdateUser : Form
    {
        public delegate void DataBackEventHandeler(int userId);
        public event DataBackEventHandeler DataBack;
        enum enMode { Add = 0, Update = 1 }


        private clsUser _user;
        private int _userID = 0;
        private enMode _mode = enMode.Add;

        public Add_UpdateUser()
        {
            InitializeComponent();
        }

        public Add_UpdateUser(int userid)
        {
            InitializeComponent();
            _userID = userid;
            _mode = enMode.Update;
        }

        private void _LoadLoginData()
        {
            labUserID.Text = _userID.ToString();
            txtUserName.Text = _user.Username;
            txtPassword.Text = _user.Password;
            txtConfirmPass.Text = _user.Password;
            chkIsActive.Checked = _user.IsActive;
        }

        private void _LoadDataToUser()
        {
            _user.PersonID = personCardWithFilter1.PersonId;
            _user.Username = txtUserName.Text;
            _user.Password = clsUtil.GetHasedPassword(txtPassword.Text);
            _user.IsActive = chkIsActive.Checked;
        }

        private void personCardWithFilter1_OnPersonSelected(int obj)
        {
            btnNext.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_mode == enMode.Add && clsUser.IsUserExistByPersonID(personCardWithFilter1.PersonId))
            {
                MessageBox.Show("This person already linked to user");
                return;
            }

            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;
            panLoginInfo.Enabled = true;
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_UpdateUser_Load(object sender, EventArgs e)
        {
            if (_mode == enMode.Add)
            {
                labTitle.Text = "Add New User";
                _user = new clsUser();
            }
            else
            {
                labTitle.Text = "Update User";

                _user = clsUser.GetUserByID(_userID);
                personCardWithFilter1.FilterEnabled = false;
                personCardWithFilter1.loadForSpecificID(_user.PersonID);
                btnNext.Enabled = true;
                _LoadLoginData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.ValidateChildren(ValidationConstraints.None);

            if (clsUtil.HasValidationErrors(errorProvider1, panLoginInfo))
            {
                MessageBox.Show("Please fix validation errors");
                return;
            }

            try
            {
                _LoadDataToUser();
                if (_user.Save())
                {
                    MessageBox.Show("Data Saved Successfylly");
                    
                    if (_mode == enMode.Add)
                    {
                        _mode = enMode.Update;
                        labTitle.Text = "Update User";
                        _userID = _user.UserID;
                        labUserID.Text = _userID.ToString();
                    }

                    DataBack?.Invoke(_userID);
                }
                else
                {
                    MessageBox.Show("Error Saving Data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            Control ctrl = sender as Control;

            if (string.IsNullOrEmpty(ctrl.Text))
            {
                errorProvider1.SetError(ctrl, "This filed is reqired");
            }
            else
            {
                errorProvider1.SetError(ctrl, "");
            }
        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPass.Text))
            {
                errorProvider1.SetError(txtConfirmPass, "This filed is reqired");
            }
            else if (txtConfirmPass.Text != txtPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPass, "Do not match with password");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPass, "");
            }
        }

        
    }
}
