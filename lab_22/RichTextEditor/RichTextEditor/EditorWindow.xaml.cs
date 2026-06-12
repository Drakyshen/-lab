using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using RichTextEditor.Localization;

namespace RichTextEditor
{
    public partial class EditorWindow : Window, INotifyPropertyChanged
    {
        // ─── State ────────────────────────────────────────────────────────────
        private string? _filePath;
        private bool _isModified;
        private bool _suppressSelectionUpdate;
        private Color _currentFontColor = Colors.Black;

        // ─── Localization binding ─────────────────────────────────────────────
        public LocalizationManager Loc => LocalizationManager.Instance;

        public bool IsUkrainian => Loc.Language == AppLanguage.Ukrainian;
        public bool IsEnglish   => Loc.Language == AppLanguage.English;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Notify(string p) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        // ─── Constructor ──────────────────────────────────────────────────────
        public EditorWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Populate font families
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies
                .OrderBy(f => f.Source)
                .Select(f => f.Source)
                .ToList();

            // Populate font sizes
            cmbFontSize.ItemsSource = new List<double>
                { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

            // Default font
            cmbFontFamily.SelectedItem = "Segoe UI";
            cmbFontSize.Text = "14";

            // Hook document change for "modified" flag
            rtbEditor.TextChanged += (_, _) =>
            {
                if (!_isModified) { _isModified = true; RefreshTitle(); }
            };

            Loc.PropertyChanged += (_, _) =>
            {
                Notify(null!);
                SetStatus(Loc.StatusReady);
            };

            SetStatus(Loc.StatusReady);
        }

        // ─── Title management ─────────────────────────────────────────────────
        public void RefreshTitle(int index = 0, int total = 0)
        {
            var name = _filePath != null
                ? Path.GetFileName(_filePath)
                : Loc.Untitled;
            var mod = _isModified ? " *" : "";
            var win = total > 1 ? $" [{index}/{total}]" : "";
            Title = $"{name}{mod}{win} — {Loc.AppTitle}";
        }

        private void RefreshTitle() => RefreshTitle(0, 0);

