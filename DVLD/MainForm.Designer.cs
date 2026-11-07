namespace DVLD
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drivingLicensesServicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.localLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internatonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reNewDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceLostOrDamagedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseDetainedDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localDrivingLiceseApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalDrivingLicenseApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detainedLicenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detainLicensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testsTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managePeopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManageUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.driversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AccountSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.currentUserInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationsToolStripMenuItem,
            this.managePeopleToolStripMenuItem,
            this.ManageUsers,
            this.driversToolStripMenuItem,
            this.AccountSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1274, 86);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // applicationsToolStripMenuItem
            // 
            this.applicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drivingLicensesServicToolStripMenuItem,
            this.manageApplicationsToolStripMenuItem,
            this.detainedLicenesToolStripMenuItem,
            this.applicationTypesToolStripMenuItem,
            this.testsTypesToolStripMenuItem});
            this.applicationsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applicationsToolStripMenuItem.Image = global::DVLD.Properties.Resources.Manage_Applications_64;
            this.applicationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.applicationsToolStripMenuItem.Name = "applicationsToolStripMenuItem";
            this.applicationsToolStripMenuItem.Size = new System.Drawing.Size(172, 82);
            this.applicationsToolStripMenuItem.Text = "Applications";
            // 
            // drivingLicensesServicToolStripMenuItem
            // 
            this.drivingLicensesServicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.reNewDrivingLicenseToolStripMenuItem,
            this.replaceLostOrDamagedLicenseToolStripMenuItem,
            this.releaseDetainedDrivingLicenseToolStripMenuItem});
            this.drivingLicensesServicToolStripMenuItem.Image = global::DVLD.Properties.Resources.Driver_License_48;
            this.drivingLicensesServicToolStripMenuItem.Name = "drivingLicensesServicToolStripMenuItem";
            this.drivingLicensesServicToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.drivingLicensesServicToolStripMenuItem.Text = "Driving Licenses Services";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localLicenseToolStripMenuItem,
            this.internatonalToolStripMenuItem});
            this.toolStripMenuItem1.Image = global::DVLD.Properties.Resources.New_Driving_License_32;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(313, 24);
            this.toolStripMenuItem1.Text = "New Driving License";
            // 
            // localLicenseToolStripMenuItem
            // 
            this.localLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.Local_32;
            this.localLicenseToolStripMenuItem.Name = "localLicenseToolStripMenuItem";
            this.localLicenseToolStripMenuItem.Size = new System.Drawing.Size(224, 24);
            this.localLicenseToolStripMenuItem.Text = "Local License";
            this.localLicenseToolStripMenuItem.Click += new System.EventHandler(this.localLicenseToolStripMenuItem_Click);
            // 
            // internatonalToolStripMenuItem
            // 
            this.internatonalToolStripMenuItem.Image = global::DVLD.Properties.Resources.International_32;
            this.internatonalToolStripMenuItem.Name = "internatonalToolStripMenuItem";
            this.internatonalToolStripMenuItem.Size = new System.Drawing.Size(224, 24);
            this.internatonalToolStripMenuItem.Text = "International License";
            this.internatonalToolStripMenuItem.Click += new System.EventHandler(this.internatonalToolStripMenuItem_Click);
            // 
            // reNewDrivingLicenseToolStripMenuItem
            // 
            this.reNewDrivingLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.Renew_Driving_License_32;
            this.reNewDrivingLicenseToolStripMenuItem.Name = "reNewDrivingLicenseToolStripMenuItem";
            this.reNewDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(313, 24);
            this.reNewDrivingLicenseToolStripMenuItem.Text = "ReNew Driving License";
            this.reNewDrivingLicenseToolStripMenuItem.Click += new System.EventHandler(this.reNewDrivingLicenseToolStripMenuItem_Click);
            // 
            // replaceLostOrDamagedLicenseToolStripMenuItem
            // 
            this.replaceLostOrDamagedLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.Lost_Driving_License_32;
            this.replaceLostOrDamagedLicenseToolStripMenuItem.Name = "replaceLostOrDamagedLicenseToolStripMenuItem";
            this.replaceLostOrDamagedLicenseToolStripMenuItem.Size = new System.Drawing.Size(313, 24);
            this.replaceLostOrDamagedLicenseToolStripMenuItem.Text = "Replace Lost Or Damaged License";
            this.replaceLostOrDamagedLicenseToolStripMenuItem.Click += new System.EventHandler(this.replaceLostOrDamagedLicenseToolStripMenuItem_Click);
            // 
            // releaseDetainedDrivingLicenseToolStripMenuItem
            // 
            this.releaseDetainedDrivingLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.Detained_Driving_License_32;
            this.releaseDetainedDrivingLicenseToolStripMenuItem.Name = "releaseDetainedDrivingLicenseToolStripMenuItem";
            this.releaseDetainedDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(313, 24);
            this.releaseDetainedDrivingLicenseToolStripMenuItem.Text = "Release Detained Driving License";
            this.releaseDetainedDrivingLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedDrivingLicenseToolStripMenuItem_Click);
            // 
            // manageApplicationsToolStripMenuItem
            // 
            this.manageApplicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localDrivingLiceseApplicationsToolStripMenuItem,
            this.internationalDrivingLicenseApplicationsToolStripMenuItem});
            this.manageApplicationsToolStripMenuItem.Image = global::DVLD.Properties.Resources.Manage_Applications_64;
            this.manageApplicationsToolStripMenuItem.Name = "manageApplicationsToolStripMenuItem";
            this.manageApplicationsToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.manageApplicationsToolStripMenuItem.Text = "Manage Applications";
            // 
            // localDrivingLiceseApplicationsToolStripMenuItem
            // 
            this.localDrivingLiceseApplicationsToolStripMenuItem.Image = global::DVLD.Properties.Resources.LocalDriving_License;
            this.localDrivingLiceseApplicationsToolStripMenuItem.Name = "localDrivingLiceseApplicationsToolStripMenuItem";
            this.localDrivingLiceseApplicationsToolStripMenuItem.Size = new System.Drawing.Size(370, 24);
            this.localDrivingLiceseApplicationsToolStripMenuItem.Text = "Local Driving Licese Applications";
            this.localDrivingLiceseApplicationsToolStripMenuItem.Click += new System.EventHandler(this.localDrivingLiceseApplicationsToolStripMenuItem_Click);
            // 
            // internationalDrivingLicenseApplicationsToolStripMenuItem
            // 
            this.internationalDrivingLicenseApplicationsToolStripMenuItem.Image = global::DVLD.Properties.Resources.International_32;
            this.internationalDrivingLicenseApplicationsToolStripMenuItem.Name = "internationalDrivingLicenseApplicationsToolStripMenuItem";
            this.internationalDrivingLicenseApplicationsToolStripMenuItem.Size = new System.Drawing.Size(370, 24);
            this.internationalDrivingLicenseApplicationsToolStripMenuItem.Text = "International Driving License Applications";
            this.internationalDrivingLicenseApplicationsToolStripMenuItem.Click += new System.EventHandler(this.internationalDrivingLicenseApplicationsToolStripMenuItem_Click);
            // 
            // detainedLicenesToolStripMenuItem
            // 
            this.detainedLicenesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageDetainedLicenseToolStripMenuItem,
            this.detainLicensToolStripMenuItem,
            this.releaseLicenseToolStripMenuItem});
            this.detainedLicenesToolStripMenuItem.Image = global::DVLD.Properties.Resources.Detain_32;
            this.detainedLicenesToolStripMenuItem.Name = "detainedLicenesToolStripMenuItem";
            this.detainedLicenesToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.detainedLicenesToolStripMenuItem.Text = "Detained Licenes";
            // 
            // manageDetainedLicenseToolStripMenuItem
            // 
            this.manageDetainedLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.Detain_64;
            this.manageDetainedLicenseToolStripMenuItem.Name = "manageDetainedLicenseToolStripMenuItem";
            this.manageDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.manageDetainedLicenseToolStripMenuItem.Text = "Manage Detained License";
            this.manageDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.manageDetainedLicenseToolStripMenuItem_Click);
            // 
            // detainLicensToolStripMenuItem
            // 
            this.detainLicensToolStripMenuItem.Image = global::DVLD.Properties.Resources.Detain_64;
            this.detainLicensToolStripMenuItem.Name = "detainLicensToolStripMenuItem";
            this.detainLicensToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.detainLicensToolStripMenuItem.Text = "Detain License";
            this.detainLicensToolStripMenuItem.Click += new System.EventHandler(this.detainLicensToolStripMenuItem_Click);
            // 
            // releaseLicenseToolStripMenuItem
            // 
            this.releaseLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.Release_Detained_License_64;
            this.releaseLicenseToolStripMenuItem.Name = "releaseLicenseToolStripMenuItem";
            this.releaseLicenseToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.releaseLicenseToolStripMenuItem.Text = "Release License";
            this.releaseLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseLicenseToolStripMenuItem_Click);
            // 
            // applicationTypesToolStripMenuItem
            // 
            this.applicationTypesToolStripMenuItem.Image = global::DVLD.Properties.Resources.Application_Types_64;
            this.applicationTypesToolStripMenuItem.Name = "applicationTypesToolStripMenuItem";
            this.applicationTypesToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.applicationTypesToolStripMenuItem.Text = "Application Types";
            this.applicationTypesToolStripMenuItem.Click += new System.EventHandler(this.applicationTypesToolStripMenuItem_Click);
            // 
            // testsTypesToolStripMenuItem
            // 
            this.testsTypesToolStripMenuItem.Image = global::DVLD.Properties.Resources.Test_Type_64;
            this.testsTypesToolStripMenuItem.Name = "testsTypesToolStripMenuItem";
            this.testsTypesToolStripMenuItem.Size = new System.Drawing.Size(252, 24);
            this.testsTypesToolStripMenuItem.Text = "Test Types";
            this.testsTypesToolStripMenuItem.Click += new System.EventHandler(this.testsTypesToolStripMenuItem_Click);
            // 
            // managePeopleToolStripMenuItem
            // 
            this.managePeopleToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managePeopleToolStripMenuItem.Image = global::DVLD.Properties.Resources.People_64;
            this.managePeopleToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.managePeopleToolStripMenuItem.Name = "managePeopleToolStripMenuItem";
            this.managePeopleToolStripMenuItem.Size = new System.Drawing.Size(131, 82);
            this.managePeopleToolStripMenuItem.Text = "People";
            this.managePeopleToolStripMenuItem.Click += new System.EventHandler(this.managePeopleToolStripMenuItem_Click);
            // 
            // ManageUsers
            // 
            this.ManageUsers.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManageUsers.Image = global::DVLD.Properties.Resources.Users_2_64;
            this.ManageUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ManageUsers.Name = "ManageUsers";
            this.ManageUsers.Size = new System.Drawing.Size(122, 82);
            this.ManageUsers.Text = "Users";
            this.ManageUsers.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // driversToolStripMenuItem
            // 
            this.driversToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.driversToolStripMenuItem.Image = global::DVLD.Properties.Resources.Drivers_64;
            this.driversToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.driversToolStripMenuItem.Name = "driversToolStripMenuItem";
            this.driversToolStripMenuItem.Size = new System.Drawing.Size(135, 82);
            this.driversToolStripMenuItem.Text = "Drivers";
            this.driversToolStripMenuItem.Click += new System.EventHandler(this.driversToolStripMenuItem_Click);
            // 
            // AccountSettings
            // 
            this.AccountSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentUserInfoToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.signOutToolStripMenuItem});
            this.AccountSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountSettings.Image = global::DVLD.Properties.Resources.account_settings_64;
            this.AccountSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AccountSettings.Name = "AccountSettings";
            this.AccountSettings.Size = new System.Drawing.Size(199, 82);
            this.AccountSettings.Text = "Account Settings";
            // 
            // currentUserInfoToolStripMenuItem
            // 
            this.currentUserInfoToolStripMenuItem.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.currentUserInfoToolStripMenuItem.Name = "currentUserInfoToolStripMenuItem";
            this.currentUserInfoToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.currentUserInfoToolStripMenuItem.Text = "Current User Info";
            this.currentUserInfoToolStripMenuItem.Click += new System.EventHandler(this.currentUserInfoToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::DVLD.Properties.Resources.Password_32;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Image = global::DVLD.Properties.Resources.sign_out_32__2;
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.signOutToolStripMenuItem.Text = "Sign Out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::DVLD.Properties.Resources.BackgroundImage;
            this.pictureBox1.Location = new System.Drawing.Point(0, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1274, 430);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 516);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "  DVLD - Driving License Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem managePeopleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ManageUsers;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem AccountSettings;
        private System.Windows.Forms.ToolStripMenuItem currentUserInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem driversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testsTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drivingLicensesServicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem localLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internatonalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDrivingLiceseApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalDrivingLicenseApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reNewDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceLostOrDamagedLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detainedLicenesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageDetainedLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detainLicensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedDrivingLicenseToolStripMenuItem;
    }
}

