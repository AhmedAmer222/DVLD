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
using DVLD.Properties;
using System.Runtime.InteropServices;
using DVLD.GLobal_Classes;

namespace DVLD
{
    public partial class Add_UpdatePerson : Form
    {
        public delegate void DataBackEventHandeler(int personId);
        public event DataBackEventHandeler DataBack;

        private clsPerson person = new clsPerson();
        private int _personID;
        private short _mode;
       

        public Add_UpdatePerson()
        {
            InitializeComponent();
            _mode = 0;
        }

        public Add_UpdatePerson(int personID)
        {
            InitializeComponent();
            _personID = personID;
            _mode = 1;
        }

        private void _LoadDataToPerson()
        {
            person.FirstName = txtFirst.Text;
            person.SecondName = txtSecond.Text;
            person.ThirdName = txtThird.Text;
            person.LastName = txtLast.Text;

            person.NationalNo = txtNationalNo.Text;

            if (rbMale.Checked)
                person.Gender = 0;
            else
                person.Gender = 1;

            person.Email = txtEmail.Text;
            person.Address = txtAddress.Text;

            person.DateOfBirth = dateTimePicker1.Value;
            person.Phone = txtPhone.Text;
            person.NationalityCountryID = (cbbCountry.SelectedIndex + 1).ToString();
            person.NationalityCountryName = cbbCountry.SelectedItem.ToString();

            if (pbProfileImage.ImageLocation != null)
            {
                person.ImagePath = pbProfileImage.ImageLocation;
            }
            else
            {
                person.ImagePath = null;
            }

        }
        private void _LoadDataFromPerons()
        {
            txtTitle.Text = "Update Person";

            labPersonID.Text = person.PersonID.ToString();

            txtFirst.Text = person.FirstName;
            txtSecond.Text = person.SecondName;
            txtThird.Text = person.ThirdName;
            txtLast.Text = person.LastName;

            txtNationalNo.Text = person.NationalNo;



            txtEmail.Text = person.Email;
            txtAddress.Text = person.Address;

            dateTimePicker1.Value = person.DateOfBirth;
            txtPhone.Text = person.Phone;

            cbbCountry.SelectedItem = person.NationalityCountryName;

            if (string.IsNullOrEmpty(person.ImagePath))
            {
                if (person.Gender == 0)
                    pbProfileImage.Image = Properties.Resources.Male_512;
                else
                    pbProfileImage.Image = Properties.Resources.Female_512;
            }
            else
            {
                if (File.Exists(person.ImagePath))
                {
                    pbProfileImage.ImageLocation = person.ImagePath;
                    linkRemove.Visible = true;
                }
            }

            if (person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;
        }

        private bool _HandelSaveImage()
        {
            if (pbProfileImage.ImageLocation != person.ImagePath)
            {
                if (!string.IsNullOrEmpty(person.ImagePath))
                {
                    try
                    {
                        File.Delete(person.ImagePath);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (pbProfileImage.ImageLocation != null)
                {
                    string imagePath = pbProfileImage.ImageLocation;

                    if (clsUtil.CopyImageToProjectImagesFolder(ref imagePath))
                    {
                        pbProfileImage.ImageLocation = imagePath;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }

            return true;
        }

       


        private void Add_UpdatePerson_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            
            if (_mode == 0)
            {
                txtTitle.Text = "Add New Person";
            }
            else
            {
                person = clsPerson.GetPersonByID(_personID);

                if (person == null)
                {
                    MessageBox.Show("this person not foound");
                    this.Close();
                    return;
                }
                else
                {
                    _LoadDataFromPerons();
                }
            }
        }

        private void btnSava_Click(object sender, EventArgs e)
        {

            this.ValidateChildren(ValidationConstraints.None); // ✅ runs all Validating events at once

            if (clsUtil.HasValidationErrors(errorProvider1, this))
            {
                MessageBox.Show("Please fix the highlighted errors before saving.");
                return;
            }

            if (!_HandelSaveImage())
                return;  


            _LoadDataToPerson();

            if (person.Save())
            {
                txtTitle.Text = "Update Person";
                _mode = 1;
                labPersonID.Text = person.PersonID.ToString();
               
                MessageBox.Show("Data saved successfully");
                DataBack?.Invoke(person.PersonID);
            }
            else
            {
                MessageBox.Show("Error data not saved");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //OnCheckIsUpdated.Invoke(_IsUpdated);
            this.Close();
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                //e.Cancel = true;
                //txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "NationalNo is required");
            }
            else if (clsPerson.Find(txtNationalNo.Text) && person.NationalNo != txtNationalNo.Text)
            {
                //e.Cancel = true;
                //txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "This NationalNo is used for another person");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, "");
            }

        }

        private void requiredFields_Validating(object sender, CancelEventArgs e)
        {
            Control control = sender as Control;


            if (string.IsNullOrEmpty(control.Text))
            {
                errorProvider1.SetError(control, "This fiels is required");
            }
            else
            {
                errorProvider1.SetError(control, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                if (!txtEmail.Text.EndsWith("@gmail.com"))
                {
                    errorProvider1.SetError(txtEmail, "Wrong email format");
                }
                else
                {
                    errorProvider1.SetError(txtEmail, "");
                }
            }
        }
    
        private void Gender_CheckedChanged_1(object sender, EventArgs e)
        {
            if (pbProfileImage.ImageLocation == null)
            {
                if (rbMale.Checked)
                {
                    pbProfileImage.Image = Properties.Resources.Male_512;
                }              
                else if (rbFemale.Checked)
                {
                    pbProfileImage.Image = Properties.Resources.Female_512;
                }
            }
        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbProfileImage.ImageLocation = null;


            if (rbFemale.Checked)
            {
                pbProfileImage.Image = Properties.Resources.Female_512;

            }
            else
            {
                pbProfileImage.Image = Properties.Resources.Male_512;
            }

           
            linkRemove.Visible = false;
        }

        private void linkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            
            dlg.Title = "Select Profile Picture";
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dlg.FileName))
                {
                    pbProfileImage.ImageLocation = dlg.FileName;
                    linkRemove.Visible = true;
                }
            }
        }
    
    
    }
    
}
