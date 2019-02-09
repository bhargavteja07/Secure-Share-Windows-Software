using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using Encryption;




namespace integration
{
    public partial class Form1 : Form
    {
        private MenuItem saveAsFileMenuItem;
        private MenuItem saveMenuItem;
        private MenuItem selectAllmenuItem;
        private MenuItem separator1;
        private MenuItem separator2;
        private MenuItem separator3;
        private MenuItem undoMenuItem;
        private MenuItem wordWrapMenuItem;
        private MainMenu mainMenu;
        private OpenFileDialog openFileDialog;
        private Panel panel;
        private PictureBox pictureBox;
        private SaveFileDialog saveFileDialog;
        private TextBox inputTextBox;
        public static int currentBitStrength = 0;


        public Form1()
        {
            InitializeComponent();
        }
        public static void SetBitStrength(int bitStrength)
        { currentBitStrength = bitStrength; }

        private void button1_Click(object sender, EventArgs e)
        {
            frmEncode encodingForm = new frmEncode();
            encodingForm.Tag = this;
            encodingForm.Show(this);
            Hide();

        }
        private bool saveFile(string title, string filterString, string outputString)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = title;
            saveFileDialog.Filter = filterString;
            saveFileDialog.FileName = "";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
                    if (outputString != null)
                    { streamWriter.Write(outputString); }
                    streamWriter.Close();
                    return true;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    return false;
                }
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KeyPairGeneratorForm generator = new KeyPairGeneratorForm();
          //  if (generator.ShowDialog() == DialogResult.OK)
            //{
                RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(currentBitStrength);
                string publicAndPrivateKeys = "<BitStrength>" + currentBitStrength.ToString() + "</BitStrength>" + RSAProvider.ToXmlString(true);
                string justPublicKey = "<BitStrength>" + currentBitStrength.ToString() + "</BitStrength>" + RSAProvider.ToXmlString(false);
                if (saveFile("Save Public/Private Keys As", "Public/Private Keys Document( *.kez )|*.kez", publicAndPrivateKeys))
                { while (!saveFile("Save Public Key As", "Public Key Document( *.pke )|*.pke", justPublicKey)) { ; } }
            //}
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDecode decodingForm = new frmDecode();
            decodingForm.Tag = this;
            decodingForm.Show(this);
            Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmEncode decodingForm = new frmEncode();
            decodingForm.Tag = this;
            decodingForm.Show(this);
            Hide();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            KeyPairGeneratorForm1 frm = new KeyPairGeneratorForm1();
            frm.Tag = this;
            frm.Show(this);
            Hide();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmDecode decodingForm = new frmDecode();
            decodingForm.Tag = this;
            decodingForm.Show(this);
            Hide();
        }
    }
}
