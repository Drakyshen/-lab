using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace _25
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Підключаємо кнопки до обробників
            btnAddPlant.Click += BtnAddPlant_Click;
            btnDeletePlant.Click += BtnDeletePlant_Click;
            btnReport1.Click += BtnReport1_Click;
            btnReport2.Click += BtnReport2_Click;
            btnSearch.Click += BtnSearch_Click;
            btnAddProp.Click += BtnAddProp_Click;

            // Текст кнопок
            btnAddPlant.Text = "Додати";
            btnDeletePlant.Text = "Видалити";
            btnReport1.Text = "Звіт по назві";
            btnReport2.Text = "Звіт по ефекту";
            btnSearch.Text = "Пошук";
            label1.Text = "Назва:";
            label2.Text = "Латинська назва:";
            label3.Text = "Опис:";
            label7.Text = "Ефект:";
            label8.Text = "Застосування:";
            btnAddProp.Text = "Додати властивість";
            cmbSearchField.Items.AddRange(new string[] { "name", "latin_name", "description" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPlants();
            LoadProperties();
        }

        private void LoadPlants()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var da = new NpgsqlDataAdapter("SELECT * FROM public.plants ORDER BY id", conn);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewPlants.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Помилка: " + ex.Message); }
        }

        private void LoadProperties()
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var da = new NpgsqlDataAdapter("SELECT * FROM public.prperties ORDER BY property_id", conn);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewProps.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Помилка: " + ex.Message); }
        }

        private void BtnAddPlant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву рослини!");
                return;
            }
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand(
                        "INSERT INTO public.plants (name, latin_name, description) VALUES (@n, @l, @d)", conn);
                    cmd.Parameters.AddWithValue("n", txtName.Text);
                    cmd.Parameters.AddWithValue("l", txtLatin.Text);
                    cmd.Parameters.AddWithValue("d", txtDesc.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Додано!");
                    txtName.Clear();
                    txtLatin.Clear();
                    txtDesc.Clear();
                    LoadPlants();
                }
            }
            catch (Exception ex) { MessageBox.Show("Помилка: " + ex.Message); }
        }

        private void BtnDeletePlant_Click(object sender, EventArgs e)
        {
            if (dataGridViewPlants.CurrentRow == null)
            {
                MessageBox.Show("Виберіть рядок для видалення!");
                return;
            }
            var id = dataGridViewPlants.CurrentRow.Cells["id"].Value;
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand("DELETE FROM public.plants WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    LoadPlants();
                }
            }
            catch (Exception ex) { MessageBox.Show("Помилка: " + ex.Message); }
        }

        private void BtnReport1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand(
                        "SELECT * FROM public.plants WHERE name ILIKE @n", conn);
                    cmd.Parameters.AddWithValue("n", "%" + txtReport1.Text + "%");
                    var da = new NpgsqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewReport1.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Помилка: " + ex.Message); }
        }

        private void BtnReport2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand(
                        "SELECT * FROM public.prperties WHERE effect ILIKE @e", conn);
                    cmd.Parameters.AddWithValue("e", "%" + txtReport2.Text + "%");
                    var da = new NpgsqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewReport2.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Помилка: " + ex.Message); }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string field = cmbSearchField.Text;
            string value = txtSearch.Text;

            if (string.IsNullOrEmpty(field))
            {
                MessageBox.Show("Виберіть поле для пошуку!");
                return;
            }
            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = $"SELECT * FROM public.plants WHERE \"{field}\" ILIKE @v";
                    var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("v", "%" + value + "%");
                    var da = new NpgsqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewSearch.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Помилка: " + ex.Message); }
        }
        private void BtnAddProp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPropEffect.Text))
            {
                MessageBox.Show("Введіть ефект!");
                return;
            }
            if (dataGridViewPlants.CurrentRow == null)
            {
                MessageBox.Show("Виберіть рослину в таблиці зліва!");
                return;
            }

            var plantId = dataGridViewPlants.CurrentRow.Cells["id"].Value;

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand(
                        "INSERT INTO public.prperties (plant_id, effect, usage) VALUES (@pid, @ef, @us)", conn);
                    cmd.Parameters.AddWithValue("pid", plantId);
                    cmd.Parameters.AddWithValue("ef", txtPropEffect.Text);
                    cmd.Parameters.AddWithValue("us", txtPropUsage.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Додано!");
                    txtPropEffect.Clear();
                    txtPropUsage.Clear();
                    LoadProperties();
                }
            }
            catch (Exception ex) { MessageBox.Show("Помилка: " + ex.Message); }
        }

    }
}
