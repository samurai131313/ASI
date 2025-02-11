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
    public partial class Новая_запись_Пользователи : Form
    {
        ConectionDB connectionDB = new ConectionDB();

        public Новая_запись_Пользователи()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(126, 96, 99);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connectionDB.openConnection();
            var name = textBox_Name.Text;
            var surname = textBox_Surname.Text;
            var login = textBox_Password.Text;
            int password;
            var role = textBox_Role.Text;
            if (int.TryParse(textBox_Login.Text, out password))
            {
                var addQuery = $"insert into Пользователи (Name_User, Surname_User, Login_User, Password_User, Role) values ('{name}', '{surname}', '{login}', '{password}', '{role}')";
                var command = new SqlCommand(addQuery, connectionDB.GetConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно добавлена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось создать запись!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            connectionDB.closeConnection();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_Login.Clear();
            textBox_Name.Clear();
            textBox_Surname.Clear();
            textBox_Password.Clear();
            textBox_Role.Clear();
        }

        private void Новая_запись_Пользователи_Load(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
