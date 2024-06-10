using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
            // TODO: данная строка кода позволяет загрузить данные в таблицу "test_1DataSet.log_1". При необходимости она может быть перемещена или удалена.
            this.log_1TableAdapter.Fill(this.test_1DataSet.log_1);

        }
        private readonly string _connectionString = "Data Source=DESKTOP-8RU4O9V\\MSSQLFLUFFY; Initial Catalog=test_1; Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int nomer_3 = Convert.ToInt32(textBox1.Text); // Предполагается, что в txtAge вводится возраст как число.
                string login = textBox2.Text;
                string pass = textBox3.Text;
                string yroven = textBox4.Text;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO log_1 (nomer_3, login, pass, yroven) VALUES (@nomer_3, @login, @pass, @yroven)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nomer_3", nomer_3);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@pass", pass);
                        command.Parameters.AddWithValue("@yroven", yroven);
                        command.ExecuteNonQuery();
                    }
                }
                RefreshDataGridView();

                MessageBox.Show("Запись успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении записи: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT *  FROM log_1"; // Обновленный запрос для выборки данных

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "log_1");
                        dataGridView1.DataSource = dataSet.Tables["log_1"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем значение nomer_3 из TextBox
                int nomer_3 = Convert.ToInt32(textBox3.Text);

                // Удаляем запись из базы данных
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM log_1 WHERE nomer_3 = @nomer_3";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nomer_3", nomer_3);
                        command.ExecuteNonQuery();
                    }
                }

                // Обновляем DataGridView после удаления записи из базы данных
                RefreshDataGridView();
                MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении записи: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                int nomer_3 = Convert.ToInt32(comboBox1.SelectedItem.ToString()); // Предполагаем, что в ComboBox содержатся числовые значения
                string login = textBox2.Text;  // Предполагаем, что login является уникальным идентификатором
                string pass = textBox3.Text;
                string yroven = textBox4.Text;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE log_1 SET login = @login, pass = @pass, yroven = @yroven WHERE nomer_3 = @nomer_3";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nomer_3", nomer_3);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@pass", pass);
                        command.Parameters.AddWithValue("@yroven", yroven);
                        command.ExecuteNonQuery();
                    }
                }
                // Обновляем DataGridView после обновления записи в базе данных
                RefreshDataGridView();
                MessageBox.Show("Запись успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении записи: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }
        private int roomId;

        private void button4_Click(object sender, EventArgs e)
        {
         

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Здесь ваш код для получения данных о комнате из базы данных по roomId и заполнения текстовых полей
            string query = "SELECT [nomer_3], [login], [pass], [yroven] FROM log_1 WHERE nomer_3 = @nomer";
            // Предположим, что вы используете ADO.NET для работы с базой данных
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nomer", roomId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox1.Text = reader["nomer_3"].ToString();
                        textBox2.Text = reader["login"].ToString();
                        textBox3.Text = reader["pass"].ToString();
                        textBox4.Text = reader["yroven"].ToString();
                    }
                }
            }
        }
    }
}