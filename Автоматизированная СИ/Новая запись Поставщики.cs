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

namespace Автоматизированная_СИ
{
    public partial class Новая_запись_поставщики : Form
    {
        ConectionDB ConectionDB = new ConectionDB();
        public Новая_запись_поставщики()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(126, 96, 99);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConectionDB.openConnection();
            var name = textBox_Name.Text;
            var address = textBox_Address.Text;
            var number = textBox_Number.Text;
            if (!string.IsNullOrWhiteSpace(textBox_Number.Text))
            {
                var addQuery = $"insert into Поставщики (Name_Suppliers, Аddress_Suppliers, Number) values ('{name}', '{address}', '{number}')";
                var command = new SqlCommand(addQuery, ConectionDB.GetConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно добавлена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось создать запись!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ConectionDB.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_Name.Clear();
            textBox_Address.Clear();
            textBox_Number.Clear();
        }

        private void Новая_запись_поставщики_Load(object sender, EventArgs e)
        {

        }
    }
}
