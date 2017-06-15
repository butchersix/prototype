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
    public partial class FormRegisterDgroup : Form
    {
        public MySqlConnection conn;
        public FormLogin reference_to_frmlogin { get; set; }
        public FormRegisterMain reference_to_frmregistermain { get; set; }
        public FormChooseLeader reference_to_frmchooseleader { get; set; }
        public int memberID;
        String citizenship, civilstatus;
        int gender;
        public FormRegisterDgroup()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void FormRegisterDgroup_Load(object sender, EventArgs e)
        {
            fillComboBoxes();
        }

        private void FormRegisterDgroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            reference_to_frmlogin.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void fillComboBoxes()
        {
            String[] days = { "", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            String[] time = { "", "12:00", "12:30", "1:00", "1:30", "2:00", "2:30", "3:00", "3:30", "4:00", "4:30", "5:00",
                              "5:30", "6:00", "6:30", "7:00", "7:30", "8:00", "9:00", "9:30", "10:00", "10:30", "11:00",
                              "11:30"};
            String[] ampm = { "", "AM", "PM" };
            for (int i = 0; i < days.Length; i++)
                cmbDay1.Items.Add(days[i]);
            for (int i = 0; i < days.Length; i++)
                cmbDay2.Items.Add(days[i]);
            for (int i = 0; i < time.Length; i++)
                cmbTime1Op1.Items.Add(time[i]);
            for (int i = 0; i < time.Length; i++)
                cmbTime1Op2.Items.Add(time[i]);
            for (int i = 0; i < time.Length; i++)
                cmbTime2Op1.Items.Add(time[i]);
            for (int i = 0; i < time.Length; i++)
                cmbTime2Op2.Items.Add(time[i]);
            for (int i = 0; i < ampm.Length; i++)
                cmbTime1Op1AM.Items.Add(ampm[i]);
            for (int i = 0; i < ampm.Length; i++)
                cmbTime1Op2PM.Items.Add(ampm[i]);
            for (int i = 0; i < ampm.Length; i++)
                cmbTime2Op1AM.Items.Add(ampm[i]);
            for (int i = 0; i < ampm.Length; i++)
                cmbTime2Op2PM.Items.Add(ampm[i]);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("INSERT INTO member_tbl(firstName, middleName, lastName, "
                    + "nickName, birthdate, gender, civilStatus, citizenship, homeAddress, homePhoneNumber, contactNum, emailAd, "
                    + "occupation, companyName, companyContactNum, companyAddress, schoolName, schoolContactNum, "
                    + "schoolAddress, spouseName, spouseContactNum, spouseBirthdate, preferredLanguage, "
                    + "preferredMeetingDay1, preferredMeetingDay2, preferredMeetingTime1, preferredMeetingTime2, "
                    + "preferredVenue1, preferredVenue2, memberType) VALUES('" + txtFirstName.Text + "', '" + txtMiddleName.Text + "', "
                    + "'" + txtLastName.Text + "', '" + txtNickName.Text + "', '" + convertBirthDate()[0] + "', "
                    + "'" + convertGender() + "','" + convertCivilStatus() + "', '" + convertCitizenship() + "', "
                    + "'" + txtHomeAddress.Text + "', '" + txtHomeNumber.Text + "', '" + txtMobileNumber.Text + "', "
                    + "'" + txtEmailAddress.Text + "', '" + txtProfession.Text + "', '" + txtCompanySchoolName.Text + "', "
                    + "'" + txtCompanyPhoneNumber.Text + "', '" + txtCompanySchoolAddress.Text + "', "
                    + "'" + txtCompanySchoolName.Text + "', '" + txtCompanyPhoneNumber.Text + "', "
                    + "'" + txtCompanySchoolAddress.Text + "', '" + txtSpouse.Text + "', '" + txtMobileNumberSpouse.Text + "', "
                    + "'" + convertBirthDate()[1] + "', '" + txtPreferredLanguage.Text + "', '" + cmbDay1.Text + "', "
                    + "'" + cmbDay2.Text + "', '" + convertMeetingDay()[0] + "', '" + convertMeetingDay()[1] + "', "
                    + "'" + txtVenue1.Text + "', '" + txtVenue2.Text + "', 1);", conn);
                comm.ExecuteNonQuery();
                comm = new MySqlCommand("INSERT INTO discipleshipgroupmembers_tbl(receivedChrist, attendCCF, regularlyAttendsAt) VALUES("
                    + "'" + txtReceivedChrist.Text + "', '" + txtAttendCCF.Text + "', '" + txtReguarlyAttendsAt.Text + "');", conn);
                comm.ExecuteNonQuery();
                conn.Close();
                FormChooseLeader frmchooseleader = new FormChooseLeader();
                frmchooseleader.reference_to_frmregisterdgroup = this;
                frmchooseleader.reference_to_frmlogin = reference_to_frmlogin;
                this.Hide();
                frmchooseleader.Show();
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
            for (int i=0; i < birthdates.Length; i++)
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

        private String[] convertMeetingDay()
        {
            String[] meetingdays = new String[2];
            String meetingtime1 = cmbTime1Op1.Text + " " + cmbTime1Op1AM.Text + " - " + cmbTime1Op2.Text + " " + cmbTime1Op2PM.Text;
            String meetingtime2 = cmbTime2Op1.Text + " " + cmbTime2Op1AM.Text + " - " + cmbTime2Op2.Text + " " + cmbTime2Op2PM.Text;
            for(int i = 0; i < meetingdays.Length; i++)
            {
                if (i == 0)
                    meetingdays[0] = meetingtime1;
                else if (i == 1)
                    meetingdays[1] = meetingtime2;
            }
            return meetingdays;
        }

        public int getMemberID()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT memberID FROM member_tbl ORDER BY memberID DESC LIMIT 1", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if(dt.Rows.Count == 1)
                {
                    memberID = int.Parse(dt.Rows[0]["memberID"].ToString());
                }
                return memberID;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
    }
}
