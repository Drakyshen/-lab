using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab16_v21
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cmbOp.SelectedIndex = 0; // Ставимо "+" за замовчуванням
        }

        // Обробка кнопки ОБЧИСЛИТИ
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                HexNumber num1 = new HexNumber(txtHex1.Text);
                HexNumber num2 = new HexNumber(txtHex2.Text);
                HexNumber res = new HexNumber();

                string operation = cmbOp.Text;

                if (operation == "+") res = num1.Add(num2);
                else if (operation == "-") res = num1.Sub(num2);
                else if (operation == "*") res = num1.Mult(num2);
                else if (operation == "/") res = num1.Div(num2);
                else if (operation == ">") { lblResult.Text = "Результат: " + num1.IsGreater(num2); return; }
                else if (operation == "==") { lblResult.Text = "Результат: " + num1.IsEqual(num2); return; }

                lblResult.Text = "Hex: " + res.Value + " | Dec: " + res.ToDecimal();
            }
            catch { MessageBox.Show("Помилка! Перевірте правильність введення."); }
        }

        // Обробка сортування масиву (Варіант 21)
        private void btnSort_Click(object sender, EventArgs e)
        {
            string[] rawInputs = txtArray.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<HexNumber> array = new List<HexNumber>();

            foreach (string s in rawInputs) array.Add(new HexNumber(s));

            // Класичне сортування бульбашкою
            for (int i = 0; i < array.Count - 1; i++)
            {
                for (int j = 0; j < array.Count - i - 1; j++)
                {
                    if (array[j].ToDecimal() < array[j + 1].ToDecimal())
                    {
                        HexNumber temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            string resultText = "Відсортовано (убування):\n";
            foreach (HexNumber h in array)
                resultText += h.Value + " (Dec: " + h.ToDecimal() + ")\n";

            MessageBox.Show(resultText, "Результат варіанту 21");
        }
    }
}