namespace _26
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.textBoxReplace = new System.Windows.Forms.TextBox();
            this.listBoxTemplates = new System.Windows.Forms.ListBox();
            this.labelTemplates = new System.Windows.Forms.Label();
            this.labelFind = new System.Windows.Forms.Label();
            this.labelReplace = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // ── Ліва колонка: поля вводу ─────────────────────────────────

            // label1 — ПІБ
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Text = "label1";

            // textBox1
            this.textBox1.Location = new System.Drawing.Point(230, 27);
            this.textBox1.Size = new System.Drawing.Size(380, 26);
            this.textBox1.TabIndex = 1;

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 80);
            this.label2.Text = "label2";

            // textBox2
            this.textBox2.Location = new System.Drawing.Point(230, 77);
            this.textBox2.Size = new System.Drawing.Size(380, 26);
            this.textBox2.TabIndex = 2;

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 130);
            this.label3.Text = "label3";

            // textBox3
            this.textBox3.Location = new System.Drawing.Point(230, 127);
            this.textBox3.Size = new System.Drawing.Size(380, 26);
            this.textBox3.TabIndex = 3;

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 180);
            this.label4.Text = "label4";

            // textBox4
            this.textBox4.Location = new System.Drawing.Point(230, 177);
            this.textBox4.Size = new System.Drawing.Size(380, 26);
            this.textBox4.TabIndex = 4;

            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 230);
            this.label5.Text = "label5";

            // textBox5
            this.textBox5.Location = new System.Drawing.Point(230, 227);
            this.textBox5.Size = new System.Drawing.Size(380, 26);
            this.textBox5.TabIndex = 5;

            // ── Права колонка: список шаблонів ────────────────────────────

            // labelTemplates
            this.labelTemplates.AutoSize = true;
            this.labelTemplates.Location = new System.Drawing.Point(650, 15);
            this.labelTemplates.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.labelTemplates.Text = "Оберіть шаблон:";

            // listBoxTemplates — великий, щоб добре видно
            this.listBoxTemplates.Location = new System.Drawing.Point(650, 40);
            this.listBoxTemplates.Size = new System.Drawing.Size(340, 200);
            this.listBoxTemplates.Font = new System.Drawing.Font("Segoe UI", 11f);
            this.listBoxTemplates.ItemHeight = 28;
            this.listBoxTemplates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxTemplates.TabIndex = 6;

            // ── Рядок кнопок ──────────────────────────────────────────────

            // button1 — Створити наказ
            this.button1.Location = new System.Drawing.Point(20, 290);
            this.button1.Size = new System.Drawing.Size(160, 50);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // buttonPreview — Переглянути
            this.buttonPreview.Location = new System.Drawing.Point(200, 290);
            this.buttonPreview.Size = new System.Drawing.Size(160, 50);
            this.buttonPreview.TabIndex = 11;
            this.buttonPreview.Text = "button2";
            this.buttonPreview.UseVisualStyleBackColor = true;

            // buttonSave — Зберегти як
            this.buttonSave.Location = new System.Drawing.Point(380, 290);
            this.buttonSave.Size = new System.Drawing.Size(160, 50);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "button3";
            this.buttonSave.UseVisualStyleBackColor = true;

            // ── Рядок пошуку/заміни ───────────────────────────────────────

            // labelFind
            this.labelFind.AutoSize = true;
            this.labelFind.Location = new System.Drawing.Point(20, 375);
            this.labelFind.Text = "Знайти:";

            // textBoxFind
            this.textBoxFind.Location = new System.Drawing.Point(100, 372);
            this.textBoxFind.Size = new System.Drawing.Size(200, 26);
            this.textBoxFind.TabIndex = 13;

            // labelReplace
            this.labelReplace.AutoSize = true;
            this.labelReplace.Location = new System.Drawing.Point(320, 375);
            this.labelReplace.Text = "Замінити:";

            // textBoxReplace
            this.textBoxReplace.Location = new System.Drawing.Point(410, 372);
            this.textBoxReplace.Size = new System.Drawing.Size(200, 26);
            this.textBoxReplace.TabIndex = 14;

            // buttonReplace
            this.buttonReplace.Location = new System.Drawing.Point(630, 365);
            this.buttonReplace.Size = new System.Drawing.Size(160, 40);
            this.buttonReplace.TabIndex = 15;
            this.buttonReplace.Text = "button4";
            this.buttonReplace.UseVisualStyleBackColor = true;

            // ── Форма ────────────────────────────────────────────────────

            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 440);
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.labelTemplates);
            this.Controls.Add(this.listBoxTemplates);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonPreview);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelFind);
            this.Controls.Add(this.textBoxFind);
            this.Controls.Add(this.labelReplace);
            this.Controls.Add(this.textBoxReplace);
            this.Controls.Add(this.buttonReplace);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.TextBox textBoxFind;
        private System.Windows.Forms.TextBox textBoxReplace;
        private System.Windows.Forms.ListBox listBoxTemplates;
        private System.Windows.Forms.Label labelTemplates;
        private System.Windows.Forms.Label labelFind;
        private System.Windows.Forms.Label labelReplace;
    }
}
