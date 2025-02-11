using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Автоматизированная_СИ
{
    public partial class Request : Form
    {
        ConectionDB ConectionDB = new ConectionDB();
        public Request()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(126, 96, 99);
            UploadingDataUserInComboBox();
            UploadingDataComponentInComboBox();
        }

        public ComboBox ComboBoxIdUser => comboBox_Id_User;
        public ComboBox ComboBoxIdComponent => comboBox_IdComponenta;
        private void Request_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConectionDB.openConnection();

            var order_date = textBox_Order_Date.Text;
            var status = textBox_Status.Text;
            var UserItem = comboBox_Id_User.Text;
            int Id_User;
            if (!int.TryParse(UserItem.Split('-')[0], out Id_User))
            {
                MessageBox.Show("Неверный формат пользователя!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConectionDB.closeConnection();
                return;
            }


            var ComponentItem = comboBox_IdComponenta.Text;
            int Id_Componenta;
            if (!int.TryParse(ComponentItem.Split('-')[0], out Id_Componenta))
            {
                MessageBox.Show("Неверный формат склада!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConectionDB.closeConnection();
                return;
            }

            // Проверяем, что цена введена корректно
            if (!string.IsNullOrWhiteSpace(textBox_Status.Text))
            {
                // Формируем SQL-запрос
                var addQuery = $"insert into Заказы (Order_Date, Status, Id_User, Id_Component) " +
                               $"values ('{order_date}', '{status}', '{Id_User}', {Id_Componenta})";

                // Выполняем запрос
                var command = new SqlCommand(addQuery, ConectionDB.GetConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Заявка успешно создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось создать заявку! Проверьте поле 'Статус'.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ConectionDB.closeConnection();
        }
        public void UploadingDataUserInComboBox()
        {
            try
            {
                ConectionDB.openConnection();


                var query = "SELECT Id_User FROM Пользователи";

                using (var command = new SqlCommand(query, ConectionDB.GetConnection()))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        // Очищаем ComboBox перед загрузкой новых данных
                        comboBox_Id_User.Items.Clear();

                        // Читаем данные и добавляем их в ComboBox
                        while (reader.Read())
                        {
                            var name = reader["Id_User"].ToString();
                            comboBox_Id_User.Items.Add(name); // Добавляем только имя
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ConectionDB.closeConnection();
            }
        }
        public void UploadingDataComponentInComboBox()
        {
            try
            {
                ConectionDB.openConnection();
                var query = "SELECT Id_Component FROM Комплектующие";

                using (var command = new SqlCommand(query, ConectionDB.GetConnection()))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        comboBox_IdComponenta.Items.Clear();
                        while (reader.Read())
                        {
                            var name = reader["Id_Component"].ToString();
                            comboBox_IdComponenta.Items.Add(name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ConectionDB.closeConnection();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        
    }
}

