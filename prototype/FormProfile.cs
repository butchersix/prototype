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
    public partial class FormProfile : Form
    {
        public MySqlConnection conn;
        public FormHome reference_to_frmhome { get; set; }
        public FormLogin reference_to_frmlogin { get; set; }
        public FormDgroup reference_to_frmdgroup { get; set; }
        public FormRegisterUpdate reference_to_frmregisterupdate { get; set; }
        public String username, firstname, middlename, lastname, fullname, nickname, birthday, gender, civilstatus, citizenship, homeaddress, homephonenumber,
            mobilenumber, emailaddress, companyschoolname, profession, companyschoolphonenumber, companyschooladdress,
            namespouse, mobilenumberspouse, birthdayspouse;

        private void btnDgroup_Click(object sender, EventArgs e)
        {
            FormDgroup frmdgroup = new FormDgroup();
            frmdgroup.reference_to_frmprofile = this;
            this.Hide();
            frmdgroup.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            reference_to_frmlogin.Show();
        }

        Boolean sidepanel = true;
        public FormProfile()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void FormProfile_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            Rifrish();
        }

        public void Rifrish()
        {
            try
            {
                username = reference_to_frmhome.username; // originally String username= reference_to_frmhome.username;
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM member_tbl WHERE membUsername = '" + username + "'", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    firstname = dt.Rows[0]["firstName"].ToString();
                    middlename = dt.Rows[0]["middleName"].ToString();
                    lastname = dt.Rows[0]["lastName"].ToString();
                    nickname = dt.Rows[0]["nickName"].ToString();
                    birthday = dt.Rows[0]["birthdate"].ToString();
                    gender = dt.Rows[0]["gender"].ToString();
                    if (gender == "0")
                        gender = "Male";
                    else
                        gender = "Female";
                    civilstatus= dt.Rows[0]["civilStatus"].ToString();
                    citizenship = dt.Rows[0]["citizenship"].ToString();
                    homeaddress = dt.Rows[0]["homeAddress"].ToString();
                    homephonenumber= dt.Rows[0]["homePhoneNumber"].ToString();
                    mobilenumber = dt.Rows[0]["contactNum"].ToString();
                    emailaddress = dt.Rows[0]["emailAd"].ToString();
                    profession = dt.Rows[0]["occupation"].ToString();
                    companyschoolname = dt.Rows[0]["companyName"].ToString();
                    companyschoolphonenumber = dt.Rows[0]["companyContactNum"].ToString();
                    companyschooladdress = dt.Rows[0]["companyAddress"].ToString();
                    namespouse = dt.Rows[0]["spouseName"].ToString();
                    mobilenumberspouse = dt.Rows[0]["spouseContactNum"].ToString();
                    birthdayspouse = dt.Rows[0]["spouseBirthdate"].ToString();
                    fullname = firstname + " " + middlename + " " + lastname;
                    lblFullname.Text = fullname;
                    lblNickname.Text = nickname;
                    lblBirthday.Text = birthday;
                    lblGender.Text = gender;
                    lblCivilStatus.Text = civilstatus;
                    lblCitizenship.Text = citizenship;
                    lblHomeAddress.Text = homeaddress;
                    lblHonePhoneNumber.Text = homephonenumber;
                    lblMobileNumber.Text = mobilenumber;
                    lblEmailAddress.Text = emailaddress;
                    lblProfessionOccupation.Text = profession;
                    lblCompanySchoolName.Text = companyschoolname;
                    lblCompanyPhoneNumber.Text = companyschoolphonenumber;
                    lblCompanySchoolAddress.Text = companyschooladdress;
                    lblNameSpouse.Text = namespouse;
                    lblMobileNumberSpouse.Text = mobilenumberspouse;
                    lblBirthdaySpouse.Text = birthdayspouse;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void FormProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            reference_to_frmhome.Show();
        }

        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            FormRegisterUpdate frmregisterupdate = new FormRegisterUpdate();
            frmregisterupdate.reference_to_frmprofile = this;
            username = reference_to_frmhome.username;
            firstname = lblFullname.Text.Split(' ')[0];
            middlename = lblFullname.Text.Split(' ')[1];
            lastname = lblFullname.Text.Split(' ')[2];
            nickname = lblNickname.Text;
            birthday = lblBirthday.Text;
            gender = lblGender.Text;
            civilstatus = lblCivilStatus.Text;
            citizenship = lblCitizenship.Text;
            homeaddress = lblHomeAddress.Text;
            homephonenumber = lblHonePhoneNumber.Text;
            mobilenumber = lblMobileNumber.Text;
            emailaddress = lblEmailAddress.Text;
            profession = lblProfessionOccupation.Text;
            companyschoolname = lblCompanySchoolName.Text;
            companyschoolphonenumber = lblCompanyPhoneNumber.Text;
            companyschooladdress = lblCompanySchoolAddress.Text;
            namespouse = lblNameSpouse.Text;
            mobilenumberspouse = lblMobileNumberSpouse.Text;
            birthdayspouse = lblBirthdaySpouse.Text;
            this.Hide();
            frmregisterupdate.Show();
            //frmregisterupdate.
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (sidepanel)
            {
                panel2.Visible = true;
                sidepanel = false;
            }
            else
            {
                panel2.Visible = false;
                sidepanel = true;
                panel2.Dock = DockStyle.Left;
            }
        }
    }
}
