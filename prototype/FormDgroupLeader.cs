using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prototype
{
    public partial class FormDgroupLeader : Form
    {
        public FormDgroupLeader()
        {
            InitializeComponent();
        }

        private void btnProposeMinistry_MouseHover(object sender, EventArgs e)
        {
            //btnProposeMinistry.Image = prototype.Properties.Resources.promotion__1_;
            //btnProposeMinistry.ForeColor = Color.White;
            //btnProposeMinistry.FlatAppearance.BorderColor = Color.FromArgb(22, 165, 184);
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

        private void btnEndorseDgroupMember_MouseLeave(object sender, EventArgs e)
        {
            btnEndorseDgroupMember.Image = prototype.Properties.Resources.promotion;
            btnEndorseDgroupMember.ForeColor = Color.FromArgb(22, 165, 184);
        }
    }
}
