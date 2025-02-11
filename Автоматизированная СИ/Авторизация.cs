using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Автоматизированная_СИ
{   
    public partial class Авторизация : Form
    {

        ConectionDB ConnectionDB = new ConectionDB();
       

        //Здесь объявлены две строковые переменные defaultText1 и defaultText2, 
        //которые используются в качестве текста по умолчанию для текстовых полей ввода логина и пароля.
        private string defaultText1 = "Логин";
        private string defaultText2 = "Пароль";

        public Авторизация()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(126, 96,99);
            



            //инициализируются компоненты формы
            textBoxLoginAdmin.Text = defaultText1;
            textBoxPassAdmin.Text = defaultText2;
            // Подписываемся на события
            textBoxLoginAdmin.GotFocus += TextBox_GotFocus;
            textBoxLoginAdmin.LostFocus += TextBox_LostFocus;
            textBoxPassAdmin.GotFocus += TextBox_GotFocus;
            textBoxPassAdmin.LostFocus += TextBox_LostFocus;
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == defaultText1 || textBox.Text == defaultText2)
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = System.Drawing.Color.Black; // Меняем цвет текста на черный
            }
        }
        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            // Проверяем, пуст ли текстбокс, и если да, то восстанавливаем текст по умолчанию
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox == textBoxLoginAdmin ? defaultText1 : defaultText2;
                textBox.ForeColor = System.Drawing.Color.Gray; // Меняем цвет текста на серый
            }
        }

        // дает градиент блэкколор
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    //DrawLinearGradient(e.Graphics); 

        //}

        // делает градиент блэкколор
        //private void DrawLinearGradient(Graphics graphics)
        //{
        //    // Определение области для градиента
        //    Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
        //    // Создание градиента
        //    using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.LightGray, Color.DarkBlue, LinearGradientMode.ForwardDiagonal))
        //    {
        //        graphics.FillRectangle(brush, rect);
        //    };
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.BackColor = System.Drawing.Color.Transparent;
            LabelNameHat.BackColor = System.Drawing.Color.Transparent;
            
            
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBoxLoginAdmin.Text;
            var password = textBoxPassAdmin.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select Login_User, Password_User, Role from Пользователи where Login_User = '{login}' and Password_User = '{password}'";
            SqlCommand command = new SqlCommand(querystring, ConnectionDB.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                string role = table.Rows[0]["Role"].ToString();
                if (role == "админ")
                {
                    MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    База_данных база_Данных = new База_данных();
                    this.Hide();
                    база_Данных.Show();
                }
                else if (role == "пользователь")
                {
                    MessageBox.Show("Вы успешно вошли как пользователь!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Компктующие комплектующие = new Компктующие();
                    this.Hide();
                    комплектующие.Show();
                }
            }
            else
            {
                MessageBox.Show("Аккаунта не существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}

