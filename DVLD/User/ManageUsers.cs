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
    public partial class ManageUsers : Form
    {
        DataTable _dtUsers;
        DataTable _dtUsersView;
        public ManageUsers()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            _dtUsers = clsUser.GetAllUsers();
            _dtUsersView = _dtUsers.DefaultView.ToTable(false, "UserID", "PersonID", "FullName", "UserName", "IsActive");
            dataGridView1.DataSource = _dtUsersView;
            TotalNumber.Text = _dtUsers.Rows.Count.ToString();
            FilterList.SelectedIndex = 0;
        }

        private void _RefreshData(int userid)
        {
            if (userid > 0)
            {
                _LoadData();
            }
        }
        

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            _LoadData();

            if (_dtUsers.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "User ID";
                dataGridView1.Columns[0].Width = 70;

                dataGridView1.Columns[1].HeaderText = "Person ID";
                dataGridView1.Columns[1].Width = 80;

                dataGridView1.Columns[2].HeaderText = "Full Name";
                dataGridView1.Columns[2].Width = 250;

                dataGridView1.Columns[3].HeaderText = "UserName";
                dataGridView1.Columns[3].Width = 180;

                dataGridView1.Columns[4].HeaderText = "Is Active";
                dataGridView1.Columns[4].Width = 80;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FilterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilterList.Text == "Is Active")
            {
                _dtUsersView.DefaultView.RowFilter = "IsActive = true";
                TotalNumber.Text = _dtUsersView.DefaultView.Count.ToString();
                return;
            }
            else
            {
                _dtUsersView.DefaultView.RowFilter = "";
                TotalNumber.Text = _dtUsersView.DefaultView.Count.ToString();
            }

            txtdataFilter.Visible = (FilterList.Text.ToString() != "None");

            if (txtdataFilter.Visible)
            {
                txtdataFilter.Text = "";
                txtdataFilter.Focus();
            }
        }

        private void txtdataFilter_TextChanged(object sender, EventArgs e)
        {
            string filtername = "";

            switch(FilterList.Text)
            {
                case "User ID":
                    filtername = "UserID";
                    break;
                case "Person ID":
                    filtername = "PersonID";
                    break;
                case "Full Name":
                    filtername = "FullName";
                    break;
                case "UserName":
                    filtername = "UserName";
                    break;
                case "Is Active":
                    filtername = "IsActive";
                    break;
                default:
                    filtername = "None";
                    break;
            }

            if (filtername == "None" || txtdataFilter.Text.Trim() == "")
            {
                _dtUsersView.DefaultView.RowFilter = "";
                TotalNumber.Text = _dtUsers.Rows.Count.ToString();
                return;
            }

            if (filtername == "PersonID" || filtername == "UserID")
            {
                _dtUsersView.DefaultView.RowFilter = $"{filtername} = {txtdataFilter.Text}";
            }
            else
            {
                _dtUsersView.DefaultView.RowFilter = $"{filtername} like '%{txtdataFilter.Text}%'";
            }

            TotalNumber.Text = _dtUsers.Rows.Count.ToString();
        }

        private void txtdataFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FilterList.Text == "Person ID" || FilterList.Text == "User ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userid = (int)dataGridView1.CurrentRow.Cells[0].Value;

            UserDeatails userDeatils = new UserDeatails(userid);
            userDeatils.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userid = (int)dataGridView1.CurrentRow.Cells[0].Value;

            ChangePasswordForm form = new ChangePasswordForm(userid);
            form.ShowDialog();
        }

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userid = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (clsUser.DeleteUser(userid))
            {
                MessageBox.Show("User deleted successfully");
                _LoadData();
            }
            else
            {
                MessageBox.Show("User has data linked to it");
            }
        }

        private void addPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_UpdateUser adduser = new Add_UpdateUser();
            adduser.DataBack += _RefreshData;
            adduser.ShowDialog();
        }

        private void updatePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userid = (int)dataGridView1.CurrentRow.Cells[0].Value;
            Add_UpdateUser updateUser = new Add_UpdateUser(userid);
            updateUser.DataBack += _RefreshData;
            updateUser.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_UpdateUser adduser = new Add_UpdateUser();
            adduser.DataBack += _RefreshData;
            adduser.ShowDialog();
        }
   
    
    }
}
