using System;
using System.Drawing;
using System.Windows.Forms;

namespace MDITextEditor
{
    public class DarkMenuRenderer : ToolStripProfessionalRenderer
    {
        public DarkMenuRenderer() : base(new DarkColorTable()) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                var r = new Rectangle(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(62, 62, 66)), r);
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Color.FromArgb(241, 241, 241);
            base.OnRenderItemText(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
    }

    public class DarkColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected => Color.FromArgb(62, 62, 66);
        public override Color MenuItemBorder => Color.FromArgb(62, 62, 66);
        public override Color MenuBorder => Color.FromArgb(60, 60, 60);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(62, 62, 66);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(62, 62, 66);
        public override Color MenuItemPressedGradientBegin => Color.FromArgb(37, 37, 38);
        public override Color MenuItemPressedGradientEnd => Color.FromArgb(37, 37, 38);
        public override Color MenuStripGradientBegin => Color.FromArgb(37, 37, 38);
        public override Color MenuStripGradientEnd => Color.FromArgb(37, 37, 38);
        public override Color ToolStripDropDownBackground => Color.FromArgb(37, 37, 38);
        public override Color ImageMarginGradientBegin => Color.FromArgb(37, 37, 38);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(37, 37, 38);
        public override Color ImageMarginGradientEnd => Color.FromArgb(37, 37, 38);
        public override Color SeparatorDark => Color.FromArgb(68, 68, 68);
        public override Color SeparatorLight => Color.FromArgb(68, 68, 68);
        public override Color ToolStripGradientBegin => Color.FromArgb(37, 37, 38);
        public override Color ToolStripGradientMiddle => Color.FromArgb(37, 37, 38);
        public override Color ToolStripGradientEnd => Color.FromArgb(37, 37, 38);
        public override Color ButtonSelectedHighlight => Color.FromArgb(62, 62, 66);
        public override Color ButtonSelectedBorder => Color.FromArgb(62, 62, 66);
        public override Color StatusStripGradientBegin => Color.FromArgb(0, 122, 204);
        public override Color StatusStripGradientEnd => Color.FromArgb(0, 122, 204);
    }

    public static class RichTextBoxExtensions
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0x000B;

        public static void BeginUpdate(this RichTextBox rtb)
        {
            SendMessage(rtb.Handle, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
        }

        public static void EndUpdate(this RichTextBox rtb)
        {
            SendMessage(rtb.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            rtb.Invalidate();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
