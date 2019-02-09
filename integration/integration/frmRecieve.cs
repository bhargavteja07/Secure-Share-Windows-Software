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
using System.Threading;


namespace integration
{
    public partial class frmRecieve : Form
    {
        public frmRecieve()
        {
            InitializeComponent();
        }

        TcpListener tcpPort;
        Socket socket;
        NetworkStream networkStream;
        StreamReader streamReader;
        Thread thread;


        void recieveImage()
        {
            try
            {
                tcpPort = new TcpListener(53100);
                tcpPort.Start();
                socket = tcpPort.AcceptSocket();
                networkStream = new NetworkStream(socket);
                pictureBox1.Image = Image.FromStream(networkStream);
                tcpPort.Stop();

                if (socket.Connected == true)
                {
                    while (true)
                    {
                        recieveImage();
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            /* Image img = pictureBox1.Image;
             Bitmap b = new Bitmap(img);
             Color pixel = b.GetPixel(0, 0);
             Console.WriteLine("R = " + pixel.R);
             Console.WriteLine("G = " + pixel.G);
             Console.WriteLine("G = " + pixel.B);*/

        }
       
        private void frmRecieve_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(recieveImage));
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
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
            frmDecode frm = new frmDecode();
            frm.Tag = this;
            Hide();
            frm.Show(this);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveEncodedImage = new SaveFileDialog();
            saveEncodedImage.Filter = "Image Files (*.png, *.jpg) | *.png; *.jpg";
            saveEncodedImage.InitialDirectory = @"C:\Users\Bhargav Teja\Pictures";

            if (saveEncodedImage.ShowDialog() == DialogResult.OK)
            {
                TbxPathForImage.Text = saveEncodedImage.FileName.ToString();
                pictureBox1.ImageLocation = TbxPathForImage.Text;
                Image imageToHideInfo = pictureBox1.Image;
                imageToHideInfo.Save(TbxPathForImage.Text);
            }
        }
    }
}
