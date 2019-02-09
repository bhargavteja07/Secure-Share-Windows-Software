using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.IO.MemoryMappedFiles;



namespace integration
{
    public partial class frmEmbedMessage : Form
    {
        public frmEmbedMessage()
        {
            InitializeComponent();
        }

        private void frmEmbedMessage_Load(object sender, EventArgs e)
        {
            TbxIpAddress.Text = getIpAddress();
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

       /* public int getKey()
        {
            int value=0;
            String text = TbxKeyToEncrypt.Text;
            int textLength = text.Length;
            for (int i = 0; i < textLength;i++)
            {
                value += text[i];

            }
            value = value % 255;
            return value;
        }*/

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnEncodeAndSave_Click(object sender, EventArgs e)
        {
            
        }

        
        private void FrmSecureShareEncoding_Load(object sender, EventArgs e)
        {
            TbxIpAddress.Text = getIpAddress();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            main mainForm = new main();
            mainForm.Tag = this;
            mainForm.Show(this);
            Hide();
        }

        private void BtnEncodeAndSend_Click_1(object sender, EventArgs e)
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
            frmEncode frm = new frmEncode();
            frm.Tag = this;
            Hide();
            frm.Show(this);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmEmbedMessage frm = new frmEmbedMessage();
            frm.Show();
            this.Close();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
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
                TbxPathForImage.Text = pathForImage;
                ImageBoxToHide.Image = Image.FromFile(pathForImage);
                // ImageBoxToHide.Image.sho

            }
          
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (TbxPathForImage.Text == "")
            {
                MessageBox.Show("Please choose an image to send");
                return;
            }
            Bitmap imageToHideInfo = new Bitmap(TbxPathForImage.Text);
            int innerIterator, outerIterator, imageWidth, imageHeight;
            innerIterator = 0;
            outerIterator = 0;
            imageWidth = imageToHideInfo.Width;
            imageHeight = imageToHideInfo.Height;
            int maxBytesStoredByImage;
            maxBytesStoredByImage = imageWidth * imageHeight;
            int sizeOfEnteredData = TbxEnteredData.Text.Length;
            char characterOfData;
            int charReadCount = 0;
            int charToAsciiValue;
            int flag = 0;


            if (sizeOfEnteredData <= maxBytesStoredByImage - 2)
            {
                Color pixel;

                for (outerIterator = 0; outerIterator < imageWidth; outerIterator++)
                {

                    if (flag == 1)
                    {
                        break;
                    }

                    for (innerIterator = 0; innerIterator < imageHeight; innerIterator++)
                    {
                        if (charReadCount == sizeOfEnteredData)
                        {
                            flag = 1;
                            break;
                        }
                        pixel = imageToHideInfo.GetPixel(outerIterator, innerIterator);

                        Console.WriteLine("R [" + outerIterator + "][" + innerIterator + "] = " + pixel.R);
                        Console.WriteLine("G [" + outerIterator + "][" + innerIterator + "] = " + pixel.G);
                        Console.WriteLine("G [" + outerIterator + "][" + innerIterator + "] = " + pixel.B);

                        characterOfData = Convert.ToChar(TbxEnteredData.Text.Substring(charReadCount, 1));
                        charToAsciiValue = Convert.ToInt32(characterOfData);
                        imageToHideInfo.SetPixel(outerIterator, innerIterator, Color.FromArgb(pixel.R, pixel.G, charToAsciiValue));
                        charReadCount++;

                    }
                }
                pixel = imageToHideInfo.GetPixel(imageWidth - 1, imageHeight - 1);
                imageToHideInfo.SetPixel(imageWidth - 1, imageHeight - 1, Color.FromArgb(pixel.R, pixel.G, sizeOfEnteredData));

                Console.WriteLine("R [" + imageWidth + "][" + imageHeight + "] = " + pixel.R);
                Console.WriteLine("G [" + imageWidth + "][" + imageHeight + "] = " + pixel.G);
                Console.WriteLine("G [" + imageWidth + "][" + imageHeight + "] = " + pixel.B);

            }
            else
            {
                MessageBox.Show("The data entered is too large to hide in this image. Please choose a bigger image and try again");
            }


