using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _23
{
    public partial class Form1 : Form
    {
        private double _coefA;
        private double _coefB;
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Намалювати";
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            int cx = width / 2;
            int cy = height / 2;

            float scale = 40f;

            Pen axisPen = new Pen(Color.Black, 2);

            g.DrawLine(axisPen, 0, cy, width, cy);

            g.DrawLine(axisPen, cx, 0, cx, height);

            g.DrawLine(axisPen, width - 10, cy - 5, width, cy);
            g.DrawLine(axisPen, width - 10, cy + 5, width, cy);
            g.DrawLine(axisPen, cx - 5, 10, cx, 0);
            g.DrawLine(axisPen, cx + 5, 10, cx, 0);

            Font axisFont = new Font("Arial", 10, FontStyle.Bold);
            g.DrawString("X", axisFont, Brushes.Black, width - 20, cy + 5);
            g.DrawString("Y", axisFont, Brushes.Black, cx + 5, 5);

            Font tickFont = new Font("Arial", 7);
            Pen tickPen = new Pen(Color.Gray, 1);
            tickPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            for (int i = -20; i <= 20; i++)
            {
                if (i == 0) continue;

                int px = cx + (int)(i * scale);
                int py = cy - (int)(i * scale);

                if (px > 0 && px < width)
                {
                    g.DrawLine(tickPen, px, 0, px, height);
                    g.DrawLine(new Pen(Color.Black, 1), px, cy - 4, px, cy + 4);
                    if (i % 2 == 0)
                        g.DrawString(i.ToString(), tickFont, Brushes.Black, px - 5, cy + 6);
                }

                if (py > 0 && py < height)
                {
                    g.DrawLine(tickPen, 0, py, width, py);
                    g.DrawLine(new Pen(Color.Black, 1), cx - 4, py, cx + 4, py);
                    if (i % 2 == 0)
                        g.DrawString(i.ToString(), tickFont, Brushes.Black, cx + 6, py - 6);
                }
            }

            if (_coefA == 0 && _coefB == 0) return;

            Pen curvePen = new Pen(Color.Blue, 2);
            List<PointF> points = new List<PointF>();

            int steps = 1000;
            double tMax = 4 * Math.PI;

            for (int i = 0; i <= steps; i++)
            {
                double t = (tMax / steps) * i;

                double worldX = _coefA * (Math.Sqrt(t) - Math.Sin(t));
                double worldY = _coefB * (t - Math.Cos(t));

                float screenX = cx + (float)(worldX * scale);
                float screenY = cy - (float)(worldY * scale);

                points.Add(new PointF(screenX, screenY));
            }

            if (points.Count > 1)
                g.DrawLines(curvePen, points.ToArray());
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double a) &&
                double.TryParse(textBox2.Text, out double b))
            {
                _coefA = a;
                _coefB = b;
                this.Invalidate();
            }
            else
            {
                MessageBox.Show("Введіть коректні числа!");
            }
        }
    }


}
