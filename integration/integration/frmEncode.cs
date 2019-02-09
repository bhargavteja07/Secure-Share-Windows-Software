using Encryption;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace integration
{
    public partial class frmEncode : Form
    {
        public frmEncode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmEmbedMessage frmEmbmsg = new frmEmbedMessage();
            frmEmbmsg.Tag = this;
            frmEmbmsg.Show(this);
            Hide();
        }

        private void embed_file_Click(object sender, EventArgs e)
        {
           
        }

        private void encrypt_file_Click(object sender, EventArgs e)
        {
            
        }

        private void send_image_Click(object sender, EventArgs e)
        {
            
        }

        private void frmEncode_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmEmbedMessage frmEmbmsg = new frmEmbedMessage();
            frmEmbmsg.Tag = this;
            frmEmbmsg.Show(this);
            Hide();
        
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmEmbedFile frmEmbFile = new frmEmbedFile();
            frmEmbFile.Tag = this;
            frmEmbFile.Show(this);
            Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            frmEncrypt frm = new frmEncrypt();
            frm.Tag = this;
            frm.Show(this);
            Hide();
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            frmSend frmEmbFile = new frmSend();
            frmEmbFile.Tag = this;
            frmEmbFile.Show(this);
            Hide();

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
