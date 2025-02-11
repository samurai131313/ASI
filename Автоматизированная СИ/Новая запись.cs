using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Автоматизированная_СИ
{
    public partial class Новая_запись : Form
    {

        

        ConectionDB ConectionDB = new ConectionDB();
        public Новая_запись()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(126, 96, 99);
            UploadingDataStorageInComboBox();
            UploadingDataSuppliersInComboBox();
        }
        public ComboBox ComboBoxIdStorage => comboBox_IdStorage;
        public ComboBox ComboBoxIdSuppliers => comboBox_Id_Suppliers;
        private void Новая_запись_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConectionDB.openConnection();

            var name = textBox_Name.Text;
            var description = textBox_Description.Text;
            var model = textBox_Model.Text;
            int price;

            // Извлекаем числовую часть из comboBox_Id_Suppliers
            var supplierItem = comboBox_Id_Suppliers.Text;
            int Id_Suppliers;
            if (!int.TryParse(supplierItem.Split('-')[0], out Id_Suppliers))
            {
                MessageBox.Show("Неверный формат поставщика!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConectionDB.closeConnection();
                return;
            }

            // Извлекаем числовую часть из comboBox_IdStorage
            var storageItem = comboBox_IdStorage.Text;
            int Id_Storage;
            if (!int.TryParse(storageItem.Split('-')[0], out Id_Storage))
            {
                MessageBox.Show("Неверный формат склада!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConectionDB.closeConnection();
                return;
            }

            // Проверяем, что цена введена корректно
            if (int.TryParse(textBox_Price.Text, out price))
            {
                // Формируем SQL-запрос
                var addQuery = $"insert into Комплектующие (Name_component, Description, Model, Price, Id_Suppliers, Id_Storаge) " +
                               $"values ('{name}', '{description}', '{model}', {price}, {Id_Suppliers}, {Id_Storage})";

                // Выполняем запрос
                var command = new SqlCommand(addQuery, ConectionDB.GetConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно добавлена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось создать запись! Проверьте поле 'Цена'.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ConectionDB.closeConnection();
        }
        public void UploadingDataStorageInComboBox()
        {
            try
            {
                ConectionDB.openConnection();

                // SQL-запрос для получения всех записей из таблицы Склад
                var query = "SELECT Name_Storаge FROM Склад"; // Выбираем только Name_Storage

                using (var command = new SqlCommand(query, ConectionDB.GetConnection()))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        // Очищаем ComboBox перед загрузкой новых данных
                        ComboBoxIdStorage.Items.Clear();

                        // Читаем данные и добавляем их в ComboBox
                        while (reader.Read())
                        {
                            var name = reader["Name_Storаge"].ToString(); // Получаем только Name_Storage
                            ComboBoxIdStorage.Items.Add(name); // Добавляем только имя
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
        public void UploadingDataSuppliersInComboBox()
        {
            try
            {
                ConectionDB.openConnection();

                // SQL-запрос для получения всех записей из таблицы Склад
                var query = "SELECT Name_Suppliers FROM Поставщики"; // Выбираем только Name_Suppliers

                using (var command = new SqlCommand(query, ConectionDB.GetConnection()))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        // Очищаем ComboBox перед загрузкой новых данных
                        ComboBoxIdSuppliers.Items.Clear();

                        // Читаем данные и добавляем их в ComboBox
                        while (reader.Read())
                        {
                            var name = reader["Name_Suppliers"].ToString(); // Получаем только Name_Suppliers
                            ComboBoxIdSuppliers.Items.Add(name); // Добавляем только имя
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

        

        private void pictureBox_isPinned_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox_isPinned_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }


        private void Новая_запись_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            textBox_Name.Clear();
            textBox_Description.Clear();
            textBox_Model.Clear();
            textBox_Price.Clear();
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}