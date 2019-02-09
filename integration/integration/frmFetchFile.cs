using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;


namespace integration
{
    public partial class frmFetchFile : Form
    {
        public frmFetchFile()
        {
            InitializeComponent();
        }

        string loadedTrueImagePath, loadedFilePath, saveToImage,DLoadImagePath,DSaveFilePath;
        int height, width;
        long fileSize, fileNameSize;
        Image loadedTrueImage, DecryptedImage ,AfterEncryption;
        Bitmap loadedTrueBitmap, DecryptedBitmap;
        Rectangle previewImage = new Rectangle(20,160,490,470);
        bool canPaint = false, EncriptionDone = false;
        byte[] fileContainer;

        private void DeLoadImageBrowse_btn_Click(object sender, EventArgs e)
        {
            
        }

        private void DeSaveFileBrowse_btn_Click(object sender, EventArgs e)
        {
            
        }

        private void Decrypt_btn_Click(object sender, EventArgs e)
        {
            
            
        }

        private void DecryptLayer()
        {
            toolStripStatusLabel1.Text = "Fetching File... Please wait";
            Application.DoEvents();
            int i, j = 0;
            bool[] t = new bool[8];
            bool[] rb = new bool[8];
            bool[] gb = new bool[8];
            bool[] bb = new bool[8];
            Color pixel = new Color();
            byte r, g, b;
            pixel = DecryptedBitmap.GetPixel(width - 1, height - 1);
            long fSize = pixel.R + pixel.G * 100 + pixel.B * 10000;
            pixel = DecryptedBitmap.GetPixel(width - 2, height - 1);
            long fNameSize = pixel.R + pixel.G * 100 + pixel.B * 10000;
            byte[] res = new byte[fSize];
            string resFName = "";
            byte temp;

            //Read file name:
            for (i = 0; i < height && i * (height / 3) < fNameSize; i++)
                for (j = 0; j < (width / 3) * 3 && i * (height / 3) + (j / 3) < fNameSize; j++)
                {
                    pixel = DecryptedBitmap.GetPixel(j, i);
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    byte2bool(r, ref rb);
                    byte2bool(g, ref gb);
                    byte2bool(b, ref bb);
                    if (j % 3 == 0)
                    {
                        t[0] = rb[7];
                        t[1] = gb[7];
                        t[2] = bb[7];
                    }
                    else if (j % 3 == 1)
                    {
                        t[3] = rb[7];
                        t[4] = gb[7];
                        t[5] = bb[7];
                    }
                    else
                    {
                        t[6] = rb[7];
                        t[7] = gb[7];
                        temp = bool2byte(t);
                        resFName += (char)temp;
                    }
                }

            //Read file on layer 8 (after file name):
            int tempj = j;
            i--;

            for (; i < height && i * (height / 3) < fSize + fNameSize; i++)
                for (j = 0; j < (width / 3) * 3 && i * (height / 3) + (j / 3) < (height * (width / 3) * 3) / 3 - 1 && i * (height / 3) + (j / 3) < fSize + fNameSize; j++)
                {
                    if (tempj != 0)
                    {
                        j = tempj;
                        tempj = 0;
                    }
                    pixel = DecryptedBitmap.GetPixel(j, i);
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    byte2bool(r, ref rb);
                    byte2bool(g, ref gb);
                    byte2bool(b, ref bb);
                    if (j % 3 == 0)
                    {
                        t[0] = rb[7];
                        t[1] = gb[7];
                        t[2] = bb[7];
                    }
                    else if (j % 3 == 1)
                    {
                        t[3] = rb[7];
                        t[4] = gb[7];
                        t[5] = bb[7];
                    }
                    else
                    {
                        t[6] = rb[7];
                        t[7] = gb[7];
                        temp = bool2byte(t);
                        res[i * (height / 3) + j / 3 - fNameSize] = temp;
                    }
                }

            //Read file on other layers:
            long readedOnL8 = (height * (width/3)*3) /3 - fNameSize - 1;

            for (int layer = 6; layer >= 0 && readedOnL8 + (6 - layer) * ((height * (width / 3) * 3) / 3 - 1) < fSize; layer--)
                for (i = 0; i < height && i * (height / 3) + readedOnL8 + (6 - layer) * ((height * (width / 3) * 3) / 3 - 1) < fSize; i++)
                    for (j = 0; j < (width / 3) * 3 && i * (height / 3) + (j / 3) + readedOnL8 + (6 - layer) * ((height * (width / 3) * 3) / 3 - 1) < fSize; j++)
                    {
                        pixel = DecryptedBitmap.GetPixel(j, i);
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        byte2bool(r, ref rb);
                        byte2bool(g, ref gb);
                        byte2bool(b, ref bb);
                        if (j % 3 == 0)
                        {
                            t[0] = rb[layer];
                            t[1] = gb[layer];
                            t[2] = bb[layer];
                        }
                        else if (j % 3 == 1)
                        {
                            t[3] = rb[layer];
                            t[4] = gb[layer];
                            t[5] = bb[layer];
                        }
                        else
                        {
                            t[6] = rb[layer];
                            t[7] = gb[layer];
                            temp = bool2byte(t);
                            res[i * (height / 3) + j / 3 + (6 - layer) * ((height * (width / 3) * 3) / 3 - 1) + readedOnL8] = temp;
                        }
                    }

            if (File.Exists(DSaveFilePath + "\\" + resFName))
            {
                MessageBox.Show("File \"" + resFName + "\" already exist please choose another path to save file", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
                File.WriteAllBytes(DSaveFilePath + "\\" + resFName, res);
            toolStripStatusLabel1.Text = "File has been Fetched and saved successfully.";
            Application.DoEvents();
        }

        private void byte2bool(byte inp, ref bool[] outp)
        {
            if(inp>=0 && inp<=255)
                for (short i = 7; i >= 0; i--)
                {
                    if (inp % 2 == 1)
                        outp[i] = true;
                    else
                        outp[i] = false;
                    inp /= 2;
                }
            else
                throw new Exception("Input number is illegal.");
        }

        private byte bool2byte(bool[] inp)
        {
            byte outp = 0;
            for (short i = 7; i >= 0; i--)
            {
                if (inp[i])
                    outp += (byte)Math.Pow(2.0, (double)(7-i));
            }
            return outp;
        }

        private void frmFetchFile_Paint(object sender, PaintEventArgs e)
        {
            if (canPaint)
                try
                {
                    if (!EncriptionDone)
                        e.Graphics.DrawImage(loadedTrueImage, previewImage);
                    else
                        e.Graphics.DrawImage(AfterEncryption, previewImage);
                }
                catch
                {
                    e.Graphics.DrawImage(DecryptedImage, previewImage);
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmFetchFile frm = new frmFetchFile();
            frm.Show();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DLoadImagePath = openFileDialog1.FileName;
                DeLoadImage_tbx.Text = DLoadImagePath;
                DecryptedImage = Image.FromFile(DLoadImagePath);
                height = DecryptedImage.Height;
                width = DecryptedImage.Width;
                DecryptedBitmap = new Bitmap(DecryptedImage);

                FileInfo imginf = new FileInfo(DLoadImagePath);
                float fs = (float)imginf.Length / 1024;

                canPaint = true;
                pictureBox1.Image = DecryptedImage;
                //this.Invalidate();
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DSaveFilePath = folderBrowserDialog1.SelectedPath;
                DeSaveFile_tbx.Text = DSaveFilePath;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (DeSaveFile_tbx.Text == String.Empty || DeLoadImage_tbx.Text == String.Empty)
            {
                MessageBox.Show("Text boxes must not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (System.IO.File.Exists(DeLoadImage_tbx.Text) == false)
            {
                MessageBox.Show("Select image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DeLoadImage_tbx.Focus();
                return;
            }
            DecryptLayer();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(recieveImage));
            thread.Start();
        
        }

        private void frmFetchFile_Load(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
       

        }
    }
