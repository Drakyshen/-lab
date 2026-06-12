using System.Windows;

namespace RichTextEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Open the first real editor window
            var editor = new EditorWindow();
            WindowManager.Register(editor);
            editor.Show();
            // Hide this invisible host window
            Hide();
        }
    }
}
