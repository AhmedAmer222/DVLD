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
    public partial class PersonDeatils : Form
    {
      
        public PersonDeatils(int personID)
        {
            InitializeComponent();

            personCard1.LoadPersonInfo(personID);
        }

        public PersonDeatils(string NationalNo)
        {
            InitializeComponent();

            personCard1.LoadPersonInfo(NationalNo);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
