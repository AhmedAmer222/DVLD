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
    public partial class ChangePasswordForm : Form
    {
        private clsUser _user;
        public ChangePasswordForm(int userid)
        {
            InitializeComponent();
            userCard1.LoadUserInfo(userid);
            _user = userCard1.SelectedUser;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void txtCurrentPass_Validating(object sender, CancelEventArgs e)
        {
            string hashedCurrentPass = clsUtil.GetHasedPassword(txtCurrentPass.Text);
            if (hashedCurrentPass != _user.Password)
            {
                txtCurrentPass.BackColor = Color.LightPink;
                errorProvider1.SetError(txtCurrentPass, "Wrong password");
            }
            else if (txtCurrentPass.Text == "")
            {
                txtCurrentPass.BackColor = Color.LightPink;
                errorProvider1.SetError(txtCurrentPass, "This filed is required");
            }
            else
            {
                txtCurrentPass.BackColor = Color.White;
                errorProvider1.SetError(txtCurrentPass, "");
            }
        }

        private void txtNewPass_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPass.Text == "")
            {
                txtNewPass.BackColor = Color.LightPink;
                errorProvider1.SetError(txtNewPass, "This filed is required");
            }
            else
            {
                txtNewPass.BackColor = Color.White;
                errorProvider1.SetError(txtNewPass, "");
            }
        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPass.Text != txtNewPass.Text)
            {
                txtConfirmPass.BackColor = Color.LightPink;
                errorProvider1.SetError(txtConfirmPass, "Confirm password do not match the new password");
            }
            else if (txtConfirmPass.Text == "")
            {
                txtConfirmPass.BackColor = Color.LightPink;
                errorProvider1.SetError(txtConfirmPass, "This filed is required");
            }
            else
            {
                txtConfirmPass.BackColor = Color.White;
                errorProvider1.SetError(txtConfirmPass, "");
            }
        }

       
        private void btnSava_Click(object sender, EventArgs e)
        {
            this.ValidateChildren(ValidationConstraints.None);

            if (clsUtil.HasValidationErrors(errorProvider1, this))
            {
                MessageBox.Show("Please fix the highlighted errors before saving.");
                return;
            }

            string hashedNewPass = clsUtil.GetHasedPassword(txtNewPass.Text.Trim());
            _user.Password = hashedNewPass;

            if (_user.Save())
            {
                MessageBox.Show("Data saved successfully");
                txtCurrentPass.Text = "";
                txtNewPass.Text = "";
                txtConfirmPass.Text = "";
            }
            else
            {
                MessageBox.Show("Failed to save data. Please try again.");
            }
        }

       
   
    
    
    }
}
