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
    public partial class ManageTestTypes : Form
    {
        DataTable _dtTestTypes;
        public ManageTestTypes()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            _dtTestTypes = clsTestType.GetAllTestTypes();
            dataGridView1.DataSource = _dtTestTypes;
            TotalNumber.Text = _dtTestTypes.Rows.Count.ToString();
        }

        private void RefreshData(clsTestType.enTestTypes TestTypeID)
        {
            if (TestTypeID > 0)
            {
                _LoadData();
            }
        }

        private void ManageTestTypes_Load(object sender, EventArgs e)
        {
            _LoadData();

            if (_dtTestTypes.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[0].Width = 50;

                dataGridView1.Columns[1].HeaderText = "Title";
                dataGridView1.Columns[1].Width = 100;

                dataGridView1.Columns[2].HeaderText = "Description";
                dataGridView1.Columns[2].Width = 480;

                dataGridView1.Columns[3].HeaderText = "Fees";
                dataGridView1.Columns[3].Width = 80;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTestType updateTestType = new UpdateTestType((clsTestType.enTestTypes)dataGridView1.CurrentRow.Cells[0].Value);
            updateTestType.DataBack += RefreshData;
            updateTestType.ShowDialog();
        }
    }
}
