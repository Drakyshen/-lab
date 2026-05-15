using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MDITextEditor
{
    public class ChildForm : Form
    {
        private RichTextBox richTextBox;
        private string currentFilePath = null;
        private string language = "uk";
        private bool isSyntaxHighlightingEnabled = false;
        private bool isHighlighting = false; // prevent recursive highlighting

        public event Action<string> OnStatusUpdate;

        public ChildForm(string lang)
        {
            language = lang;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(700, 500);
            this.BackColor = Color.FromArgb(30, 30, 30);

            richTextBox = new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.BackColor = Color.FromArgb(28, 28, 28);
            richTextBox.ForeColor = Color.FromArgb(212, 212, 212);
            richTextBox.Font = new Font("Consolas", 12f);
            richTextBox.BorderStyle = BorderStyle.None;
            richTextBox.ScrollBars = RichTextBoxScrollBars.Both;
            richTextBox.WordWrap = true;
            richTextBox.AcceptsTab = true;
            richTextBox.TextChanged += RichTextBox_TextChanged;
            richTextBox.SelectionChanged += (s, e) => UpdateStatus();

            
            var cms = new ContextMenuStrip();
            var cmsHighlight = new ToolStripMenuItem("C/C++ Syntax Highlighting");
            cmsHighlight.CheckOnClick = true;
            cmsHighlight.CheckedChanged += (s, e) =>
            {
                isSyntaxHighlightingEnabled = cmsHighlight.Checked;
                if (isSyntaxHighlightingEnabled) ApplySyntaxHighlighting();
            };
            cms.Items.Add(cmsHighlight);
            richTextBox.ContextMenuStrip = cms;

            this.Controls.Add(richTextBox);
        }

        public void SetLanguage(string lang)
        {
            language = lang;
        }

        public RichTextBox GetEditor() => richTextBox;

        

        public void LoadFile(string path)
        {
            currentFilePath = path;
            string ext = Path.GetExtension(path).ToLower();
            if (ext == ".rtf")
                richTextBox.LoadFile(path, RichTextBoxStreamType.RichText);
            else
                richTextBox.Text = File.ReadAllText(path);

            UpdateStatus();
        }

        public void SaveFile()
        {
            if (currentFilePath == null)
                SaveFileAs();
            else
                SaveToPath(currentFilePath);
        }

        public void SaveFileAs()
        {
            using var sfd = new SaveFileDialog();
            sfd.Filter = "RTF файли (*.rtf)|*.rtf|Текстові файли (*.txt)|*.txt|Всі файли (*.*)|*.*";
            sfd.DefaultExt = "rtf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = sfd.FileName;
                this.Text = Path.GetFileName(currentFilePath);
                SaveToPath(currentFilePath);
            }
        }

        private void SaveToPath(string path)
        {
            string ext = Path.GetExtension(path).ToLower();
            if (ext == ".rtf")
                richTextBox.SaveFile(path, RichTextBoxStreamType.RichText);
            else
                File.WriteAllText(path, richTextBox.Text);

            OnStatusUpdate?.Invoke(language == "uk"
                ? $"Збережено: {Path.GetFileName(path)}"
                : $"Saved: {Path.GetFileName(path)}");
        }

      

        public void ChangeFont()
        {
            using var fd = new FontDialog();
            fd.Font = richTextBox.SelectionFont ?? richTextBox.Font;
            fd.ShowColor = true;
            fd.Color = richTextBox.SelectionColor;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SelectionFont = fd.Font;
                richTextBox.SelectionColor = fd.Color;
            }
        }

        
        public void SetAlignment(HorizontalAlignment alignment)
        {
            richTextBox.SelectionAlignment = alignment switch
            {
                HorizontalAlignment.Left => HorizontalAlignment.Left,
                HorizontalAlignment.Center => HorizontalAlignment.Center,
                HorizontalAlignment.Right => HorizontalAlignment.Right,
                _ => HorizontalAlignment.Left
            };
        }

        public void SetRtfAlignment()
        {
            
            richTextBox.SelectionAlignment = HorizontalAlignment.Left;
            OnStatusUpdate?.Invoke(language == "uk"
                ? "По ширині (не підтримується RichTextBox, встановлено 'По лівому краю')"
                : "Justify not supported by RichTextBox, set to Left");
        }

        

        public void InsertImage()
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Зображення (*.png;*.jpg;*.bmp;*.gif)|*.png;*.jpg;*.bmp;*.gif|Всі файли (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image img = Image.FromFile(ofd.FileName);
                    // Scale if too large
                    int maxW = richTextBox.Width - 20;
                    if (img.Width > maxW)
                    {
                        double ratio = (double)maxW / img.Width;
                        img = new Bitmap(img, new Size(maxW, (int)(img.Height * ratio)));
                    }

                    Clipboard.SetImage(img);
                    richTextBox.Paste();

                    OnStatusUpdate?.Invoke(language == "uk"
                        ? $"Зображення вставлено: {Path.GetFileName(ofd.FileName)}"
                        : $"Image inserted: {Path.GetFileName(ofd.FileName)}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateStatus();
            if (isSyntaxHighlightingEnabled && !isHighlighting)
                ApplySyntaxHighlighting();
        }

        private void ApplySyntaxHighlighting()
        {
            if (isHighlighting) return;
            isHighlighting = true;

            int selStart = richTextBox.SelectionStart;
            int selLength = richTextBox.SelectionLength;

            richTextBox.BeginUpdate();

            
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.FromArgb(212, 212, 212);
            richTextBox.SelectionFont = new Font("Consolas", 12f);
            richTextBox.DeselectAll();

            string text = richTextBox.Text;

          
            string[] keywords = {
                "auto","break","case","char","const","continue","default","do","double",
                "else","enum","extern","float","for","goto","if","inline","int","long",
                "register","restrict","return","short","signed","sizeof","static","struct",
                "switch","typedef","union","unsigned","void","volatile","while",
                "class","namespace","new","delete","public","private","protected",
                "virtual","override","template","typename","bool","true","false","nullptr",
                "using","try","catch","throw","this","operator","friend","explicit","mutable"
            };
            foreach (var kw in keywords)
                HighlightPattern(text, @"\b" + kw + @"\b", Color.FromArgb(86, 156, 214));

            
            HighlightPattern(text, @"#\s*(include|define|ifdef|ifndef|endif|pragma|if|else|elif|undef)\b.*", Color.FromArgb(155, 110, 180));

          
            HighlightPattern(text, @"""(?:[^""\\]|\\.)*""", Color.FromArgb(214, 157, 133));

           
            HighlightPattern(text, @"'(?:[^'\\]|\\.)'", Color.FromArgb(214, 157, 133));

           
            HighlightPattern(text, @"\b(0x[0-9a-fA-F]+|\d+\.?\d*([eE][+-]?\d+)?[fFuUlL]*)\b", Color.FromArgb(181, 206, 168));

            
            HighlightPattern(text, @"//.*", Color.FromArgb(106, 153, 85));

            
            HighlightPattern(text, @"/\*[\s\S]*?\*/", Color.FromArgb(106, 153, 85));

           
            HighlightPattern(text, @"\b(FILE|size_t|ptrdiff_t|int8_t|int16_t|int32_t|int64_t|uint8_t|uint16_t|uint32_t|uint64_t|string|vector|map|set|list|pair|queue|stack|deque|array|wstring)\b",
                Color.FromArgb(78, 201, 176));

            richTextBox.SelectionStart = selStart;
            richTextBox.SelectionLength = selLength;

            richTextBox.EndUpdate();
            isHighlighting = false;
        }

        private void HighlightPattern(string text, string pattern, Color color)
        {
            foreach (Match m in Regex.Matches(text, pattern, RegexOptions.Multiline))
            {
                richTextBox.SelectionStart = m.Index;
                richTextBox.SelectionLength = m.Length;
                richTextBox.SelectionColor = color;
            }
        }

        private void UpdateStatus()
        {
            int line = richTextBox.GetLineFromCharIndex(richTextBox.SelectionStart) + 1;
            int col = richTextBox.SelectionStart - richTextBox.GetFirstCharIndexOfCurrentLine() + 1;
            int words = string.IsNullOrWhiteSpace(richTextBox.Text) ? 0
                : richTextBox.Text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

            OnStatusUpdate?.Invoke(language == "uk"
                ? $"Рядок: {line}  Стовпець: {col}  Слів: {words}"
                : $"Line: {line}  Col: {col}  Words: {words}");
        }
    }
}
