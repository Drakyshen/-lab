namespace _25
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewPlants = new System.Windows.Forms.DataGridView();
            this.dataGridViewProps = new System.Windows.Forms.DataGridView();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtLatin = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.btnAddPlant = new System.Windows.Forms.Button();
            this.btnDeletePlant = new System.Windows.Forms.Button();
            this.cmbSearchField = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridViewSearch = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridViewReport1 = new System.Windows.Forms.DataGridView();
            this.btnReport1 = new System.Windows.Forms.Button();
            this.txtReport1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewReport2 = new System.Windows.Forms.DataGridView();
            this.txtReport2 = new System.Windows.Forms.TextBox();
            this.btnReport2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPropUsage = new System.Windows.Forms.TextBox();
            this.txtPropEffect = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAddProp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearch)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPlants
            // 
            this.dataGridViewPlants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlants.Location = new System.Drawing.Point(1393, 4);
            this.dataGridViewPlants.Name = "dataGridViewPlants";
            this.dataGridViewPlants.RowHeadersWidth = 62;
            this.dataGridViewPlants.RowTemplate.Height = 28;
            this.dataGridViewPlants.Size = new System.Drawing.Size(469, 302);
            this.dataGridViewPlants.TabIndex = 1;
            // 
            // dataGridViewProps
            // 
            this.dataGridViewProps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProps.Location = new System.Drawing.Point(1393, 312);
            this.dataGridViewProps.Name = "dataGridViewProps";
            this.dataGridViewProps.RowHeadersWidth = 62;
            this.dataGridViewProps.Size = new System.Drawing.Size(463, 302);
            this.dataGridViewProps.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(21, 46);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 26);
            this.txtName.TabIndex = 3;
            // 
            // txtLatin
            // 
            this.txtLatin.Location = new System.Drawing.Point(21, 124);
            this.txtLatin.Name = "txtLatin";
            this.txtLatin.Size = new System.Drawing.Size(100, 26);
            this.txtLatin.TabIndex = 4;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(21, 184);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(100, 26);
            this.txtDesc.TabIndex = 5;
            // 
            // btnAddPlant
            // 
            this.btnAddPlant.Location = new System.Drawing.Point(212, 32);
            this.btnAddPlant.Name = "btnAddPlant";
            this.btnAddPlant.Size = new System.Drawing.Size(144, 40);
            this.btnAddPlant.TabIndex = 6;
            this.btnAddPlant.Text = "button1";
            this.btnAddPlant.UseVisualStyleBackColor = true;
            // 
            // btnDeletePlant
            // 
            this.btnDeletePlant.Location = new System.Drawing.Point(212, 90);
            this.btnDeletePlant.Name = "btnDeletePlant";
            this.btnDeletePlant.Size = new System.Drawing.Size(144, 42);
            this.btnDeletePlant.TabIndex = 7;
            this.btnDeletePlant.Text = "button2";
            this.btnDeletePlant.UseVisualStyleBackColor = true;
            // 
            // cmbSearchField
            // 
            this.cmbSearchField.FormattingEnabled = true;
            this.cmbSearchField.Items.AddRange(new object[] {
            "name",
            "latin_name",
            "description"});
            this.cmbSearchField.Location = new System.Drawing.Point(38, 232);
            this.cmbSearchField.Name = "cmbSearchField";
            this.cmbSearchField.Size = new System.Drawing.Size(121, 28);
            this.cmbSearchField.TabIndex = 8;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(38, 280);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 26);
            this.txtSearch.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(38, 328);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(161, 40);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "button1";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSearch
            // 
            this.dataGridViewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSearch.Location = new System.Drawing.Point(12, 445);
            this.dataGridViewSearch.Name = "dataGridViewSearch";
            this.dataGridViewSearch.RowHeadersWidth = 62;
            this.dataGridViewSearch.RowTemplate.Height = 28;
            this.dataGridViewSearch.Size = new System.Drawing.Size(469, 302);
            this.dataGridViewSearch.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewReport1);
            this.tabPage1.Controls.Add(this.btnReport1);
            this.tabPage1.Controls.Add(this.txtReport1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(527, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewReport1
            // 
            this.dataGridViewReport1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport1.Location = new System.Drawing.Point(32, 78);
            this.dataGridViewReport1.Name = "dataGridViewReport1";
            this.dataGridViewReport1.RowHeadersWidth = 62;
            this.dataGridViewReport1.RowTemplate.Height = 28;
            this.dataGridViewReport1.Size = new System.Drawing.Size(469, 302);
            this.dataGridViewReport1.TabIndex = 8;
            // 
            // btnReport1
            // 
            this.btnReport1.Location = new System.Drawing.Point(139, 15);
            this.btnReport1.Name = "btnReport1";
            this.btnReport1.Size = new System.Drawing.Size(263, 40);
            this.btnReport1.TabIndex = 8;
            this.btnReport1.Text = "button1";
            this.btnReport1.UseVisualStyleBackColor = true;
            // 
            // txtReport1
            // 
            this.txtReport1.Location = new System.Drawing.Point(6, 15);
            this.txtReport1.Name = "txtReport1";
            this.txtReport1.Size = new System.Drawing.Size(100, 26);
            this.txtReport1.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(856, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(535, 439);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewReport2);
            this.tabPage2.Controls.Add(this.txtReport2);
            this.tabPage2.Controls.Add(this.btnReport2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(527, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewReport2
            // 
            this.dataGridViewReport2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport2.Location = new System.Drawing.Point(32, 83);
            this.dataGridViewReport2.Name = "dataGridViewReport2";
            this.dataGridViewReport2.RowHeadersWidth = 62;
            this.dataGridViewReport2.Size = new System.Drawing.Size(463, 302);
            this.dataGridViewReport2.TabIndex = 8;
            // 
            // txtReport2
            // 
            this.txtReport2.Location = new System.Drawing.Point(32, 35);
            this.txtReport2.Name = "txtReport2";
            this.txtReport2.Size = new System.Drawing.Size(100, 26);
            this.txtReport2.TabIndex = 8;
            // 
            // btnReport2
            // 
            this.btnReport2.Location = new System.Drawing.Point(195, 28);
            this.btnReport2.Name = "btnReport2";
            this.btnReport2.Size = new System.Drawing.Size(226, 40);
            this.btnReport2.TabIndex = 8;
            this.btnReport2.Text = "button1";
            this.btnReport2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "label3";
            // 
            // txtPropUsage
            // 
            this.txtPropUsage.Location = new System.Drawing.Point(425, 198);
            this.txtPropUsage.Name = "txtPropUsage";
            this.txtPropUsage.Size = new System.Drawing.Size(100, 26);
            this.txtPropUsage.TabIndex = 24;
            // 
            // txtPropEffect
            // 
            this.txtPropEffect.Location = new System.Drawing.Point(434, 46);
            this.txtPropEffect.Name = "txtPropEffect";
            this.txtPropEffect.Size = new System.Drawing.Size(100, 26);
            this.txtPropEffect.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(430, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(430, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 20);
            this.label8.TabIndex = 27;
            this.label8.Text = "label8";
            // 
            // btnAddProp
            // 
            this.btnAddProp.Location = new System.Drawing.Point(676, 46);
            this.btnAddProp.Name = "btnAddProp";
            this.btnAddProp.Size = new System.Drawing.Size(144, 42);
            this.btnAddProp.TabIndex = 28;
            this.btnAddProp.Text = "button2";
            this.btnAddProp.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1731, 651);
            this.Controls.Add(this.btnAddProp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPropUsage);
            this.Controls.Add(this.txtPropEffect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbSearchField);
            this.Controls.Add(this.btnDeletePlant);
            this.Controls.Add(this.btnAddPlant);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtLatin);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.dataGridViewProps);
            this.Controls.Add(this.dataGridViewPlants);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearch)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewPlants;
        private System.Windows.Forms.DataGridView dataGridViewProps;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtLatin;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Button btnAddPlant;
        private System.Windows.Forms.Button btnDeletePlant;
        private System.Windows.Forms.ComboBox cmbSearchField;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridViewSearch;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridViewReport1;
        private System.Windows.Forms.Button btnReport1;
        private System.Windows.Forms.TextBox txtReport1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridViewReport2;
        private System.Windows.Forms.TextBox txtReport2;
        private System.Windows.Forms.Button btnReport2;
        private System.Windows.Forms.TextBox txtPropUsage;
        private System.Windows.Forms.TextBox txtPropEffect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAddProp;
    }
}

