namespace Lab16_v21
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtHex1 = new System.Windows.Forms.TextBox();
            this.txtHex2 = new System.Windows.Forms.TextBox();
            this.cmbOp = new System.Windows.Forms.ComboBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtArray = new System.Windows.Forms.TextBox();
            this.btnSort = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // Поля вводу
            this.label1.Text = "Введіть Hex числа:";
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.AutoSize = true;

            this.txtHex1.Location = new System.Drawing.Point(20, 40);
            this.txtHex1.Size = new System.Drawing.Size(80, 25);

            this.cmbOp.Items.AddRange(new object[] { "+", "-", "*", "/", ">", "==" });
            this.cmbOp.Location = new System.Drawing.Point(105, 40);
            this.cmbOp.Size = new System.Drawing.Size(45, 25);
            this.cmbOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.txtHex2.Location = new System.Drawing.Point(155, 40);
            this.txtHex2.Size = new System.Drawing.Size(80, 25);

            // КНОПКА ОБЧИСЛИТИ (Акцент)
            this.btnCalculate.Text = "ОБЧИСЛИТИ";
            this.btnCalculate.Location = new System.Drawing.Point(250, 35);
            this.btnCalculate.Size = new System.Drawing.Size(120, 35);
            this.btnCalculate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

            this.lblResult.Text = "Результат з'явиться тут...";
            this.lblResult.Location = new System.Drawing.Point(20, 85);
            this.lblResult.Size = new System.Drawing.Size(350, 40);
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResult.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Блок масиву
            this.label2.Text = "Масив для сортування (через пробіл):";
            this.label2.Location = new System.Drawing.Point(20, 150);
            this.label2.AutoSize = true;

            this.txtArray.Location = new System.Drawing.Point(20, 170);
            this.txtArray.Size = new System.Drawing.Size(350, 25);

            this.btnSort.Text = "ВІДСОРТУВАТИ МАСИВ (21 варіант)";
            this.btnSort.Location = new System.Drawing.Point(20, 205);
            this.btnSort.Size = new System.Drawing.Size(350, 35);
            this.btnSort.BackColor = System.Drawing.Color.LightGray;
            this.btnSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);

            // Форма
            this.ClientSize = new System.Drawing.Size(400, 270);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.txtHex1, this.txtHex2, this.cmbOp, this.btnCalculate,
                this.lblResult, this.txtArray, this.btnSort, this.label1, this.label2
            });
            this.Name = "Form1";
            this.Text = "Lab 16: Hex Sorter v21";
            this.BackColor = System.Drawing.Color.White;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtHex1, txtHex2, txtArray;
        private System.Windows.Forms.Button btnCalculate, btnSort;
        private System.Windows.Forms.Label lblResult, label1, label2;
        private System.Windows.Forms.ComboBox cmbOp;
    }
}