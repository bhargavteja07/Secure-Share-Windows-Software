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
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Encryption;



namespace integration
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        public static int currentBitStrength = 0;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmEncode frm = new frmEncode();
            frm.Tag = this;
            frm.Show(this);
            Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmDecode frm = new frmDecode();
            frm.Tag = this;
            frm.Show(this);
            Hide();
        }

        private bool saveFile(string title, string filterString, string outputString)
        {
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

        public static void SetBitStrength(int bitStrength)
        { currentBitStrength = bitStrength; }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            KeyPairGeneratorForm generator = new KeyPairGeneratorForm();
            if (generator.ShowDialog() == DialogResult.OK)
            {
                RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(currentBitStrength);
                string publicAndPrivateKeys = "<BitStrength>" + currentBitStrength.ToString() + "</BitStrength>" + RSAProvider.ToXmlString(true);
                string justPublicKey = "<BitStrength>" + currentBitStrength.ToString() + "</BitStrength>" + RSAProvider.ToXmlString(false);
                if (saveFile("Save Public/Private Keys As", "Public/Private Keys Document( *.kez )|*.kez", publicAndPrivateKeys))
                { while (!saveFile("Save Public Key As", "Public Key Document( *.pke )|*.pke", justPublicKey)) { ; } }
            }
        }
    }
}
