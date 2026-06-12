namespace lav30
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblHost = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.groupConn = new System.Windows.Forms.GroupBox();
            this.chkPassive = new System.Windows.Forms.CheckBox();
            this.groupFile = new System.Windows.Forms.GroupBox();
            this.groupConn.SuspendLayout();
            this.groupFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(12, 30);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(152, 20);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Хост (FTP адреса):";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(12, 110);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(101, 20);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "Користувач:";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(12, 150);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(71, 20);
            this.lblPass.TabIndex = 6;
            this.lblPass.Text = "Пароль:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(12, 70);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(52, 20);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Порт:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(160, 27);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(260, 26);
            this.txtHost.TabIndex = 1;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(160, 107);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(260, 26);
            this.txtUser.TabIndex = 5;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(160, 147);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(260, 26);
            this.txtPass.TabIndex = 7;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(160, 67);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(80, 26);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "21";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightGreen;
            this.btnSave.Location = new System.Drawing.Point(12, 345);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 36);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(160, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 36);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(350, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 30);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Огляд...";
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 30);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(130, 20);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "Шлях до файлу:";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(130, 27);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(210, 26);
            this.txtFile.TabIndex = 1;
            // 
            // groupConn
            // 
            this.groupConn.Controls.Add(this.lblHost);
            this.groupConn.Controls.Add(this.txtHost);
            this.groupConn.Controls.Add(this.lblPort);
            this.groupConn.Controls.Add(this.txtPort);
            this.groupConn.Controls.Add(this.lblUser);
            this.groupConn.Controls.Add(this.txtUser);
            this.groupConn.Controls.Add(this.lblPass);
            this.groupConn.Controls.Add(this.txtPass);
            this.groupConn.Controls.Add(this.chkPassive);
            this.groupConn.Location = new System.Drawing.Point(12, 12);
            this.groupConn.Name = "groupConn";
            this.groupConn.Size = new System.Drawing.Size(440, 220);
            this.groupConn.TabIndex = 0;
            this.groupConn.TabStop = false;
            this.groupConn.Text = "Параметри підключення";
            // 
            // chkPassive
            // 
            this.chkPassive.AutoSize = true;
            this.chkPassive.Checked = true;
            this.chkPassive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPassive.Location = new System.Drawing.Point(12, 185);
            this.chkPassive.Name = "chkPassive";
            this.chkPassive.Size = new System.Drawing.Size(274, 24);
            this.chkPassive.TabIndex = 8;
            this.chkPassive.Text = "Пасивний режим (Passive mode)";
            // 
            // groupFile
            // 
            this.groupFile.Controls.Add(this.lblFile);
            this.groupFile.Controls.Add(this.txtFile);
            this.groupFile.Controls.Add(this.btnBrowse);
            this.groupFile.Location = new System.Drawing.Point(12, 245);
            this.groupFile.Name = "groupFile";
            this.groupFile.Size = new System.Drawing.Size(440, 80);
            this.groupFile.TabIndex = 1;
            this.groupFile.TabStop = false;
            this.groupFile.Text = "Файл налаштувань";
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(468, 400);
            this.Controls.Add(this.groupConn);
            this.Controls.Add(this.groupFile);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Налаштування FTP клієнта";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupConn.ResumeLayout(false);
            this.groupConn.PerformLayout();
            this.groupFile.ResumeLayout(false);
            this.groupFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label   lblHost, lblUser, lblPass, lblPort, lblFile;
        private System.Windows.Forms.TextBox txtHost, txtUser, txtPass, txtPort, txtFile;
        private System.Windows.Forms.Button  btnSave, btnCancel, btnBrowse;
        private System.Windows.Forms.GroupBox groupConn, groupFile;
        private System.Windows.Forms.CheckBox chkPassive;
    }
}
