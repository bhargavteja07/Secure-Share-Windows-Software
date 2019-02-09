using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encryption;

namespace integration
{
    public partial class frmDecode : Form
    {
        public frmDecode()
        {
            InitializeComponent();
        }

        private void fetch_data_Click(object sender, EventArgs e)
        {
            }

        private void fetch_file_Click(object sender, EventArgs e)
        {
            }

        private void decrypt_file_Click(object sender, EventArgs e)
        {
        
        }

        private void recieve_image_Click(object sender, EventArgs e)
        {
            
        }

        private void frmDecode_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmFetchFile fetchFrm = new frmFetchFile();
            fetchFrm.Tag = this;
            fetchFrm.Show(this);
            Hide();
        

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmFetchMessage fetchFrm = new frmFetchMessage();
            fetchFrm.Tag = this;
            fetchFrm.Show(this);
            Hide();        

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmDecryp frm = new frmDecryp();
            frm.Tag = this;
            frm.Show(this);
            Hide();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmRecieve fetchFrm = new frmRecieve();
            fetchFrm.Tag = this;
            fetchFrm.Show(this);
            Hide();

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
           
        
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            main frm = new main();
            frm.Tag = this;
            frm.Show(this);
            Hide();
        }
    }
}
