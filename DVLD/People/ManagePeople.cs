using DVLD_Buisness;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ManagePeople : Form
    {
        private DataTable _PeopleData;
        private DataTable _PeopleDataView;


        public ManagePeople()
        {
            InitializeComponent();       
        }

        private void _LoadData()
        {
            _PeopleData = clsPerson.GetAllPeople();        
            _PeopleDataView = _PeopleData.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gender", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");     
            dataGridView1.DataSource = _PeopleDataView;
            TotalNumber.Text = _PeopleData.Rows.Count.ToString(); 
        }

        private void _RefreshData(int personId)
        {
            if (personId != 0)
            {
                _LoadData();
            }
        }

        private void ManagePeople_Load(object sender, EventArgs e)
        {
            _LoadData();



            if (dataGridView1.Rows.Count > 0)
            {

                dataGridView1.Columns[0].HeaderText = "Person ID";
                dataGridView1.Columns[0].Width = 110;

                dataGridView1.Columns[1].HeaderText = "National No.";
                dataGridView1.Columns[1].Width = 120;


                dataGridView1.Columns[2].HeaderText = "First Name";
                dataGridView1.Columns[2].Width = 120;

                dataGridView1.Columns[3].HeaderText = "Second Name";
                dataGridView1.Columns[3].Width = 140;


                dataGridView1.Columns[4].HeaderText = "Third Name";
                dataGridView1.Columns[4].Width = 120;

                dataGridView1.Columns[5].HeaderText = "Last Name";
                dataGridView1.Columns[5].Width = 120;

                dataGridView1.Columns[6].HeaderText = "Gendor";
                dataGridView1.Columns[6].Width = 120;

                dataGridView1.Columns[7].HeaderText = "Date Of Birth";
                dataGridView1.Columns[7].Width = 140;

                dataGridView1.Columns[8].HeaderText = "Nationality";
                dataGridView1.Columns[8].Width = 120;


                dataGridView1.Columns[9].HeaderText = "Phone";
                dataGridView1.Columns[9].Width = 120;


                dataGridView1.Columns[10].HeaderText = "Email";
                dataGridView1.Columns[10].Width = 170;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtdataFilter.Visible = (FilterList.Text != "None");

            if (txtdataFilter.Visible)
            {
                txtdataFilter.Text = "";
                txtdataFilter.Focus();
            }
        }

        private void txtdataFilter_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (FilterList.Text)
            {
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "National No":
                    filterColumn = "NationalNo";
                    break;
                case "First Name":
                    filterColumn = "FirstName";
                    break;
                case "Second Name":
                    filterColumn = "SecondName";
                    break;
                case "Third Name":
                    filterColumn = "ThirdName";
                    break;
                case "Last Name":
                    filterColumn = "LastName";
                    break;
                 default:
                    filterColumn = "None";
                    break;
            }

            if (txtdataFilter.Text.Trim() == "" || filterColumn == "None")
            {
                _PeopleDataView.DefaultView.RowFilter = "";
                TotalNumber.Text = _PeopleData.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "PersonID")
            {
                _PeopleDataView.DefaultView.RowFilter = $"{filterColumn} = {txtdataFilter.Text.Trim()}";
            }
            else
            {
                _PeopleDataView.DefaultView.RowFilter = $"{filterColumn} like '%{txtdataFilter.Text}%'";
            }


            dataGridView1.DataSource = _PeopleDataView;
            TotalNumber.Text = _PeopleDataView.DefaultView.Count.ToString();

        }

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int personIdToDelete = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (personIdToDelete == 0)
            {
                MessageBox.Show("Please select a person to delete.");
                return;
            }


            

            if (MessageBox.Show($"Are you sure you want to delete person with ID: {personIdToDelete}",
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                clsPerson person = clsPerson.GetPersonByID(personIdToDelete);

                if (clsPerson.DeletePersonByID(personIdToDelete))
                {
                    if (!string.IsNullOrEmpty(person.ImagePath))
                    {
                        File.Delete(person.ImagePath);
                    }

                    MessageBox.Show("Person deleted successfully");
                    _LoadData();
                }
                else
                {
                    MessageBox.Show("This person has data linked to it");
                }
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form PersonForm = new PersonDeatils((int)dataGridView1.CurrentRow.Cells[0].Value);
            PersonForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_UpdatePerson addPerosn = new Add_UpdatePerson();
            addPerosn.DataBack += _RefreshData;
            addPerosn.ShowDialog();
        }

        private void addPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_UpdatePerson addPerosn = new Add_UpdatePerson();
            addPerosn.DataBack += _RefreshData;
            addPerosn.ShowDialog();
        }

        private void updatePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_UpdatePerson updateperson = new Add_UpdatePerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            updateperson.DataBack += _RefreshData;
            updateperson.ShowDialog();
        }

        private void txtdataFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(FilterList.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    
    
    }
}
