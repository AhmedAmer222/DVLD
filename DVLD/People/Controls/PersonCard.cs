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
using System.IO;

namespace DVLD
{
    public partial class PersonCard : UserControl
    {
        private clsPerson person;
        private int _personID = 0;

        public int PersonID
        {
            get { return _personID; }
        }

        public clsPerson SelectedPerson
        {
            get { return person; }
        }


        public PersonCard()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int personID)
        {
            person = clsPerson.GetPersonByID(personID);

            if (person == null)
                return;

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            person = clsPerson.GetPersonByNationalNo(NationalNo);

            if (person == null)
                return;

            _FillPersonInfo();
        }

        
        private void _FillPersonInfo()
        {
            _personID = person.PersonID;
            txtPersonID.Text = _personID.ToString();
            txtName.Text = person.FullName;
            txtNationalNo.Text = person.NationalNo;

            if (person.Gender == 0)
                txtGender.Text = "Male";
            else
                txtGender.Text = "Female";

            txtEmail.Text = person.Email;
            txtAddress.Text = person.Address;
            txtDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            txtPhone.Text = person.Phone;
            txtCountry.Text = person.NationalityCountryName;

            _LoadPerosonImage();
        }
        private void _LoadPerosonImage()
        {
            if (!string.IsNullOrEmpty(person.ImagePath))
            {
                if (File.Exists(person.ImagePath))
                {
                    pbProfileImage.ImageLocation = person.ImagePath;
                }
            }
            else
            {
                if (person.Gender == 0)
                    pbProfileImage.Image = Properties.Resources.Male_512;
                else
                    pbProfileImage.Image = Properties.Resources.Female_512;
            }
        }

        
        
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_UpdatePerson frm = new Add_UpdatePerson(_personID);
            frm.ShowDialog();

            LoadPersonInfo(_personID);
        }
    
    
    }
}
