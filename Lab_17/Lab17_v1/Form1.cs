using System;
using System.Windows.Forms;

namespace Lab17_v1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (cmbFigureType.Items.Count > 0)
                cmbFigureType.SelectedIndex = 0;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                Figure figure = null;

                if (!double.TryParse(txtParam1.Text, out double p1))
                {
                    MessageBox.Show("Введіть перше число!");
                    return;
                }
                switch (cmbFigureType.SelectedIndex)
                {
                    case 0: 
                        double p2_rect = double.Parse(txtParam2.Text);
                        figure = new Rectangle(p1, p2_rect);
                        break;

                    case 1: 
                        figure = new Circle(p1);
                        break;

                    case 2: 
                        double p2_trap = double.Parse(txtParam2.Text);
                        double p3_trap = double.Parse(txtParam3.Text);
                        figure = new Trapezium(p1, p2_trap, p3_trap);
                        break;
                }

                if (figure != null)
                {
                    lblResult.Text = $"Фігура: {figure.Name}\n" +
                                     $"Площа: {figure.GetArea():F2}\n" +
                                     $"Периметр: {figure.GetPerimeter():F2}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: перевірте, чи всі поля заповнені числами!");
            }
        }
        private void cmbFigureType_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            if (cmbFigureType.SelectedIndex == 1)
            {
                txtParam2.Visible = false;
                txtParam3.Visible = false;
                lblDesc.Text = "Введіть радіус:";
            }
            
            else if (cmbFigureType.SelectedIndex == 0)
            {
                txtParam2.Visible = true;
                txtParam3.Visible = false;
                lblDesc.Text = "Введіть сторони A та B:";
            }
            
            else
            {
                txtParam2.Visible = true;
                txtParam3.Visible = true;
                lblDesc.Text = "Основи A, B та бічна сторона C:";
            }
        }
    }
}