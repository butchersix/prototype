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
    public partial class FormDgroup : Form
    {
        public MySqlConnection conn;
        public MySqlCommand comm, comm1;
        public FormHome reference_to_frmhome { get; set; }
        public FormProfile reference_to_frmprofile { get; set; }
        public int membertype, membID, dgroupID, dgleaderID, dgroupIDForLeader, dgleader;
        public String username, leadername;
        Boolean sidepanel = true, confirmdgroupleader = false;
        public FormDgroup()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void FormDgroup_Load(object sender, EventArgs e)
        {
            // put try catch here to catch the error null exception in referencing of username
            panel3.Visible = false;
            panel4.Visible = false;
            try
            {
                username = reference_to_frmhome.username;
                comm = new MySqlCommand("SELECT firstName, lastName FROM member_tbl INNER JOIN discipleshipgroupmembers_tbl "
                    + "ON member_tbl.memberID = discipleshipgroupmembers_tbl.memberID WHERE dgroupID = " + getDgroupID() + "", conn);
                comm1 = new MySqlCommand("SELECT firstName, lastName FROM member_tbl INNER JOIN discipleshipgroupmembers_tbl "
                    + "ON member_tbl.memberID = discipleshipgroupmembers_tbl.memberID WHERE dgroupID = "
                    + "" + getDgroupIDForLeader() + "", conn);
                Rifrish();
                lblDgroupLeader.Text = getDgroupLeaderName();
            }
            catch(Exception ex)
            {

            }
        }

        private void Rifrish()
        {
            try
            {
                conn.Open();
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                dtgFellow.DataSource = dt;
                dtgFellow.Columns["lastName"].Visible = false;
                dtgFellow.Columns["firstName"].HeaderText = "Fullname";
                checkDgroupLeader();
                if (confirmdgroupleader)
                {
                    panel4.Visible = true;
                    if (panel4.Visible)
                    {
                        adp = new MySqlDataAdapter(comm1);
                        dt = new DataTable();
                        adp.Fill(dt);

                        dtgDgroupMembers.DataSource = dt;
                        dtgDgroupMembers.Columns["lastName"].Visible = false;
                        dtgDgroupMembers.Columns["firstName"].HeaderText = "Fullname";
                    }
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (sidepanel)
            {
                panel3.Visible = true;
                sidepanel = false;
            }
            else
            {
                panel3.Visible = false;
                sidepanel = true;
                panel3.Dock = DockStyle.Left;
            }
        }

        private void checkDgroupLeader()
        {
            try
            {
                username = reference_to_frmhome.username;
                MySqlCommand comm = new MySqlCommand("SELECT memberType FROM member_tbl WHERE membUsername = "
                    + "'" + username + "';", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    membertype = int.Parse(dt.Rows[0]["memberType"].ToString());
                    if (membertype == 2)
                        confirmdgroupleader = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private int getDgroupLeaderID()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT dgleader FROM member_tbl INNER JOIN discipleshipgroup_tbl ON "
                    + "memberID = dgleader", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    dgleaderID = int.Parse(dt.Rows[0]["dgleader"].ToString());
                }
                return dgleaderID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIWantDgroupLeader_Click(object sender, EventArgs e)
        {

        }

        private void FormDgroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            reference_to_frmhome.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgFellow_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void dtgDgroupMembers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            String fn, ln;
            MySqlDataAdapter adp = new MySqlDataAdapter(comm1);
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

        private int getDgroupID()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT dgroupID FROM discipleshipgroupmembers_tbl WHERE memberID = "
                    + "'" + getMemberID() + "';", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    dgroupID = int.Parse(dt.Rows[0]["dgroupID"].ToString());
                }
                return dgroupID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
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

        private String getDgroupLeaderName()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT dgleader FROM member_tbl INNER JOIN "
                    + "discipleshipgroupmembers_tbl ON member_tbl.memberID = discipleshipgroupmembers_tbl.memberID INNER JOIN "
                    + "discipleshipgroup_tbl ON discipleshipgroupmembers_tbl.dgroupID = discipleshipgroup_tbl.dgroupID "
                    + "WHERE discipleshipgroupmembers_tbl.memberID = '" + getMemberID() + "';", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    dgleader = int.Parse(dt.Rows[0]["dgleader"].ToString());
                }

                comm = new MySqlCommand("SELECT firstName, lastName FROM member_tbl WHERE memberID = " + dgleader + "", conn);
                adp = new MySqlDataAdapter(comm);
                dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    leadername = dt.Rows[0]["firstName"].ToString() + " " + dt.Rows[0]["lastName"].ToString();
                }
                return leadername;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }
        }
    }
}
