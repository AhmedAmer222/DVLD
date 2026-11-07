namespace DVLD.Applications.Local_Driving_Licenses_Applications
{
    partial class ShowLocalDrivingLicenseApplicationInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labTitle = new System.Windows.Forms.Label();
            this.localDrivingLicenseApplicationInfo2 = new DVLD.Applications.Local_Driving_Licenses_Applications.LocalDrivingLicenseApplicationInfo();
            this.localDrivingLicenseApplicationInfo1 = new DVLD.Applications.Local_Driving_Licenses_Applications.LocalDrivingLicenseApplicationInfo();
            this.SuspendLayout();
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labTitle.Location = new System.Drawing.Point(224, 36);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(503, 31);
            this.labTitle.TabIndex = 13;
            this.labTitle.Text = "Local Driving License Application Info";
            // 
            // localDrivingLicenseApplicationInfo2
            // 
            this.localDrivingLicenseApplicationInfo2.Location = new System.Drawing.Point(25, 94);
            this.localDrivingLicenseApplicationInfo2.Name = "localDrivingLicenseApplicationInfo2";
            this.localDrivingLicenseApplicationInfo2.Size = new System.Drawing.Size(908, 418);
            this.localDrivingLicenseApplicationInfo2.TabIndex = 14;
            // 
            // localDrivingLicenseApplicationInfo1
            // 
            this.localDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(356, 147);
            this.localDrivingLicenseApplicationInfo1.Name = "localDrivingLicenseApplicationInfo1";
            this.localDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(14, 8);
            this.localDrivingLicenseApplicationInfo1.TabIndex = 0;
            // 
            // ShowLocalDrivingLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 513);
            this.Controls.Add(this.localDrivingLicenseApplicationInfo2);
            this.Controls.Add(this.labTitle);
            this.Controls.Add(this.localDrivingLicenseApplicationInfo1);
            this.Name = "ShowLocalDrivingLicenseApplicationInfo";
            this.Text = "ShowLocalDrivingLicenseApplicationInfo";
            this.Load += new System.EventHandler(this.ShowLocalDrivingLicenseApplicationInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LocalDrivingLicenseApplicationInfo localDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Label labTitle;
        private LocalDrivingLicenseApplicationInfo localDrivingLicenseApplicationInfo2;
    }
}