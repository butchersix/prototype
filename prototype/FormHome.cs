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
    public partial class FormHome : Form
    {
        public MySqlConnection conn;
        public FormLogin reference_to_frmlogin { get; set; }
        public FormProfile reference_to_frmprofile { get; set; }
        public FormRegisterUsePass reference_to_frmregisterusepass { get; set; }
        public FormDgroup reference_to_frmdgroup { get; set; }
        public FormChooseDgroupLeader reference_to_frmchoosedgroupleader { get; set; }
        public String username;
        public int membertype;
        Boolean sidepanel = true, confirmdgroupleader = false;
        public FormHome()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel4.Visible = false;
            btnProfile1.Location = new Point(561, 85);
            btnDgroupLeaderView.Visible = false;
            Rifrish();
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

        private void FormHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            //FormLogin frmlogin = new FormLogin();
            //this.Hide();
            //frmlogin.Show();
            try
            {
                reference_to_frmlogin.Show();
            }
            catch(Exception ex)
            {
                FormLogin frmlogin = new FormLogin();
                this.Hide();
                frmlogin.Show();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            FormProfile frmprofile = new FormProfile();
            frmprofile.reference_to_frmhome = this;
            frmprofile.reference_to_frmlogin = reference_to_frmlogin;
            getUsername();
            this.Hide();
            frmprofile.Show();
        }

        private void getUsername()
        {
            try
            {
                if (reference_to_frmregisterusepass.ifFormRegisterUsePass())
                    username = reference_to_frmregisterusepass.username;
                else if (reference_to_frmlogin.ifFormLogin())
                    username = reference_to_frmlogin.user;
            }
            catch(Exception ex)
            {
                username = reference_to_frmlogin.user;
            }
        }

        private void checkDgroupLeader()
        {
            try
            {
                conn.Open();
                getUsername();
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
                conn.Close();
            }
            catch(Exception ex)
            {

            }
        }

        private void Rifrish()
        {
            checkDgroupLeader();
            if (confirmdgroupleader)
            {
                panel4.Visible = true;
                btnDgroupLeaderView.Visible = true;
                btnProfile1.Location = new Point(523, 85);
            }
        }

        private void btnProposeMinistry_MouseLeave(object sender, EventArgs e)
        {
            btnProposeMinistry.Image = prototype.Properties.Resources.add_group_button__1_;
            btnProposeMinistry.ForeColor = Color.FromArgb(22, 165, 184);
        }

        private void btnProposeMinistry_MouseEnter(object sender, EventArgs e)
        {
            btnProposeMinistry.Image = prototype.Properties.Resources.add_group_button;
            btnProposeMinistry.ForeColor = Color.White;
        }

        private void btnEndorseDgroupMember_MouseEnter(object sender, EventArgs e)
        {
            btnEndorseDgroupMember.Image = prototype.Properties.Resources.promotion__1_;
            btnEndorseDgroupMember.ForeColor = Color.White;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        private void btnDgroup_Click(object sender, EventArgs e)
        {
            FormDgroup frmdgroup = new FormDgroup();
            frmdgroup.reference_to_frmhome = this;
            getUsername();
            this.Hide();
            frmdgroup.Show();
        }

        private void btnEndorseDgroupMember_Click(object sender, EventArgs e)
        {
            FormChooseDgroupLeader frmchoosedgroupleader = new FormChooseDgroupLeader();
            frmchoosedgroupleader.reference_to_frmHome = this;
            this.Hide();
            frmchoosedgroupleader.Show();
        }

        private void btnEndorseDgroupMember_MouseLeave(object sender, EventArgs e)
        {
            btnEndorseDgroupMember.Image = prototype.Properties.Resources.promotion;
            btnEndorseDgroupMember.ForeColor = Color.FromArgb(22, 165, 184);
        }
    }
}
