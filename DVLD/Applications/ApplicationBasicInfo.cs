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

namespace DVLD.Applications
{
    public partial class ApplicationBasicInfo : UserControl
    {
        private int _AppId;
        private clsApplication _ApplicationInfo;

        public int SelectAppId
        {
            get
            {
                return _AppId;
            }
        }

        public clsApplication SelectedApplicationInfo
        {
            get
            {
                return _ApplicationInfo;
            }
        }


        public ApplicationBasicInfo()
        {
            InitializeComponent();

        }

        public void LoadBaseApplicationData(int appId)
        {
            _AppId = appId;

            if (_AppId > 0)
            {              
                FindApplication();
            }
            else
            {
                MessageBox.Show("Error while loading data");
            }
        }

        private void FindApplication()
        {
            _ApplicationInfo = clsApplication.GetBaseApplicationInfo(_AppId);

            if (_ApplicationInfo != null)
            {
                lblApplicationID.Text = _ApplicationInfo.ApplicationID.ToString();
                lblStatus.Text = _ApplicationInfo.StatusText;
                lblFees.Text = _ApplicationInfo.PaiedFees.ToString();
                lblType.Text = _ApplicationInfo.ApplicationTypeData.ApplicationTypeTitle;
                lblApplicant.Text = _ApplicationInfo.ApplicantFullName;
                lblDate.Text = _ApplicationInfo.ApplicationDate.ToShortDateString();
                lblStatusDate.Text = _ApplicationInfo.LastStatusDate.ToShortDateString();
                lblCreatedByUser.Text = _ApplicationInfo.CreatedByUserInfo.Username;
            }
            else
                MessageBox.Show("Error while loading data");
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDeatils person = new PersonDeatils(_ApplicationInfo.ApplicantPersonID);
            person.ShowDialog();
            lblApplicant.Text = _ApplicationInfo.ApplicantFullName;

        }
    }
}
