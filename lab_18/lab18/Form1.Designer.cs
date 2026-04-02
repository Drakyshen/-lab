namespace lab18
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

      
        /// <param name="disposing"
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
            this.txtN = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            
            this.txtN.Location = new System.Drawing.Point(45, 45);
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(140, 22);
            this.txtN.TabIndex = 0;
            this.txtN.TextChanged += new System.EventHandler(this.txtN_TextChanged);
            
            this.btnRun.Location = new System.Drawing.Point(45, 94);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(140, 43);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Виконати";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click_1);
             
            this.txtOutput.Location = new System.Drawing.Point(276, 44);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(473, 354);
            this.txtOutput.TabIndex = 2;
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtN);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtN;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtOutput;
    }
}

