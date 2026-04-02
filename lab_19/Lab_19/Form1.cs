using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab_19
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
         
            string input = txtInput.Text;

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Будь ласка, введіть текст!");
                return;
            }

            
            string result = Regex.Replace(input, @"\s+", " ");

           
            txtOutput.Text = result.Trim();
        }
    }
}