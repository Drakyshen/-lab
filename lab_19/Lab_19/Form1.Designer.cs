namespace Lab_19
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
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            
            this.txtInput.Location = new System.Drawing.Point(30, 30);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(500, 22);
            this.txtInput.TabIndex = 0;
            this.btnExecute.Location = new System.Drawing.Point(30, 70);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(120, 40);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "Виконати";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            
            this.txtOutput.Location = new System.Drawing.Point(30, 130);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(500, 150);
            this.txtOutput.TabIndex = 2;
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 320);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.txtInput);
            this.Name = "Form1";
            this.Text = "Лабораторна 19 - Варіант 1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox txtOutput;
    }
}