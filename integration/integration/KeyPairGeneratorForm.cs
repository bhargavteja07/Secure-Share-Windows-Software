using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Encryption
{
	public class KeyPairGeneratorForm: System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button generateKeysButton;
		private System.Windows.Forms.NumericUpDown numericUpDown;
		private System.Windows.Forms.PictureBox keyPictureBox;
        private PictureBox pictureBox5;
		private System.ComponentModel.Container components = null;

		public KeyPairGeneratorForm()
		{ InitializeComponent(); }
		
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{ 
				if( components != null )
				{ components.Dispose(); }
			}
			base.Dispose( disposing );
		}
		
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyPairGeneratorForm));
            this.generateKeysButton = new System.Windows.Forms.Button();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.keyPictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // generateKeysButton
            // 
            this.generateKeysButton.BackColor = System.Drawing.SystemColors.Control;
            this.generateKeysButton.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generateKeysButton.ForeColor = System.Drawing.Color.Black;
            this.generateKeysButton.Location = new System.Drawing.Point(183, 119);
            this.generateKeysButton.Name = "generateKeysButton";
            this.generateKeysButton.Size = new System.Drawing.Size(149, 28);
            this.generateKeysButton.TabIndex = 0;
            this.generateKeysButton.Text = "Generate Keys";
            this.generateKeysButton.UseVisualStyleBackColor = false;
            this.generateKeysButton.Click += new System.EventHandler(this.generateKeysButton_Click);
            // 
            // numericUpDown
            // 
            this.numericUpDown.BackColor = System.Drawing.Color.White;
            this.numericUpDown.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown.Location = new System.Drawing.Point(183, 74);
            this.numericUpDown.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            384,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.ReadOnly = true;
            this.numericUpDown.Size = new System.Drawing.Size(149, 26);
            this.numericUpDown.TabIndex = 0;
            this.numericUpDown.ThousandsSeparator = true;
            this.numericUpDown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericUpDown.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // keyPictureBox
            // 
            this.keyPictureBox.BackColor = System.Drawing.Color.Black;
            this.keyPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.keyPictureBox.Location = new System.Drawing.Point(60, 74);
            this.keyPictureBox.Name = "keyPictureBox";
            this.keyPictureBox.Size = new System.Drawing.Size(66, 75);
            this.keyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.keyPictureBox.TabIndex = 1;
            this.keyPictureBox.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = global::integration.Properties.Resources.Untitled1;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(103, 12);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(154, 25);
            this.pictureBox5.TabIndex = 17;
            this.pictureBox5.TabStop = false;
            // 
            // KeyPairGeneratorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(366, 205);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.keyPictureBox);
            this.Controls.Add(this.generateKeysButton);
            this.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeyPairGeneratorForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Secure Share - Generate Keys";
            this.Load += new System.EventHandler(this.KeyPairGeneratorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void generateKeysButton_Click( object sender, System.EventArgs e )
		{
			integration.main.SetBitStrength( Convert.ToInt32( numericUpDown.Value ) );
			this.DialogResult = DialogResult.OK;
			this.Dispose( true );
		}

		private void KeyPairGeneratorForm_Load( object sender, EventArgs e )
		{ integration.main.SetBitStrength( 1024 ); }		
	}
}