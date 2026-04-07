using System;
using System.Windows.Forms;

namespace Lab20
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
              
                double a = double.Parse(txtA.Text);
                double b = double.Parse(txtB.Text);
                double c = double.Parse(txtC.Text);

              
                if (a <= 0 || b <= 0 || c <= 0)
                {
                    throw new Exception("Сторони мають бути більші за нуль!");
                }

                if (a + b <= c || a + c <= b || b + c <= a)
                {
                    throw new Exception("Трикутник із такими сторонами неможливий!");
                }

              
                double p = (a + b + c) / 2;
                double area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

                txtResult.Text = $"Площа: {area:F2}";
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть числа, а не текст!", "Помилка формату");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка");
            }
            finally
            {
                
                this.Text = "Обчислення завершено";
            }
        }
    }
}