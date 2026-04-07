namespace Lab20
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBoxInputs = new System.Windows.Forms.GroupBox();
            this.txtA = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtC = new System.Windows.Forms.TextBox();
            this.lblA = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.lblC = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.panelResult = new System.Windows.Forms.Panel();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblResHeader = new System.Windows.Forms.Label();
            this.groupBoxInputs.SuspendLayout();
            this.panelResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInputs
            // 
            this.groupBoxInputs.Controls.Add(this.lblC);
            this.groupBoxInputs.Controls.Add(this.lblB);
            this.groupBoxInputs.Controls.Add(this.lblA);
            this.groupBoxInputs.Controls.Add(this.txtC);
            this.groupBoxInputs.Controls.Add(this.txtB);
            this.groupBoxInputs.Controls.Add(this.txtA);
            this.groupBoxInputs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxInputs.Location = new System.Drawing.Point(20, 20);
            this.groupBoxInputs.Name = "groupBoxInputs";
            this.groupBoxInputs.Size = new System.Drawing.Size(240, 150);
            this.groupBoxInputs.TabIndex = 0;
            this.groupBoxInputs.TabStop = false;
            this.groupBoxInputs.Text = "Параметри трикутника";
            // 
            // lblA, lblB, lblC (Налаштування міток)
            // 
            this.lblA.Text = "Сторона A:"; this.lblA.Location = new System.Drawing.Point(15, 35);
            this.lblB.Text = "Сторона B:"; this.lblB.Location = new System.Drawing.Point(15, 70);
            this.lblC.Text = "Сторона C:"; this.lblC.Location = new System.Drawing.Point(15, 105);
            // 
            // txtA, txtB, txtC (Налаштування полів)
            // 
            this.txtA.Location = new System.Drawing.Point(110, 32); this.txtA.Name = "txtA"; this.txtA.Size = new System.Drawing.Size(100, 27);
            this.txtB.Location = new System.Drawing.Point(110, 67); this.txtB.Name = "txtB"; this.txtB.Size = new System.Drawing.Size(100, 27);
            this.txtC.Location = new System.Drawing.Point(110, 102); this.txtC.Name = "txtC"; this.txtC.Size = new System.Drawing.Size(100, 27);
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.Location = new System.Drawing.Point(20, 185);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(240, 45);
            this.btnCalculate.Text = "ОБЧИСЛИТИ ПЛОЩУ";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // panelResult
            // 
            this.panelResult.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResult.Controls.Add(this.txtResult);
            this.panelResult.Controls.Add(this.lblResHeader);
            this.panelResult.Location = new System.Drawing.Point(20, 245);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(240, 80);
            // 
            // lblResHeader
            // 
            this.lblResHeader.AutoSize = true;
            this.lblResHeader.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblResHeader.Location = new System.Drawing.Point(5, 5);
            this.lblResHeader.Text = "Результат обчислень:";
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtResult.Location = new System.Drawing.Point(10, 30);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(220, 27);
            this.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 350);
            this.Controls.Add(this.panelResult);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.groupBoxInputs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Triangle Area Pro";
            this.groupBoxInputs.ResumeLayout(false);
            this.groupBoxInputs.PerformLayout();
            this.panelResult.ResumeLayout(false);
            this.panelResult.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInputs;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblResHeader;
    }
}