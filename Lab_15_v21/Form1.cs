using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lab_15_v21
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double x = double.Parse(textBoxX.Text);
                double z = (2 * (1.0 / Math.Tan(3 * x)) - Math.Log(Math.Abs(Math.Cos(x)))) / Math.Log(1 + x * x);
                MessageBox.Show("Результат Z = " + z.ToString("F4"));
            }
            catch { MessageBox.Show("Помилка в X!"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double a = double.Parse(textBoxA.Text), b = double.Parse(textBoxB.Text);
                MessageBox.Show($"Арифм. кубів: {(Math.Pow(a, 3) + Math.Pow(b, 3)) / 2:F2}\nГеом. модулів: {Math.Sqrt(Math.Abs(a * b)):F2}");
            }
            catch { MessageBox.Show("Введіть числа!"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Результат: " + (textBoxN.Text.Length == 4 && textBoxN.Text.Contains(textBoxM.Text)));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBoxN.Text.Length > 0)
            {
                char min = textBoxN.Text.Min();
                MessageBox.Show($"Мін. цифра: {min}, зустрічається: {textBoxN.Text.Count(f => f == min)}");
            }
        }

        // Завдання 4: Паралелограм (через середини діагоналей)
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string[] s = textBoxPoints.Text.Split(' ');
                double x1 = double.Parse(s[0]), y1 = double.Parse(s[1]);
                double x2 = double.Parse(s[2]), y2 = double.Parse(s[3]);
                double x3 = double.Parse(s[4]), y3 = double.Parse(s[5]);
                double x4 = double.Parse(s[6]), y4 = double.Parse(s[7]);
                bool ok = (x1 + x3 == x2 + x4) && (y1 + y3 == y2 + y4);
                MessageBox.Show("Це паралелограм: " + ok);
            }
            catch { MessageBox.Show("Введіть 8 чисел через пробіл (x1 y1 x2 y2...)!"); }
        }

        // Завдання 6: Добутки P1 та P2
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                double[] arr = textBoxArray.Text.Split(' ').Select(double.Parse).ToArray();
                double p1 = 1, p2 = 1;
                bool h1 = false, h2 = false;
                foreach (var x in arr)
                {
                    if (x < 0) { p1 *= x; h1 = true; }
                    else if (x > 0) { p2 *= x; h2 = true; }
                }
                p1 = h1 ? p1 : 0; p2 = h2 ? p2 : 0;
                string res = Math.Abs(p2) > Math.Abs(p1) ? "|P2| > |P1|" : "|P1| > |P2|";
                MessageBox.Show($"P1 (від'ємні): {p1}\nP2 (додатні): {p2}\n{res}");
            }
            catch { MessageBox.Show("Введіть числа через пробіл!"); }
        }

        // Завдання 7: Пошук слова у рядку X
        private void button7_Click(object sender, EventArgs e)
        {
            string text = textBoxX.Text;
            string word = textBoxWord.Text;
            if (string.IsNullOrEmpty(word)) return;
            int count = text.Split(' ', '.', ',').Count(w => w.Trim().ToLower() == word.ToLower());
            MessageBox.Show($"Слово '{word}' зустрічається {count} разів.");
        }
    }
}