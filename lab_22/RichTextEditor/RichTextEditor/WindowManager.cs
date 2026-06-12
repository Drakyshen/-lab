using System.Collections.Generic;
using System.Linq;

namespace RichTextEditor
{
    /// <summary>
    /// Tracks all open editor windows and provides helper methods.
    /// </summary>
    public static class WindowManager
    {
        private static readonly List<EditorWindow> _windows = new();

        public static IReadOnlyList<EditorWindow> OpenWindows => _windows;

        public static void Register(EditorWindow w)
        {
            _windows.Add(w);
            w.Closed += (_, _) => _windows.Remove(w);
            RefreshTitles();
        }

        public static void RefreshTitles()
        {
            for (int i = 0; i < _windows.Count; i++)
                _windows[i].RefreshTitle(i + 1, _windows.Count);
        }

        public static EditorWindow CreateNewWindow()
        {
            var w = new EditorWindow();
            Register(w);
            w.Show();
            return w;
        }
    }
}
