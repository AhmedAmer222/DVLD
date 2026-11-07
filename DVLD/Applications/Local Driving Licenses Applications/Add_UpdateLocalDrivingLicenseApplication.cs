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
    public partial class Add_UpdateLocalDrivingLicenseApplication : Form
    {
        public delegate void DataBackEventHandeler(int LocalAppId);
        public event DataBackEventHandeler DataBack;
        private enum enMode { Add, Update };

        private enMode _Mode;
        private int _LocalAppId = 0;

        clsLocalDrivingLicenseApplication _localDrivingApp;
        public Add_UpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.Add;
        }

        public Add_UpdateLocalDrivingLicenseApplication(int LocalAppId)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalAppId = LocalAppId;
            
        }

        private void _FillLicenseClassList()
        {
            DataTable LicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in LicenseClasses.Rows)
            {
                cbbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }

        private void _LoadDataToNewApplication()
        {
            _localDrivingApp = new clsLocalDrivingLicenseApplication();

            _localDrivingApp.ApplicantPersonID = personCardWithFilter1.PersonId;
            _localDrivingApp.ApplicationDate = DateTime.Now;
            _localDrivingApp.ApplicationTypeID = (int) clsApplication.enApplicationType.NewLocalDrivingLicenseService;
            _localDrivingApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _localDrivingApp.LastStatusDate = DateTime.Now;
            _localDrivingApp.PaiedFees = int.Parse(labAppFees.Text);
            _localDrivingApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _localDrivingApp.LicenseClassesID = clsLicenseClass.GetLicenseClassByName(cbbLicenseClasses.Text).LicenseClassID;
        }

        private void _loadDataToUpdateApplication()
        {
            _localDrivingApp.LastStatusDate = DateTime.Now;
            _localDrivingApp.LicenseClassesID = clsLicenseClass.GetLicenseClassByName(cbbLicenseClasses.Text).LicenseClassID;
            _localDrivingApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _localDrivingApp.PaiedFees = int.Parse(labAppFees.Text);
            _localDrivingApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

        private void Add_UpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillLicenseClassList();

            labAppFees.Text = clsApplicationType.GetApplicationTypeByID((int)clsApplication.enApplicationType.NewLocalDrivingLicenseService).ApplicationFees.ToString();

            if (_Mode == enMode.Add)
            {
                labCreatedBy.Text = clsGlobal.CurrentUser.Username;
                labAppDate.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                personCardWithFilter1.FilterEnabled = false;
                _localDrivingApp = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(_LocalAppId);
                personCardWithFilter1.loadForSpecificID(_localDrivingApp.ApplicantPersonID);
                btnNext.Enabled = true;
                labAppID.Text = _localDrivingApp.LocalDrivingLicenseApplicationID.ToString();
                labAppDate.Text = _localDrivingApp.ApplicationDate.ToShortDateString();
                cbbLicenseClasses.SelectedItem = clsLicenseClass.GetLicenseClassByID(_localDrivingApp.LicenseClassesID).ClassName;
                labCreatedBy.Text = clsUser.GetUserByID(_localDrivingApp.CreatedByUserID).Username;

            }
        }

        private void personCardWithFilter1_OnPersonSelected(int obj)
        {
            btnNext.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;
            panLoginInfo.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int personId = personCardWithFilter1.PersonId;
            int LicenseClassid = clsLicenseClass.GetLicenseClassByName(cbbLicenseClasses.Text).LicenseClassID;

            if (clsLocalDrivingLicenseApplication.DoesHasActiveApplicationForLicenseClass(personId, LicenseClassid) > 0)
            {
                MessageBox.Show("This person already has an active application for this license class");
                return;
            }

            if (_Mode == enMode.Add)
            {
                _LoadDataToNewApplication();         
            }
            else
            {
                _loadDataToUpdateApplication();
            }


            if (_localDrivingApp.Save())
            {

                if (_Mode == enMode.Add)
                {
                    _Mode = enMode.Update;
                    labTitle.Text = "Update Local Driving License Application";
                    labAppID.Text = _localDrivingApp.LocalDrivingLicenseApplicationID.ToString();
                }

                MessageBox.Show("Data Saved Successfully");
                DataBack?.Invoke(_localDrivingApp.LocalDrivingLicenseApplicationID);
            }
            else
            {
                MessageBox.Show("Error while saving data");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
   
    
    
    }
}
