using System.Windows.Input;

namespace RichTextEditor
{
    public static class EditorCommands
    {
        public static readonly RoutedUICommand SaveAs = new(
            "Save As", "SaveAs", typeof(EditorCommands),
            new InputGestureCollection { new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift) });

        public static readonly RoutedUICommand NewWindow = new(
            "New Window", "NewWindow", typeof(EditorCommands),
            new InputGestureCollection { new KeyGesture(Key.N, ModifierKeys.Control | ModifierKeys.Shift) });

        public static readonly RoutedUICommand InsertImage = new(
            "Insert Image", "InsertImage", typeof(EditorCommands));

        public static readonly RoutedUICommand FontColor = new(
            "Font Color", "FontColor", typeof(EditorCommands));
    }
}
