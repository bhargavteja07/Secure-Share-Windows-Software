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
    public partial class frmFetchMessage : Form
    {
        public frmFetchMessage()
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
                recievedImageBox.Image = Image.FromStream(networkStream);
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

        private void btnRecieve_Click(object sender, EventArgs e)
        {
            //lblRecieveStatus.Text = "Image";
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            
        }

       
        private void btnLoad_Click(object sender, EventArgs e)
        {
            
        }
        private void frmFetchMessage_Load(object sender, EventArgs e)
        {
            
        }
        private void tbxDecodedMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            main frm = new main();
            frm.Tag = this;
            Hide();
            frm.Show(this);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmDecode frm = new frmDecode();
            frm.Tag = this;
            Hide();
            frm.Show(this);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmFetchMessage frm = new frmFetchMessage();
            frm.Show();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(recieveImage));
            thread.Start();
            
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            try
            {
                Image recievedImage = recievedImageBox.Image;
                Bitmap recievedImgBitMap = new Bitmap(recievedImage);

                string decodedData = "";

                int innerIterator, outerIterator, imageWidth, imageHeight;
                innerIterator = 0;
                outerIterator = 0;
                imageWidth = recievedImage.Width;
                imageHeight = recievedImage.Height;
                int maxBytesStoredByImage;
                maxBytesStoredByImage = imageWidth * imageHeight;
                Color lastpixel = recievedImgBitMap.GetPixel(recievedImgBitMap.Width - 1, recievedImgBitMap.Height - 1);
                int recievedBytes = lastpixel.B;
                Color pixel;
                int flag = 0, valueOfData;
                int charReadCount = 0;
                char charOfValue;
                String letter;

                for (outerIterator = 0; outerIterator < imageWidth; outerIterator++)
                {

                    if (flag == 1)
                    {
                        break;
                    }

                    for (innerIterator = 0; innerIterator < imageHeight; innerIterator++)
                    {
                        if (charReadCount == recievedBytes)
                        {
                            flag = 1;
                            break;
                        }
                        pixel = recievedImgBitMap.GetPixel(outerIterator, innerIterator);
                        valueOfData = pixel.B;
                        charOfValue = Convert.ToChar(valueOfData);
                        letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(charOfValue) });
                        decodedData += letter;
                        charReadCount++;

                    }
                }

                tbxDecodedMessage.Text = decodedData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Image to decode");

            }
  
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files (*.png, *.jpg) | *.png; *.jpg";
            openDialog.InitialDirectory = @"C:\Users\Bhargav Teja\Pictures";
            Console.WriteLine("Opening dialogue box to choose image");
            // openFileDialog1.ShowDialog();
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string pathForImage = openDialog.FileName.ToString();
                Console.WriteLine("Path for Image is : " + pathForImage);
                //TbxPathForImage.Text = pathForImage;
                recievedImageBox.Image = Image.FromFile(pathForImage);
                // ImageBoxToHide.Image.sho

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void recievedImageBox_Click(object sender, EventArgs e)
        {

        }
    }
}
