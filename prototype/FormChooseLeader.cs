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
    public partial class FormChooseLeader : Form
    {
        public MySqlConnection conn;
        public FormLogin reference_to_frmlogin { get; set; }
        public FormRegisterDgroup reference_to_frmregisterdgroup { get; set; }
        public FormRegisterUsePass reference_to_frmregisterusepass { get; set; }
        public MySqlCommand comm;
        public int dgroupID, schedID, memberID, dgroupmemberID;
        String fullname;
        public FormChooseLeader()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
            comm = new MySqlCommand("SELECT discipleshipgroup_tbl.dgroupID, firstName, lastName, scheduledmeeting_tbl.schedID, "
                + "schedDay, schedTime FROM member_tbl INNER JOIN discipleshipgroup_tbl ON dgleader = memberID INNER JOIN "
                + "scheduledmeeting_tbl ON discipleshipgroup_tbl.schedID = scheduledmeeting_tbl.schedID WHERE memberType = 2", conn);
        }

        private void FormChooseLeader_Load(object sender, EventArgs e)
        {
            lblLeader.Text = "";
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

                dtgDgLeader.DataSource = dt;
                dtgDgLeader.Columns["lastName"].Visible = false;
                dtgDgLeader.Columns["dgroupID"].Visible = false;
                dtgDgLeader.Columns["schedID"].Visible = false;
                dtgDgLeader.Columns["firstName"].HeaderText = "Fullname";
                dtgDgLeader.Columns["schedDay"].HeaderText = "Day";
                dtgDgLeader.Columns["schedTime"].HeaderText = "Time";
                conn.Close();
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void dtgDgLeader_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgroupID = int.Parse(dtgDgLeader.Rows[e.RowIndex].Cells["dgroupID"].Value.ToString());
                schedID = int.Parse(dtgDgLeader.Rows[e.RowIndex].Cells["schedID"].Value.ToString());
                fullname = dtgDgLeader.Rows[e.RowIndex].Cells["firstName"].Value.ToString() + " " +
                           dtgDgLeader.Rows[e.RowIndex].Cells["lastName"].Value.ToString();
                lblLeader.Text = fullname;
            }
            catch(Exception ex)
            {

            }
        }

        private void dtgDgLeader_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            String fn, ln;
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            
            if(dt.Rows.Count >= 1)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    fn = dt.Rows[i]["firstName"].ToString();
                    ln = dt.Rows[i]["lastName"].ToString();
                    if (e.ColumnIndex == 1)
                    {
                        String value = e.Value.ToString();
                        if (value == fn)
                            e.Value = fn + " " + ln;
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void FormChooseLeader_FormClosing(object sender, FormClosingEventArgs e)
        {
            reference_to_frmregisterdgroup.Show();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                memberID = reference_to_frmregisterdgroup.getMemberID();
                conn.Open();
                MySqlCommand comm1 = new MySqlCommand("UPDATE discipleshipgroupmembers_tbl SET dgroupID = " + dgroupID + ", "
                    + "memberID = " + memberID + " WHERE dgroupmemberID = " + getDgroupMemberID() + ";", conn);
                comm1.ExecuteNonQuery();
                conn.Close();
                FormRegisterUsePass frmregisterusepass = new FormRegisterUsePass();
                frmregisterusepass.reference_to_frmchooseleader = this;
                frmregisterusepass.reference_to_frmlogin = reference_to_frmlogin;
                this.Hide();
                frmregisterusepass.Show();
            }
            catch(Exception ex)
            {

            }
        }

        public int getDgroupMemberID()
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT dgroupmemberID FROM discipleshipgroupmembers_tbl ORDER BY "
                    + "dgroupmemberID DESC LIMIT 1", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    dgroupmemberID = int.Parse(dt.Rows[0]["dgroupmemberID"].ToString());
                }
                return dgroupmemberID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int getMemberID()
        {
            try
            {
                return memberID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Boolean ifFormChooseLeader()
        {
            return true;
        }
    }
}
