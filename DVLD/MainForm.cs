using DVLD.Applications.International_License_Applications;
using DVLD.Applications.Release_License_Applications;
using DVLD.Applications.Renew_Driving_License_Application;
using DVLD.Applications.Replace_For_Lost_Or_Damage;
using DVLD.GLobal_Classes;
using DVLD.Licenses.Detained_Licenses;
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
    public partial class MainForm : Form
    {
        LoginForm _loginfrm;
        public MainForm(LoginForm frm)
        {
            InitializeComponent();
            _loginfrm = frm;
        }

        private void managePeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form people = new ManagePeople();

            //people.MdiParent = this;
            people.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageUsers manageUsers = new ManageUsers();
            //manageUsers.MdiParent = this;
            manageUsers.ShowDialog();
        }

      

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDeatails userDeatails = new UserDeatails(clsGlobal.CurrentUser.UserID, true);
            userDeatails.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm change = new ChangePasswordForm(clsGlobal.CurrentUser.UserID);
            change.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _loginfrm.Show();
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_loginfrm.Visible)
            {
                Application.Exit();
            }
        }

        private void testsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageTestTypes manageTestTypes = new ManageTestTypes();
            manageTestTypes.ShowDialog();
        }

        

        private void applicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageApplicationTypes manageApplicationTypes = new ManageApplicationTypes();
            manageApplicationTypes.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_UpdateLocalDrivingLicenseApplication addLocalApp = new Add_UpdateLocalDrivingLicenseApplication();
            addLocalApp.ShowDialog();
        }

        private void localDrivingLiceseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageLocalDrivingLicenseApplications manage = new ManageLocalDrivingLicenseApplications();
            manage.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDrivers Drivers = new ManageDrivers();
            Drivers.ShowDialog();
        }

        private void reNewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReNewDrivingLicense reNewLicense = new ReNewDrivingLicense();
            reNewLicense.ShowDialog();
        }

        private void replaceLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceLostOrDamagedLicense replace = new ReplaceLostOrDamagedLicense();
            replace.ShowDialog();
        }

        private void internatonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewInternationalLicenseApplication addNewInternationalLicenseApplication = new AddNewInternationalLicenseApplication();
            addNewInternationalLicenseApplication.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageInternationalLicenseApplications manageInternationalLicenseApplications = new ManageInternationalLicenseApplications();
            manageInternationalLicenseApplications.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseLicense releaseLicense = new ReleaseLicense();
            releaseLicense.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDetainedLicenses manageDetainedLicenses = new ManageDetainedLicenses();
            manageDetainedLicenses.ShowDialog();
        }

        private void detainLicensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Detain_License detainLicense = new Detain_License();
            detainLicense.ShowDialog();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseLicense releaseLicense = new ReleaseLicense();
            releaseLicense.ShowDialog();
        }
    }
    }
 