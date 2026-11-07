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
    public partial class UpdateTestType : Form
    {
        public delegate void DataBackEventHandeler(clsTestType.enTestTypes TestTypeId);
        public event DataBackEventHandeler DataBack;

        private clsTestType.enTestTypes _testTypeId = clsTestType.enTestTypes.VisionTest;
        private clsTestType _TestType;

        public UpdateTestType(clsTestType.enTestTypes testTypeId)
        {
            InitializeComponent();
            _testTypeId = testTypeId;
        }

        private void UpdateTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.GetTestTypeByID(_testTypeId);

            labId.Text = _testTypeId.ToString();
            txtTitle.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.TestTypeFees.ToString();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            Control ctrl = sender as Control;

            if (ctrl.Text == "")
            {
                errorProvider1.SetError(ctrl, "This filed is required");
            }
            else
            {
                errorProvider1.SetError(ctrl, string.Empty);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSava_Click(object sender, EventArgs e)
        {
            this.ValidateChildren(ValidationConstraints.None);

            if (clsUtil.HasValidationErrors(errorProvider1, this))
            {
                MessageBox.Show("Please fix highlited errors", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTypeTitle = txtTitle.Text;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeFees = Convert.ToSingle(txtFees.Text);

            if (_TestType.Save())
            {
                MessageBox.Show("Application Type Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(_testTypeId);
            }
            else
            {
                MessageBox.Show("Failed to Update Application Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
