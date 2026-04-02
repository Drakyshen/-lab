namespace Lab17_v1
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
            this.cmbFigureType = new System.Windows.Forms.ComboBox();
            this.txtParam1 = new System.Windows.Forms.TextBox();
            this.txtParam2 = new System.Windows.Forms.TextBox();
            this.txtParam3 = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.cmbFigureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFigureType.FormattingEnabled = true;
            this.cmbFigureType.Items.AddRange(new object[] { "Прямокутник", "Коло", "Трапеція" });
            this.cmbFigureType.Location = new System.Drawing.Point(20, 40);
            this.cmbFigureType.Size = new System.Drawing.Size(200, 25);
            this.cmbFigureType.SelectedIndexChanged += new System.EventHandler(this.cmbFigureType_SelectedIndexChanged);

            
            this.lblDesc.Text = "Введіть параметри:";
            this.lblDesc.Location = new System.Drawing.Point(20, 80);
            this.lblDesc.AutoSize = true;

            
            this.txtParam1.Location = new System.Drawing.Point(20, 100);
            this.txtParam1.Size = new System.Drawing.Size(100, 25);

            this.txtParam2.Location = new System.Drawing.Point(130, 100);
            this.txtParam2.Size = new System.Drawing.Size(100, 25);

            this.txtParam3.Location = new System.Drawing.Point(240, 100);
            this.txtParam3.Size = new System.Drawing.Size(100, 25);

            this.btnCalculate.Text = "ОБЧИСЛИТИ";
            this.btnCalculate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Location = new System.Drawing.Point(20, 145);
            this.btnCalculate.Size = new System.Drawing.Size(320, 40);
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

            this.lblResult.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblResult.Location = new System.Drawing.Point(20, 200);
            this.lblResult.Size = new System.Drawing.Size(320, 80);
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResult.Text = "Оберіть фігуру та введіть дані";

          
            this.ClientSize = new System.Drawing.Size(370, 300);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.cmbFigureType, this.txtParam1, this.txtParam2,
                this.txtParam3, this.btnCalculate, this.lblResult, this.lblDesc
            });
            this.Name = "Form1";
            this.Text = "Лаб 17: Фігури (Варіант 1)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox cmbFigureType;
        private System.Windows.Forms.TextBox txtParam1, txtParam2, txtParam3;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblResult, lblDesc;
    }
}