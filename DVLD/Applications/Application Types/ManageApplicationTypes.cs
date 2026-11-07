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
    public partial class ManageApplicationTypes : Form
    {
        DataTable _dtApplicationTypes;
        public ManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dataGridView1.DataSource = _dtApplicationTypes;
            TotalNumber.Text = _dtApplicationTypes.Rows.Count.ToString();
        }

        private void RefreshData(int AppTypeID)
        {
            if (AppTypeID > 0)
            {
                _LoadData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _LoadData();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[0].Width = 110;

                dataGridView1.Columns[1].HeaderText = "Title";
                dataGridView1.Columns[1].Width = 380;

                dataGridView1.Columns[2].HeaderText = "Fees";
                dataGridView1.Columns[2].Width = 110;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateApplicationType updateApplicationType = new UpdateApplicationType(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
            updateApplicationType.DataBack += RefreshData;
            updateApplicationType.ShowDialog();
        }
    }
}
