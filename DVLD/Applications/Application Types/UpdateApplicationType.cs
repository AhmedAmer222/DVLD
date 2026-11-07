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
    public partial class UpdateApplicationType : Form
    {
        public delegate void DataBackEventHandeler(int AppTypeID);
        public event DataBackEventHandeler DataBack;

        private clsApplicationType _applicationType;
        private int _ApplicationTypeID;
        public UpdateApplicationType(int AppTypeId)
        {
            InitializeComponent();
            _ApplicationTypeID = AppTypeId;
        }

        private void UpdateApplicationType_Load(object sender, EventArgs e)
        {
            _applicationType = clsApplicationType.GetApplicationTypeByID(_ApplicationTypeID);

            txtTitle.Text = _applicationType.ApplicationTypeTitle;
            txtFees.Text = _applicationType.ApplicationFees.ToString();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            Control ctrl = sender as Control;

            if (string.IsNullOrEmpty(ctrl.Text))
            {
                errorProvider1.SetError(ctrl, "This field is reqired");
            }
            else
            {
                errorProvider1.SetError(ctrl, string.Empty);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSava_Click(object sender, EventArgs e)
        {
            this.ValidateChildren(ValidationConstraints.None);

            if (clsUtil.HasValidationErrors(errorProvider1, this))
                return;

            _applicationType.ApplicationTypeTitle = txtTitle.Text;
            _applicationType.ApplicationFees = Convert.ToSingle(txtFees.Text);

            if (_applicationType.Save())
            {
                MessageBox.Show("Application Type Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(_applicationType.ApplicationTypeID);
            }
            else
            {
                MessageBox.Show("Failed to Update Application Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
