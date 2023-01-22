using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleDatabaseWalkthrough
{
    public partial class Form1 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VS2022_Projects\SampleDatabaseWalkthrough\SampleDatabaseWalkthrough\SampleDatabase.mdf;Integrated Security=True;Connect Timeout=30";
        string sql = "SELECT * FROM Customers";

        public Form1()
        {
            InitializeComponent();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                // делаем недоступным столбец id для изменения
                // TrE:19.12.2022 => dataGridView1.Columns["CustomerID"].ReadOnly = true;
            }
        }

        // кнопка добавления
        private void addButton_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds.Tables[0].Rows.Add(row);
        }

        // кнопка удаления
        private void deleteButton_Click(object sender, EventArgs e)
        {
            // удаляем выделенные строки из dataGridView1
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        // кнопка сохранения
        private void saveButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_InsertCustomer", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, "Id"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@companyName", SqlDbType.NVarChar, 50, "CompanyName"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@contactName", SqlDbType.NVarChar, 50, "ContactName"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 24, "Phone"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@customerID", SqlDbType.Char, 5, "CustomerID");
                // parameter.Direction = ParameterDirection.Output;
                parameter.Direction = ParameterDirection.Input;

                adapter.Update(ds);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
