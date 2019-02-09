using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Encryption;

namespace RSACryptoPad
{
    public partial class frmlogin1 : Form
    {
        public frmlogin1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                MainForm frm = new MainForm();
                this.Hide();
                frm.Show();

            }
            else
            {
                MessageBox.Show("Login Error! Please try again");
            }
        }
    }
}