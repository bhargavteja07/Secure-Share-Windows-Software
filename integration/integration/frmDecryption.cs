using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using integration;

namespace Encryption
{
    public class frmDecryp : Form
    {
        private FontDialog fontDialog;
        private IContainer components;
        private Label processingLabel;
        private MenuItem copyMenuItem;
        private MenuItem cutMenuItem;
        private MenuItem decryptMenuItem;
        private MenuItem deleteMenuItem;
        private MenuItem editMenuItem;
        private MenuItem encryptionMenuItem;
        private MenuItem exitMenuItem;
        private MenuItem fileMenuItem;
        private MenuItem fontMenuItem;
        private MenuItem formatMenuItem;
        private MenuItem newFileMenuItem;
        private MenuItem openFileMenuItem;
        private MenuItem pasteMenuItem;
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
        private bool cleanForm = true;
        private string currentFileName = "Untitled";

        public delegate void FinishedProcessDelegate();
        public delegate void UpdateBitStrengthDelegate(int bitStrength);
        public delegate void UpdateTextDelegate(string inputText);

        public frmDecryp()
        { InitializeComponent(); }

        public frmDecryp(string fileName)
        {
            InitializeComponent();
            if (File.Exists(fileName))
            {
                currentFileName = fileName;
                StreamReader streamReader = new StreamReader(fileName, true);
                SetText(streamReader.ReadToEnd());
                streamReader.Close();
                this.Text = GetFileName(fileName) + " - Encryption";
                cleanForm = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                { components.Dispose(); }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDecryp));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenuItem = new System.Windows.Forms.MenuItem();
            this.newFileMenuItem = new System.Windows.Forms.MenuItem();
            this.openFileMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.saveAsFileMenuItem = new System.Windows.Forms.MenuItem();
            this.separator1 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.editMenuItem = new System.Windows.Forms.MenuItem();
            this.undoMenuItem = new System.Windows.Forms.MenuItem();
            this.separator2 = new System.Windows.Forms.MenuItem();
            this.cutMenuItem = new System.Windows.Forms.MenuItem();
            this.copyMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteMenuItem = new System.Windows.Forms.MenuItem();
            this.separator3 = new System.Windows.Forms.MenuItem();
            this.selectAllmenuItem = new System.Windows.Forms.MenuItem();
            this.formatMenuItem = new System.Windows.Forms.MenuItem();
            this.wordWrapMenuItem = new System.Windows.Forms.MenuItem();
            this.fontMenuItem = new System.Windows.Forms.MenuItem();
            this.encryptionMenuItem = new System.Windows.Forms.MenuItem();
            this.decryptMenuItem = new System.Windows.Forms.MenuItem();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.panel = new System.Windows.Forms.Panel();
            this.processingLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.formatMenuItem,
            this.encryptionMenuItem});
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Index = 0;
            this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.newFileMenuItem,
            this.openFileMenuItem,
            this.saveMenuItem,
            this.saveAsFileMenuItem,
            this.separator1,
            this.menuItem1,
            this.menuItem2,
            this.exitMenuItem});
            this.fileMenuItem.Text = "File";
            // 
            // newFileMenuItem
            // 
            this.newFileMenuItem.Index = 0;
            this.newFileMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.newFileMenuItem.Text = "New";
            this.newFileMenuItem.Click += new System.EventHandler(this.newFileMenuItem_Click);
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Index = 1;
            this.openFileMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.openFileMenuItem.Text = "Open...";
            this.openFileMenuItem.Click += new System.EventHandler(this.openFileMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Index = 2;
            this.saveMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.saveMenuItem.Text = "Save...";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // saveAsFileMenuItem
            // 
            this.saveAsFileMenuItem.Index = 3;
            this.saveAsFileMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.saveAsFileMenuItem.Text = "Save As... ";
            this.saveAsFileMenuItem.Click += new System.EventHandler(this.saveAsFileMenuItem_Click);
            // 
            // separator1
            // 
            this.separator1.Index = 4;
            this.separator1.Text = "-";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 5;
            this.menuItem1.Text = "Back";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 6;
            this.menuItem2.Text = "Home";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 7;
            this.exitMenuItem.Text = "Exit ";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.Index = 1;
            this.editMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.undoMenuItem,
            this.separator2,
            this.cutMenuItem,
            this.copyMenuItem,
            this.pasteMenuItem,
            this.deleteMenuItem,
            this.separator3,
            this.selectAllmenuItem});
            this.editMenuItem.Text = "Edit";
            // 
            // undoMenuItem
            // 
            this.undoMenuItem.Index = 0;
            this.undoMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.undoMenuItem.Text = "Undo";
            this.undoMenuItem.Click += new System.EventHandler(this.undoMenuItem_Click);
            // 
            // separator2
            // 
            this.separator2.Index = 1;
            this.separator2.Text = "-";
            // 
            // cutMenuItem
            // 
            this.cutMenuItem.Index = 2;
            this.cutMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.cutMenuItem.Text = "Cut";
            this.cutMenuItem.Click += new System.EventHandler(this.cutMenuItem_Click);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Index = 3;
            this.copyMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.copyMenuItem.Text = "Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Index = 4;
            this.pasteMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.pasteMenuItem.Text = "Paste";
            this.pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Index = 5;
            this.deleteMenuItem.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.deleteMenuItem.Text = "Delete";
            this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // separator3
            // 
            this.separator3.Index = 6;
            this.separator3.Text = "-";
            // 
            // selectAllmenuItem
            // 
            this.selectAllmenuItem.Index = 7;
            this.selectAllmenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.selectAllmenuItem.Text = "Select All";
            this.selectAllmenuItem.Click += new System.EventHandler(this.selectAllmenuItem_Click);
            // 
            // formatMenuItem
            // 
            this.formatMenuItem.Index = 2;
            this.formatMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.wordWrapMenuItem,
            this.fontMenuItem});
            this.formatMenuItem.Text = "Format";
            // 
            // wordWrapMenuItem
            // 
            this.wordWrapMenuItem.Checked = true;
            this.wordWrapMenuItem.Index = 0;
            this.wordWrapMenuItem.RadioCheck = true;
            this.wordWrapMenuItem.Text = "Word Wrap";
            this.wordWrapMenuItem.Click += new System.EventHandler(this.wordWrapMenuItem_Click);
            // 
            // fontMenuItem
            // 
            this.fontMenuItem.Index = 1;
            this.fontMenuItem.Text = "Font...";
            this.fontMenuItem.Click += new System.EventHandler(this.fontMenuItem_Click);
            // 
            // encryptionMenuItem
            // 
            this.encryptionMenuItem.Index = 3;
            this.encryptionMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.decryptMenuItem});
            this.encryptionMenuItem.Text = "Decryption";
            this.encryptionMenuItem.Click += new System.EventHandler(this.encryptionMenuItem_Click);
            // 
            // decryptMenuItem
            // 
            this.decryptMenuItem.Index = 0;
            this.decryptMenuItem.Text = "Decrypt...";
            this.decryptMenuItem.Click += new System.EventHandler(this.decryptMenuItem_Click);
            // 
            // inputTextBox
            // 
            this.inputTextBox.AcceptsReturn = true;
            this.inputTextBox.AcceptsTab = true;
            this.inputTextBox.AllowDrop = true;
            this.inputTextBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.inputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputTextBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextBox.ForeColor = System.Drawing.Color.Black;
            this.inputTextBox.Location = new System.Drawing.Point(0, 0);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputTextBox.Size = new System.Drawing.Size(804, 473);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.TabStop = false;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            this.inputTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.inputTextBox_DragDrop);
            this.inputTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.inputTextBox_DragEnter);
            // 
            // fontDialog
            // 
            this.fontDialog.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontDialog.FontMustExist = true;
            this.fontDialog.ShowEffects = false;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Black;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel.Controls.Add(this.processingLabel);
            this.panel.Controls.Add(this.pictureBox);
            this.panel.ForeColor = System.Drawing.Color.Lime;
            this.panel.Location = new System.Drawing.Point(-1, 411);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 82);
            this.panel.TabIndex = 0;
            this.panel.Visible = false;
            // 
            // processingLabel
            // 
            this.processingLabel.Location = new System.Drawing.Point(8, 48);
            this.processingLabel.Name = "processingLabel";
            this.processingLabel.Size = new System.Drawing.Size(184, 23);
            this.processingLabel.TabIndex = 0;
            this.processingLabel.Text = "Processing please wait...";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(100, 50);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // frmDecryp
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(804, 479);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.inputTextBox);
            this.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu;
            this.Name = "frmDecryp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Share - Decrypt File";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        private void FinishedProcess()
        {
            panel.Visible = false;
            fileMenuItem.Enabled = true;
            editMenuItem.Enabled = true;
            formatMenuItem.Enabled = true;
            encryptionMenuItem.Enabled = true;
           // helpMenuItem.Enabled = true;
            Application.DoEvents();
        }

        public static void SetBitStrength(int bitStrength)
        { currentBitStrength = bitStrength; }

        private void UpdateText(string inputText)
        { inputTextBox.Text = inputText; }

        private string GetFileName(string fileName)
        {
            string[] fileParts = fileName.Split("\\".ToCharArray());
            return fileParts[fileParts.Length - 1];
        }

        private void SetText(string text)
        {
            this.inputTextBox.TextChanged -= new System.EventHandler(this.inputTextBox_TextChanged);
            inputTextBox.Text = text;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
        }

        private Settings GetSettings()
        {
            Settings settings = null;
            if (File.Exists("Settings.bin"))
            {
                StreamReader streamReader = new StreamReader("Settings.bin");
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                settings = (Settings)binaryFormatter.Deserialize(streamReader.BaseStream);
                streamReader.Close();
            }
            return settings;
        }

        private void SaveSettings(string settingChanged)
        {
            Settings settings = new Settings();
            if (File.Exists("Settings.bin"))
            {
                StreamReader streamReader = new StreamReader("Settings.bin");
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                settings = (Settings)binaryFormatter.Deserialize(streamReader.BaseStream);
                streamReader.Close();
                switch (settingChanged)
                {
                    case "LOCATION":
                        {
                            settings.Location = this.Location;
                            break;
                        }
                    case "SIZE":
                        {
                            settings.Width = this.Width;
                            settings.Height = this.Height;
                            break;
                        }
                    case "FONT":
                        {
                            settings.Font = inputTextBox.Font;
                            break;
                        }
                    case "WRAPPING":
                        {
                            settings.Wrapping = inputTextBox.WordWrap;
                            break;
                        }
                }
                StreamWriter streamWriter = new StreamWriter("Settings.bin", false);
                binaryFormatter.Serialize(streamWriter.BaseStream, settings);
                streamWriter.Close();
            }
            else
            {
                settings.Location = this.Location;
                settings.Width = this.Width;
                settings.Height = this.Height;
                settings.Font = inputTextBox.Font;
                settings.Wrapping = inputTextBox.WordWrap;
                StreamWriter streamWriter = new StreamWriter("Settings.bin", false);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(streamWriter.BaseStream, settings);
                streamWriter.Close();
            }
        }

        private string openFile(string title, string filterString)
        {
            openFileDialog.FileName = "";
            openFileDialog.Title = title;
            openFileDialog.Filter = filterString;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    StreamReader streamReader = new StreamReader(openFileDialog.FileName, true);
                    string fileString = streamReader.ReadToEnd();
                    streamReader.Close();
                    if (fileString.Length >= inputTextBox.MaxLength)
                    {
                        MessageBox.Show("ERROR: \nThe file you are trying to open is too big for the text editor to display properly.\nPlease open a smaller document!\nOperation Aborted!");
                        return null;
                    }
                    if (fileString != null)
                    {
                        this.Text = GetFileName(openFileDialog.FileName) + " - Encryption";
                        currentFileName = openFileDialog.FileName;
                    }
                    return fileString;
                }
            }
            return null;
        }

        private bool saveFile()
        {
            saveFileDialog.Title = "Save As";
            saveFileDialog.FileName = "*.txt";
            saveFileDialog.Filter = "Text Document( *.txt )|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName, false);
                    streamWriter.Write(inputTextBox.Text);
                    streamWriter.Close();
                    this.Text = GetFileName(saveFileDialog.FileName) + " - Encryption";
                    currentFileName = saveFileDialog.FileName;
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

        private void mainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists("Settings.bin"))
            {
                Settings settings = GetSettings();
                this.Location = settings.Location;
                this.Width = settings.Width;
                this.Height = settings.Height;
                inputTextBox.Font = settings.Font;
                inputTextBox.WordWrap = settings.Wrapping;
                wordWrapMenuItem.Checked = (settings.Wrapping == true) ? true : false;
            }
            this.Text = GetFileName(currentFileName) + " - Encryption";
            inputTextBox.Focus();
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.Move += new System.EventHandler(this.MainForm_Move);
            inputTextBox.Height = this.Size.Height - 53;
            inputTextBox.Width = this.Size.Width - 7;
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            inputTextBox.Height = this.Size.Height - 53;
            inputTextBox.Width = this.Size.Width - 7;
            SaveSettings("SIZE");
        }

        private void MainForm_Move(object sender, EventArgs e)
        { SaveSettings("LOCATION"); }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Resize -= new EventHandler(this.mainForm_Resize);
            this.Move -= new EventHandler(this.MainForm_Move);
            if (!cleanForm)
            {
                string dialogText = "The text in the " + currentFileName + " file has changed." + Environment.NewLine + Environment.NewLine + "Do you want to save the changes?";
                switch (MessageBox.Show(dialogText, "Encryption", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        {
                            if (!saveFile()) { e.Cancel = true; }
                            break;
                        }
                    case DialogResult.Cancel:
                        {
                            e.Cancel = true;
                            break;
                        }
                }
            }
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            if (currentFileName.Equals("Untitled"))
            { cleanForm = (!inputTextBox.Text.Equals("")) ? false : true; }
            else
            { cleanForm = false; }
        }

        private void inputTextBox_DragEnter(object sender, DragEventArgs e)
        { e.Effect = DragDropEffects.All; }

        private void inputTextBox_DragDrop(object sender, DragEventArgs e)
        {
            bool clean = false;
            if (!cleanForm)
            {
                string dialogText = "The text in the " + currentFileName + " file has changed." + Environment.NewLine + Environment.NewLine + "Do you want to save the changes?";
                switch (MessageBox.Show(dialogText, "Encryption", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        {
                            if (saveFile()) { clean = true; }
                            break;
                        }
                    case DialogResult.No:
                        {
                            clean = true;
                            break;
                        }
                }
            }
            else
            { clean = true; }
            if (clean)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true) == true)
                {
                    string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, true);
                    StreamReader streamReader = new StreamReader(fileNames[0], true);
                    string fileContents = streamReader.ReadToEnd();
                    streamReader.Close();
                    currentFileName = fileNames[0];
                    this.Text = GetFileName(fileNames[0]) + " - Encryption";
                    SetText(fileContents);
                    cleanForm = true;
                }
            }
        }

        private void newFileMenuItem_Click(object sender, EventArgs e)
        {
            if (!cleanForm)
            {
                string dialogText = "The text in the " + currentFileName + " file has changed." + Environment.NewLine + Environment.NewLine + "Do you want to save the changes?";
                switch (MessageBox.Show(dialogText, "Encryption", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        {
                            if (saveFile())
                            {
                                SetText("");
                                currentFileName = "Untitled";
                                this.Text = "Untitled - Encryption";
                                cleanForm = true;
                            }
                            break;
                        }
                    case DialogResult.No:
                        {
                            SetText("");
                            currentFileName = "Untitled";
                            this.Text = "Untitled - Encryption";
                            cleanForm = true;
                            break;
                        }
                    case DialogResult.Cancel:
                        { break; }
                    default:
                        { break; }
                }
            }
            else
            {
                SetText("");
                currentFileName = "Untitled";
                this.Text = "Untitled - Encryption";
            }
        }

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            if (!cleanForm)
            {
                string dialogText = "The text in the " + currentFileName + " file has changed." + Environment.NewLine + Environment.NewLine + "Do you want to save the changes?";
                switch (MessageBox.Show(dialogText, "Encryption", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        {
                            if (saveFile()) { SetText(""); }
                            else
                            { return; }
                            break;
                        }
                    case DialogResult.No:
                        {
                            SetText("");
                            break;
                        }
                    case DialogResult.Cancel:
                        { return; }
                    default:
                        { break; }
                }
            }
            try
            {
                string fileString = openFile("Open", "Text Document( *.txt )|*.txt|All Files|*.*");
                if (fileString != null)
                {
                    SetText(fileString);
                    cleanForm = true;
                }
            }
            catch (Exception Ex) { MessageBox.Show("ERROR: \n" + Ex.Message); }
        }

        private void saveAsFileMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFile())
            {
                cleanForm = true;
                this.Text = GetFileName(currentFileName) + " - Encryption";
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFileName.Equals("Untitled"))
            {
                if (saveFile()) { cleanForm = true; }
            }
            else
            {
                try
                {
                    StreamWriter streamWriter = new StreamWriter(currentFileName);
                    streamWriter.Write(inputTextBox.Text);
                    streamWriter.Flush();
                    streamWriter.Close();
                    cleanForm = true;
                }
                catch (Exception Ex)
                { MessageBox.Show("ERROR: \n" + Ex.Message); }
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            //Encryption.frmlogin1 log = new frmlogin1 ();
            //log.MdiParent = this;
            //this.Hide();
            //log.Show();//if (!cleanForm)
            //{
            //    string dialogText = "The text in the " + currentFileName + " file has changed." + Environment.NewLine + Environment.NewLine + "Do you want to save the changes?";
            //    switch (MessageBox.Show(dialogText, "Encryption", MessageBoxButtons.YesNoCancel))
            //    {
            //        case DialogResult.Yes:
            //            {
            //                if (saveFile()) { Dispose(true); }
            //                break;
            //            }
            //        case DialogResult.No:
            //            {
            //                Dispose(true);
            //                break;
            //            }
            //        case DialogResult.Cancel:
            //            { break; }
            //        default:
            //            { break; }
            //    }
            //}
            //else
            //{ Dispose(true); }
            //Application.Close();
            //Encriptions.Login  = new Login();
            //this.MdiParent = this;
            //this.Hide();
            //log.Show();
        }

        private void undoMenuItem_Click(object sender, EventArgs e)
        { inputTextBox.Undo(); }

        private void cutMenuItem_Click(object sender, EventArgs e)
        {
            try { inputTextBox.Cut(); }
            catch (Exception Ex)
            { Console.WriteLine(Ex.Message); }
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            try { inputTextBox.Copy(); }
            catch (Exception Ex) { Console.WriteLine(Ex.Message); }
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            try { inputTextBox.Paste(); }
            catch (Exception Ex) { Console.WriteLine(Ex.Message); }
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            if ((inputTextBox.Text.Length != 0) & (inputTextBox.SelectionStart != inputTextBox.Text.Length))
            {
                if (!inputTextBox.SelectedText.Equals(""))
                {
                    try
                    {
                        int cursorPosition = inputTextBox.SelectionStart;
                        inputTextBox.Text = inputTextBox.Text.Remove(cursorPosition, inputTextBox.SelectedText.Length);
                        inputTextBox.SelectionStart = cursorPosition;
                    }
                    catch (Exception Ex) { Console.WriteLine(Ex.Message); }
                }
                else
                {
                    try
                    {
                        int cursorPosition = inputTextBox.SelectionStart;
                        inputTextBox.Text = inputTextBox.Text.Remove(cursorPosition, 1);
                        inputTextBox.SelectionStart = cursorPosition;
                    }
                    catch (Exception Ex) { Console.WriteLine(Ex.Message); }
                }
            }
        }

        private void selectAllmenuItem_Click(object sender, EventArgs e)
        {
            try { inputTextBox.SelectAll(); }
            catch (Exception Ex) { Console.WriteLine(Ex.Message); }
        }

        private void wordWrapMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapMenuItem.Checked == true)
            {
                wordWrapMenuItem.Checked = false;
                inputTextBox.WordWrap = false;
            }
            else
            {
                wordWrapMenuItem.Checked = true;
                inputTextBox.WordWrap = true;
            }
            SaveSettings("WRAPPING");
        }

        private void fontMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                inputTextBox.Font = fontDialog.Font;
                SaveSettings("FONT");
            }
        }

        private void decryptMenuItem_Click(object sender, EventArgs e)
        {
            if (inputTextBox.Text.Length != 0)
            {
                openFileDialog.FileName = "";
                openFileDialog.Title = "Open Private Key File";
                openFileDialog.Filter = "Private Key Document( *.kez )|*.kez";
                string fileString = null;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(openFileDialog.FileName))
                    {
                        StreamReader streamReader = new StreamReader(openFileDialog.FileName, true);
                        fileString = streamReader.ReadToEnd();
                        streamReader.Close();
                        if (fileString.Length >= inputTextBox.MaxLength)
                        { MessageBox.Show("ERROR: \nThe file you are trying to open is too big for the text editor to display properly.\nPlease open a smaller document!\nOperation Aborted!"); }
                    }
                }
                if (File.Exists(openFileDialog.FileName))
                {
                    string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
                    fileString = fileString.Replace(bitStrengthString, "");
                    int bitStrength = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));
                    Point point = new Point((inputTextBox.Size.Width / 2) - (panel.Size.Width / 2), (inputTextBox.Size.Height / 2) - (panel.Size.Height / 2));
                    panel.Location = point;
                    panel.Visible = true;
                    this.Refresh();
                    fileMenuItem.Enabled = false;
                    editMenuItem.Enabled = false;
                    formatMenuItem.Enabled = false;
                    encryptionMenuItem.Enabled = false;
                   // helpMenuItem.Enabled = false;
                    string tempStorage = inputTextBox.Text;
                    if (fileString != null)
                    {
                        FinishedProcessDelegate finishedProcessDelegate = new FinishedProcessDelegate(FinishedProcess);
                        UpdateTextDelegate updateTextDelegate = new UpdateTextDelegate(UpdateText);
                        try
                        {
                            EncryptionThread decryptionThread = new EncryptionThread();
                            Thread decryptThread = new Thread(decryptionThread.Decrypt);
                            decryptThread.IsBackground = true;
                            decryptThread.Start(new Object[] { this, finishedProcessDelegate, updateTextDelegate, inputTextBox.Text, bitStrength, fileString });
                        }
                        catch (CryptographicException CEx)
                        { MessageBox.Show("ERROR: \nOne of the following has occured.\nThe cryptographic service provider cannot be acquired.\nThe length of the text being encrypted is greater than the maximum allowed length.\nThe OAEP padding is not supported on this computer.\n" + "Exact error: " + CEx.Message); }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("ERROR:\n" + Ex.Message);
                            SetText(tempStorage);
                        }
                    }
                }
            }
            else
            { MessageBox.Show("ERROR: You Can Not Decrypt A NULL Value!!!"); }
        }

        private void encryptMenuItem_Click(object sender, EventArgs e)
        {
            if (inputTextBox.Text.Length != 0)
            {
                openFileDialog.FileName = "";
                openFileDialog.Title = "Open Public Key File";
                openFileDialog.Filter = "Public Key Document( *.pke )|*.pke";
                string fileString = null;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(openFileDialog.FileName))
                    {
                        StreamReader streamReader = new StreamReader(openFileDialog.FileName, true);
                        fileString = streamReader.ReadToEnd();
                        streamReader.Close();
                        if (fileString.Length >= inputTextBox.MaxLength)
                        { MessageBox.Show("ERROR: \nThe file you are trying to open is too big for the text editor to display properly.\nPlease open a smaller document!\nOperation Aborted!"); }
                    }
                }
                if (fileString != null)
                {
                    FinishedProcessDelegate finishedProcessDelegate = new FinishedProcessDelegate(FinishedProcess);
                    UpdateTextDelegate updateTextDelegate = new UpdateTextDelegate(UpdateText);
                    string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
                    fileString = fileString.Replace(bitStrengthString, "");
                    int bitStrength = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));
                    Point point = new Point((inputTextBox.Size.Width / 2) - (panel.Size.Width / 2), (inputTextBox.Size.Height / 2) - (panel.Size.Height / 2));
                    panel.Location = point;
                    panel.Visible = true;
                    this.Refresh();
                    fileMenuItem.Enabled = false;
                    editMenuItem.Enabled = false;
                    formatMenuItem.Enabled = false;
                    encryptionMenuItem.Enabled = false;
                    //helpMenuItem.Enabled = false;
                    if (fileString != null)
                    {
                        try
                        {
                            EncryptionThread encryptionThread = new EncryptionThread();
                            Thread encryptThread = new Thread(encryptionThread.Encrypt);
                            encryptThread.IsBackground = true;
                            encryptThread.Start(new Object[] { this, finishedProcessDelegate, updateTextDelegate, inputTextBox.Text, bitStrength, fileString });
                        }
                        catch (CryptographicException CEx)
                        { MessageBox.Show("ERROR: \nOne of the following has occured.\nThe cryptographic service provider cannot be acquired.\nThe length of the text being encrypted is greater than the maximum allowed length.\nThe OAEP padding is not supported on this computer.\n" + "Exact error: " + CEx.Message); }
                        catch (Exception Ex)
                        { MessageBox.Show("ERROR: \n" + Ex.Message); }
                    }
                }
            }
            else
            { MessageBox.Show("ERROR: You Can Not Encrypt A NULL Value!!!"); }
        }

      

        private void logout_Click(object sender, EventArgs e)
        {
            //frmlogin1 log = new frmlogin1();
            //log.MdiParent = this;
            //this.Hide();
            this.Close();
        }

        private void encryptionMenuItem_Click(object sender, EventArgs e)
        {

        }

        private MenuItem menuItem1;
        private MenuItem menuItem2;

        private void menuItem2_Click(object sender, EventArgs e)
        {
            main frm = new main();
            frm.Tag = this;
            Hide();
            frm.Show(this);
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            frmDecode frm = new frmDecode();
            frm.Tag = this;
            Hide();
            frm.Show(this);
       
        }

         

        
        }
    }
