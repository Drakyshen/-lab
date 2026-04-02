namespace Lab_15_v21
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.labelX = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelAB = new System.Windows.Forms.Label();
            this.textBoxA = new System.Windows.Forms.TextBox();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.labelNM = new System.Windows.Forms.Label();
            this.textBoxN = new System.Windows.Forms.TextBox();
            this.textBoxM = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.labelPoints = new System.Windows.Forms.Label();
            this.textBoxPoints = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.labelArray = new System.Windows.Forms.Label();
            this.textBoxArray = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.labelWord = new System.Windows.Forms.Label();
            this.textBoxWord = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
           
            // Завдання 1
            this.labelX.Text = "Введіть X:";
            this.labelX.Location = new System.Drawing.Point(20, 20);
            this.labelX.AutoSize = true;
            this.textBoxX.Location = new System.Drawing.Point(130, 17);
            this.textBoxX.Size = new System.Drawing.Size(100, 23);
            this.button1.Text = "1. Обчислити Z";
            this.button1.Location = new System.Drawing.Point(240, 16);
            this.button1.Size = new System.Drawing.Size(180, 25);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            
            // Завдання 2
            this.labelAB.Text = "Числа a, b:";
            this.labelAB.Location = new System.Drawing.Point(20, 55);
            this.labelAB.AutoSize = true;
            this.textBoxA.Location = new System.Drawing.Point(130, 52);
            this.textBoxA.Size = new System.Drawing.Size(45, 23);
            this.textBoxB.Location = new System.Drawing.Point(185, 52);
            this.textBoxB.Size = new System.Drawing.Size(45, 23);
            this.button2.Text = "2. Сер. значення";
            this.button2.Location = new System.Drawing.Point(240, 51);
            this.button2.Size = new System.Drawing.Size(180, 25);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            
            // Завдання 3 та 5
            this.labelNM.Text = "Число N, M:";
            this.labelNM.Location = new System.Drawing.Point(20, 90);
            this.labelNM.AutoSize = true;
            this.textBoxN.Location = new System.Drawing.Point(130, 87);
            this.textBoxN.Size = new System.Drawing.Size(65, 23);
            this.textBoxM.Location = new System.Drawing.Point(200, 87);
            this.textBoxM.Size = new System.Drawing.Size(30, 23);
            this.button3.Text = "3. Перевірка M";
            this.button3.Location = new System.Drawing.Point(240, 86);
            this.button3.Size = new System.Drawing.Size(180, 25);
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button5.Text = "5. К-сть мін. цифр";
            this.button5.Location = new System.Drawing.Point(240, 115);
            this.button5.Size = new System.Drawing.Size(180, 25);
            this.button5.Click += new System.EventHandler(this.button5_Click);
           
            // Завдання 4 (Паралелограм)
            this.labelPoints.Text = "Коорд (x1 y1...):";
            this.labelPoints.Location = new System.Drawing.Point(20, 155);
            this.labelPoints.AutoSize = true;
            this.textBoxPoints.Location = new System.Drawing.Point(130, 152);
            this.textBoxPoints.Size = new System.Drawing.Size(100, 23);
            this.button4.Text = "4. Паралелограм?";
            this.button4.Location = new System.Drawing.Point(240, 151);
            this.button4.Size = new System.Drawing.Size(180, 25);
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // Завдання 6 (Добутки)
            this.labelArray.Text = "Масив (1 -2 3):";
            this.labelArray.Location = new System.Drawing.Point(20, 190);
            this.labelArray.AutoSize = true;
            this.textBoxArray.Location = new System.Drawing.Point(130, 187);
            this.textBoxArray.Size = new System.Drawing.Size(100, 23);
            this.button6.Text = "6. Добутки P1, P2";
            this.button6.Location = new System.Drawing.Point(240, 186);
            this.button6.Size = new System.Drawing.Size(180, 25);
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // Завдання 7 (Слово)
            this.labelWord.Text = "Шукане слово:";
            this.labelWord.Location = new System.Drawing.Point(20, 225);
            this.labelWord.AutoSize = true;
            this.textBoxWord.Location = new System.Drawing.Point(130, 222);
            this.textBoxWord.Size = new System.Drawing.Size(100, 23);
            this.button7.Text = "7. Пошук у X";
            this.button7.Location = new System.Drawing.Point(240, 221);
            this.button7.Size = new System.Drawing.Size(180, 25);
            this.button7.Click += new System.EventHandler(this.button7_Click);
            
            this.ClientSize = new System.Drawing.Size(460, 320);
            this.Controls.Add(this.labelWord); this.Controls.Add(this.textBoxWord); this.Controls.Add(this.button7);
            this.Controls.Add(this.labelArray); this.Controls.Add(this.textBoxArray); this.Controls.Add(this.button6);
            this.Controls.Add(this.labelPoints); this.Controls.Add(this.textBoxPoints); this.Controls.Add(this.button4);
            this.Controls.Add(this.labelNM); this.Controls.Add(this.textBoxN); this.Controls.Add(this.textBoxM); this.Controls.Add(this.button3); this.Controls.Add(this.button5);
            this.Controls.Add(this.labelAB); this.Controls.Add(this.textBoxA); this.Controls.Add(this.textBoxB); this.Controls.Add(this.button2);
            this.Controls.Add(this.labelX); this.Controls.Add(this.textBoxX); this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Лабораторна 15 - Варіант 21";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelX, labelAB, labelNM, labelPoints, labelArray, labelWord;
        private System.Windows.Forms.TextBox textBoxX, textBoxA, textBoxB, textBoxN, textBoxM, textBoxPoints, textBoxArray, textBoxWord;
        private System.Windows.Forms.Button button1, button2, button3, button4, button5, button6, button7;
    }
}