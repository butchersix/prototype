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
    public partial class FormRegisterMain : Form
    {
        public MySqlConnection conn;
        public FormLogin reference_to_frmlogin { get; set; }
        public FormRegisterDgroup reference_to_frmregisterdgroup { get; set; }
        public String username;
        public FormRegisterMain()
        {
            InitializeComponent();
        }

        private void FormRegisterMain_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rdbDgroup.Checked == true)
            {
                FormRegisterDgroup frd = new FormRegisterDgroup();
                frd.reference_to_frmregistermain = this;
                frd.reference_to_frmlogin = reference_to_frmlogin;
                this.Hide();
                frd.Show();
            }
            else if (rdbUpdate.Checked == true)
            {
                FormRegisterUpdate fru = new FormRegisterUpdate();
                fru.reference_to_frmregistermain = this;
                username = reference_to_frmlogin.user;
                this.Hide();
                fru.Show();
            }
        }

        private void FormRegisterMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            reference_to_frmlogin.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
