using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _29
{
    public partial class Form1 : Form
    {
        string currentHost = "235.5.5.1";
        int currentLocalPort = 8001;
        int currentRemotePort = 8001;
        bool alive = false;
        UdpClient client;
        const int LOCALPORT = 8001;
        const int REMOTEPORT = 8001;
        const int TTL = 20;
        const string HOST = "235.5.5.1";
        IPAddress groupAddress;
        string userName;

        public Form1()
        {
            InitializeComponent();

            this.Text = "UDP Chat";
            groupBox1.Text = "";

            label1.Text = "Введіть ім'я";

            button1.Text = "Вхід";
            button2.Text = "Вихід";
            button3.Text = "Відправити";
            button4.Text = "Налаштування";  // використовуємо button4 з дизайнера

            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            richTextBox1.ReadOnly = true;

            groupAddress = IPAddress.Parse(HOST);

            // події
            button1.Click += loginButton_Click;
            button2.Click += logoutButton_Click;
            button3.Click += sendButton_Click;
            button4.Click += BtnSettings_Click;  // ← підключаємо button4
            button5.Text = "Зберегти лог";
            button5.Click += BtnSaveLog_Click;
            this.FormClosing += Form1_FormClosing;

        }

        private void Form1_Load(object sender, EventArgs e) { }

        // Вхід
        private void loginButton_Click(object sender, EventArgs e)
        {
            userName = maskedTextBox1.Text;
            maskedTextBox1.ReadOnly = true;

            try
            {
                client = new UdpClient(currentLocalPort);
                groupAddress = IPAddress.Parse(currentHost);
                client.JoinMulticastGroup(groupAddress, TTL);

                Task receiveTask = new Task(ReceiveMessages);
                receiveTask.Start();

                string message = userName + " увійшов у чат";
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, currentHost, currentRemotePort);

                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Приймання повідомлень
        private void ReceiveMessages()
        {
            alive = true;
            try
            {
                while (alive)
                {
                    IPEndPoint remoteIp = null;
                    byte[] data = client.Receive(ref remoteIp);
                    string message = Encoding.Unicode.GetString(data);

                    this.Invoke(new MethodInvoker(() =>
                    {
                        string time = DateTime.Now.ToShortTimeString();
                        richTextBox1.Text = time + " " + message + "\r\n" + richTextBox1.Text;
                    }));
                }
            }
            catch (ObjectDisposedException)
            {
                if (!alive) return;
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Відправити повідомлення
        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                string message = string.Format("{0}: {1}", userName, maskedTextBox2.Text);
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, HOST, REMOTEPORT);
                maskedTextBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Вихід
        private void logoutButton_Click(object sender, EventArgs e)
        {
            ExitChat();
        }

        private void ExitChat()
        {
            string message = userName + " покинув чат";
            byte[] data = Encoding.Unicode.GetBytes(message);
            client.Send(data, data.Length, HOST, REMOTEPORT);
            client.DropMulticastGroup(groupAddress);

            alive = false;
            client.Close();

            maskedTextBox1.ReadOnly = false;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (alive) ExitChat();
        }
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm(currentHost, currentLocalPort, currentRemotePort, richTextBox1.Font);
            if (sf.ShowDialog() == DialogResult.OK)
            {
                currentHost = sf.Host;
                currentLocalPort = sf.LocalPort;
                currentRemotePort = sf.RemotePort;
                richTextBox1.Font = sf.ChatFont;
                MessageBox.Show("Налаштування збережено!", "Інфо");
            }
        }
        private void BtnSaveLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Текстовий файл|*.txt",
                FileName = "chat_log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt",
                Title = "Зберегти лог чату"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllText(dlg.FileName, richTextBox1.Text, Encoding.Unicode);
                    MessageBox.Show("Лог збережено:\n" + dlg.FileName, "Збережено");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка збереження: " + ex.Message, "Помилка");
                }
            }
        }
    }
}