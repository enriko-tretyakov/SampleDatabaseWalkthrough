using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System;

namespace SampleDatabaseWalkthrough
{
    /// <summary>
    /// Класс SampleDatabase использует классические sql запросы
    /// </summary>
    public class SampleDatabase : Customer
    {
        // Создание экземпляра списка customers
        List<Customer> customers = new List<Customer>();
        public bool Result { get; set; }

        DataSet ds; // Объявление набора данных
        SqlDataAdapter adapter; // Объявление адаптера данных
        SqlCommandBuilder commandBuilder;  // Объявление построителя SQL запросов SQL клиента
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VS2022_Projects\SampleDatabaseWalkthrough\SampleDatabaseWalkthrough\SampleDatabase.mdf;Integrated Security=True;Connect Timeout=30";
        string sql = "SELECT * FROM Customers";

        // Конструктор по умолчанию - просто синтаксический сахар
        public SampleDatabase()
        {
        }

        // Пользовательский конструктор
        public SampleDatabase(string companyName, string contactName, string phone)
        {
            CompanyName = companyName;
            ContactName = contactName;
            Phone = phone;
        }

        // Метод для проверки того, что указанный Id заказчика существует в базе данных и возврат его для сохранения 
        // в глобальной переменной
        public int SampleDatabaseSetId(int id) 
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);

                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE Id ='" + id + "'", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = (int)reader["Id"];
                    }
                }
                else
                {
                    // TODO: 
                }
                reader.Close();
            }
                return Id;
        }

        // Метод для обновления данных для существующего заказчика с данными, переданными в поля класса Customers
        public int SampleDatabaseUpdate(int id)
        {
            int Id = id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);

                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = new SqlCommand("UPDATE Customers SET CompanyName = @companyName, contactName = @contactName, Phone = @Phone  WHERE Id ='" + id + "'", connection);
                adapter.UpdateCommand.Parameters.AddWithValue("@companyName", CompanyName);
                adapter.UpdateCommand.Parameters.AddWithValue("@contactName", ContactName);
                adapter.UpdateCommand.Parameters.AddWithValue("@phone", Phone);

                adapter.UpdateCommand.ExecuteNonQuery();

            }
            return Id;
        }

        /// <summary>
        /// Чтение содержимого существующей записи с глобальной переменной Id и возврат CustomerID для последующей
        /// проверки в классе SampleDatabase описания шагов  на соотвествие ожидаемому результату
        /// </summary>
        /// <param name="id">идентификатор клиента</param>
        /// <returns>Возвращает CustomerID</returns>
        public string SampleDatabaseRead(int id)
        {
            int Id = id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);

                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE Id = '" + id + "'", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var customer = new Customer();
                        customer.Id = (int)reader["Id"];
                        customer.CustomerID = (string)reader["CustomerID"];
                        customer.CompanyName = (string)reader["CompanyName"];
                        customer.ContactName = (string)reader["ContactName"];
                        customer.Phone = (string)reader["Phone"];

                        customers.Add(customer);

                        CustomerID = customer.CustomerID;
                    }
                }
                else
                {
                    // TODO: 
                }
                reader.Close();

            }
            return CustomerID;
        }
    }

    public class SampleDatabaseLinq : Customer
    {
        // Создание списка клиентов
        List<Customer> customers = new List<Customer>();
        public int SelectIdSampleDatabaseLinq(int id)
        {
            // Строка подключения.
            DataContext db = new DataContext
                (@"(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VS2022_Projects\SampleDatabaseWalkthrough\SampleDatabaseWalkthrough\SampleDatabase.mdf");

            // Получить типизированную таблицу для выполнения запроса.
            Table<Customer> Customers = db.GetTable<Customer>();

            // Запрос по клиентам с указанным id
            IQueryable<Customer> custQuery =
                from cust in Customers
                where cust.Id == id
                select cust;

            foreach (Customer cust in custQuery)
            {
                var customer = new Customer();
                customer.Id = cust.Id;
                customer.CustomerID = cust.CustomerID;
                customer.CompanyName = cust.CompanyName;
                customer.ContactName = cust.ContactName;
                customer.Phone = cust.Phone;

                customers.Add(customer);
            }
            return Id;
        }

    }
}
