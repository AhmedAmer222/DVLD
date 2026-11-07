using DVLD.GLobal_Classes;
using DVLD_Buisness;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            string username = string.Empty, password = string.Empty;

            //if (clsGlobal.GetUserCredentials(ref username, ref password))
            if (clsGlobal.GetUserCredentialsUsingRegistry(ref username, ref password))
            {
                txtUserName.Text = username;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
                
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.ValidateChildren(ValidationConstraints.None);

            if (clsUtil.HasValidationErrors(errorProvider1 ,this))
            {
                MessageBox.Show("Please fill all required fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hashString = clsUtil.GetHasedPassword(txtPassword.Text);
          

            clsUser user = clsUser.Login(txtUserName.Text, hashString);
            if (user != null)
            {
                if (!user.IsActive)
                {
                    MessageBox.Show("Your account is not Active, Contact your Admin.", "Is Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clsEventLogger.LoginFailedEvent(user.Username);
                    return;
                }

                if (chkRememberMe.Checked)
                {
                    //clsGlobal.RememberUser(txtUserName.Text, txtPassword.Text);
                    clsGlobal.RememberUserUsingRegistry(txtUserName.Text, txtPassword.Text);

                }
                else
                {
                    clsGlobal.RememberUserUsingRegistry("", "");
                }


                

                clsGlobal.CurrentUser = user;

                clsEventLogger.LoginSuccessfulEvent(user.Username);

                this.Hide();
                MainForm main = new MainForm(this);
                main.ShowDialog();
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLogger.LoginFailedEvent(user.Username);
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            Control ctrl = sender as Control;

            if (string.IsNullOrEmpty(ctrl.Text))
            {
                ctrl.BackColor = Color.LightPink;
                errorProvider1.SetError(ctrl ,"This field is reqired");
            }
            else
            {
                ctrl.BackColor = Color.White;
                errorProvider1.SetError(ctrl, "");
            }
        }

      
    }
}
