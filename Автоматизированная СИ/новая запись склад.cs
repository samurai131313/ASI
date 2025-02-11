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
using System.Data.SQLite;

namespace Автоматизированная_СИ
{
    public partial class новая_запись_склад : Form
    {

        
        ConectionDB ConectionDB = new ConectionDB();
        public новая_запись_склад()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(126, 96, 99);
       
        }

        private void новая_запись_склад_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConectionDB.openConnection();
            var name = textBox_Name.Text;
            var address = textBox_Address.Text;
            if (!string.IsNullOrWhiteSpace(textBox_Name.Text))
            {
                var addQuery = $"insert into Склад (Name_Storаge, Address_Storage) values ('{name}', '{address}')";
                var command = new SqlCommand(addQuery, ConectionDB.GetConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно добавлена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось создать запись!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ConectionDB.closeConnection();
             //try
            //{
            //    ConectionDB.openConnection();

            //    var name = textBox_Name.Text;
            //    var address = textBox_Address.Text;

            //    if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(address))
            //    {
            //        // SQL-запрос для добавления записи и получения ID
            //        var addQuery = $"insert into Склад (Name_Storage, Address_Storage) values ('{name}', '{address}'); SELECT SCOPE_IDENTITY();";

            //        var command = new SqlCommand(addQuery, ConectionDB.GetConnection());

            //        // Выполняем запрос и получаем ID добавленной записи
            //        var newId = command.ExecuteScalar();

            //        if (newId != null)
            //        {
            //            // Формируем строку для добавления в comboBox
            //            string comboBoxItem = $"{newId}-{name}";

            //            // Находим открытую форму Новая_запись
            //            var новая_запись = Application.OpenForms.OfType<Новая_запись>().FirstOrDefault();

            //            if (новая_запись != null)
            //                новая_запись.ComboBoxIdStorage.Items.Add(comboBoxItem);

            //            // Очищаем текстовые поля
            //            textBox_Name.Clear();
            //            textBox_Address.Clear();

            //            MessageBox.Show("Запись успешно добавлена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Не удалось получить ID добавленной записи!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Поля 'Наименование' и 'Адрес' не могут быть пустыми!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    ConectionDB.closeConnection();
            //}

           
        }


    

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_Name.Clear();
            textBox_Address.Clear();
        }
    }
}
