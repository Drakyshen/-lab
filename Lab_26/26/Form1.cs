using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace _26
{
    public partial class Form1 : Form
    {
        // Відкритий документ Word (щоб мати доступ з різних кнопок)
        private Microsoft.Office.Interop.Word.Application wordApp = null;
        private Document currentDoc = null;

        public Form1()
        {
            InitializeComponent();

            // Підписи полів
            label1.Text = "ПІБ працівника:";
            label2.Text = "Назва підприємства:";
            label3.Text = "Структурний підрозділ:";
            label4.Text = "Посада / професія:";
            label5.Text = "Оклад (грн):";

            // Підписи кнопок
            button1.Text = "Створити наказ";
            buttonPreview.Text = "Переглянути шаблон";
            buttonSave.Text = "Зберегти як...";
            buttonReplace.Text = "Знайти і замінити";

            // Підказки для полів пошуку/заміни
            textBoxFind.Text = "Знайти...";
            textBoxReplace.Text = "Замінити на...";

            // Підключаємо обробники
            buttonPreview.Click += buttonPreview_Click;
            buttonSave.Click += buttonSave_Click;
            buttonReplace.Click += buttonReplace_Click;
            listBoxTemplates.SelectedIndexChanged += listBoxTemplates_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTemplates();
        }

        // Завантажуємо список шаблонів з папки програми
        private void LoadTemplates()
        {
            listBoxTemplates.Items.Clear();
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            foreach (string file in Directory.GetFiles(folder, "*.doc"))
            {
                listBoxTemplates.Items.Add(Path.GetFileName(file));
            }
            foreach (string file in Directory.GetFiles(folder, "*.docx"))
            {
                listBoxTemplates.Items.Add(Path.GetFileName(file));
            }
            if (listBoxTemplates.Items.Count > 0)
                listBoxTemplates.SelectedIndex = 0;
        }

        // При виборі шаблону — показуємо його назву
        private void listBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTemplates.SelectedItem != null)
                this.Text = "Наказ П-1 — шаблон: " + listBoxTemplates.SelectedItem.ToString();
        }

        // Кнопка "Переглянути шаблон"
        private void buttonPreview_Click(object sender, EventArgs e)
        {
            if (listBoxTemplates.SelectedItem == null)
            {
                MessageBox.Show("Виберіть шаблон зі списку!", "Увага",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                listBoxTemplates.SelectedItem.ToString());

            try
            {
                // Просто відкриваємо для перегляду
                var app = new Microsoft.Office.Interop.Word.Application();
                app.Visible = true;
                app.Documents.Open(path, ReadOnly: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка перегляду: " + ex.Message, "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка "Створити наказ"
        private void button1_Click(object sender, EventArgs e)
        {
            string pib = textBox1.Text.Trim();
            string pidpryemstvo = textBox2.Text.Trim();
            string pidrozdil = textBox3.Text.Trim();
            string posada = textBox4.Text.Trim();
            string oklad = textBox5.Text.Trim();

            if (string.IsNullOrWhiteSpace(pib))
            {
                MessageBox.Show("Заповніть ПІБ!", "Увага",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxTemplates.SelectedItem == null)
            {
                MessageBox.Show("Виберіть шаблон зі списку!", "Увага",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string templatePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                listBoxTemplates.SelectedItem.ToString());

            if (!File.Exists(templatePath))
            {
                MessageBox.Show("Файл шаблону не знайдено:\n" + templatePath,
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Копіюємо шаблон на робочий стіл
            string savePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Наказ_" + pib.Split(' ')[0] + ".doc");

            File.Copy(templatePath, savePath, true);

            try
            {
                // Закриваємо попередній якщо Word ще живий
                if (IsWordAlive() && currentDoc != null)
                {
                    try { currentDoc.Close(false); } catch { }
                    currentDoc = null;
                }

                // Завжди створюємо новий екземпляр
                wordApp = new Microsoft.Office.Interop.Word.Application();
                wordApp.Visible = true;
                currentDoc = wordApp.Documents.Open(savePath);

                // Замінюємо текст

                FindReplace("Найменування підприємства (установи, організації)", pidpryemstvo);
                FindReplace("назва структурного підрозділу", pidrozdil);
                FindReplace("назва професії (посади), кваліфікація", posada);
                FindReplace("прізвище, ім'я, по батькові", pib); 

                currentDoc.Save();



                MessageBox.Show("Наказ створено:\n" + savePath,
                    "Готово!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка "Зберегти як..."
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!IsWordAlive() || currentDoc == null)
            {
                MessageBox.Show("Спочатку створіть наказ!", "Увага",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Зберегти наказ як...";
                dlg.Filter = "Word документ (*.doc)|*.doc|Word документ (*.docx)|*.docx";
                dlg.FileName = "Наказ_П1";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        WdSaveFormat fmt = dlg.FileName.EndsWith(".docx")
                            ? WdSaveFormat.wdFormatXMLDocument
                            : WdSaveFormat.wdFormatDocument;

                        currentDoc.SaveAs2(dlg.FileName, FileFormat: fmt);

                        MessageBox.Show("Збережено:\n" + dlg.FileName,
                            "Готово!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (System.Runtime.InteropServices.COMException)
                    {
                        // Word був закритий — скидаємо стан
                        wordApp = null;
                        currentDoc = null;
                        MessageBox.Show("Word був закритий. Створіть наказ заново.",
                            "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка збереження: " + ex.Message, "Помилка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Кнопка "Знайти і замінити"
        private void buttonReplace_Click(object sender, EventArgs e)
        {
            if (!IsWordAlive() || currentDoc == null)
            {
                MessageBox.Show("Спочатку створіть наказ!", "Увага",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string find = textBoxFind.Text.Trim();
            string replace = textBoxReplace.Text.Trim();

            if (string.IsNullOrWhiteSpace(find))
            {
                MessageBox.Show("Введіть текст для пошуку!", "Увага",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                FindReplace(find, replace);

                if (IsWordAlive() && currentDoc != null)
                {
                    currentDoc.Save();
                    MessageBox.Show($"Замінено '{find}' на '{replace}'",
                        "Готово!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Перевірка чи Word ще відкритий
        private bool IsWordAlive()
        {
            try
            {
                if (wordApp == null) return false;
                var test = wordApp.Version; // якщо Word закритий — кине виняток
                return true;
            }
            catch
            {
                wordApp = null;
                currentDoc = null;
                return false;
            }
        }

        // Універсальна функція Find & Replace
        private void FindReplace(string find, string replace)
        {
            if (!IsWordAlive())
            {
                MessageBox.Show("Word був закритий. Спочатку створіть наказ заново.",
                    "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Find findObj = currentDoc.Content.Find;
            findObj.ClearFormatting();
            findObj.Replacement.ClearFormatting();

            findObj.Text = find;
            findObj.Replacement.Text = replace;
            findObj.Forward = true;
            findObj.Wrap = WdFindWrap.wdFindContinue;
            findObj.Format = false;
            findObj.MatchCase = false;
            findObj.MatchWholeWord = false;
            findObj.MatchWildcards = false;
            findObj.MatchSoundsLike = false;
            findObj.MatchAllWordForms = false;

            findObj.Execute(Replace: WdReplace.wdReplaceAll);
        }
    }
}