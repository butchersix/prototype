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
    public partial class FormLogin : Form
    {
        public MySqlConnection conn;
        public int membertype;
        public String user, pass;
        public FormDgroup reference_to_frmdgroup { get; set; }
        public RegisterNewComer reference_to_registernewcomer { get; set; }
        public FormRegisterMain reference_to_frmregistermain { get; set; }
        public FormLogin()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            setTextUsername();
            setTextPassword();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM member_tbl WHERE membUsername = '" + txtUsername.Text + "';", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if(dt.Rows.Count == 1)
                {
                    user = dt.Rows[0]["membUsername"].ToString();
                    pass = dt.Rows[0]["membPassword"].ToString();
                    membertype = int.Parse(dt.Rows[0]["memberType"].ToString());
                }
                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("The user must supply the necessary fields.");
                }
                else if (txtUsername.Text == user && txtPassword.Text == pass && membertype <= 2)
                {
                    FormHome frmhome = new FormHome();
                    frmhome.reference_to_frmlogin = this;
                    this.Hide();
                    frmhome.Show();
                }
                else
                {
                    if (txtUsername.Text != user || txtPassword.Text != pass)
                        MessageBox.Show("The user credentials are incorrect.");
                    //else if (accstatus != 0)
                    //    MessageBox.Show("The user is inactive.");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
            }
        }

        private void linkLoginNewcomer_MouseEnter(object sender, EventArgs e)
        {
            linkLoginNewcomer.Font = new Font(linkLoginNewcomer.Font, FontStyle.Underline);
            linkLoginNewcomer.Cursor = Cursors.Hand;
        }

        private void linkLoginNewcomer_MouseLeave(object sender, EventArgs e)
        {
            linkLoginNewcomer.Font = new Font(linkLoginNewcomer.Font, FontStyle.Regular);
            linkLoginNewcomer.Cursor = Cursors.Default;
        }

        private void linkRegister_MouseEnter(object sender, EventArgs e)
        {
            linkRegister.Font = new Font(linkRegister.Font, FontStyle.Underline);
            linkRegister.Cursor = Cursors.Hand;
        }

        private void linkRegister_MouseLeave(object sender, EventArgs e)
        {
            linkRegister.Font = new Font(linkRegister.Font, FontStyle.Regular);
            linkRegister.Cursor = Cursors.Default;
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
                setUsername();
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
                setTextUsername();
            else
                setUsername();
        }

        private void setTextUsername()
        {
            txtUsername.Text = "Username";
            txtUsername.ForeColor = Color.Gray;
            txtUsername.Font = new Font(txtUsername.Font, FontStyle.Italic);
        }

        private void setUsername()
        {
            txtUsername.ForeColor = Color.Black;
            txtUsername.Font = new Font(txtUsername.Font, FontStyle.Regular);
        }

        private void setTextPassword()
        {
            txtPassword.Text = "Password";
            txtPassword.PasswordChar = '\0';
            txtPassword.ForeColor = Color.Gray;
            txtPassword.Font = new Font(txtPassword.Font, FontStyle.Italic);
        }

        private void setPassword()
        {
            txtPassword.PasswordChar = '•';
            txtPassword.ForeColor = Color.Black;
            txtPassword.Font = new Font(txtPassword.Font, FontStyle.Regular);
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                setPassword();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLoginNewcomer_Click(object sender, EventArgs e)
        {
            RegisterNewComer frmregisternewcomer = new RegisterNewComer();
            frmregisternewcomer.reference_to_frmlogin = this;
            this.Hide();
            frmregisternewcomer.Show();
        }

        private void linkRegister_Click(object sender, EventArgs e)
        {
            FormRegisterDgroup frmregisterdgroup = new FormRegisterDgroup();
            frmregisterdgroup.reference_to_frmlogin = this;
            this.Hide();
            frmregisterdgroup.Show();
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
                setTextPassword();
            else
                setPassword();
        }
        //FormDgroupMember dgroupmemberform = new prototype.FormDgroupMember();
        //dgroupmemberform.reference_to_loginform = this;
        //this.Hide();
        //dgroupmemberform.ShowDialog();
        //this.Close();

        public Boolean ifFormLogin()
        {
            return true;
        }
    }
}
