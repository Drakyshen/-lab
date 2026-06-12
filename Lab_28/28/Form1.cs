using System;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;

namespace _28
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

        // ══════════════════════════════════════════════════════════════════════
        //  ІНІЦІАЛІЗАЦІЯ
        // ══════════════════════════════════════════════════════════════════════
        private void SetupControls()
        {
            label1.Text = "Поточний шлях:";

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            SetPlaceholder(textBox1, "Фільтр файлів");
            SetPlaceholder(textBox2, "Фільтр папок");

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

            // Контекстне меню
            ContextMenuStrip ctx = new ContextMenuStrip();
            ctx.Items.Add("Створити папку", null, (s, e) => CreateFolder());
            ctx.Items.Add("Створити файл", null, (s, e) => CreateFile());
            ctx.Items.Add(new ToolStripSeparator());
            ctx.Items.Add("Перемістити", null, (s, e) => MoveSelected());
            ctx.Items.Add("Копіювати", null, (s, e) => CopySelected());
            ctx.Items.Add("Видалити", null, (s, e) => DeleteSelected());
            ctx.Items.Add(new ToolStripSeparator());
            ctx.Items.Add("Редагувати атрибути", null, (s, e) => EditAttributes());
            ctx.Items.Add("Редагувати текст", null, (s, e) => EditTextFile());
            ctx.Items.Add(new ToolStripSeparator());
            ctx.Items.Add("Архівувати (ZIP)", null, (s, e) => ZipSelected());
            ctx.Items.Add("Розпакувати ZIP", null, (s, e) => UnzipSelected());
            listView1.ContextMenuStrip = ctx;

            // TabControl
            tabControl1.Dock = DockStyle.Fill;
            tabPage1.Text = "Перегляд";
            tabPage2.Text = "Атрибути безпеки";

            // tabPage1
            RichTextBox rtb = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Name = "rtbPreview",
                BackColor = SystemColors.Window
            };
            PictureBox pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Name = "pbPreview",
                Visible = false
            };
            tabPage1.Controls.Add(pb);
            tabPage1.Controls.Add(rtb);

            // tabPage2
            DataGridView dgv = new DataGridView
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

            // GroupBox
            groupBox1.Text = "Властивості";
            groupBox1.Dock = DockStyle.Bottom;
            Label lblProps = new Label
            {
                Name = "lblProps",
                Dock = DockStyle.Fill,
                Text = "Виберіть диск, папку або файл",
                AutoSize = false
            };
            groupBox1.Controls.Add(lblProps);

            // StatusStrip
            ToolStripStatusLabel sslInfo = new ToolStripStatusLabel
            {
                Name = "sslInfo",
                Text = "Готово",
                Spring = true,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            statusStrip1.Items.Add(sslInfo);
        }

        private static void SetPlaceholder(TextBox tb, string hint)
        {
            tb.Text = hint;
            tb.ForeColor = Color.Gray;
            tb.GotFocus += (s, e) =>
            {
                if (tb.Text == hint) { tb.Text = ""; tb.ForeColor = SystemColors.WindowText; }
            };
            tb.LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(tb.Text)) { tb.Text = hint; tb.ForeColor = Color.Gray; }
            };
        }

        // ══════════════════════════════════════════════════════════════════════
        //  НАВІГАЦІЯ
        // ══════════════════════════════════════════════════════════════════════
        private void LoadDrives()
        {
            comboBox1.Items.Clear();
            foreach (DriveInfo d in DriveInfo.GetDrives())
                comboBox1.Items.Add(d.Name);
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string drive = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(drive)) return;
            ShowDriveProperties(drive);
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode(drive) { Tag = drive };
            root.Nodes.Add("");
            treeView1.Nodes.Add(root);
            root.Expand();
            NavigateTo(drive);
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "")
            {
                node.Nodes.Clear();
                try
                {
                    foreach (string dir in Directory.GetDirectories(node.Tag?.ToString() ?? ""))
                    {
                        TreeNode child = new TreeNode(Path.GetFileName(dir)) { Tag = dir };
                        child.Nodes.Add("");
                        node.Nodes.Add(child);
                    }
                }
                catch { }
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = e.Node.Tag?.ToString() ?? "";
            if (!string.IsNullOrEmpty(path)) { ShowDirectoryProperties(path); NavigateTo(path); }
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            string path = SelectedPath();
            if (Directory.Exists(path)) { ShowDirectoryProperties(path); NavigateTo(path); }
        }

        private void NavigateTo(string path)
        {
            currentPath = path;
            label1.Text = "Поточний шлях: " + path;
            LoadFiles(path);
        }

        private void LoadFiles(string path, string fileFilter = "", string dirFilter = "")
        {
            listView1.Items.Clear();
            try
            {
                foreach (string dir in Directory.GetDirectories(path))
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    if (!string.IsNullOrEmpty(dirFilter) &&
                        di.Name.IndexOf(dirFilter, StringComparison.OrdinalIgnoreCase) < 0) continue;
                    ListViewItem it = new ListViewItem(di.Name);
                    it.SubItems.Add("<папка>"); it.SubItems.Add("Папка");
                    it.SubItems.Add(di.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    it.Tag = dir; it.ForeColor = Color.DarkBlue;
                    listView1.Items.Add(it);
                }
                foreach (string file in Directory.GetFiles(path))
                {
                    FileInfo fi = new FileInfo(file);
                    if (!string.IsNullOrEmpty(fileFilter) &&
                        fi.Name.IndexOf(fileFilter, StringComparison.OrdinalIgnoreCase) < 0) continue;
                    ListViewItem it = new ListViewItem(fi.Name);
                    it.SubItems.Add(FormatSize(fi.Length));
                    it.SubItems.Add(fi.Extension.ToUpper());
                    it.SubItems.Add(fi.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                    it.Tag = file;
                    listView1.Items.Add(it);
                }
                SetStatus($"Елементів: {listView1.Items.Count}  |  {path}");
            }
            catch (Exception ex) { SetStatus("Помилка: " + ex.Message); }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = SelectedPath();
            if (string.IsNullOrEmpty(path)) return;
            if (Directory.Exists(path)) ShowDirectoryProperties(path);
            else if (File.Exists(path)) { ShowFileProperties(path); PreviewFile(path); ShowSecurityInfo(path); }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string ff = textBox1.Text == "Фільтр файлів" ? "" : textBox1.Text.Trim();
            string df = textBox2.Text == "Фільтр папок" ? "" : textBox2.Text.Trim();
            LoadFiles(currentPath, ff, df);
        }

        // ══════════════════════════════════════════════════════════════════════
        //  ОПЕРАЦІЇ З ФАЙЛАМИ / ПАПКАМИ
        // ══════════════════════════════════════════════════════════════════════
        private void CreateFolder()
        {
            string name = Prompt("Нова папка", "Введіть назву папки:");
            if (string.IsNullOrEmpty(name)) return;
            try { Directory.CreateDirectory(Path.Combine(currentPath, name)); LoadFiles(currentPath); SetStatus("Папку створено: " + name); }
            catch (Exception ex) { Err(ex); }
        }

        private void CreateFile()
        {
            string name = Prompt("Новий файл", "Введіть назву файлу (наприклад, test.txt):");
            if (string.IsNullOrEmpty(name)) return;
            try { File.Create(Path.Combine(currentPath, name)).Close(); LoadFiles(currentPath); SetStatus("Файл створено: " + name); }
            catch (Exception ex) { Err(ex); }
        }

        private void MoveSelected()
        {
            string src = SelectedPath();
            if (string.IsNullOrEmpty(src)) return;
            string dest = PickFolder("Виберіть папку призначення");
            if (string.IsNullOrEmpty(dest)) return;
            try
            {
                string target = Path.Combine(dest, Path.GetFileName(src));
                if (Directory.Exists(src)) Directory.Move(src, target);
                else File.Move(src, target);
                LoadFiles(currentPath);
                SetStatus("Переміщено до: " + dest);
            }
            catch (Exception ex) { Err(ex); }
        }

        private void CopySelected()
        {
            string src = SelectedPath();
            if (string.IsNullOrEmpty(src)) return;
            string dest = PickFolder("Виберіть папку для копіювання");
            if (string.IsNullOrEmpty(dest)) return;
            try
            {
                string target = Path.Combine(dest, Path.GetFileName(src));
                if (Directory.Exists(src)) CopyDirectory(src, target);
                else File.Copy(src, target, true);
                LoadFiles(currentPath);
                SetStatus("Скопійовано до: " + dest);
            }
            catch (Exception ex) { Err(ex); }
        }

        private static void CopyDirectory(string src, string dest)
        {
            Directory.CreateDirectory(dest);
            foreach (string f in Directory.GetFiles(src))
                File.Copy(f, Path.Combine(dest, Path.GetFileName(f)), true);
            foreach (string d in Directory.GetDirectories(src))
                CopyDirectory(d, Path.Combine(dest, Path.GetFileName(d)));
        }

        private void DeleteSelected()
        {
            string path = SelectedPath();
            if (string.IsNullOrEmpty(path)) return;
            string name = Path.GetFileName(path);
            if (MessageBox.Show("Видалити «" + name + "»?", "Підтвердження",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                if (Directory.Exists(path)) Directory.Delete(path, true);
                else File.Delete(path);
                LoadFiles(currentPath);
                SetStatus("Видалено: " + name);
            }
            catch (Exception ex) { Err(ex); }
        }

        private void EditAttributes()
        {
            string path = SelectedPath();
            if (string.IsNullOrEmpty(path)) return;
            bool isFile = File.Exists(path);
            FileAttributes cur = isFile
                ? File.GetAttributes(path)
                : new DirectoryInfo(path).Attributes;

            Form dlg = new Form
            {
                Text = "Атрибути: " + Path.GetFileName(path),
                Size = new Size(280, 230),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };
            CheckBox chkRO = new CheckBox { Text = "Тільки читання (ReadOnly)", Checked = cur.HasFlag(FileAttributes.ReadOnly), Location = new Point(16, 16), AutoSize = true };
            CheckBox chkHide = new CheckBox { Text = "Прихований (Hidden)", Checked = cur.HasFlag(FileAttributes.Hidden), Location = new Point(16, 48), AutoSize = true };
            CheckBox chkSys = new CheckBox { Text = "Системний (System)", Checked = cur.HasFlag(FileAttributes.System), Location = new Point(16, 80), AutoSize = true };
            CheckBox chkArch = new CheckBox { Text = "Архівний (Archive)", Checked = cur.HasFlag(FileAttributes.Archive), Location = new Point(16, 112), AutoSize = true };
            Button btnOk = new Button { Text = "Зберегти", DialogResult = DialogResult.OK, Location = new Point(16, 152), Size = new Size(100, 32) };
            Button btnCan = new Button { Text = "Скасувати", DialogResult = DialogResult.Cancel, Location = new Point(130, 152), Size = new Size(100, 32) };
            dlg.Controls.AddRange(new Control[] { chkRO, chkHide, chkSys, chkArch, btnOk, btnCan });
            dlg.AcceptButton = btnOk; dlg.CancelButton = btnCan;

            if (dlg.ShowDialog(this) != DialogResult.OK) { dlg.Dispose(); return; }

            FileAttributes newAttr = 0;
            if (chkRO.Checked) newAttr |= FileAttributes.ReadOnly;
            if (chkHide.Checked) newAttr |= FileAttributes.Hidden;
            if (chkSys.Checked) newAttr |= FileAttributes.System;
            if (chkArch.Checked) newAttr |= FileAttributes.Archive;
            dlg.Dispose();

            try { File.SetAttributes(path, newAttr); SetStatus("Атрибути збережено."); LoadFiles(currentPath); }
            catch (Exception ex) { Err(ex); }
        }

        private void EditTextFile()
        {
            string path = SelectedPath();
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;
            string ext = Path.GetExtension(path).ToLower();
            bool isText = ext == ".txt" || ext == ".cs" || ext == ".xml" ||
                          ext == ".json" || ext == ".ini" || ext == ".log" ||
                          ext == ".html" || ext == ".htm" || ext == ".md" ||
                          ext == ".bat" || ext == "";
            if (!isText)
            {
                MessageBox.Show("Редагування доступне лише для текстових файлів.",
                    "Інфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Form dlg = new Form
            {
                Text = "Редагування: " + Path.GetFileName(path),
                Size = new Size(750, 550),
                StartPosition = FormStartPosition.CenterParent
            };
            RichTextBox rtb = new RichTextBox { Dock = DockStyle.Fill, Font = new Font("Consolas", 10f) };
            try { rtb.Text = File.ReadAllText(path); }
            catch (Exception ex) { Err(ex); dlg.Dispose(); return; }

            Panel pnl = new Panel { Dock = DockStyle.Bottom, Height = 40 };
            Button btnSave = new Button { Text = "Зберегти", Location = new Point(8, 6), Size = new Size(110, 28) };
            Button btnClose = new Button { Text = "Закрити", Location = new Point(130, 6), Size = new Size(100, 28), DialogResult = DialogResult.Cancel };
            btnSave.Click += (s, ev) =>
            {
                try { File.WriteAllText(path, rtb.Text); SetStatus("Збережено: " + Path.GetFileName(path)); }
                catch (Exception ex2) { Err(ex2); }
            };
            pnl.Controls.AddRange(new Control[] { btnSave, btnClose });
            dlg.Controls.Add(rtb);
            dlg.Controls.Add(pnl);
            dlg.CancelButton = btnClose;
            dlg.ShowDialog(this);
            dlg.Dispose();
        }

        private void ZipSelected()
        {
            string src = SelectedPath();
            if (string.IsNullOrEmpty(src)) return;

            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Зберегти архів як...",
                Filter = "ZIP архів (*.zip)|*.zip",
                FileName = Path.GetFileNameWithoutExtension(src) + ".zip",
                InitialDirectory = currentPath
            };
            if (sfd.ShowDialog(this) != DialogResult.OK) { sfd.Dispose(); return; }

            try
            {
                if (File.Exists(sfd.FileName)) File.Delete(sfd.FileName);
                if (Directory.Exists(src))
                    ZipFile.CreateFromDirectory(src, sfd.FileName);
                else
                {
                    using (ZipArchive zip = ZipFile.Open(sfd.FileName, ZipArchiveMode.Create))
                        zip.CreateEntryFromFile(src, Path.GetFileName(src));
                }
                LoadFiles(currentPath);
                SetStatus("Архів створено: " + Path.GetFileName(sfd.FileName));
            }
            catch (Exception ex) { Err(ex); }
            sfd.Dispose();
        }

        private void UnzipSelected()
        {
            string src = SelectedPath();
            if (string.IsNullOrEmpty(src) || !File.Exists(src)) return;
            if (!src.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Виберіть ZIP-архів.", "Інфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string dest = PickFolder("Виберіть папку для розпакування");
            if (string.IsNullOrEmpty(dest)) return;
            try
            {
                ZipFile.ExtractToDirectory(src, dest);
                LoadFiles(currentPath);
                SetStatus("Розпаковано до: " + dest);
            }
            catch (Exception ex) { Err(ex); }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  ПЕРЕГЛЯД / ВЛАСТИВОСТІ
        // ══════════════════════════════════════════════════════════════════════
        private void PreviewFile(string path)
        {
            RichTextBox rtb = tabPage1.Controls["rtbPreview"] as RichTextBox;
            PictureBox pb = tabPage1.Controls["pbPreview"] as PictureBox;
            if (rtb == null || pb == null) return;

            string ext = Path.GetExtension(path).ToLower();
            bool isImg = ext == ".png" || ext == ".jpg" || ext == ".jpeg" ||
                           ext == ".bmp" || ext == ".gif" || ext == ".ico";
            if (isImg)
            {
                try
                {
                    pb.Image?.Dispose();
                    pb.Image = Image.FromFile(path);
                    pb.Visible = true; rtb.Visible = false;
                    tabControl1.SelectedTab = tabPage1; return;
                }
                catch { }
            }
            pb.Visible = false; rtb.Visible = true;
            tabControl1.SelectedTab = tabPage1;
            try
            {
                long sz = new FileInfo(path).Length;
                rtb.Text = sz > 512 * 1024 ? "[Файл занадто великий]" : File.ReadAllText(path);
            }
            catch (Exception ex) { rtb.Text = "Помилка: " + ex.Message; }
        }

        private void ShowSecurityInfo(string path)
        {
            DataGridView dgv = tabPage2.Controls["dgvSecurity"] as DataGridView;
            if (dgv == null) return;
            dgv.Rows.Clear();
            try
            {
                AuthorizationRuleCollection rules =
                    File.GetAccessControl(path).GetAccessRules(true, true, typeof(NTAccount));
                foreach (FileSystemAccessRule r in rules)
                    dgv.Rows.Add(r.IdentityReference.Value, r.FileSystemRights.ToString(),
                        r.AccessControlType == AccessControlType.Allow ? "Так" : "Ні");
            }
            catch { dgv.Rows.Add("Немає доступу", "-", "-"); }
        }

        private void ShowDriveProperties(string name)
        {
            Label lbl = GetPropsLabel(); if (lbl == null) return;
            try
            {
                DriveInfo d = new DriveInfo(name);
                lbl.Text = "Диск:            " + d.Name + "\n" +
                           "Мітка:           " + (d.IsReady ? d.VolumeLabel : "-") + "\n" +
                           "Тип:             " + d.DriveType + "\n" +
                           "Файлова система: " + (d.IsReady ? d.DriveFormat : "-") + "\n" +
                           "Всього:          " + (d.IsReady ? FormatSize(d.TotalSize) : "-") + "\n" +
                           "Вільно:          " + (d.IsReady ? FormatSize(d.AvailableFreeSpace) : "-");
            }
            catch { lbl.Text = "Немає доступу до диску"; }
        }

        private void ShowDirectoryProperties(string path)
        {
            Label lbl = GetPropsLabel(); if (lbl == null) return;
            try
            {
                DirectoryInfo d = new DirectoryInfo(path);
                lbl.Text = "Папка:    " + d.Name + "\n" +
                           "Шлях:     " + d.FullName + "\n" +
                           "Файлів:   " + d.GetFiles().Length + "   Підпапок: " + d.GetDirectories().Length + "\n" +
                           "Атрибути: " + d.Attributes + "\n" +
                           "Створено: " + d.CreationTime.ToString("dd.MM.yyyy HH:mm") + "\n" +
                           "Змінено:  " + d.LastWriteTime.ToString("dd.MM.yyyy HH:mm");
            }
            catch { lbl.Text = "Немає доступу"; }
        }

        private void ShowFileProperties(string path)
        {
            Label lbl = GetPropsLabel(); if (lbl == null) return;
            try
            {
                FileInfo f = new FileInfo(path);
                lbl.Text = "Файл:     " + f.Name + "\n" +
                           "Розмір:   " + FormatSize(f.Length) + "\n" +
                           "Тип:      " + f.Extension.ToUpper() + "\n" +
                           "Атрибути: " + f.Attributes + "\n" +
                           "Створено: " + f.CreationTime.ToString("dd.MM.yyyy HH:mm") + "\n" +
                           "Змінено:  " + f.LastWriteTime.ToString("dd.MM.yyyy HH:mm");
            }
            catch { lbl.Text = "Немає доступу"; }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  ДОПОМІЖНІ
        // ══════════════════════════════════════════════════════════════════════
        private string SelectedPath() =>
            listView1.SelectedItems.Count > 0
                ? listView1.SelectedItems[0].Tag?.ToString() ?? ""
                : "";

        private Label GetPropsLabel() => groupBox1.Controls["lblProps"] as Label;

        private void SetStatus(string msg)
        {
            if (statusStrip1.Items["sslInfo"] is ToolStripStatusLabel l) l.Text = msg;
        }

        private static string Prompt(string title, string prompt)
        {
            Form frm = new Form
            {
                Text = title,
                Size = new Size(380, 140),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };
            Label lbl = new Label { Text = prompt, Location = new Point(12, 12), AutoSize = true };
            TextBox tb = new TextBox { Location = new Point(12, 36), Width = 340 };
            Button ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(12, 68), Width = 80 };
            Button can = new Button { Text = "Скасув.", DialogResult = DialogResult.Cancel, Location = new Point(100, 68), Width = 80 };
            frm.Controls.AddRange(new Control[] { lbl, tb, ok, can });
            frm.AcceptButton = ok; frm.CancelButton = can;
            string result = frm.ShowDialog() == DialogResult.OK ? tb.Text.Trim() : "";
            frm.Dispose();
            return result;
        }

        private static string PickFolder(string description)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog { Description = description };
            string result = dlg.ShowDialog() == DialogResult.OK ? dlg.SelectedPath : "";
            dlg.Dispose();
            return result;
        }

        private void Err(Exception ex) =>
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private static string FormatSize(long b)
        {
            if (b < 1024) return b + " Б";
            if (b < 1_048_576) return (b / 1024.0).ToString("F1") + " КБ";
            if (b < 1_073_741_824) return (b / 1_048_576.0).ToString("F1") + " МБ";
            return (b / 1_073_741_824.0).ToString("F2") + " ГБ";
        }

        private void Form1_Load(object sender, EventArgs e) { }
        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e) { }
    }
}
