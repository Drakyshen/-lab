using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace lav30
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ── Модель елементу FTP ──────────────────────────────────────
        private class FtpItem
        {
            public bool IsDir { get; set; }
            public string Name { get; set; }
            public string Size { get; set; }
            public string Date { get; set; }
            public string FtpPath { get; set; }
        }

        // ── Допоміжні ────────────────────────────────────────────────
        private string Host { get { return textBox1.Text.TrimEnd('/') + "/"; } }

        private FtpWebRequest CreateRequest(string url, string method)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(url);
            req.Credentials = new NetworkCredential(textBox2.Text, textBox3.Text);
            req.UsePassive = true;
            req.UseBinary = true;
            req.Method = method;
            return req;
        }

        private void SetStatus(string text) { textBox4.Text = text; }

        // ── Парсинг рядка LIST (Unix формат) ─────────────────────────
        private FtpItem ParseLine(string line, string parentFtpPath)
        {
            if (string.IsNullOrWhiteSpace(line)) return null;
            try
            {
                string[] p = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (p.Length < 9) return null;
                bool isDir = line.StartsWith("d");
                string name = string.Join(" ", p, 8, p.Length - 8);
                if (name == "." || name == "..") return null;
                return new FtpItem
                {
                    IsDir = isDir,
                    Name = name,
                    Size = isDir ? "<DIR>" : p[4],
                    Date = p[5] + " " + p[6] + " " + p[7],
                    FtpPath = parentFtpPath.TrimEnd('/') + "/" + name
                };
            }
            catch { return null; }
        }

        // ── Отримати LIST рядки з FTP ─────────────────────────────────
        private List<string> GetListLines(string url, bool detailed)
        {
            var lines = new List<string>();
            FtpWebRequest req = CreateRequest(url,
                detailed ? WebRequestMethods.Ftp.ListDirectoryDetails
                         : WebRequestMethods.Ftp.ListDirectory);
            FtpWebResponse resp = (FtpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(resp.GetResponseStream());
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                    lines.Add(line);
            }
            reader.Close();
            resp.Close();
            return lines;
        }

        // ── Побудова дерева ───────────────────────────────────────────
        private void BuildTree(string ftpUrl, string ftpPath, TreeNodeCollection nodes)
        {
            nodes.Clear();
            List<string> lines;
            try { lines = GetListLines(ftpUrl, true); }
            catch (Exception ex) { SetStatus("Помилка отримання списку: " + ex.Message); return; }

            bool full = rbFull.Checked;

            foreach (string line in lines)
            {
                FtpItem item = ParseLine(line, ftpPath);
                if (item == null) continue;

                string nodeText = full
                    ? string.Format("{0,-8}  {1,-14}  {2}",
                        item.Size, item.Date, (item.IsDir ? "[" + item.Name + "]" : item.Name))
                    : item.Name;

                TreeNode node = new TreeNode(nodeText);
                node.Tag = item;

                if (item.IsDir)
                    node.Nodes.Add(new TreeNode("...") { Tag = "loading" });

                nodes.Add(node);
            }
        }

        // ── Розгортання папки ─────────────────────────────────────────
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count == 1 && node.Nodes[0].Tag is string t && t == "loading")
            {
                FtpItem item = node.Tag as FtpItem;
                if (item == null) return;
                node.Nodes.Clear();
                BuildTree(Host + item.FtpPath.TrimStart('/') + "/", item.FtpPath, node.Nodes);
            }
        }

        // ── Перемикач вигляду ─────────────────────────────────────────
        private void RbView_Changed(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count > 0)
                BuildTree(Host, "/", treeView1.Nodes);
        }

        // ── Form Load ─────────────────────────────────────────────────
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Хост:";
            label2.Text = "Користувач:";
            label3.Text = "Пароль:";
            label4.Text = "Статус:";
            label5.Text = "Файл:";
            label6.Text = "Каталог:";
            label7.Text = "Каталог:";
            label8.Text = "Файл:";
            label9.Text = "Каталог:";
            label10.Text = "Файл:";
            label11.Text = "Файл:";
            label12.Text = "Файл:";
            label13.Text = "Файл:";
            label14.Text = "Старе ім'я:";
            label15.Text = "Нове ім'я:";

            button1.Text = "Підключитись (LIST)";
            button2.Text = "Розмір файлу (SIZE)";
            button3.Text = "Видалити каталог (RMD)";
            button4.Text = "Створити каталог (MKD)";
            button5.Text = "Видалити файл (DELE)";
            button6.Text = "Завантажити на FTP (STOR)";
            button7.Text = "Завантажити з FTP (RETR)";
            button8.Text = "Дозавантажити (APPE)";
            button9.Text = "Дата модифікації (MDT)";
            button10.Text = "Список імен (NLIST)";
            button11.Text = "Перейменувати (RENAME)";
            button12.Text = "Завантажити унікально (STOU)";

            btnUploadMany.Text = "Завантажити групу файлів";
            btnUploadDir.Text = "Завантажити каталог";

            rbFull.Text = "Повний вигляд";
            rbShort.Text = "Скорочений вигляд";
            rbFull.Checked = true;

            groupBox1.Text = "Операції з FTP";

            textBox1.Text = "ftp://127.0.0.1/";
            textBox2.Text = "test";
            textBox3.Text = "test";

            // Події
            button1.Click += BtnConnect_Click;
            button2.Click += BtnSize_Click;
            button3.Click += BtnRmd_Click;
            button4.Click += BtnMkd_Click;
            button5.Click += BtnDele_Click;
            button6.Click += BtnStor_Click;
            button7.Click += BtnRetr_Click;
            button8.Click += BtnAppe_Click;
            button9.Click += BtnMdt_Click;
            button10.Click += BtnNlist_Click;
            button11.Click += BtnRename_Click;
            button12.Click += BtnStou_Click;

            btnUploadMany.Click += BtnUploadMany_Click;
            btnUploadDir.Click += BtnUploadDir_Click;

            rbFull.CheckedChanged += RbView_Changed;
            rbShort.CheckedChanged += RbView_Changed;

            treeView1.BeforeExpand += treeView1_BeforeExpand;

            // Кнопка налаштувань
            btnSettings = new System.Windows.Forms.Button();
            btnSettings.Text = "⚙ Налаштування";
            btnSettings.Size = new System.Drawing.Size(160, 36);
            btnSettings.Location = new System.Drawing.Point(880, 35);
            btnSettings.BackColor = System.Drawing.Color.LightYellow;
            btnSettings.Click += BtnSettings_Click;
            this.Controls.Add(btnSettings);

            // Завантажити налаштування з файлу при старті
            ApplySettings(SettingsForm.ReadSettings(SettingsForm.DefaultSettingsFile));
        }

        // ── LIST ──────────────────────────────────────────────────────
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                BuildTree(Host, "/", treeView1.Nodes);
                SetStatus("LIST: підключено. Кореневий каталог завантажено.");
            }
            catch (Exception ex) { SetStatus("Помилка LIST: " + ex.Message); }
        }

        // ── SIZE ──────────────────────────────────────────────────────
        private void BtnSize_Click(object sender, EventArgs e)
        {
            try
            {
                FtpWebRequest req = CreateRequest(Host + textBox5.Text, WebRequestMethods.Ftp.GetFileSize);
                FtpWebResponse resp = (FtpWebResponse)req.GetResponse();
                SetStatus("SIZE «" + textBox5.Text + "»: " + resp.ContentLength + " байт");
                resp.Close();
            }
            catch (Exception ex) { SetStatus("Помилка SIZE: " + ex.Message); }
        }

        // ── MKD ───────────────────────────────────────────────────────
        private void BtnMkd_Click(object sender, EventArgs e)
        {
            try
            {
                FtpWebRequest req = CreateRequest(Host + textBox6.Text, WebRequestMethods.Ftp.MakeDirectory);
                ((FtpWebResponse)req.GetResponse()).Close();
                SetStatus("MKD: каталог «" + textBox6.Text + "» створено.");
                BtnConnect_Click(null, null);
            }
            catch (Exception ex) { SetStatus("Помилка MKD: " + ex.Message); }
        }

        // ── RMD ───────────────────────────────────────────────────────
        private void BtnRmd_Click(object sender, EventArgs e)
        {
            try
            {
                FtpWebRequest req = CreateRequest(Host + textBox7.Text, WebRequestMethods.Ftp.RemoveDirectory);
                ((FtpWebResponse)req.GetResponse()).Close();
                SetStatus("RMD: каталог «" + textBox7.Text + "» видалено.");
                BtnConnect_Click(null, null);
            }
            catch (Exception ex) { SetStatus("Помилка RMD: " + ex.Message); }
        }

        // ── DELE ──────────────────────────────────────────────────────
        private void BtnDele_Click(object sender, EventArgs e)
        {
            try
            {
                FtpWebRequest req = CreateRequest(Host + textBox9.Text, WebRequestMethods.Ftp.DeleteFile);
                ((FtpWebResponse)req.GetResponse()).Close();
                SetStatus("DELE: файл «" + textBox9.Text + "» видалено.");
                BtnConnect_Click(null, null);
            }
            catch (Exception ex) { SetStatus("Помилка DELE: " + ex.Message); }
        }

        // ── STOR ──────────────────────────────────────────────────────
        private void BtnStor_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "Всі файли|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    UploadFile(dlg.FileName, textBox10.Text);
                    SetStatus("STOR: файл «" + dlg.SafeFileName + "» завантажено.");
                    BtnConnect_Click(null, null);
                }
                catch (Exception ex) { SetStatus("Помилка STOR: " + ex.Message); }
            }
        }

        // ── RETR ──────────────────────────────────────────────────────
        private void BtnRetr_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog { FileName = textBox8.Text };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FtpWebRequest req = CreateRequest(Host + textBox8.Text, WebRequestMethods.Ftp.DownloadFile);
                    FtpWebResponse resp = (FtpWebResponse)req.GetResponse();
                    using (FileStream fs = File.Create(dlg.FileName))
                        resp.GetResponseStream().CopyTo(fs);
                    SetStatus("RETR: файл «" + textBox8.Text + "» збережено.");
                    resp.Close();
                }
                catch (Exception ex) { SetStatus("Помилка RETR: " + ex.Message); }
            }
        }

        // ── MDT ───────────────────────────────────────────────────────
        private void BtnMdt_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = textBox11.Text.Trim();
                FtpWebRequest req = CreateRequest(Host, WebRequestMethods.Ftp.ListDirectoryDetails);
                FtpWebResponse resp = (FtpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                string found = "";
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.ToLower().Contains(fileName.ToLower()))
                        found = line;
                }
                reader.Close();
                resp.Close();

                if (found != "")
                {
                    FtpItem item = ParseLine(found, "/");
                    SetStatus(item != null
                        ? "MDT «" + fileName + "»: " + item.Date
                        : "MDT: " + found);
                }
                else
                    SetStatus("MDT: файл «" + fileName + "» не знайдено.");
            }
            catch (Exception ex) { SetStatus("Помилка MDT: " + ex.Message); }
        }

        // ── APPE ──────────────────────────────────────────────────────
        private void BtnAppe_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "Всі файли|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string url = Host + textBox12.Text.TrimEnd('/') + "/" + dlg.SafeFileName;
                    FtpWebRequest req = CreateRequest(url, WebRequestMethods.Ftp.AppendFile);
                    byte[] data = File.ReadAllBytes(dlg.FileName);
                    Stream s = req.GetRequestStream();
                    s.Write(data, 0, data.Length);
                    s.Close();
                    SetStatus("APPE: файл «" + dlg.SafeFileName + "» дозавантажено.");
                    BtnConnect_Click(null, null);
                }
                catch (Exception ex) { SetStatus("Помилка APPE: " + ex.Message); }
            }
        }

        // ── NLIST ─────────────────────────────────────────────────────
        private void BtnNlist_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.IsNullOrWhiteSpace(textBox13.Text)
                    ? Host : Host + textBox13.Text;
                List<string> lines = GetListLines(path, false);
                treeView1.Nodes.Clear();
                foreach (string line in lines)
                    treeView1.Nodes.Add(new TreeNode(line));
                SetStatus("NLIST: отримано " + lines.Count + " імен.");
            }
            catch (Exception ex) { SetStatus("Помилка NLIST: " + ex.Message); }
        }

        // ── RENAME ────────────────────────────────────────────────────
        private void BtnRename_Click(object sender, EventArgs e)
        {
            try
            {
                FtpWebRequest req = CreateRequest(Host + textBox14.Text, WebRequestMethods.Ftp.Rename);
                req.RenameTo = textBox15.Text;
                ((FtpWebResponse)req.GetResponse()).Close();
                SetStatus("RENAME: «" + textBox14.Text + "» → «" + textBox15.Text + "» виконано.");
                BtnConnect_Click(null, null);
            }
            catch (Exception ex) { SetStatus("Помилка RENAME: " + ex.Message); }
        }

        // ── STOU ──────────────────────────────────────────────────────
        private void BtnStou_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "Всі файли|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string uniqueName = Path.GetFileNameWithoutExtension(dlg.SafeFileName)
                                      + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss")
                                      + Path.GetExtension(dlg.SafeFileName);
                    FtpWebRequest req = CreateRequest(Host + uniqueName, WebRequestMethods.Ftp.UploadFile);
                    byte[] data = File.ReadAllBytes(dlg.FileName);
                    Stream s = req.GetRequestStream();
                    s.Write(data, 0, data.Length);
                    s.Close();
                    SetStatus("STOU: файл збережено як «" + uniqueName + "».");
                    BtnConnect_Click(null, null);
                }
                catch (Exception ex) { SetStatus("Помилка STOU: " + ex.Message); }
            }
        }

        // ── Завантажити групу файлів ──────────────────────────────────
        private void BtnUploadMany_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Всі файли|*.*",
                Multiselect = true,
                Title = "Виберіть кілька файлів (Ctrl+клік)"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int ok = 0, fail = 0;
                foreach (string filePath in dlg.FileNames)
                {
                    try { UploadFile(filePath, "/"); ok++; }
                    catch { fail++; }
                }
                SetStatus("Група файлів: " + ok + " завантажено, " + fail + " помилок.");
                BtnConnect_Click(null, null);
            }
        }

        // ── Завантажити каталог ───────────────────────────────────────
        private void BtnUploadDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog
            {
                Description = "Виберіть папку для завантаження на FTP"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string localDir = dlg.SelectedPath;
                    string dirName = Path.GetFileName(localDir);

                    // Створити папку на сервері
                    try
                    {
                        FtpWebRequest mkd = CreateRequest(Host + dirName, WebRequestMethods.Ftp.MakeDirectory);
                        ((FtpWebResponse)mkd.GetResponse()).Close();
                    }
                    catch { }

                    int ok = 0, fail = 0;
                    foreach (string filePath in Directory.GetFiles(localDir))
                    {
                        try { UploadFile(filePath, "/" + dirName); ok++; }
                        catch { fail++; }
                    }
                    SetStatus("Каталог «" + dirName + "»: " + ok + " файлів завантажено, " + fail + " помилок.");
                    BtnConnect_Click(null, null);
                }
                catch (Exception ex) { SetStatus("Помилка завантаження каталогу: " + ex.Message); }
            }
        }

        // ── Допоміжний метод завантаження ────────────────────────────
        private void UploadFile(string localPath, string remoteDir)
        {
            string fileName = Path.GetFileName(localPath);
            string url = Host + remoteDir.Trim('/') + "/" + fileName;
            FtpWebRequest req = CreateRequest(url, WebRequestMethods.Ftp.UploadFile);
            byte[] data = File.ReadAllBytes(localPath);
            Stream s = req.GetRequestStream();
            s.Write(data, 0, data.Length);
            s.Close();
        }


        private System.Windows.Forms.Button btnSettings;

        // ── Відкрити форму налаштувань ────────────────────────────────
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ApplySettings(SettingsForm.ReadSettings(SettingsForm.DefaultSettingsFile));
                SetStatus("Налаштування завантажено з файлу.");
            }
        }

        // ── Застосувати налаштування до форми ─────────────────────────
        private void ApplySettings(FtpSettings s)
        {
            if (!string.IsNullOrEmpty(s.Host)) textBox1.Text = s.Host;
            if (!string.IsNullOrEmpty(s.User)) textBox2.Text = s.User;
            if (!string.IsNullOrEmpty(s.Pass)) textBox3.Text = s.Pass;
        }

        private void label8_Click(object sender, EventArgs e) { }
    }
}
