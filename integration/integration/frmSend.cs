using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.IO.MemoryMappedFiles;




namespace integration
{
    public partial class frmSend : Form
    {
        public frmSend()
        {
            InitializeComponent();
        }

        MemoryStream memStream;
        TcpClient client;
        NetworkStream networkStream;
        BinaryWriter binaryWriter;

        string getIpAddress()
        {
            IPHostEntry host;
            string localIp = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork") ;
                localIp = ip.ToString();
            }
            return localIp;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            

        }

        private void frmSend_Load(object sender, EventArgs e)
        {
            TbxIpAddress.Text = getIpAddress();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            main frm = new main();
            frm.Tag = this;
            Hide();
            frm.Show(this);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmEncode frm = new frmEncode();
            frm.Tag = this;
            Hide();
            frm.Show(this);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmSend frm = new frmSend();
            frm.Show();
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "BMP Files (*.bmp) | *.bmp";
            openDialog.InitialDirectory = @"C:\Users\Bhargav Teja\Pictures";
            Console.WriteLine("Opening dialogue box to choose image");
            // openFileDialog1.ShowDialog();
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string pathForImage = openDialog.FileName.ToString();
                Console.WriteLine("Path for Image is : " + pathForImage);
                textBox1.Text = pathForImage;
                pictureBox1.Image = Image.FromFile(pathForImage);
                // ImageBoxToHide.Image.sho

            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            try
            {

                memStream = new MemoryStream();
                pictureBox1.Image.Save(memStream, pictureBox1.Image.RawFormat);
                Byte[] buffer = memStream.GetBuffer();
                memStream.Close();
                client = new TcpClient(TbxIpAddress.Text, 53100);
                networkStream = client.GetStream();
                binaryWriter = new BinaryWriter(networkStream);
                binaryWriter.Write(buffer);
                binaryWriter.Close();
                networkStream.Close();
                client.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
