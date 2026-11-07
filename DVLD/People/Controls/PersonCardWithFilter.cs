using DVLD_Buisness;
using System;
using System.CodeDom;
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
    public partial class PersonCardWithFilter : UserControl
    {
        public PersonCardWithFilter()
        {
            InitializeComponent();
        }

        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }


        public int PersonId
        {
            get
            {
                return personCard1.PersonID;
            }
        }

        public clsPerson SelectedPerson
        {
            get
            {
                return personCard1.SelectedPerson;
            }
        }

        private bool _ShowAddPersonButton = true;
        public bool ShowAddPersonButton
        {
            get { return _ShowAddPersonButton; }

            set
            {
                _ShowAddPersonButton = value;
                btnAddPerson.Visible = _ShowAddPersonButton;
            }
        }
       
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                groupBox1.Enabled = value;
            }
        }

        public void loadForSpecificID(int perssonID)
        {
            if (clsPerson.Find(perssonID))
            {
                personCard1.LoadPersonInfo(perssonID);

                txtFindBy.Text = perssonID.ToString();
                cbFindBy.Text = "Person ID";
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFindBy.Text.Trim() == "" || cbFindBy.Text == "")
            {
                MessageBox.Show("Enter Person you want to search for");
                return;
            }


            if (cbFindBy.Text == "Person ID")
            {
                
                if (int.TryParse(txtFindBy.Text, out int PersonID))
                {
                    if (clsPerson.Find(PersonID))
                    {
                        personCard1.LoadPersonInfo(PersonID);
                    }
                    else
                    {
                        MessageBox.Show("Person not found");
                    }
                }
                else
                {
                    MessageBox.Show("wrong formate");
                }
            }
            else
            {
                if (clsPerson.Find(txtFindBy.Text))
                {
                    personCard1.LoadPersonInfo(txtFindBy.Text);
                }
                else
                {
                    MessageBox.Show("Person not found");
                }
            }

            if (OnPersonSelected != null && FilterEnabled && personCard1.PersonID > 0)
            {
                PersonSelected(personCard1.PersonID);
            }

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Add_UpdatePerson add_UpdatePerson = new Add_UpdatePerson();
            add_UpdatePerson.DataBack += loadForSpecificID;

            add_UpdatePerson.ShowDialog();
        }
    }
}
