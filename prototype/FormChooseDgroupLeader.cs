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
    public partial class FormChooseDgroupLeader : Form
    {
        public MySqlConnection conn;
        public MySqlCommand comm;
        public FormHome reference_to_frmHome { get; set; }
        public FormEndorse reference_frmEndorse { get; set; }
        public String username, fullname, contactnum, civilstatus, emailad, occupation, birthdate;
        public int dgroupIDForLeader, membID, memberID; //membID for dgroup leader, memberID for dgroup member
        public FormChooseDgroupLeader()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void FormChooseDgroupLeader_Load(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            username = reference_to_frmHome.username;
            comm = new MySqlCommand("SELECT firstName, member_tbl.memberID, lastName, contactNum, civilStatus, emailAd, occupation, birthdate FROM "
                + "member_tbl INNER JOIN discipleshipgroupmembers_tbl ON member_tbl.memberID = "
                + "discipleshipgroupmembers_tbl.memberID WHERE dgroupID = " + getDgroupIDForLeader() + "", conn);
            Rifrish();
        }

        private void Rifrish()
        {
            try
            {
                conn.Open();
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                dtgDgMemberToLeader.DataSource = dt;
                dtgDgMemberToLeader.Columns["memberID"].Visible = false;
                dtgDgMemberToLeader.Columns["lastName"].Visible = false;
                dtgDgMemberToLeader.Columns["contactNum"].Visible = false;
                dtgDgMemberToLeader.Columns["civilStatus"].Visible = false;
                dtgDgMemberToLeader.Columns["emailAd"].Visible = false;
                dtgDgMemberToLeader.Columns["occupation"].Visible = false;
                dtgDgMemberToLeader.Columns["birthdate"].Visible = false;
                dtgDgMemberToLeader.Columns["firstName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private int getDgroupIDForLeader()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT dgroupID FROM discipleshipgroup_tbl WHERE dgleader = "
                    + "" + getMemberID() + ";", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    dgroupIDForLeader = int.Parse(dt.Rows[0]["dgroupID"].ToString());
                }
                return dgroupIDForLeader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FormEndorse frmendrose = new FormEndorse();
            frmendrose.reference_to_frmchoosedgroupleader = this;
            this.Hide();
            frmendrose.Show();
        }

        private void dtgDgMemberToLeader_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            String fn, ln;
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count >= 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fn = dt.Rows[i]["firstName"].ToString();
                    ln = dt.Rows[i]["lastName"].ToString();
                    if (e.ColumnIndex == 0)
                    {
                        String value = e.Value.ToString();
                        if (value == fn)
                            e.Value = fn + " " + ln;
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void FormChooseDgroupLeader_FormClosing(object sender, FormClosingEventArgs e)
        {
            reference_to_frmHome.Show();
        }

        private void dtgDgMemberToLeader_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnNext.Enabled = true;
                fullname = dtgDgMemberToLeader.Rows[e.RowIndex].Cells["firstName"].Value.ToString() + " " +
                           dtgDgMemberToLeader.Rows[e.RowIndex].Cells["lastName"].Value.ToString();
                contactnum = dtgDgMemberToLeader.Rows[e.RowIndex].Cells["contactNum"].Value.ToString();
                civilstatus = dtgDgMemberToLeader.Rows[e.RowIndex].Cells["civilStatus"].Value.ToString();
                emailad = dtgDgMemberToLeader.Rows[e.RowIndex].Cells["emailAd"].Value.ToString();
                occupation = dtgDgMemberToLeader.Rows[e.RowIndex].Cells["occupation"].Value.ToString();
                birthdate = dtgDgMemberToLeader.Rows[e.RowIndex].Cells["birthdate"].Value.ToString();
                memberID = int.Parse(dtgDgMemberToLeader.Rows[e.RowIndex].Cells["memberID"].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
    }
}