        // ─── File Operations ──────────────────────────────────────────────────
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!ConfirmDiscard()) return;
            rtbEditor.Document.Blocks.Clear();
            _filePath = null;
            _isModified = false;
            RefreshTitle();
            SetStatus(Loc.StatusReady);
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!ConfirmDiscard()) return;

            var dlg = new OpenFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                using var fs = new FileStream(dlg.FileName, FileMode.Open);
                var range = new TextRange(rtbEditor.Document.ContentStart,
                                          rtbEditor.Document.ContentEnd);
                range.Load(fs, DataFormats.Rtf);
                _filePath = dlg.FileName;
                _isModified = false;
                RefreshTitle();
                SetStatus(Loc.StatusReady);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_filePath == null) { SaveAs_Executed(sender, e); return; }
            SaveToPath(_filePath);
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*",
                FileName = _filePath != null
                    ? Path.GetFileName(_filePath)
                    : Loc.Untitled + ".rtf"
            };
            if (dlg.ShowDialog() != true) return;
            SaveToPath(dlg.FileName);
        }

        private void SaveToPath(string path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Create);
                var range = new TextRange(rtbEditor.Document.ContentStart,
                                          rtbEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Rtf);
                _filePath = path;
                _isModified = false;
                RefreshTitle();
                SetStatus(Loc.StatusSaved);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ─── New Window ───────────────────────────────────────────────────────
        private void NewWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowManager.CreateNewWindow();
        }

        // ─── Insert Image ─────────────────────────────────────────────────────
        private void InsertImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tiff;*.webp|All files (*.*)|*.*",
                Title = Loc.InsertImage
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(dlg.FileName, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                // Scale down to max 600px wide if needed
                double maxW = 600;
                double displayW = bitmap.PixelWidth > maxW ? maxW : bitmap.PixelWidth;
                double displayH = bitmap.PixelWidth > maxW
                    ? bitmap.PixelHeight * (maxW / bitmap.PixelWidth)
                    : bitmap.PixelHeight;

                var image = new Image
                {
                    Source = bitmap,
                    Width = displayW,
                    Height = displayH,
                    Stretch = Stretch.Uniform
                };

                // Wrap in InlineUIContainer and insert at caret
                var container = new InlineUIContainer(image, rtbEditor.CaretPosition);
                _ = container; // attached to document via constructor side-effect

                _isModified = true;
                RefreshTitle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inserting image",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ─── Font Color ───────────────────────────────────────────────────────
        private void FontColor_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Use a simple color picker dialog (Windows Forms ColorDialog via interop)
            var dlg = new System.Windows.Forms.ColorDialog
            {
                Color = System.Drawing.Color.FromArgb(
                    _currentFontColor.A,
                    _currentFontColor.R,
                    _currentFontColor.G,
                    _currentFontColor.B)
            };
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            var c = dlg.Color;
            _currentFontColor = Color.FromArgb(c.A, c.R, c.G, c.B);
            rectFontColor.Fill = new SolidColorBrush(_currentFontColor);

            if (!rtbEditor.Selection.IsEmpty)
            {
                rtbEditor.Selection.ApplyPropertyValue(
                    TextElement.ForegroundProperty,
                    new SolidColorBrush(_currentFontColor));
            }
        }

        // ─── Font Family & Size ───────────────────────────────────────────────
        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_suppressSelectionUpdate) return;
            if (cmbFontFamily.SelectedItem is string fontName)
            {
                rtbEditor.Selection.ApplyPropertyValue(
                    TextElement.FontFamilyProperty,
                    new FontFamily(fontName));
                rtbEditor.Focus();
            }
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_suppressSelectionUpdate) return;
            if (double.TryParse(cmbFontSize.Text, out double size) && size > 0)
            {
                rtbEditor.Selection.ApplyPropertyValue(
                    TextElement.FontSizeProperty, size);
            }
        }

        // ─── Selection Changed → update toolbar state ─────────────────────────
        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _suppressSelectionUpdate = true;
            try
            {
                // Bold
                var fw = rtbEditor.Selection.GetPropertyValue(TextElement.FontWeightProperty);
                btnBold.IsChecked = fw != DependencyProperty.UnsetValue &&
                                    fw.Equals(FontWeights.Bold);

                // Italic
                var fs = rtbEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
                btnItalic.IsChecked = fs != DependencyProperty.UnsetValue &&
                                      fs.Equals(FontStyles.Italic);

                // Underline
                var td = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                btnUnderline.IsChecked = td != DependencyProperty.UnsetValue &&
                                         td.Equals(TextDecorations.Underline);

                // Alignment
                var align = rtbEditor.Selection.GetPropertyValue(Block.TextAlignmentProperty);
                btnAlignLeft.IsChecked    = align.Equals(TextAlignment.Left);
                btnAlignCenter.IsChecked  = align.Equals(TextAlignment.Center);
                btnAlignRight.IsChecked   = align.Equals(TextAlignment.Right);
                btnAlignJustify.IsChecked = align.Equals(TextAlignment.Justify);

                // Font family
                var ff = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
                if (ff is FontFamily fontFamily)
                    cmbFontFamily.SelectedItem = fontFamily.Source;

                // Font size
                var fsize = rtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
                if (fsize != DependencyProperty.UnsetValue)
                    cmbFontSize.Text = ((double)fsize).ToString();
            }
            finally
            {
                _suppressSelectionUpdate = false;
            }

            UpdateStatusBar();
        }

        private void rtbEditor_KeyUp(object sender, KeyEventArgs e) => UpdateStatusBar();

        // ─── Status Bar ───────────────────────────────────────────────────────
        private void SetStatus(string msg) => tbStatusMsg.Text = msg;

        private void UpdateStatusBar()
        {
            try
            {
                var pos = rtbEditor.CaretPosition;
                var line = rtbEditor.Document.ContentStart
                    .GetLineStartPosition(0, out _);
                _ = line;
                // Calculate line/col via GetOffsetToPosition
                var docStart = rtbEditor.Document.ContentStart;
                var offset = docStart.GetOffsetToPosition(rtbEditor.CaretPosition);
                tbStatusPos.Text = $"{Loc.StatusLine}: — | {Loc.StatusCol}: {offset}";
            }
            catch { /* ignore */ }
        }

        // ─── Menu Handlers ────────────────────────────────────────────────────
        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            foreach (var w in WindowManager.OpenWindows.ToList())
                w.Close();
        }

        private void LangUkr_Click(object sender, RoutedEventArgs e)
        {
            Loc.Language = AppLanguage.Ukrainian;
            Notify(nameof(IsUkrainian));
            Notify(nameof(IsEnglish));
            WindowManager.RefreshTitles();
        }

        private void LangEng_Click(object sender, RoutedEventArgs e)
        {
            Loc.Language = AppLanguage.English;
            Notify(nameof(IsUkrainian));
            Notify(nameof(IsEnglish));
            WindowManager.RefreshTitles();
        }

        private void ShowWindowList_Click(object sender, RoutedEventArgs e)
        {
            var list = string.Join("\n", WindowManager.OpenWindows.Select((w, i) =>
                $"{i + 1}. {w.Title}"));
            MessageBox.Show(list, Loc.Windows, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ─── Close / Discard Confirmation ─────────────────────────────────────
        private bool ConfirmDiscard()
        {
            if (!_isModified) return true;
            var result = MessageBox.Show(
                Loc.UnsavedMessage,
                Loc.UnsavedTitle,
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)   { Save_Executed(this, null!); return !_isModified; }
            if (result == MessageBoxResult.No)    return true;
            return false; // Cancel
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            // If all windows are closed, shut down
            if (WindowManager.OpenWindows.Count == 0)
                Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!ConfirmDiscard()) e.Cancel = true;
        }
    }
}
