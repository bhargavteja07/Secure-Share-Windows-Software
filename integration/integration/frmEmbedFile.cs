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
    public partial class frmEmbedFile : Form
    {
        public frmEmbedFile()
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
                if (ip.AddressFamily.ToString() == "InterNetwork");
                localIp = ip.ToString();
            }
            return localIp;
        }

        string loadedTrueImagePath, loadedFilePath, saveToImage, DLoadImagePath, DSaveFilePath;
        int height, width;
        long fileSize, fileNameSize;
        Image loadedTrueImage, DecryptedImage, AfterEncryption;
        Bitmap loadedTrueBitmap, DecryptedBitmap;
        Rectangle previewImage = new Rectangle(20, 200, 490, 470);
        bool canPaint = false, EncriptionDone = false;
        byte[] fileContainer;



        private void EnImageBrowse_btn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loadedTrueImagePath = openFileDialog1.FileName;
                EnImage_tbx.Text = loadedTrueImagePath;
                loadedTrueImage = Image.FromFile(loadedTrueImagePath);
                height = loadedTrueImage.Height;
                width = loadedTrueImage.Width;
                loadedTrueBitmap = new Bitmap(loadedTrueImage);

                FileInfo imginf = new FileInfo(loadedTrueImagePath);
                float fs = (float)imginf.Length / 1024;
                ImageSize_lbl.Text = smalldecimal(fs.ToString(), 2) + " KB";
                ImageHeight_lbl.Text = loadedTrueImage.Height.ToString() + " Pixel";
                ImageWidth_lbl.Text = loadedTrueImage.Width.ToString() + " Pixel";
                double cansave = (8.0 * ((height * (width / 3) * 3) / 3 - 1)) / 1024;
                CanSave_lbl.Text = smalldecimal(cansave.ToString(), 2) + " KB";

                canPaint = true;
                pictureBox1.Image = loadedTrueBitmap;
                
               //this.Invalidate();
            }
        }

        private void frmEmbedFile_Paint(object sender, PaintEventArgs e)
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

        private string smalldecimal(string inp, int dec)
        {
            int i;
            for (i = inp.Length - 1; i > 0; i--)
                if (inp[i] == '.')
                    break;
            try
            {
                return inp.Substring(0, i + dec + 1);
            }
            catch
            {
                return inp;
            }
        }




        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveToImage = saveFileDialog1.FileName;
            }
            else
                return;
            if (EnImage_tbx.Text == String.Empty || EnFile_tbx.Text == String.Empty)
            {
                MessageBox.Show("Encrypton information is incomplete!\nPlease complete them frist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (8 * ((height * width) / 3 - 1) < fileSize + fileNameSize)
            {
                MessageBox.Show("File size is too large!\nPlease use a larger image to hide this file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            fileContainer = File.ReadAllBytes(loadedFilePath);
            EncryptLayer();
        }


        private void EncryptLayer()
        {
            toolStripStatusLabel1.Text = "Embeding File... Please wait";
            Application.DoEvents();
            long FSize = fileSize;
            Bitmap changedBitmap = EncryptLayer(8, loadedTrueBitmap, 0, (height * (width / 3) * 3) / 3 - fileNameSize - 1, true);
            FSize -= (height * (width / 3) * 3) / 3 - fileNameSize - 1;
            if (FSize > 0)
            {
                for (int i = 7; i >= 0 && FSize > 0; i--)
                {
                    changedBitmap = EncryptLayer(i, changedBitmap, (((8 - i) * height * (width / 3) * 3) / 3 - fileNameSize - (8 - i)), (((9 - i) * height * (width / 3) * 3) / 3 - fileNameSize - (9 - i)), false);
                    FSize -= (height * (width / 3) * 3) / 3 - 1;
                }
            }
            changedBitmap.Save(saveToImage);
            toolStripStatusLabel1.Text = "Embedded image has been successfully saved.";
            EncriptionDone = true;
            AfterEncryption = Image.FromFile(saveToImage);
            pictureBox1.Image = AfterEncryption;
            //this.Invalidate();
        }


        private Bitmap EncryptLayer(int layer, Bitmap inputBitmap, long startPosition, long endPosition, bool writeFileName)
        {
            Bitmap outputBitmap = inputBitmap;
            layer--;
            int i = 0, j = 0;
            long FNSize = 0;
            bool[] t = new bool[8];
            bool[] rb = new bool[8];
            bool[] gb = new bool[8];
            bool[] bb = new bool[8];
            Color pixel = new Color();
            byte r, g, b;

            if (writeFileName)
            {
                FNSize = fileNameSize;
                string fileName = justFName(loadedFilePath);

                //write fileName:
                for (i = 0; i < height && i * (height / 3) < fileNameSize; i++)
                    for (j = 0; j < (width / 3) * 3 && i * (height / 3) + (j / 3) < fileNameSize; j++)
                    {
                        byte2bool((byte)fileName[i * (height / 3) + j / 3], ref t);
                        pixel = inputBitmap.GetPixel(j, i);
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        byte2bool(r, ref rb);
                        byte2bool(g, ref gb);
                        byte2bool(b, ref bb);
                        if (j % 3 == 0)
                        {
                            rb[7] = t[0];
                            gb[7] = t[1];
                            bb[7] = t[2];
                        }
                        else if (j % 3 == 1)
                        {
                            rb[7] = t[3];
                            gb[7] = t[4];
                            bb[7] = t[5];
                        }
                        else
                        {
                            rb[7] = t[6];
                            gb[7] = t[7];
                        }
                        Color result = Color.FromArgb((int)bool2byte(rb), (int)bool2byte(gb), (int)bool2byte(bb));
                        outputBitmap.SetPixel(j, i, result);
                    }
                i--;
            }
            //write file (after file name):
            int tempj = j;

            for (; i < height && i * (height / 3) < endPosition - startPosition + FNSize && startPosition + i * (height / 3) < fileSize + FNSize; i++)
                for (j = 0; j < (width / 3) * 3 && i * (height / 3) + (j / 3) < endPosition - startPosition + FNSize && startPosition + i * (height / 3) + (j / 3) < fileSize + FNSize; j++)
                {
                    if (tempj != 0)
                    {
                        j = tempj;
                        tempj = 0;
                    }
                    byte2bool((byte)fileContainer[startPosition + i * (height / 3) + j / 3 - FNSize], ref t);
                    pixel = inputBitmap.GetPixel(j, i);
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    byte2bool(r, ref rb);
                    byte2bool(g, ref gb);
                    byte2bool(b, ref bb);
                    if (j % 3 == 0)
                    {
                        rb[layer] = t[0];
                        gb[layer] = t[1];
                        bb[layer] = t[2];
                    }
                    else if (j % 3 == 1)
                    {
                        rb[layer] = t[3];
                        gb[layer] = t[4];
                        bb[layer] = t[5];
                    }
                    else
                    {
                        rb[layer] = t[6];
                        gb[layer] = t[7];
                    }
                    Color result = Color.FromArgb((int)bool2byte(rb), (int)bool2byte(gb), (int)bool2byte(bb));
                    outputBitmap.SetPixel(j, i, result);

                }
            long tempFS = fileSize, tempFNS = fileNameSize;
            r = (byte)(tempFS % 100);
            tempFS /= 100;
            g = (byte)(tempFS % 100);
            tempFS /= 100;
            b = (byte)(tempFS % 100);
            Color flenColor = Color.FromArgb(r, g, b);
            outputBitmap.SetPixel(width - 1, height - 1, flenColor);

            r = (byte)(tempFNS % 100);
            tempFNS /= 100;
            g = (byte)(tempFNS % 100);
            tempFNS /= 100;
            b = (byte)(tempFNS % 100);
            Color fnlenColor = Color.FromArgb(r, g, b);
            outputBitmap.SetPixel(width - 2, height - 1, fnlenColor);

            return outputBitmap;
        }

        private void frmEmbedFile_Paint_1(object sender, PaintEventArgs e)
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

       


        private void frmEmbedFile_Load(object sender, EventArgs e)
        {
            TbxIpAddress.Text = getIpAddress();
        }
        private void byte2bool(byte inp, ref bool[] outp)
        {
            if (inp >= 0 && inp <= 255)
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
                    outp += (byte)Math.Pow(2.0, (double)(7 - i));
            }
            return outp;
        }

        private void EnFileBrowse_btn_Click(object sender, EventArgs e)
        {
           
        }
        private string justFName(string path)
        {
            string output;
            int i;
            if (path.Length == 3)   // i.e: "C:\\"
                return path.Substring(0, 1);
            for (i = path.Length - 1; i > 0; i--)
                if (path[i] == '\\')
                    break;
            output = path.Substring(i + 1);
            return output;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmEmbedFile frm = new frmEmbedFile();
            frm.Show();
            this.Close();

        }

        private void btnSend_Click(object sender, EventArgs e)
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
        private void button1_Click(object sender, EventArgs e)

        {
            frmEmbedFile frm = new frmEmbedFile();
            frm.Show();
            this.Close();
        }

        private void Encrypt_btn_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loadedTrueImagePath = openFileDialog1.FileName;
                EnImage_tbx.Text = loadedTrueImagePath;
                loadedTrueImage = Image.FromFile(loadedTrueImagePath);
                height = loadedTrueImage.Height;
                width = loadedTrueImage.Width;
                loadedTrueBitmap = new Bitmap(loadedTrueImage);

                FileInfo imginf = new FileInfo(loadedTrueImagePath);
                float fs = (float)imginf.Length / 1024;
                ImageSize_lbl.Text = smalldecimal(fs.ToString(), 2) + " KB";
                ImageHeight_lbl.Text = loadedTrueImage.Height.ToString() + " Pixel";
                ImageWidth_lbl.Text = loadedTrueImage.Width.ToString() + " Pixel";
                double cansave = (8.0 * ((height * (width / 3) * 3) / 3 - 1)) / 1024;
                CanSave_lbl.Text = smalldecimal(cansave.ToString(), 2) + " KB";

                canPaint = true;
                pictureBox1.Image = loadedTrueBitmap;

                //this.Invalidate();
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                loadedFilePath = openFileDialog2.FileName;
                EnFile_tbx.Text = loadedFilePath;
                FileInfo finfo = new FileInfo(loadedFilePath);
                fileSize = finfo.Length;
                fileNameSize = justFName(loadedFilePath).Length;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Console.WriteLine(TbxIpAddress);
            try
            {

                memStream = new MemoryStream();
                AfterEncryption.Save(memStream, AfterEncryption.RawFormat);
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
