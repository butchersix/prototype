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
    public partial class FormRegisterUsePass : Form
    {
        public MySqlConnection conn;
        public FormLogin reference_to_frmlogin { get; set; }
        public FormChooseLeader reference_to_frmchooseleader { get; set; }
        public RegisterNewComer reference_to_frmregisternewcomer { get; set; }
        public FormRegisterUpdate reference_to_frmregisterupdate { get; set; }
        public FormHome reference_to_frmHome { get; set; }
        public string username;
        public FormRegisterUsePass()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=dbccf1;Uid=root;Pwd=root;");
        }

        private void FormRegisterUsePass_Load(object sender, EventArgs e)
        {

        }

        private void FormRegisterUsePass_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (reference_to_frmchooseleader.ifFormChooseLeader())
                    reference_to_frmchooseleader.Show();
                else if (reference_to_frmregisternewcomer.ifFormRegisterNewComer())
                    reference_to_frmregisternewcomer.Show();
            }
            catch(Exception ex)
            {
                reference_to_frmregisternewcomer.Show();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (reference_to_frmchooseleader.ifFormChooseLeader())
                {
                    MySqlCommand comm = new MySqlCommand("UPDATE member_tbl SET membUsername = '" + txtUsername.Text + "', "
                        + "membPassword = '" + txtPassword.Text + "' WHERE memberID = "
                        + "" + reference_to_frmchooseleader.getMemberID() + ";", conn);
                    comm.ExecuteNonQuery();
                }
                else if (reference_to_frmregisternewcomer.ifFormRegisterNewComer())
                {
                    MySqlCommand comm = new MySqlCommand("UPDATE member_tbl SET membUsername = '" + txtUsername.Text + "', "
                        + "membPassword = '" + txtPassword.Text + "' WHERE memberID = "
                        + "" + reference_to_frmregisternewcomer.getMemberID() + ";", conn);
                    comm.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                MySqlCommand comm = new MySqlCommand("UPDATE member_tbl SET membUsername = '" + txtUsername.Text + "', "
                           + "membPassword = '" + txtPassword.Text + "' WHERE memberID = "
                           + "" + reference_to_frmregisternewcomer.getMemberID() + ";", conn);
                comm.ExecuteNonQuery();
                //MessageBox.Show(ex.ToString());
            }
            FormHome frmhome = new FormHome();
            frmhome.reference_to_frmregisterusepass = this;
            frmhome.reference_to_frmlogin = reference_to_frmlogin;
            username = txtUsername.Text;
            this.Hide();
            frmhome.Show();
        }

        public Boolean ifFormRegisterUsePass()
        {
            return true;
        }
    }
}
