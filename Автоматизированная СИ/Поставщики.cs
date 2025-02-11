using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace Автоматизированная_СИ
{
    public partial class Поставщики : Form
    {
        ConectionDB ConnectionDB = new ConectionDB();
     
        int selectedRow;
        public Поставщики()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(126, 96, 99);
           
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Id_Suppliers", "ID поставщика");
            dataGridView1.Columns.Add("Name_Suppliers", "Наименование поставщика");
            dataGridView1.Columns.Add("Аddress_Suppliers", "Адрес");
            dataGridView1.Columns.Add("Number", "Номер телефона");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetDecimal(0), record.GetString(1), record.GetString(2), record.GetString(3), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Поставщики";
            SqlCommand command = new SqlCommand(queryString, ConnectionDB.GetConnection());
            ConnectionDB.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }

        private void Поставщики_Load(object sender, EventArgs e)
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
                textBox_Address.Text = row.Cells[2].Value.ToString();
                textBox_Number.Text = row.Cells[3].Value.ToString();
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            База_данных база_Данных = new База_данных();
            база_Данных.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Новая_запись_поставщики новая_запись_поставщики = new Новая_запись_поставщики();
            новая_запись_поставщики.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_Name.Clear();
            textBox_Address.Clear();
            textBox_Number.Clear();
            
        }

        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"select * from Поставщики where concat (Id_Suppliers, Name_Suppliers, Address_Suppliers, Number) like '%" + textBox_Search.Text + "%'";
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string query = "DELETE FROM Поставщики WHERE Id_Suppliers = @Id_Suppliers";
                ConnectionDB.openConnection();
                using (SqlCommand command = new SqlCommand(query, ConnectionDB.GetConnection()))
                {
                    command.Parameters.AddWithValue("@Id_Suppliers", Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Поставщик успешно удален!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                    var rowState = (RowState)dataGridView1.Rows[index].Cells[4].Value;

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
                        var address = dataGridView1.Rows[index].Cells[2].Value.ToString();
                        var number = dataGridView1.Rows[index].Cells[3].Value.ToString();


                        var changeQuery = @"UPDATE Поставщики SET Name_Suppliers = @name, Аddress_Suppliers = @address, Number = @number WHERE Id_Suppliers = @id";


                        using (var command = new SqlCommand(changeQuery, ConnectionDB.GetConnection()))
                        {
                            // Использование параметров для предотвращения SQL-инъекций
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@address", address);
                            command.Parameters.AddWithValue("@number", number);
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
            var address = textBox_Address.Text;
            var number = textBox_Number.Text;
            

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                if (!string.IsNullOrWhiteSpace(textBox_Number.Text))
                {
                    dataGridView1.Rows[SelectedRowIndex].SetValues(id, name, address, number);
                    dataGridView1.Rows[SelectedRowIndex].Cells[4].Value = RowState.Modified;
                }
                else
                {
                    MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Change();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Пользователи пользователи = new Пользователи();
            пользователи.Show();
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

        private void dOCXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToDOXC(dataGridView1, "S:\\Диплом\\отчет о поставщиках.docx");
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Заказы заказы = new Заказы();
            заказы.Show();
            this.Close();
        }

        private void создатьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
