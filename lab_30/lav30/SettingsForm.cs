using System;
using System.IO;
using System.Windows.Forms;

namespace lav30
{
    public partial class SettingsForm : Form
    {
        // Шлях до файлу налаштувань за замовчуванням
        public static string DefaultSettingsFile =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ftp_settings.txt");

        public SettingsForm()
        {
            InitializeComponent();
            txtFile.Text = DefaultSettingsFile;
            LoadFromFile(DefaultSettingsFile);
        }

        // ── Властивості для читання з Form1 ──────────────────────────
        public string FtpHost    { get { return txtHost.Text; } }
        public string FtpUser    { get { return txtUser.Text; } }
        public string FtpPass    { get { return txtPass.Text; } }
        public string FtpPort    { get { return txtPort.Text; } }
        public bool   FtpPassive { get { return chkPassive.Checked; } }

        // ── Зберегти у файл ───────────────────────────────────────────
        private void BtnSave_Click(object sender, EventArgs e)
        {
            string filePath = txtFile.Text.Trim();
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Вкажіть шлях до файлу налаштувань.",
                                "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveToFile(filePath);
                DefaultSettingsFile = filePath;
                MessageBox.Show("Налаштування збережено у файл:\n" + filePath,
                                "Збережено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження:\n" + ex.Message,
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Скасувати ─────────────────────────────────────────────────
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ── Вибрати файл ──────────────────────────────────────────────
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter      = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FileName    = "ftp_settings.txt",
                DefaultExt  = ".txt"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
                txtFile.Text = dlg.FileName;
        }

        // ── Записати налаштування у файл ──────────────────────────────
        public void SaveToFile(string path)
        {
            using (StreamWriter w = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                w.WriteLine("# FTP Client Settings");
                w.WriteLine("# Збережено: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                w.WriteLine("host="    + txtHost.Text);
                w.WriteLine("port="    + txtPort.Text);
                w.WriteLine("user="    + txtUser.Text);
                w.WriteLine("pass="    + txtPass.Text);
                w.WriteLine("passive=" + chkPassive.Checked.ToString());
            }
        }

        // ── Зчитати налаштування з файлу ──────────────────────────────
        public void LoadFromFile(string path)
        {
            if (!File.Exists(path)) return;
            try
            {
                foreach (string line in File.ReadAllLines(path, System.Text.Encoding.UTF8))
                {
                    if (line.StartsWith("#") || !line.Contains("=")) continue;
                    string key   = line.Substring(0, line.IndexOf('=')).Trim();
                    string value = line.Substring(line.IndexOf('=') + 1).Trim();
                    switch (key)
                    {
                        case "host":    txtHost.Text      = value; break;
                        case "port":    txtPort.Text      = value; break;
                        case "user":    txtUser.Text      = value; break;
                        case "pass":    txtPass.Text      = value; break;
                        case "passive": chkPassive.Checked = value == "True"; break;
                    }
                }
            }
            catch { }
        }

        // ── Статичний метод: зчитати налаштування і повернути ─────────
        public static FtpSettings ReadSettings(string path)
        {
            var s = new FtpSettings();
            if (!File.Exists(path)) return s;
            try
            {
                foreach (string line in File.ReadAllLines(path, System.Text.Encoding.UTF8))
                {
                    if (line.StartsWith("#") || !line.Contains("=")) continue;
                    string key   = line.Substring(0, line.IndexOf('=')).Trim();
                    string value = line.Substring(line.IndexOf('=') + 1).Trim();
                    switch (key)
                    {
                        case "host":    s.Host    = value; break;
                        case "port":    s.Port    = value; break;
                        case "user":    s.User    = value; break;
                        case "pass":    s.Pass    = value; break;
                        case "passive": s.Passive = value == "True"; break;
                    }
                }
            }
            catch { }
            return s;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }

    // ── Структура налаштувань ─────────────────────────────────────────
    public class FtpSettings
    {
        public string Host    { get; set; } = "ftp://127.0.0.1/";
        public string Port    { get; set; } = "21";
        public string User    { get; set; } = "test";
        public string Pass    { get; set; } = "test";
        public bool   Passive { get; set; } = true;
    }
}
