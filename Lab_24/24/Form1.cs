using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace _24
{
    public partial class Form1 : Form
    {
        Thread thread1;
        Thread thread2;
        Thread thread3;
        private CancellationTokenSource cts1;
        private CancellationTokenSource cts2;
        private CancellationTokenSource cts3;
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Запустити 1 потік";
            button2.Text = "Зупинити 1 потік";
            button3.Text = "Запустити 2 потік";
            button4.Text = "Зупинити 2 потік";
            button5.Text = "Запустити 3 потік";
            button6.Text = "Зупинити 3 потік";
            button7.Text = "Запустити всі потоки";
            button8.Text = "Зупинити всі потоки";
        }

        private void RC5_Worker(CancellationToken token)
        {
            const int w = 32;          
            const int r = 12;          
            const int b = 16;          
            const int u = w / 8;       
            const int t = 2 * (r + 1); 

            uint P32 = 0xB7E15163u;
            uint Q32 = 0x9E3779B9u;

            Random rng = new Random();
            int iteration = 0;

            while (!token.IsCancellationRequested)
            {
                iteration++;

                byte[] key = new byte[b];
                rng.NextBytes(key);

                uint A = (uint)rng.Next();
                uint B_val = (uint)rng.Next();

                uint[] L = new uint[b / u + 1];
                for (int i = b - 1; i >= 0; i--)
                    L[i / u] = (L[i / u] << 8) + key[i];

                uint[] S = new uint[t];
                S[0] = P32;
                for (int i = 1; i < t; i++)
                    S[i] = S[i - 1] + Q32;

                uint AA = 0, BB = 0;
                int jj = 0;
                int n = 3 * Math.Max(t, b / u);
                for (int k = 0; k < n; k++)
                {
                    S[AA] = RotL(S[AA] + AA + BB, 3);
                    AA = S[AA];
                    L[jj] = RotL(L[jj] + AA + BB, (int)(AA + BB));
                    BB = L[jj];
                    AA = (AA + 1) % t;
                    jj = (jj + 1) % (b / u);
                }

                A = A + S[0];
                B_val = B_val + S[1];
                for (int i = 1; i <= r; i++)
                {
                    A = RotL(A ^ B_val, (int)B_val) + S[2 * i];
                    B_val = RotL(B_val ^ A, (int)A) + S[2 * i + 1];
                }

                string result = $"[RC5 ітерація {iteration}]\r\n" +
                                $"  Ключ (hex): {BitConverter.ToString(key)}\r\n" +
                                $"  Зашифровано: A=0x{A:X8}  B=0x{B_val:X8}\r\n\r\n";

                AppendToBox(richTextBox1, result);
                Thread.Sleep(600);
            }
        }
        private static uint RotL(uint val, int shift)
        {
            shift &= 31;
            return (val << shift) | (val >> (32 - shift));
        }
        private void MD5_Worker(CancellationToken token)
        {
            Random rng = new Random();
            int iteration = 0;

            while (!token.IsCancellationRequested)
            {
                iteration++;
                int len = rng.Next(8, 33);
                byte[] buf = new byte[len];
                rng.NextBytes(buf);
                string input = Convert.ToBase64String(buf).Substring(0, len);

                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                    string hexHash = BitConverter.ToString(hash).Replace("-", "").ToLower();

                    string result = $"[MD5 ітерація {iteration}]\r\n" +
                                    $"  Вхід:  {input}\r\n" +
                                    $"  Hash:  {hexHash}\r\n\r\n";

                    AppendToBox(richTextBox2, result);
                }

                Thread.Sleep(500);
            }
        }

        private void Caesar_Worker(CancellationToken token)
        {
            string[] samples =
            {
                "ПРИВІТ СВІТ",
                "HELLO WORLD",
                "ШИФР ЦЕЗАРЯ",
                "КРИПТОГРАФІЯ",
                "SECRET MESSAGE",
                "АЛГОРИТМ ШИФРУВАННЯ",
                "БЕЗПЕКА ДАНИХ"
            };

            Random rng = new Random();
            int iteration = 0;

            while (!token.IsCancellationRequested)
            {
                iteration++;
                int shift = rng.Next(1, 26);
                string plain = samples[rng.Next(samples.Length)];
                string cipher = CaesarEncrypt(plain, shift);
                string decrypted = CaesarEncrypt(cipher, 26 - shift);

                string result = $"[Цезар ітерація {iteration}]  зсув={shift}\r\n" +
                                $"  Відкрито:    {plain}\r\n" +
                                $"  Зашифровано: {cipher}\r\n" +
                                $"  Розшифровано:{decrypted}\r\n\r\n";

                AppendToBox(richTextBox3, result);
                Thread.Sleep(700);
            }
        }

        private static string CaesarEncrypt(string text, int shift)
        {
            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c >= 'A' && c <= 'Z')
                    result[i] = (char)(((c - 'A' + shift) % 26) + 'A');
                else if (c >= 'a' && c <= 'z')
                    result[i] = (char)(((c - 'a' + shift) % 26) + 'a');
                else if (c >= 'А' && c <= 'Я')  
                    result[i] = (char)(((c - 'А' + shift) % 32) + 'А');
                else if (c >= 'а' && c <= 'я')  
                    result[i] = (char)(((c - 'а' + shift) % 32) + 'а');
                else
                    result[i] = c;
            }
            return new string(result);
        }

        private void StartThread(ref Thread t, ref CancellationTokenSource cts, Action<CancellationToken> worker)
        {
            if (t != null && t.IsAlive) return;
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            t = new Thread(() => worker(token)) { IsBackground = true };
            t.Start();
        }
 
        private void StopThread(ref CancellationTokenSource cts)
        {
            cts?.Cancel();
        }

        private void AppendToBox(RichTextBox box, string text)
        {
            if (box.InvokeRequired)
                box.Invoke(new Action(() => {
                    box.AppendText(text);
                    box.ScrollToCaret();
                }));
            else
            {
                box.AppendText(text);
                box.ScrollToCaret();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartThread(ref thread1, ref cts1, RC5_Worker);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopThread(ref cts1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartThread(ref thread2, ref cts2, MD5_Worker);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StopThread(ref cts2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StartThread(ref thread3, ref cts3, Caesar_Worker);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StopThread(ref cts3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StartThread(ref thread1, ref cts1, RC5_Worker);
            StartThread(ref thread2, ref cts2, MD5_Worker);
            StartThread(ref thread3, ref cts3, Caesar_Worker);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            StopThread(ref cts1);
            StopThread(ref cts2);
            StopThread(ref cts3);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void richTextBox2_TextChanged(object sender, EventArgs e) { }
        private void richTextBox3_TextChanged(object sender, EventArgs e) { }
    }
}
