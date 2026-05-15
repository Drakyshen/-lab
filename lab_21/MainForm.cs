using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MDITextEditor
{
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;
        private string currentLanguage = "uk";

        // UI Controls
        private MenuStrip menuStrip;
        private ToolStrip toolStrip;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        // Menu items (stored as fields for language switching)
        private ToolStripMenuItem menuFile, menuEdit, menuFormat, menuWindow, menuLanguage;
        private ToolStripMenuItem menuNew, menuOpen, menuSave, menuSaveAs, menuExit;
        private ToolStripMenuItem menuCopy, menuCut, menuPaste, menuSelectAll;
        private ToolStripMenuItem menuFont, menuAlignLeft, menuAlignCenter, menuAlignRight, menuAlignJustify;
        private ToolStripMenuItem menuInsertImage;
        private ToolStripMenuItem menuCascade, menuTileH, menuTileV, menuCloseAll;
        private ToolStripMenuItem menuLangUk, menuLangEn;

        public MainForm()
        {
            InitializeComponent();
            BuildMenu();
            BuildToolStrip();
            BuildStatusStrip();
            ApplyLanguage();
        }

        private void InitializeComponent()
        {
            this.IsMdiContainer = true;
            this.Text = "MDI Text Editor";
            this.Size = new Size(1024, 768);
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(45, 45, 48);
        }

        private void BuildMenu()
        {
            menuStrip = new MenuStrip();
            menuStrip.BackColor = Color.FromArgb(37, 37, 38);
            menuStrip.ForeColor = Color.White;
            menuStrip.RenderMode = ToolStripRenderMode.Professional;
            menuStrip.Renderer = new DarkMenuRenderer();

            // File menu
            menuFile = new ToolStripMenuItem();
            menuNew = new ToolStripMenuItem(); menuNew.ShortcutKeys = Keys.Control | Keys.N; menuNew.Click += NewFile_Click;
            menuOpen = new ToolStripMenuItem(); menuOpen.ShortcutKeys = Keys.Control | Keys.O; menuOpen.Click += OpenFile_Click;
            menuSave = new ToolStripMenuItem(); menuSave.ShortcutKeys = Keys.Control | Keys.S; menuSave.Click += SaveFile_Click;
            menuSaveAs = new ToolStripMenuItem(); menuSaveAs.Click += SaveFileAs_Click;
            menuExit = new ToolStripMenuItem(); menuExit.Click += (s, e) => Application.Exit();
            menuFile.DropDownItems.AddRange(new ToolStripItem[] {
                menuNew, menuOpen, new ToolStripSeparator(), menuSave, menuSaveAs, new ToolStripSeparator(), menuExit
            });

            // Edit menu
            menuEdit = new ToolStripMenuItem();
            menuCopy = new ToolStripMenuItem(); menuCopy.ShortcutKeys = Keys.Control | Keys.C; menuCopy.Click += (s, e) => GetActiveEditor()?.Copy();
            menuCut = new ToolStripMenuItem(); menuCut.ShortcutKeys = Keys.Control | Keys.X; menuCut.Click += (s, e) => GetActiveEditor()?.Cut();
            menuPaste = new ToolStripMenuItem(); menuPaste.ShortcutKeys = Keys.Control | Keys.V; menuPaste.Click += (s, e) => GetActiveEditor()?.Paste();
            menuSelectAll = new ToolStripMenuItem(); menuSelectAll.ShortcutKeys = Keys.Control | Keys.A; menuSelectAll.Click += (s, e) => GetActiveEditor()?.SelectAll();
            menuEdit.DropDownItems.AddRange(new ToolStripItem[] { menuCut, menuCopy, menuPaste, new ToolStripSeparator(), menuSelectAll });

            // Format menu
            menuFormat = new ToolStripMenuItem();
            menuFont = new ToolStripMenuItem(); menuFont.Click += ChangeFont_Click;
            menuAlignLeft = new ToolStripMenuItem(); menuAlignLeft.Click += (s, e) => SetAlignment(HorizontalAlignment.Left);
            menuAlignCenter = new ToolStripMenuItem(); menuAlignCenter.Click += (s, e) => SetAlignment(HorizontalAlignment.Center);
            menuAlignRight = new ToolStripMenuItem(); menuAlignRight.Click += (s, e) => SetAlignment(HorizontalAlignment.Right);
            menuAlignJustify = new ToolStripMenuItem(); menuAlignJustify.Click += AlignJustify_Click;
            menuInsertImage = new ToolStripMenuItem(); menuInsertImage.Click += InsertImage_Click;
            menuFormat.DropDownItems.AddRange(new ToolStripItem[] {
                menuFont, new ToolStripSeparator(),
                menuAlignLeft, menuAlignCenter, menuAlignRight, menuAlignJustify,
                new ToolStripSeparator(), menuInsertImage
            });

            // Window menu
            menuWindow = new ToolStripMenuItem();
            menuCascade = new ToolStripMenuItem(); menuCascade.Click += (s, e) => this.LayoutMdi(MdiLayout.Cascade);
            menuTileH = new ToolStripMenuItem(); menuTileH.Click += (s, e) => this.LayoutMdi(MdiLayout.TileHorizontal);
            menuTileV = new ToolStripMenuItem(); menuTileV.Click += (s, e) => this.LayoutMdi(MdiLayout.TileVertical);
            menuCloseAll = new ToolStripMenuItem(); menuCloseAll.Click += CloseAll_Click;
            menuWindow.DropDownItems.AddRange(new ToolStripItem[] { menuCascade, menuTileH, menuTileV, new ToolStripSeparator(), menuCloseAll });

            // Language menu
            menuLanguage = new ToolStripMenuItem();
            menuLangUk = new ToolStripMenuItem("Українська"); menuLangUk.Click += (s, e) => { currentLanguage = "uk"; ApplyLanguage(); };
            menuLangEn = new ToolStripMenuItem("English"); menuLangEn.Click += (s, e) => { currentLanguage = "en"; ApplyLanguage(); };
            menuLanguage.DropDownItems.AddRange(new ToolStripItem[] { menuLangUk, menuLangEn });

            menuStrip.Items.AddRange(new ToolStripItem[] { menuFile, menuEdit, menuFormat, menuWindow, menuLanguage });
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        private void BuildToolStrip()
        {
            toolStrip = new ToolStrip();
            toolStrip.BackColor = Color.FromArgb(37, 37, 38);
            toolStrip.Renderer = new DarkMenuRenderer();

            var btnNew = new ToolStripButton("🆕"); btnNew.ToolTipText = "New"; btnNew.Click += NewFile_Click;
            var btnOpen = new ToolStripButton("📂"); btnOpen.ToolTipText = "Open"; btnOpen.Click += OpenFile_Click;
            var btnSave = new ToolStripButton("💾"); btnSave.ToolTipText = "Save"; btnSave.Click += SaveFile_Click;
            var sep1 = new ToolStripSeparator();
            var btnCut = new ToolStripButton("✂️"); btnCut.ToolTipText = "Cut"; btnCut.Click += (s, e) => GetActiveEditor()?.Cut();
            var btnCopy = new ToolStripButton("📋"); btnCopy.ToolTipText = "Copy"; btnCopy.Click += (s, e) => GetActiveEditor()?.Copy();
            var btnPaste = new ToolStripButton("📌"); btnPaste.ToolTipText = "Paste"; btnPaste.Click += (s, e) => GetActiveEditor()?.Paste();
            var sep2 = new ToolStripSeparator();
            var btnAlignL = new ToolStripButton("⬅"); btnAlignL.ToolTipText = "Align Left"; btnAlignL.Click += (s, e) => SetAlignment(HorizontalAlignment.Left);
            var btnAlignC = new ToolStripButton("↔"); btnAlignC.ToolTipText = "Align Center"; btnAlignC.Click += (s, e) => SetAlignment(HorizontalAlignment.Center);
            var btnAlignR = new ToolStripButton("➡"); btnAlignR.ToolTipText = "Align Right"; btnAlignR.Click += (s, e) => SetAlignment(HorizontalAlignment.Right);
            var sep3 = new ToolStripSeparator();
            var btnFont = new ToolStripButton("🔤"); btnFont.ToolTipText = "Font"; btnFont.Click += ChangeFont_Click;
            var btnImage = new ToolStripButton("🖼️"); btnImage.ToolTipText = "Insert Image"; btnImage.Click += InsertImage_Click;

            foreach (var item in new ToolStripItem[] { btnNew, btnOpen, btnSave, sep1, btnCut, btnCopy, btnPaste, sep2, btnAlignL, btnAlignC, btnAlignR, sep3, btnFont, btnImage })
                toolStrip.Items.Add(item);

            this.Controls.Add(toolStrip);
        }

        private void BuildStatusStrip()
        {
            statusStrip = new StatusStrip();
            statusStrip.BackColor = Color.FromArgb(0, 122, 204);
            statusLabel = new ToolStripStatusLabel("Ready");
            statusLabel.ForeColor = Color.White;
            statusStrip.Items.Add(statusLabel);
            this.Controls.Add(statusStrip);
        }

        private void ApplyLanguage()
        {
            bool uk = currentLanguage == "uk";

            menuFile.Text = uk ? "Файл" : "File";
            menuNew.Text = uk ? "Новий" : "New";
            menuOpen.Text = uk ? "Відкрити..." : "Open...";
            menuSave.Text = uk ? "Зберегти" : "Save";
            menuSaveAs.Text = uk ? "Зберегти як..." : "Save As...";
            menuExit.Text = uk ? "Вихід" : "Exit";

            menuEdit.Text = uk ? "Редагування" : "Edit";
            menuCopy.Text = uk ? "Копіювати" : "Copy";
            menuCut.Text = uk ? "Вирізати" : "Cut";
            menuPaste.Text = uk ? "Вставити" : "Paste";
            menuSelectAll.Text = uk ? "Виділити все" : "Select All";

            menuFormat.Text = uk ? "Формат" : "Format";
            menuFont.Text = uk ? "Шрифт..." : "Font...";
            menuAlignLeft.Text = uk ? "По лівому краю" : "Align Left";
            menuAlignCenter.Text = uk ? "По центру" : "Center";
            menuAlignRight.Text = uk ? "По правому краю" : "Align Right";
            menuAlignJustify.Text = uk ? "По ширині" : "Justify";
            menuInsertImage.Text = uk ? "Вставити зображення..." : "Insert Image...";

            menuWindow.Text = uk ? "Вікно" : "Window";
            menuCascade.Text = uk ? "Каскад" : "Cascade";
            menuTileH.Text = uk ? "Плитка горизонтально" : "Tile Horizontal";
            menuTileV.Text = uk ? "Плитка вертикально" : "Tile Vertical";
            menuCloseAll.Text = uk ? "Закрити всі" : "Close All";

            menuLanguage.Text = uk ? "Мова" : "Language";

            statusLabel.Text = uk ? "Готово" : "Ready";

            // Update all child forms
            foreach (Form child in this.MdiChildren)
                if (child is ChildForm cf)
                    cf.SetLanguage(currentLanguage);
        }

     

        private void NewFile_Click(object sender, EventArgs e)
        {
            childFormNumber++;
            var child = new ChildForm(currentLanguage);
            child.MdiParent = this;
            child.Text = (currentLanguage == "uk" ? "Без назви" : "Untitled") + " " + childFormNumber;
            child.OnStatusUpdate += (msg) => statusLabel.Text = msg;
            child.Show();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "RTF файли (*.rtf)|*.rtf|Текстові файли (*.txt)|*.txt|Всі файли (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                childFormNumber++;
                var child = new ChildForm(currentLanguage);
                child.MdiParent = this;
                child.LoadFile(ofd.FileName);
                child.Text = Path.GetFileName(ofd.FileName);
                child.OnStatusUpdate += (msg) => statusLabel.Text = msg;
                child.Show();
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            GetActiveChild()?.SaveFile();
        }

        private void SaveFileAs_Click(object sender, EventArgs e)
        {
            GetActiveChild()?.SaveFileAs();
        }

        private void ChangeFont_Click(object sender, EventArgs e)
        {
            GetActiveChild()?.ChangeFont();
        }

        private void SetAlignment(HorizontalAlignment alignment)
        {
            GetActiveChild()?.SetAlignment(alignment);
        }

        private void AlignJustify_Click(object sender, EventArgs e)
        {
            
            GetActiveChild()?.SetRtfAlignment();
        }

        private void InsertImage_Click(object sender, EventArgs e)
        {
            GetActiveChild()?.InsertImage();
        }

        private void CloseAll_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
                child.Close();
        }

        private ChildForm GetActiveChild() => this.ActiveMdiChild as ChildForm;
        private RichTextBox GetActiveEditor() => GetActiveChild()?.GetEditor();
    }
}
