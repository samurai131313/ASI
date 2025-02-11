using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Автоматизированная_СИ
{
    public partial class Пользователи : Form
    {
        ConectionDB ConnectionDB = new ConectionDB();
        int selectedRow;
        public Пользователи()
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
            dataGridView1.Columns.Add("Id_User", "Id пользователя");
            dataGridView1.Columns.Add("Name_User", "Имя пользователя");
            dataGridView1.Columns.Add("Surname_User", "Фамилия");
            dataGridView1.Columns.Add("Login_User", "Логин");
            dataGridView1.Columns.Add("Password_user", "Пароль");
            dataGridView1.Columns.Add("Role", "Роль");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetDecimal(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4), record.GetString(5), RowState.ModifiedNew);
        }
        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Пользователи";
            SqlCommand command = new SqlCommand(queryString, ConnectionDB.GetConnection());
            ConnectionDB.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }

        private void Пользователи_Load(object sender, EventArgs e)
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
                textBox_Surname.Text = row.Cells[2].Value.ToString();
                textBox_Login.Text = row.Cells[3].Value.ToString();
                textBox_Password.Text = row.Cells[4].Value.ToString();
                textBox_Role.Text = row.Cells[5].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Новая_запись_Пользователи новая_запись_пользователи = new Новая_запись_Пользователи();
            новая_запись_пользователи.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_Name.Clear();
            textBox_Surname.Clear();
            textBox_Login.Clear();
            textBox_Password.Clear();
            textBox_Role.Clear();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"select * from Пользователи where concat (Id_User, Name_User, Surname_User, Login_User, Password_user, Role) like '%" + textBox_Search.Text + "%'";
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
                string query = "DELETE FROM Пользователи WHERE Id_User = @Id_User";
                ConnectionDB.openConnection();
                using (SqlCommand command = new SqlCommand(query, ConnectionDB.GetConnection()))
                {
                    command.Parameters.AddWithValue("@Id_User", Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пользователь успешно удален!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    var rowState = (RowState)dataGridView1.Rows[index].Cells[6].Value;

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
                        var surname = dataGridView1.Rows[index].Cells[2].Value.ToString();
                        var login = dataGridView1.Rows[index].Cells[3].Value.ToString();
                        var password = dataGridView1.Rows[index].Cells[4].Value.ToString();
                        var role = dataGridView1.Rows[index].Cells[5].Value.ToString();

                        var changeQuery = $"UPDATE Пользователи SET Name_User = @name, Surname_User = @surname," +
                                          $" Login_User = @login, Password_user = @password, Role = @role " +
                                          $"WHERE Id_User = @id";

                        using (var command = new SqlCommand(changeQuery, ConnectionDB.GetConnection()))
                        {
                            // Использование параметров для предотвращения SQL-инъекций
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@surname", surname);
                            command.Parameters.AddWithValue("@login", login);
                            command.Parameters.AddWithValue("@password", password);
                            command.Parameters.AddWithValue("@role", role);

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
            var surname = textBox_Surname.Text;
            var login = textBox_Login.Text;
            int password;          
            var role = textBox_Role.Text;

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                if (int.TryParse(textBox_Password.Text, out password))
                {
                    dataGridView1.Rows[SelectedRowIndex].SetValues(id, name, surname, login, password, role);
                    dataGridView1.Rows[SelectedRowIndex].Cells[6].Value = RowState.Modified;
                }
                else
                {
                    MessageBox.Show("Пароль не может начинаться с знака <-> !", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void комплектующиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            База_данных база_Данных = new База_данных();
            база_Данных.Show();
            this.Close();
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Поставщики поставщики = new Поставщики();
            поставщики.Show();
            this.Close();
        }

        private void складыToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dOXCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToDOXC(dataGridView1, "S:\\Диплом\\отчет о пользователях.docx");
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Заказы заказы = new Заказы();
            заказы.Show();
            this.Close();
        }

        private void создатьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Request request = new Request();
            request.Show();
        }
    }
}
