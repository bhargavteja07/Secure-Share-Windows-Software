using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Encryption
{
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label disclaimerLabel;
		private System.Windows.Forms.GroupBox disclaimerGroupBox;
        private System.Windows.Forms.PictureBox iconPictureBox;
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{InitializeComponent();}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
				{components.Dispose();}
			}
			base.Dispose( disposing );
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.disclaimerGroupBox = new System.Windows.Forms.GroupBox();
            this.disclaimerLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.disclaimerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("iconPictureBox.Image")));
            this.iconPictureBox.Location = new System.Drawing.Point(8, 16);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(32, 34);
            this.iconPictureBox.TabIndex = 0;
            this.iconPictureBox.TabStop = false;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.descriptionLabel.Location = new System.Drawing.Point(56, 23);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(135, 22);
            this.descriptionLabel.TabIndex = 0;
            this.descriptionLabel.Text = "Encryption";
            // 
            // disclaimerGroupBox
            // 
            this.disclaimerGroupBox.Controls.Add(this.disclaimerLabel);
            this.disclaimerGroupBox.Location = new System.Drawing.Point(8, 59);
            this.disclaimerGroupBox.Name = "disclaimerGroupBox";
            this.disclaimerGroupBox.Size = new System.Drawing.Size(384, 112);
            this.disclaimerGroupBox.TabIndex = 1;
            this.disclaimerGroupBox.TabStop = false;
            this.disclaimerGroupBox.Text = "Disclaimer";
            // 
            // disclaimerLabel
            // 
            this.disclaimerLabel.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disclaimerLabel.ForeColor = System.Drawing.Color.Black;
            this.disclaimerLabel.Location = new System.Drawing.Point(6, 21);
            this.disclaimerLabel.Name = "disclaimerLabel";
            this.disclaimerLabel.Size = new System.Drawing.Size(420, 88);
            this.disclaimerLabel.TabIndex = 0;
            this.disclaimerLabel.Text = resources.GetString("disclaimerLabel.Text");
            // 
            // AboutForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(437, 216);
            this.Controls.Add(this.disclaimerGroupBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.iconPictureBox);
            this.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Encryption";
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.disclaimerGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	}
}