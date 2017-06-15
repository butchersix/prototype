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
    public partial class RegisterNewComer : Form
    {
        public MySqlConnection conn;
        public FormLogin reference_to_frmlogin { get; set; }
        String citizenship, civilstatus;
        int gender;
        public int memberID;
        public RegisterNewComer()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void RegisterNewComer_Load(object sender, EventArgs e)
        {

        }

        private void RegisterNewComer_FormClosing(object sender, FormClosingEventArgs e)
        {
            reference_to_frmlogin.Show();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("INSERT INTO member_tbl(firstName, middleName, lastName, "
                    + "nickName, birthdate, gender, civilStatus, citizenship, homeAddress, homePhoneNumber, contactNum, emailAd, "
                    + "occupation, companyName, companyContactNum, companyAddress, schoolName, schoolContactNum, "
                    + "schoolAddress, spouseName, spouseContactNum, spouseBirthdate, memberType) VALUES('" + txtFirstName.Text + "', '" + txtMiddleName.Text + "', "
                    + "'" + txtLastName.Text + "', '" + txtNickName.Text + "', '" + convertBirthDate()[0] + "', "
                    + "'" + convertGender() + "','" + convertCivilStatus() + "', '" + convertCitizenship() + "', "
                    + "'" + txtHomeAddress.Text + "', '" + txtHomeNumber.Text + "', '" + txtMobileNumber.Text + "', "
                    + "'" + txtEmailAddress.Text + "', '" + txtProfession.Text + "', '" + txtCompanySchoolName.Text + "', "
                    + "'" + txtCompanyPhoneNumber.Text + "', '" + txtCompanySchoolAddress.Text + "', "
                    + "'" + txtCompanySchoolName.Text + "', '" + txtCompanyPhoneNumber.Text + "', "
                    + "'" + txtCompanySchoolAddress.Text + "', '" + txtSpouse.Text + "', '" + txtMobileNumberSpouse.Text + "', "
                    + "'" + convertBirthDate()[1] + "', 0);", conn);
                comm.ExecuteNonQuery();
                conn.Close();
                FormRegisterUsePass frmregisterusepass = new FormRegisterUsePass();
                frmregisterusepass.reference_to_frmregisternewcomer = this;
                frmregisterusepass.reference_to_frmlogin = reference_to_frmlogin;
                this.Hide();
                frmregisterusepass.Show();
            }
            catch(Exception ex)
            {

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
                gender = 0;
            else if (rbnFemale.Checked == true)
                gender = 1;
            return gender;
        }

        public Boolean ifFormRegisterNewComer()
        {
            return true;
        }
        public int getMemberID()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT memberID FROM member_tbl ORDER BY memberID DESC LIMIT 1", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    memberID = int.Parse(dt.Rows[0]["memberID"].ToString());
                }
                return memberID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
