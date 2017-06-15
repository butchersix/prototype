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
    public partial class FormEndorse : Form
    {
        public MySqlConnection conn;
        public FormChooseDgroupLeader reference_to_frmchoosedgroupleader { get; set; }
        public FormHome reference_to_frmHome { get; set; }
        public String username, endorser, endorsernum, endorseremailad, fullname, contactnum, civilstatus, emailad, occupation, 
            birthdate;
        public int membID, dgrouptype, memberID, schedID;
        private void FormEndorse_FormClosing(object sender, FormClosingEventArgs e)
        {
            reference_to_frmchoosedgroupleader.Show();
        }

        public FormEndorse()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void FormEndorse_Load(object sender, EventArgs e)
        {
            username = reference_to_frmchoosedgroupleader.username;
            memberID = reference_to_frmchoosedgroupleader.memberID;
            fillEndorser();
            fillEndorsee();
            fillComboBoxes();
            Rifrish();
        }

        private void rdbNo_CheckedChanged(object sender, EventArgs e)
        {
            txtYesID.Enabled = false;
        }

        private void btnEndorse_Click(object sender, EventArgs e)
        {
            try
            {
                insertEndorsee();
                FormHome frmHome = new FormHome();
                this.Hide();
                frmHome.Show();
            }
            catch(Exception ex)
            {
                
            }
        }

        private void rdbYes_CheckedChanged(object sender, EventArgs e)
        {
            txtYesID.Enabled = true;
        }

        private void Rifrish()
        {
            try
            {
                getData();
                txtDate.Text = DateTime.Now.ToString("MM/dd/yy");
            }
            catch(Exception ex)
            {

            }
        }

        private void getData()
        {
            fullname = reference_to_frmchoosedgroupleader.fullname;
            contactnum = reference_to_frmchoosedgroupleader.contactnum;
            emailad = reference_to_frmchoosedgroupleader.emailad;
        }

        private void insertEndorsee()
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("INSERT INTO endorsement_tbl(memberID, pastoralArea, remarks, ccfMemberID, "
                    + "baptismalDate, baptismalPlace, recommendMinistry, dateEndorsed) VALUES('" + memberID + "', "
                    + "'" + txtPastoralArea.Text + "', '" + txtRemarks.Text + "', '" + txtYesID.Text + "', "
                    + "'" + txtBaptismalDate.Text + "', '" + txtBaptismalPlace.Text + "', '" + txtRecommendMinistry.Text + "', "
                    + "'" + txtDate.Text + "');", conn);
                comm.ExecuteNonQuery();
                comm = new MySqlCommand("INSERT INTO scheduledmeeting_tbl(schedDay, schedTime, schedPlace) VALUES("
                    + "'" + cmbDays.Text + "', '" + convertMeetingTime() + "', '" + txtMeetingPlace.Text + "');", conn);
                comm.ExecuteNonQuery();
                comm = new MySqlCommand("INSERT INTO discipleshipgroup_tbl(dgleader, schedID, dgroupType, ageBracket) VALUES("
                    + "'" + memberID + "', '" + getSchedID() + "', '" + convertDgroupType() + "', '" + convertAgeBracket() + "');", conn);
                comm.ExecuteNonQuery();
                comm = new MySqlCommand("UPDATE member_tbl SET memberType = 2 WHERE memberID = '" + memberID + "'", conn);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private int getSchedID()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT schedID FROM scheduledmeeting_tbl ORDER BY schedID DESC LIMIT 1", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if(dt.Rows.Count == 1)
                {
                    schedID = int.Parse(dt.Rows[0]["schedID"].ToString());
                }
                return schedID;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        private void fillComboBoxes()
        {
            String[] days = { "", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            String[] time = { "", "12:00", "12:30", "1:00", "1:30", "2:00", "2:30", "3:00", "3:30", "4:00", "4:30", "5:00",
                              "5:30", "6:00", "6:30", "7:00", "7:30", "8:00", "9:00", "9:30", "10:00", "10:30", "11:00",
                              "11:30"};
            String[] ampm = { "", "AM", "PM" };
            for (int i = 0; i < days.Length; i++)
                cmbDays.Items.Add(days[i]);
            for (int i = 0; i < time.Length; i++)
                cmbTime1.Items.Add(time[i]);
            for (int i = 0; i < time.Length; i++)
                cmbTime2.Items.Add(time[i]);
            for (int i = 0; i < ampm.Length; i++)
                cmbAMPM1.Items.Add(ampm[i]);
            for (int i = 0; i < ampm.Length; i++)
                cmbAMPM2.Items.Add(ampm[i]);
        }

        private int convertDgroupType()
        {
            if (rdbYouth.Checked == true)
                dgrouptype = 0;
            else if (rdbSingles.Checked == true)
                dgrouptype = 1;
            else if (rdbSingleParents.Checked == true)
                dgrouptype = 2;
            else if (rdbMarried.Checked == true)
                dgrouptype = 3;
            else if (rdbAll.Checked == true)
                dgrouptype = 4;
            else if (rdbCouples.Checked == true)
                dgrouptype = 5;
            return dgrouptype;
        }

        private String convertMeetingTime()
        {
            return cmbTime1.Text + " " + cmbAMPM1.Text + " - " + cmbTime2.Text + " " + cmbAMPM2.Text;
        }



        private void fillEndorser()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT firstName, lastName, contactNum, emailAd FROM member_tbl WHERE "
                    + "membUsername = '" + username + "';", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    endorser = dt.Rows[0]["firstName"].ToString() + " " + dt.Rows[0]["lastName"].ToString();
                    endorsernum = dt.Rows[0]["contactNum"].ToString();
                    endorseremailad = dt.Rows[0]["emailAd"].ToString();
                    txtEndorser.Text = endorser;
                    txtMobileNumber.Text = endorsernum;
                    txtEndorserEmailAd.Text = endorseremailad;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void fillEndorsee()
        {
            try
            {
                fullname = reference_to_frmchoosedgroupleader.fullname;
                contactnum = reference_to_frmchoosedgroupleader.contactnum;
                civilstatus = reference_to_frmchoosedgroupleader.civilstatus;
                emailad = reference_to_frmchoosedgroupleader.emailad;
                occupation = reference_to_frmchoosedgroupleader.occupation;
                birthdate = reference_to_frmchoosedgroupleader.birthdate;
                txtEndorsee.Text = fullname;
                txtEndorseeMobileNumber.Text = contactnum;
                txtCivilStatus.Text = civilstatus;
                txtEndorseeEmailAd.Text = emailad;
                txtProfession.Text = occupation;
                txtBirthdate.Text = birthdate;
            }
            catch (Exception ex)
            {

            }
        }

        private int getMemberID()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT memberID FROM member_tbl WHERE membUsername = "
                    + "'" + username + "';", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    membID = int.Parse(dt.Rows[0]["memberID"].ToString());
                }
                return membID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        private String convertAgeBracket()
        {
            return nmcAge1.Value.ToString() + " - " + nmcAge2.Value.ToString();
        }
    }
}
