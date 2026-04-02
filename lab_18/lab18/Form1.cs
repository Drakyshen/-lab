using System;
using System.Linq;
using System.Windows.Forms;

namespace lab18
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click_1(object sender, EventArgs e)
        {
            txtOutput.Clear();
            Random rnd = new Random();

            if (!int.TryParse(txtN.Text, out int n) || n <= 0)
            {
                MessageBox.Show("Будь ласка, введіть число n!");
                return;
            }

            double[] A = new double[n];
            for (int i = 0; i < n; i++)
            {
                A[i] = Math.Round(rnd.NextDouble() * 20 - 10, 2);
            }

            txtOutput.AppendText("Завдання 1:\r\n");
            txtOutput.AppendText("Масив: " + string.Join("  ", A) + "\r\n");

            double sumNeg = 0;
            foreach (double x in A) if (x < 0) sumNeg += x;
            txtOutput.AppendText("Сума від'ємних: " + sumNeg + "\r\n");

            int minIdx = 0, maxIdx = 0;
            for (int i = 1; i < n; i++)
            {
                if (A[i] < A[minIdx]) minIdx = i;
                if (A[i] > A[maxIdx]) maxIdx = i;
            }

            int start = Math.Min(minIdx, maxIdx);
            int end = Math.Max(minIdx, maxIdx);
            double prod = 1;
            bool hasBetween = false;
            for (int i = start + 1; i < end; i++)
            {
                prod *= A[i];
                hasBetween = true;
            }
            txtOutput.AppendText("Добуток між мін та макс: " + (hasBetween ? prod : 0) + "\r\n");

            Array.Sort(A);
            txtOutput.AppendText("Відсортовано: " + string.Join("  ", A) + "\r\n\r\n");

            txtOutput.AppendText("Завдання 2 (Матриця 3x4):\r\n");
            int[,] m = new int[3, 4];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m[i, j] = rnd.Next(10, 100);
                    txtOutput.AppendText(m[i, j] + "\t");
                }
                txtOutput.AppendText("\r\n");
            }
            txtOutput.AppendText("Правий верхній: " + m[0, 3] + "\r\n");
            txtOutput.AppendText("Лівий нижній: " + m[2, 0] + "\r\n");
        }

        private void txtN_TextChanged(object sender, EventArgs e)
        {
        }
    }
}