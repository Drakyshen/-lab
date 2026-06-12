using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;

namespace _27._1
{
    public partial class Form1 : Form
    {
        private string currentPath = "";

        public Form1()
        {
            InitializeComponent();
            SetupControls();
            LoadDrives();
        }

        // ── Початкове налаштування контролів ──────────────────────────────────
        private void SetupControls()
        {
            label1.Text = "Поточний шлях:";

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            textBox1.Text = "Фільтр файлів";
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox1.GotFocus += (s, ev) => { if (textBox1.Text == "Фільтр файлів") { textBox1.Text = ""; textBox1.ForeColor = System.Drawing.SystemColors.WindowText; } };
            textBox1.LostFocus += (s, ev) => { if (string.IsNullOrEmpty(textBox1.Text)) { textBox1.Text = "Фільтр файлів"; textBox1.ForeColor = System.Drawing.Color.Gray; } };
            textBox2.Text = "Фільтр папок";
            textBox2.ForeColor = System.Drawing.Color.Gray;
            textBox2.GotFocus += (s, ev) => { if (textBox2.Text == "Фільтр папок") { textBox2.Text = ""; textBox2.ForeColor = System.Drawing.SystemColors.WindowText; } };
            textBox2.LostFocus += (s, ev) => { if (string.IsNullOrEmpty(textBox2.Text)) { textBox2.Text = "Фільтр папок"; textBox2.ForeColor = System.Drawing.Color.Gray; } };

            button1.Text = "Застосувати фільтр";
            button1.Click += Button1_Click;

            // TreeView
            treeView1.Dock = DockStyle.Fill;
            treeView1.AfterSelect += TreeView1_AfterSelect;
            treeView1.BeforeExpand += TreeView1_BeforeExpand;

            // ListView
            listView1.Dock = DockStyle.Fill;
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("Назва", 220);
            listView1.Columns.Add("Розмір", 90, HorizontalAlignment.Right);
            listView1.Columns.Add("Тип", 100);
            listView1.Columns.Add("Дата зміни", 140);
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;
            listView1.DoubleClick += ListView1_DoubleClick;

            // TabControl
            tabControl1.Dock = DockStyle.Fill;
            tabPage1.Text = "Перегляд";
            tabPage2.Text = "Атрибути безпеки";

            // tabPage1: RichTextBox + PictureBox
            var rtb = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Name = "rtbPreview",
                BackColor = SystemColors.Window
            };
            var pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = "pbPreview",
                Visible = false
            };
            tabPage1.Controls.Add(pb);
            tabPage1.Controls.Add(rtb);

            // tabPage2: DataGridView для прав доступу
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                Name = "dgvSecurity",
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgv.Columns.Add("colUser", "Користувач / Група");
            dgv.Columns.Add("colRights", "Права");
            dgv.Columns.Add("colAllow", "Дозволено");
            tabPage2.Controls.Add(dgv);

            // GroupBox — властивості
            groupBox1.Text = "Властивості";
            groupBox1.Dock = DockStyle.Bottom;
            var lblProps = new Label
            {
                Name = "lblProps",
                Dock = DockStyle.Fill,
                Text = "Виберіть диск, папку або файл",
                AutoSize = false
            };
            groupBox1.Controls.Add(lblProps);

            // StatusStrip
            var sslInfo = new ToolStripStatusLabel
            {
                Name = "sslInfo",
                Text = "Готово",
                Spring = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            statusStrip1.Items.Add(sslInfo);
        }

        // ── Завантаження списку дисків ────────────────────────────────────────
        private void LoadDrives()
        {
            comboBox1.Items.Clear();
            foreach (var drive in DriveInfo.GetDrives())
                comboBox1.Items.Add(drive.Name);

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        // ── Вибір диску ───────────────────────────────────────────────────────
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string driveName = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(driveName)) return;

            ShowDriveProperties(driveName);

            treeView1.Nodes.Clear();
            var root = new TreeNode(driveName) { Tag = driveName };
            root.Nodes.Add(""); // фіктивний вузол
            treeView1.Nodes.Add(root);
            root.Expand();

