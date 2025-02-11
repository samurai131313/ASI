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
    public partial class Компктующие : Form
    {
        ConectionDB ConnectionDB = new ConectionDB(); 
        public Компктующие()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(126, 96, 99);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Id_Сomponent", "ID комплектующих");
            dataGridView1.Columns.Add("Name_component", "Наименование комплектующих");
            dataGridView1.Columns.Add("Description", "Описание");
            dataGridView1.Columns.Add("Model", "Модель");
            dataGridView1.Columns.Add("Price", "Цена");
            dataGridView1.Columns.Add("Id_Suppliers", "ID поставщика");
            dataGridView1.Columns.Add("Id_Storage", "ID склада");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetDecimal(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4), record.GetDecimal(5), record.GetDecimal(6), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Комплектующие";
            SqlCommand command = new SqlCommand(queryString, ConnectionDB.GetConnection());
            ConnectionDB.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_Search.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"select * from Комплектующие where concat (Id_Component, Name_Component, Description, Model, Price, Id_Suppliers, Id_Storаge) like '%" + textBox_Search.Text + "%'";
            SqlCommand command = new SqlCommand(searchString, ConnectionDB.GetConnection());
            ConnectionDB.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }

        private void Компктующие_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void button_CreateRequest_Click(object sender, EventArgs e)
        {
            Request request = new Request();
            request.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Авторизация авторизация = new Авторизация();
            авторизация.Show();
            this.Close();
        }
    }
}
