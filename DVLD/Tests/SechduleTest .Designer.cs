namespace DVLD
{
    partial class SechduleTest
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
            this.sechduleTestControl1 = new DVLD.SechduleTestControl();
            this.SuspendLayout();
            // 
            // sechduleTestControl1
            // 
            this.sechduleTestControl1.Location = new System.Drawing.Point(3, 2);
            this.sechduleTestControl1.Name = "sechduleTestControl1";
            this.sechduleTestControl1.RetakeTestEnable = false;
            this.sechduleTestControl1.Size = new System.Drawing.Size(553, 652);
            this.sechduleTestControl1.TabIndex = 0;
            this.sechduleTestControl1.TestType = DVLD_Buisness.clsTestType.enTestTypes.StreetTest;
            this.sechduleTestControl1.Load += new System.EventHandler(this.sechduleTestControl1_Load);
            // 
            // SechduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 666);
            this.Controls.Add(this.sechduleTestControl1);
            this.Name = "SechduleTest";
            this.Text = "SechduleTest";
            this.ResumeLayout(false);

        }

        #endregion

        private SechduleTestControl sechduleTestControl1;
    }
}