            currentPath = driveName;
            label1.Text = "Поточний шлях: " + currentPath;
            LoadFiles(currentPath);
            SetStatus("Диск: " + driveName);
        }

        // ── Ліниве розкриття TreeView ─────────────────────────────────────────
        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node;
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "")
            {
                node.Nodes.Clear();
                try
                {
                    string path = node.Tag?.ToString() ?? "";
                    foreach (string dir in Directory.GetDirectories(path))
                    {
                        var child = new TreeNode(Path.GetFileName(dir)) { Tag = dir };
                        child.Nodes.Add("");
                        node.Nodes.Add(child);
                    }
                }
                catch { /* немає доступу */ }
            }
        }

        // ── Вибір папки в TreeView ────────────────────────────────────────────
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = e.Node.Tag?.ToString() ?? "";
            if (string.IsNullOrEmpty(path)) return;

            currentPath = path;
            label1.Text = "Поточний шлях: " + currentPath;
            ShowDirectoryProperties(path);
            LoadFiles(path);
            SetStatus("Папка: " + path);
        }

        // ── Подвійний клік у ListView — перехід у папку ───────────────────────
        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            string path = listView1.SelectedItems[0].Tag?.ToString() ?? "";
            if (Directory.Exists(path))
            {
                currentPath = path;
                label1.Text = "Поточний шлях: " + currentPath;
                ShowDirectoryProperties(path);
                LoadFiles(path);
                SetStatus("Папка: " + path);
            }
        }

        // ── Завантаження файлів / папок у ListView ────────────────────────────
        private void LoadFiles(string path, string fileFilter = "", string dirFilter = "")
        {
            listView1.Items.Clear();
            try
            {
                // Папки
                foreach (string dir in Directory.GetDirectories(path))
                {
                    var di = new DirectoryInfo(dir);
                    if (!string.IsNullOrEmpty(dirFilter) &&
                        di.Name.IndexOf(dirFilter, StringComparison.OrdinalIgnoreCase) < 0)
                        continue;

                    var item = new ListViewItem(di.Name);
                    item.SubItems.Add("<папка>");
                    item.SubItems.Add("Папка");
                    item.SubItems.Add(di.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.Tag = dir;
                    item.ForeColor = Color.DarkBlue;
                    listView1.Items.Add(item);
                }

                // Файли
                foreach (string file in Directory.GetFiles(path))
                {
                    var fi = new FileInfo(file);
                    if (!string.IsNullOrEmpty(fileFilter) &&
                        fi.Name.IndexOf(fileFilter, StringComparison.OrdinalIgnoreCase) < 0)
                        continue;

                    var item = new ListViewItem(fi.Name);
                    item.SubItems.Add(FormatSize(fi.Length));
                    item.SubItems.Add(fi.Extension.ToUpper());
                    item.SubItems.Add(fi.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    item.Tag = file;
                    listView1.Items.Add(item);
                }

                SetStatus($"Елементів: {listView1.Items.Count}  |  {path}");
            }
            catch (Exception ex) { SetStatus("Помилка: " + ex.Message); }
        }

        // ── Вибір елемента у ListView ─────────────────────────────────────────
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            string path = listView1.SelectedItems[0].Tag?.ToString() ?? "";
            if (string.IsNullOrEmpty(path)) return;

            if (Directory.Exists(path))
                ShowDirectoryProperties(path);
            else if (File.Exists(path))
            {
                ShowFileProperties(path);
                PreviewFile(path);
                ShowSecurityInfo(path);
            }
        }

        // ── Застосувати фільтр ────────────────────────────────────────────────
        private void Button1_Click(object sender, EventArgs e)
        {
            string fileF = textBox1.Text == "Фільтр файлів" ? "" : textBox1.Text.Trim();
            string dirF = textBox2.Text == "Фільтр папок" ? "" : textBox2.Text.Trim();
            LoadFiles(currentPath, fileF, dirF);
        }

        // ── Перегляд файлу (текст / зображення) ──────────────────────────────
        private void PreviewFile(string path)
        {
            var rtb = tabPage1.Controls["rtbPreview"] as RichTextBox;
            var pb = tabPage1.Controls["pbPreview"] as PictureBox;
            if (rtb == null || pb == null) return;

            string ext = Path.GetExtension(path).ToLower();

            bool isImage = ext == ".png" || ext == ".jpg" || ext == ".jpeg" ||
                           ext == ".bmp" || ext == ".gif" || ext == ".ico";

            if (isImage)
            {
                try
                {
                    pb.Image?.Dispose();
                    pb.Image = Image.FromFile(path);
                    pb.Visible = true;
                    rtb.Visible = false;
                    tabControl1.SelectedTab = tabPage1;
                    return;
                }
                catch { }
            }

            // Текст
            pb.Visible = false;
            rtb.Visible = true;
            tabControl1.SelectedTab = tabPage1;
            try
            {
                long size = new FileInfo(path).Length;
                rtb.Text = size > 512 * 1024
                    ? "[Файл занадто великий для попереднього перегляду]"
                    : File.ReadAllText(path);
            }
            catch (Exception ex) { rtb.Text = "Не вдалося відкрити файл:\n" + ex.Message; }
        }

        // ── Атрибути безпеки файлу ────────────────────────────────────────────
        private void ShowSecurityInfo(string path)
        {
            var dgv = tabPage2.Controls["dgvSecurity"] as DataGridView;
            if (dgv == null) return;
            dgv.Rows.Clear();
            try
            {
                var fs = File.GetAccessControl(path);
                var rules = fs.GetAccessRules(true, true, typeof(NTAccount));
                foreach (FileSystemAccessRule rule in rules)
                {
                    dgv.Rows.Add(
                        rule.IdentityReference.Value,
                        rule.FileSystemRights.ToString(),
                        rule.AccessControlType == AccessControlType.Allow ? "✔ Так" : "✘ Ні"
                    );
                }
            }
            catch { dgv.Rows.Add("Немає доступу", "-", "-"); }
        }

        // ── Властивості диску ─────────────────────────────────────────────────
        private void ShowDriveProperties(string driveName)
        {
            var lbl = GetPropsLabel();
            if (lbl == null) return;
            try
            {
                var di = new DriveInfo(driveName);
                lbl.Text =
                    $"Диск:              {di.Name}\n" +
                    $"Мітка:             {(di.IsReady ? di.VolumeLabel : "-")}\n" +
                    $"Тип:               {di.DriveType}\n" +
                    $"Файлова система:   {(di.IsReady ? di.DriveFormat : "-")}\n" +
                    $"Всього:            {(di.IsReady ? FormatSize(di.TotalSize) : "-")}\n" +
                    $"Вільно:            {(di.IsReady ? FormatSize(di.AvailableFreeSpace) : "-")}";
            }
            catch { lbl.Text = "Немає доступу до диску"; }
        }

        // ── Властивості папки ─────────────────────────────────────────────────
        private void ShowDirectoryProperties(string path)
        {
            var lbl = GetPropsLabel();
            if (lbl == null) return;
            try
            {
                var di = new DirectoryInfo(path);
                lbl.Text =
                    $"Папка:     {di.Name}\n" +
                    $"Шлях:      {di.FullName}\n" +
                    $"Файлів:    {di.GetFiles().Length}   Підпапок: {di.GetDirectories().Length}\n" +
                    $"Атрибути:  {di.Attributes}\n" +
                    $"Створено:  {di.CreationTime:dd.MM.yyyy HH:mm}\n" +
                    $"Змінено:   {di.LastWriteTime:dd.MM.yyyy HH:mm}";
            }
            catch { lbl.Text = "Немає доступу"; }
        }

        // ── Властивості файлу ─────────────────────────────────────────────────
        private void ShowFileProperties(string path)
        {
            var lbl = GetPropsLabel();
            if (lbl == null) return;
            try
            {
                var fi = new FileInfo(path);
                lbl.Text =
                    $"Файл:      {fi.Name}\n" +
                    $"Розмір:    {FormatSize(fi.Length)}\n" +
                    $"Тип:       {fi.Extension.ToUpper()}\n" +
                    $"Атрибути:  {fi.Attributes}\n" +
                    $"Створено:  {fi.CreationTime:dd.MM.yyyy HH:mm}\n" +
                    $"Змінено:   {fi.LastWriteTime:dd.MM.yyyy HH:mm}";
            }
            catch { lbl.Text = "Немає доступу"; }
        }

        // ── Допоміжні ─────────────────────────────────────────────────────────
        private Label GetPropsLabel() => groupBox1.Controls["lblProps"] as Label;

        private void SetStatus(string msg)
        {
            if (statusStrip1.Items["sslInfo"] is ToolStripStatusLabel lbl)
                lbl.Text = msg;
        }

        private static string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} Б";
            if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} КБ";
            if (bytes < 1024L * 1024 * 1024) return $"{bytes / 1048576.0:F1} МБ";
            return $"{bytes / 1073741824.0:F2} ГБ";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
