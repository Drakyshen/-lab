using System;
using System.Windows.Forms;
using System.Drawing;

namespace _29
{
    public class SettingsForm : Form
    {
        // Публічні властивості для зчитування результату
        public string Host { get; private set; }
        public int LocalPort { get; private set; }
        public int RemotePort { get; private set; }
        public Font ChatFont { get; private set; }

        private TextBox txtHost;
        private NumericUpDown nudLocalPort;
        private NumericUpDown nudRemotePort;
        private Button btnFont;
        private Button btnOk;
        private Button btnCancel;
        private Font selectedFont;

        public SettingsForm(string host, int localPort, int remotePort, Font currentFont)
        {
            this.Text = "Налаштування чату";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            // Хост
            var lblHost = new Label { Text = "Адреса групи:", Location = new Point(20, 20), AutoSize = true };
            txtHost = new TextBox { Location = new Point(160, 17), Width = 200, Text = host };

            // Локальний порт
            var lblLocal = new Label { Text = "Локальний порт:", Location = new Point(20, 60), AutoSize = true };
            nudLocalPort = new NumericUpDown { Location = new Point(160, 57), Width = 100, Minimum = 1, Maximum = 65535, Value = localPort };

            // Віддалений порт
            var lblRemote = new Label { Text = "Віддалений порт:", Location = new Point(20, 100), AutoSize = true };
            nudRemotePort = new NumericUpDown { Location = new Point(160, 97), Width = 100, Minimum = 1, Maximum = 65535, Value = remotePort };

            // Шрифт
            var lblFont = new Label { Text = "Шрифт чату:", Location = new Point(20, 140), AutoSize = true };
            selectedFont = currentFont;
            btnFont = new Button { Text = currentFont.Name + " " + (int)currentFont.Size + "pt", Location = new Point(160, 137), Width = 200 };
            btnFont.Click += (s, e) =>
            {
                FontDialog fd = new FontDialog { Font = selectedFont };
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    selectedFont = fd.Font;
                    btnFont.Text = selectedFont.Name + " " + (int)selectedFont.Size + "pt";
                }
            };

            // Кнопки
            btnOk = new Button { Text = "OK", Location = new Point(100, 210), Width = 80, DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Скасувати", Location = new Point(200, 210), Width = 100, DialogResult = DialogResult.Cancel };

            btnOk.Click += (s, e) =>
            {
                Host = txtHost.Text;
                LocalPort = (int)nudLocalPort.Value;
                RemotePort = (int)nudRemotePort.Value;
                ChatFont = selectedFont;
            };

            this.Controls.AddRange(new Control[] {
                lblHost, txtHost,
                lblLocal, nudLocalPort,
                lblRemote, nudRemotePort,
                lblFont, btnFont,
                btnOk, btnCancel
            });

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}