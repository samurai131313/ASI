using DocumentFormat.OpenXml.Packaging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Wordprocessing;
namespace Автоматизированная_СИ
{
    public partial class Заказы : Form
    {
        ConectionDB ConnectionDB = new ConectionDB();
        int selectedRow;
        public ComboBox ComboBoxIdUser => comboBox_Id_User;
        public ComboBox ComboBoxIdComponent => comboBox_Id_Component;
        public Заказы()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(126, 96, 99);
            UploadingDataUserInComboBox();
            UploadingDataCompopentaInComboBox();
        }
        private void CreateColumns()
        {
            dataGridView2.Columns.Add("Id_Order", "ID заказа");
            dataGridView2.Columns.Add("Order_Date", "Дата заказа");
            dataGridView2.Columns.Add("Status", "Статус");
            dataGridView2.Columns.Add("Id_User", "Id_пользователя");
            dataGridView2.Columns.Add("Id_Component", "ID комплектующиего");
            dataGridView2.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetDecimal(0), record.GetDateTime(1), record.GetString(2), record.GetDecimal(3), record.GetDecimal(4), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Заказы";
            SqlCommand command = new SqlCommand(queryString, ConnectionDB.GetConnection());
            ConnectionDB.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }



        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Заказы_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView2);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (selectedRow >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
                textBox_id_Request.Text = row.Cells[0].Value.ToString();
                textBox_Order_Date.Text = row.Cells[1].Value.ToString();
                textBox_Status.Text = row.Cells[2].Value.ToString();
                comboBox_Id_User.Text = row.Cells[3].Value.ToString();
                comboBox_Id_Component.Text = row.Cells[4].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Request request = new Request();
            request.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView2);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_id_Request.Clear();
            textBox_Order_Date.Clear();
            textBox_Status.Clear();
        }
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"select * from Заказы where concat (Id_Order, Order_Date, Status, Id_User, Id_Component) like '%" + textBox_Search.Text + "%'";
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
            Search(dataGridView2);
        }

        private void DeleteRow()
        {
            
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int Id = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                string query = "DELETE FROM Заказы WHERE Id_Order = @Id_order";
                ConnectionDB.openConnection();
                using (SqlCommand command = new SqlCommand(query, ConnectionDB.GetConnection()))
                {
                    command.Parameters.AddWithValue("@Id_order", Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заказ успешно удален!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                for (int index = 0; index < dataGridView2.Rows.Count; index++)
                {
                    // Убедимся, что строка действительна
                    if (dataGridView2.Rows[index].IsNewRow) continue;

                    var rowState = (RowState)dataGridView2.Rows[index].Cells[5].Value;

                
                    if (rowState == RowState.Existed)
                    {
                        continue;
                    }

                    
                    if (rowState == RowState.Modified)
                    {
                        var id = dataGridView2.Rows[index].Cells[0].Value.ToString();
                        var Order_date = dataGridView2.Rows[index].Cells[1].Value.ToString();
                        var Status = dataGridView2.Rows[index].Cells[2].Value.ToString();
                        var Id_User = dataGridView2.Rows[index].Cells[3].Value.ToString();
                        var Id_component = dataGridView2.Rows[index].Cells[4].Value.ToString();
                        

                        var changeQuery = $@"UPDATE Заказы 
                                     SET Order_Date = @Order_Date, 
                                         Status = @Status, 
                                         Id_User = @Id_User, 
                                         Id_component = @Id_Component 
                                     WHERE Id_Order = @Id";

                        using (var command = new SqlCommand(changeQuery, ConnectionDB.GetConnection()))
                        {
                            
                            command.Parameters.AddWithValue("@Id", id);
                            command.Parameters.AddWithValue("@Order_Date", Order_date);
                            command.Parameters.AddWithValue("@Status", Status);
                            command.Parameters.AddWithValue("@Id_User", Id_User);
                            command.Parameters.AddWithValue("@Id_component", Id_component);
                            command.ExecuteNonQuery(); 
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
            var SelectedRowIndex = dataGridView2.CurrentCell.RowIndex;
            var id = textBox_id_Request.Text;
            var Order_date = textBox_Order_Date.Text;
            var Status = textBox_Status.Text;
            var Id_User = comboBox_Id_User.Text;
            var Id_Component = comboBox_Id_Component.Text;

            if (dataGridView2.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                if (!string.IsNullOrWhiteSpace(textBox_Status.Text))
                {
                    dataGridView2.Rows[SelectedRowIndex].SetValues(id, Order_date, Status, Id_User, Id_Component);
                    dataGridView2.Rows[SelectedRowIndex].Cells[5].Value = RowState.Modified;
                }
                else 
                {
                    MessageBox.Show("Укажите статус!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            База_данных база_данных = new База_данных();
            база_данных.Show();
            this.Close();

        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void ExportToDOXC(DataGridView dataGridView2, string filePath)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                Table table = new Table();

                // Создание строки заголовка таблицы
                TableRow headerRow = new TableRow();
                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    TableCell headerCell = new TableCell(new Paragraph(new Run(new Text(column.HeaderText))));
                    headerRow.Append(headerCell);
                }
                table.Append(headerRow);

                // Заполнение таблицы данными
                foreach (DataGridViewRow row in dataGridView2.Rows)
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

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dOCXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToDOXC(dataGridView2, "S:\\Диплом\\отчет о заказах.docx");
        }
        public void UploadingDataUserInComboBox()
        {
            try
            {
                ConnectionDB.openConnection();

                // SQL-запрос для получения всех записей из таблицы Склад
                var query = "SELECT Id_User FROM Пользователи"; // Выбираем только Name_Suppliers

                using (var command = new SqlCommand(query, ConnectionDB.GetConnection()))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        // Очищаем ComboBox перед загрузкой новых данных
                        ComboBoxIdUser.Items.Clear();

                        // Читаем данные и добавляем их в ComboBox
                        while (reader.Read())
                        {
                            var name = reader["Id_User"].ToString(); // Получаем только Name_Suppliers
                            ComboBoxIdUser.Items.Add(name); // Добавляем только имя
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
                ConnectionDB.closeConnection();
            }
        }

        public void UploadingDataCompopentaInComboBox()
        {
            try
            {
                ConnectionDB.openConnection();

                // SQL-запрос для получения всех записей из таблицы Склад
                var query = "SELECT Id_Component FROM Комплектующие"; // Выбираем только Name_Suppliers

                using (var command = new SqlCommand(query, ConnectionDB.GetConnection()))
                {   
                    using (var reader = command.ExecuteReader())
                    {
                        // Очищаем ComboBox перед загрузкой новых данных
                        comboBox_Id_Component.Items.Clear();

                        // Читаем данные и добавляем их в ComboBox
                        while (reader.Read())
                        {
                            var name = reader["Id_Component"].ToString(); // Получаем только Name_Suppliers
                            comboBox_Id_Component.Items.Add(name); // Добавляем только имя
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
                ConnectionDB.closeConnection();
            }
        }

    }
}
