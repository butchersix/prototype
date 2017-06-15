using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace prototype
{
    public partial class FormRegisterUpdate : Form
    {
        public MySqlConnection conn;
        public FormRegisterMain reference_to_frmregistermain { get; set; }
        public FormRegisterUsePass reference_to_frmregisterusepass { get; set; }
        public FormProfile reference_to_frmprofile { get; set; }
        String username, firstname, middlename, lastname, fullname, nickname, birthday, gender, civilstatus, citizenship, homeaddress, homephonenumber,
            mobilenumber, emailaddress, companyschoolname, profession, companyschoolphonenumber, companyschooladdress,
            namespouse, mobilenumberspouse, birthdayspouse;
        int genderupdate;
        public FormRegisterUpdate()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void FormRegisterUpdate_Load(object sender, EventArgs e)
        {
            Rifrish();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                username = reference_to_frmprofile.username;
                conn.Open();
                MySqlCommand comm = new MySqlCommand("UPDATE member_tbl SET firstName = '" + txtFirstName.Text + "', "
                    + "middleName = '" + txtMiddleName.Text + "', lastName = '" + txtLastName.Text + "', "
                    + "nickName = '" + txtNickName.Text + "', birthdate = '" + convertBirthDate()[0] + "', "
                    + "gender = '" + convertGender() + "', civilStatus = '" + convertCivilStatus() + "', "
                    + "citizenship = '" + convertCitizenship() + "', homeAddress = '" + txtHomeAddress.Text + "', "
                    + "homePhoneNumber = '" + txtHomeNumber.Text + "', "
                    + "contactNum = '" + txtMobileNumber.Text + "', emailAd = '" + txtEmailAddress.Text + "', "
                    + "occupation = '" + txtProfession.Text + "', companyName = '" + txtCompanySchoolName.Text + "', "
                    + "companyContactNum = '" + txtCompanyPhoneNumber.Text + "', "
                    + "companyAddress = '" + txtCompanySchoolAddress.Text + "', "
                    + "schoolName = '" + txtCompanySchoolName.Text + "', "
                    + "schoolContactNum = '" + txtCompanyPhoneNumber.Text + "', "
                    + "schoolAddress = '" + txtCompanySchoolAddress.Text + "', spouseName = '" + txtSpouse.Text + "', "
                    + "spouseContactNum = '" + txtMobileNumberSpouse.Text + "', spouseBirthdate = '" + convertBirthDate()[1] + "' "
                    + "WHERE membUsername = '" + username + "';", conn);
                comm.ExecuteNonQuery();
                conn.Close();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private String[] convertBirthDate()
        {
            String[] birthdates = new String[2];
            String birthdate = dtpBirthday.Value.Date.ToString("MMMM dd, yyyy");
            String birthdatespouse = dtpSpouseBirthday.Value.Date.ToString("MMMM dd, yyyy");
            for (int i = 0; i < birthdates.Length; i++)
            {
                if (i == 0)
                    birthdates[i] = birthdate;
                else if (i == 1)
                    birthdates[1] = birthdatespouse;
            }
            return birthdates;
        }

        private String convertCitizenship()
        {
            if (rbnFilipino.Checked == true)
                citizenship = "Filipino";
            else if (rbnOthers.Checked == true)
                citizenship = txtOthers.Text;
            return citizenship;
        }

        private void FormRegisterUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            reference_to_frmprofile.Rifrish();
            reference_to_frmprofile.Show();
        }

        private String convertCivilStatus()
        {
            if (rbnSingle.Checked == true)
                civilstatus = "Single";
            else if (rbnMarried.Checked == true)
                civilstatus = "Married";
            else if (rbnSingleParent.Checked == true)
                civilstatus = "Single Parent";
            else if (rbnAnnulled.Checked == true)
                civilstatus = "Annulled";
            else if (rbnSeparated.Checked == true)
                civilstatus = "Separated";
            else if (rbnWidower.Checked == true)
                civilstatus = "Widower";
            return civilstatus;
        }

        private int convertGender()
        {
            if (rbnMale.Checked == true)
                genderupdate = 0;
            else if (rbnFemale.Checked == true)
                genderupdate = 1;
            return genderupdate;
        }

        private void Rifrish()
        {
            firstname = reference_to_frmprofile.firstname;
            middlename = reference_to_frmprofile.middlename;
            lastname = reference_to_frmprofile.lastname;
            nickname = reference_to_frmprofile.nickname;
            birthday = reference_to_frmprofile.birthday;
            gender = reference_to_frmprofile.gender;
            civilstatus = reference_to_frmprofile.civilstatus;
            citizenship = reference_to_frmprofile.citizenship;
            homeaddress = reference_to_frmprofile.homeaddress;
            homephonenumber = reference_to_frmprofile.homephonenumber;
            mobilenumber = reference_to_frmprofile.mobilenumber;
            emailaddress = reference_to_frmprofile.emailaddress;
            profession = reference_to_frmprofile.profession;
            companyschoolname = reference_to_frmprofile.companyschoolname;
            companyschoolphonenumber = reference_to_frmprofile.companyschoolphonenumber;
            companyschooladdress = reference_to_frmprofile.companyschooladdress;
            namespouse = reference_to_frmprofile.namespouse;
            mobilenumberspouse = reference_to_frmprofile.mobilenumberspouse;
            birthdayspouse = reference_to_frmprofile.birthdayspouse;

            txtFirstName.Text = firstname;
            txtMiddleName.Text = middlename;
            txtLastName.Text = lastname;
            txtNickName.Text = nickname;
            DateTime date = DateTime.ParseExact(birthday, "MMMM dd, yyyy", null);
            dtpBirthday.Value = date;

            if (gender == "Male")
                rbnMale.Checked = true;
            else if (gender == "Female")
                rbnFemale.Checked = true;

            if (civilstatus == "Single")
                rbnSingle.Checked = true;
            else if (civilstatus == "Married")
                rbnMarried.Checked = true;
            else if (civilstatus == "Single Parent")
                rbnSingleParent.Checked = true;
            else if (civilstatus == "Annulled")
                rbnAnnulled.Checked = true;
            else if (civilstatus == "Separated")
                rbnSeparated.Checked = true;
            else if (civilstatus == "Widower")
                rbnWidower.Checked = true;

            if (citizenship == "Filipino")
                rbnFilipino.Checked = true;
            else
                txtOthers.Text = citizenship;

            txtHomeAddress.Text = homeaddress;
            txtHomeNumber.Text = homephonenumber;
            txtMobileNumber.Text = mobilenumber;
            txtEmailAddress.Text = emailaddress;
            txtProfession.Text  = profession;
            txtCompanySchoolName.Text = companyschoolname;
            txtCompanyPhoneNumber.Text = companyschoolphonenumber;
            txtCompanySchoolAddress.Text = companyschooladdress;
            txtSpouse.Text = namespouse;
            txtMobileNumberSpouse.Text = mobilenumberspouse;

        }
    }
}