            SaveFileDialog saveEncodedImage = new SaveFileDialog();
            saveEncodedImage.Filter = "Image Files (*.png, *.jpg) | *.png; *.jpg";
            saveEncodedImage.InitialDirectory = @"C:\Users\Bhargav Teja\Pictures";

            if (saveEncodedImage.ShowDialog() == DialogResult.OK)
            {
                TbxPathForImage.Text = saveEncodedImage.FileName.ToString();
                ImageBoxToHide.ImageLocation = TbxPathForImage.Text;
                imageToHideInfo.Save(TbxPathForImage.Text);
            }

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (TbxPathForImage.Text == "")
            {
                MessageBox.Show("Please choose an image to send");
                return;
            }
            Bitmap imageToHideInfo = new Bitmap(TbxPathForImage.Text);
            int innerIterator, outerIterator, imageWidth, imageHeight;
            innerIterator = 0;
            outerIterator = 0;
            imageWidth = imageToHideInfo.Width;
            imageHeight = imageToHideInfo.Height;
            int maxBytesStoredByImage;
            maxBytesStoredByImage = imageWidth * imageHeight;
            int sizeOfEnteredData = TbxEnteredData.Text.Length;
            char characterOfData;
            int charReadCount = 0;
            int charToAsciiValue;
            int flag = 0;


            if (sizeOfEnteredData <= maxBytesStoredByImage - 2)
            {
                Color pixel;

                for (outerIterator = 0; outerIterator < imageWidth; outerIterator++)
                {

                    if (flag == 1)
                    {
                        break;
                    }

                    for (innerIterator = 0; innerIterator < imageHeight; innerIterator++)
                    {
                        if (charReadCount == sizeOfEnteredData)
                        {
                            flag = 1;
                            break;
                        }
                        pixel = imageToHideInfo.GetPixel(outerIterator, innerIterator);

                        Console.WriteLine("R [" + outerIterator + "][" + innerIterator + "] = " + pixel.R);
                        Console.WriteLine("G [" + outerIterator + "][" + innerIterator + "] = " + pixel.G);
                        Console.WriteLine("B [" + outerIterator + "][" + innerIterator + "] = " + pixel.B);

                        characterOfData = Convert.ToChar(TbxEnteredData.Text.Substring(charReadCount, 1));
                        charToAsciiValue = Convert.ToInt32(characterOfData);
                        imageToHideInfo.SetPixel(outerIterator, innerIterator, Color.FromArgb(pixel.R, pixel.G, charToAsciiValue));
                        charReadCount++;

                        pixel = imageToHideInfo.GetPixel(outerIterator, innerIterator);

                        Console.WriteLine("R [" + outerIterator + "][" + innerIterator + "] = " + pixel.R);
                        Console.WriteLine("G [" + outerIterator + "][" + innerIterator + "] = " + pixel.G);
                        Console.WriteLine("B [" + outerIterator + "][" + innerIterator + "] = " + pixel.B);

                    }
                }
                pixel = imageToHideInfo.GetPixel(imageWidth - 1, imageHeight - 1);
                imageToHideInfo.SetPixel(imageWidth - 1, imageHeight - 1, Color.FromArgb(pixel.R, pixel.G, sizeOfEnteredData));

                Console.WriteLine("R [" + imageWidth + "][" + imageHeight + "] = " + pixel.R);
                Console.WriteLine("G [" + imageWidth + "][" + imageHeight + "] = " + pixel.G);
                Console.WriteLine("B [" + imageWidth + "][" + imageHeight + "] = " + pixel.B);

            }
            else
            {
                MessageBox.Show("The data entered is too large to hide in this image. Please choose a bigger image and try again");
                return;
            }

            ImageBoxToHide.Image = imageToHideInfo;

            try
            {

                memStream = new MemoryStream();
                ImageBoxToHide.Image.Save(memStream, ImageBoxToHide.Image.RawFormat);
                // imageToHideInfo.Save(memStream, imageToHideInfo.RawFormat);
                // ImageBoxToHide.Image.Save(memStream, ImageBoxToHide.Image.RawFormat);
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
