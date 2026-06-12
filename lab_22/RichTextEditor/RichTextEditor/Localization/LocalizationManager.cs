using System.ComponentModel;

namespace RichTextEditor.Localization
{
    public enum AppLanguage { Ukrainian, English }

    public class LocalizationManager : INotifyPropertyChanged
    {
        private static LocalizationManager? _instance;
        public static LocalizationManager Instance => _instance ??= new LocalizationManager();

        private AppLanguage _language = AppLanguage.Ukrainian;

        public AppLanguage Language
        {
            get => _language;
            set { _language = value; OnPropertyChanged(null!); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // File menu
        public string File        => _language == AppLanguage.Ukrainian ? "Файл"         : "File";
        public string New         => _language == AppLanguage.Ukrainian ? "Новий"        : "New";
        public string NewWindow   => _language == AppLanguage.Ukrainian ? "Нове вікно"   : "New Window";
        public string Open        => _language == AppLanguage.Ukrainian ? "Відкрити"     : "Open";
        public string Save        => _language == AppLanguage.Ukrainian ? "Зберегти"     : "Save";
        public string SaveAs      => _language == AppLanguage.Ukrainian ? "Зберегти як…" : "Save As…";
        public string Close       => _language == AppLanguage.Ukrainian ? "Закрити"      : "Close";
        public string Exit        => _language == AppLanguage.Ukrainian ? "Вихід"        : "Exit";

        // Edit menu
        public string Edit        => _language == AppLanguage.Ukrainian ? "Правка"       : "Edit";
        public string Undo        => _language == AppLanguage.Ukrainian ? "Скасувати"    : "Undo";
        public string Redo        => _language == AppLanguage.Ukrainian ? "Повторити"    : "Redo";
        public string Cut         => _language == AppLanguage.Ukrainian ? "Вирізати"     : "Cut";
        public string Copy        => _language == AppLanguage.Ukrainian ? "Копіювати"    : "Copy";
        public string Paste       => _language == AppLanguage.Ukrainian ? "Вставити"     : "Paste";
        public string SelectAll   => _language == AppLanguage.Ukrainian ? "Виділити все" : "Select All";

        // Insert menu
        public string Insert      => _language == AppLanguage.Ukrainian ? "Вставка"      : "Insert";
        public string InsertImage => _language == AppLanguage.Ukrainian ? "Зображення…"  : "Image…";

        // Format menu
        public string Format      => _language == AppLanguage.Ukrainian ? "Формат"       : "Format";
        public string AlignLeft   => _language == AppLanguage.Ukrainian ? "По лівому краю"  : "Align Left";
        public string AlignCenter => _language == AppLanguage.Ukrainian ? "По центру"    : "Center";
        public string AlignRight  => _language == AppLanguage.Ukrainian ? "По правому краю" : "Align Right";
        public string AlignJustify=> _language == AppLanguage.Ukrainian ? "По ширині"    : "Justify";
        public string FontColor   => _language == AppLanguage.Ukrainian ? "Колір тексту…": "Font Color…";

        // Language menu
        public string LanguageMenu => _language == AppLanguage.Ukrainian ? "Мова"        : "Language";
        public string LangUkrainian => _language == AppLanguage.Ukrainian ? "Українська" : "Ukrainian";
        public string LangEnglish  => _language == AppLanguage.Ukrainian ? "Англійська"  : "English";

        // View
        public string View        => _language == AppLanguage.Ukrainian ? "Вигляд"       : "View";
        public string Windows     => _language == AppLanguage.Ukrainian ? "Вікна"        : "Windows";

        // Toolbar tooltips
        public string TtOpen      => _language == AppLanguage.Ukrainian ? "Відкрити (Ctrl+O)"   : "Open (Ctrl+O)";
        public string TtSave      => _language == AppLanguage.Ukrainian ? "Зберегти (Ctrl+S)"   : "Save (Ctrl+S)";
        public string TtBold      => _language == AppLanguage.Ukrainian ? "Жирний (Ctrl+B)"     : "Bold (Ctrl+B)";
        public string TtItalic    => _language == AppLanguage.Ukrainian ? "Курсив (Ctrl+I)"     : "Italic (Ctrl+I)";
        public string TtUnderline => _language == AppLanguage.Ukrainian ? "Підкреслений (Ctrl+U)": "Underline (Ctrl+U)";
        public string TtAlignLeft   => _language == AppLanguage.Ukrainian ? "По лівому краю"    : "Align Left";
        public string TtAlignCenter => _language == AppLanguage.Ukrainian ? "По центру"         : "Center";
        public string TtAlignRight  => _language == AppLanguage.Ukrainian ? "По правому краю"   : "Align Right";
        public string TtAlignJustify=> _language == AppLanguage.Ukrainian ? "По ширині"         : "Justify";
        public string TtFontColor   => _language == AppLanguage.Ukrainian ? "Колір тексту"      : "Font Color";
        public string TtInsertImage => _language == AppLanguage.Ukrainian ? "Вставити зображення": "Insert Image";

        // Status
        public string StatusReady => _language == AppLanguage.Ukrainian ? "Готово"       : "Ready";
        public string StatusSaved => _language == AppLanguage.Ukrainian ? "Збережено"    : "Saved";
        public string StatusLine  => _language == AppLanguage.Ukrainian ? "Рядок"        : "Line";
        public string StatusCol   => _language == AppLanguage.Ukrainian ? "Стовпець"     : "Col";

        // Dialogs
        public string UnsavedTitle   => _language == AppLanguage.Ukrainian ? "Незбережені зміни"  : "Unsaved Changes";
        public string UnsavedMessage => _language == AppLanguage.Ukrainian
            ? "Документ містить незбережені зміни. Зберегти?"
            : "The document has unsaved changes. Save?";
        public string Yes  => _language == AppLanguage.Ukrainian ? "Так"  : "Yes";
        public string No   => _language == AppLanguage.Ukrainian ? "Ні"   : "No";
        public string Cancel => _language == AppLanguage.Ukrainian ? "Скасувати" : "Cancel";
        public string Untitled => _language == AppLanguage.Ukrainian ? "Без назви" : "Untitled";
        public string AppTitle => "RTF Editor";
        public string FontFamily => _language == AppLanguage.Ukrainian ? "Шрифт" : "Font";
        public string FontSize   => _language == AppLanguage.Ukrainian ? "Розмір" : "Size";
    }
}
