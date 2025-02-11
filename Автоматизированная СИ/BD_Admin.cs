using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;



namespace Автоматизированная_СИ
{
    
    enum RowState 
    {
        Existed, //Существующий
        New, //новый
        Modified, //модификация
        ModifiedNew, // новая модификация
        Deleted //удалить
    }
    public partial class База_данных : Form
    {
        ConectionDB ConnectionDB = new ConectionDB();
        int selectedRow;
      
        public База_данных()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(126, 96, 99);
          
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
            dgv.Rows.Add(record.GetDecimal(0),record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4), record.GetDecimal(5), record.GetDecimal(6),  RowState.ModifiedNew);
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

        private void База_данных_Load(object sender, EventArgs e) 
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (selectedRow >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox_id.Text = row.Cells[0].Value.ToString();
                textBox_Name.Text = row.Cells[1].Value.ToString();
                textBox_Description.Text = row.Cells[2].Value.ToString();
                textBox_Model.Text = row.Cells[3].Value.ToString();
                textBox_Price.Text = row.Cells[4].Value.ToString();
                textBox_IdSuppliers.Text = row.Cells[5].Value.ToString();
                textBox_IdStorage.Text = row.Cells[6].Value.ToString();
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Новая_запись новая_Запись = new Новая_запись();
            новая_Запись.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_Name.Clear();
            textBox_Description.Clear();
            textBox_Model.Clear();
            textBox_Price.Clear();
            textBox_IdSuppliers.Clear();
            textBox_IdStorage.Clear();
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
        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void DeleteRow()
        {
            //int index = dataGridView1.CurrentCell.RowIndex;
            //dataGridView1.Rows[index].Visible = false;
            //if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            //{
            //    dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;
            //    return;
            //}
            //dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;


            //if (dataGridView1.Rows.Count >0)
            //{
            //    int id = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value);
            //    string query = $"Delete From Комплектующие where Id_component = '{id}'";
            //    ConnectionDB.openConnection();
            //    using (SqlCommand command = new SqlCommand(query, ConnectionDB.GetConnection())) 
            //    {
            //        command.Parameters.AddWithValue($"Id_component", id);
            //        int rows = command.ExecuteNonQuery();
            //        if (rows > 0)
            //        {
            //            MessageBox.Show("Компонент успешно удален!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Ошибка при удалении!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //    ConnectionDB.closeConnection();
            //}
            //else
            //{
            //    MessageBox.Show("Выберите ID комплектующуго!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string query = "DELETE FROM Комплектующие WHERE Id_Component = @Id_Component";
                ConnectionDB.openConnection();
                using (SqlCommand command = new SqlCommand(query, ConnectionDB.GetConnection()))
                {
                    command.Parameters.AddWithValue("@Id_Component", Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Комплектующая успешно удалена!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                ConnectionDB.closeConnection();
            }
            else
            {
                MessageBox.Show("Выберите строку, которую необходимо удалить!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Update()
        {
            try
            {
                ConnectionDB.openConnection(); // Открываем соединение с базой данных

                for (int index = 0; index < dataGridView1.Rows.Count; index++)
                {
                    // Убедимся, что строка действительна
                    if (dataGridView1.Rows[index].IsNewRow) continue;

                    var rowState = (RowState)dataGridView1.Rows[index].Cells[7].Value;

                    // Пропускаем строки с состоянием "Existed"
                    if (rowState == RowState.Existed)
                    {
                        continue;
                    }

                    // Обрабатываем строки с состоянием "Modified"
                    if (rowState == RowState.Modified)
                    {
                        var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                        var name = dataGridView1.Rows[index].Cells[1].Value.ToString();
                        var description = dataGridView1.Rows[index].Cells[2].Value.ToString();
                        var model = dataGridView1.Rows[index].Cells[3].Value.ToString();
                        var price = dataGridView1.Rows[index].Cells[4].Value.ToString();
                        var id_suppliers = dataGridView1.Rows[index].Cells[5].Value.ToString();
                        var id_storage = dataGridView1.Rows[index].Cells[6].Value.ToString();

                        var changeQuery = $@"UPDATE Комплектующие SET Name_Component = @name, Description = @description," +
                                          $@"Model = @model, Price = @price WHERE Id_Component = @id";

                        using (var command = new SqlCommand(changeQuery, ConnectionDB.GetConnection()))
                        {
                            // Использование параметров для предотвращения SQL-инъекций
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@description", description);
                            command.Parameters.AddWithValue("@Model", model);
                            command.Parameters.AddWithValue("@price", price);
                            

                            command.ExecuteNonQuery(); // Выполняем запрос
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}"); // Показываем сообщение об ошибке пользователю
            }
            finally
            {
                ConnectionDB.closeConnection(); // Закрываем соединение, даже если произошла ошибка
            }
        }

        private void Change()
        {
            var SelectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var id = textBox_id.Text;
            var name = textBox_Name.Text;
            var description = textBox_Description.Text;
            var model = textBox_Model.Text;
            int price;
            var IdStorage = textBox_IdStorage.Text;
            var IdSuppliers = textBox_IdSuppliers.Text;

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                if (int.TryParse(textBox_Price.Text, out price))
                {
                    dataGridView1.Rows[SelectedRowIndex].SetValues(id, name, description,model, price, IdSuppliers, IdStorage);
                    dataGridView1.Rows[SelectedRowIndex].Cells[7].Value = RowState.Modified;
                }
                else 
                {
                    MessageBox.Show("Цена должна быть положительная!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Change();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void База_данных_Resize(object sender, EventArgs e)
        {
            
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Пользователи пользователи = new Пользователи();
            пользователи.Show();
            this.Close();
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Поставщики поставщики = new Поставщики();
            поставщики.Show();
            this.Close();
        }

        private void складToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Склады склад = new Склады();
            склад.Show();
            this.Close();
        }

        private void ExportToDOXC(DataGridView dataGridView1, string filePath)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                Table table = new Table();

                // Создание строки заголовка таблицы
                TableRow headerRow = new TableRow();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    TableCell headerCell = new TableCell(new Paragraph(new Run(new Text(column.HeaderText))));
                    headerRow.Append(headerCell);
                }
                table.Append(headerRow);

                // Заполнение таблицы данными
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    TableRow dataRow = new TableRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        TableCell dataCell = new TableCell(new Paragraph(new Run(new Text(cell.Value?.ToString() ?? ""))));
                        dataRow.Append(dataCell);
                    }
                    table.Append(dataRow);
                }

                body.Append(table);
                mainPart.Document.Append(body);
                mainPart.Document.Save();

                MessageBox.Show("Данные успешно сохранены в файл " + filePath);
            }
        }

        

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void dOCXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToDOXC(dataGridView1, "S:\\Диплом\\отчет о комплектующих.docx");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Заказы заказы = new Заказы();
            заказы.Show();
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void создатьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Request request = new Request();
            request.Show();
        }
    }
}
