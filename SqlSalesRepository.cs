using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDepart
{
    public class SqlSalesRepository : IDisposable
    {
        private DbConnection _connection;
        private DbTransaction _transaction;
        private string _connectionString;
        private readonly DbProviderFactory _factory;

        public SqlSalesRepository(string connectionString, DbProviderFactory factory)
        {
            _factory = factory;
            Initialize(connectionString);
        }

        private void Initialize(string connectionString)
        {
            _connectionString = connectionString;
            _connection = _factory.CreateConnection(connectionString);
            _connection.Open();
            if(_connection.State != ConnectionState.Open)
            {
                Console.WriteLine("Connection Error");
                throw new Exception("Connection Fail");
            }
            _transaction = _connection.BeginTransaction();
        }

        

        public void CreateDB()
        {
            DbConnection newconnection = _factory.CreateConnection(_connectionString);
            newconnection.Open();
            if (newconnection.State != ConnectionState.Open)
            {
                Console.WriteLine("Connection Error");
                throw new Exception("Connection Fail");
            }
            string commandText = "USE MASTER; CREATE DATABASE Sales;";
            DbCommand command = _factory.CreateCommand(commandText, newconnection);
            command.ExecuteNonQuery();

        }
        public void DropDB()
        {
            DbConnection newconnection = _factory.CreateConnection(_connectionString);
            newconnection.Open();
            if (newconnection.State != ConnectionState.Open)
            {
                Console.WriteLine("Connection Error");
                throw new Exception("Connection Fail");
            }
            string commandText = "DROP DATABASE Sales;";
            DbCommand command = _factory.CreateCommand(commandText, newconnection);
            command.ExecuteNonQuery();
        }

        public void CreateTables()
        {
            string commandText = @" USE Sales 
                                    CREATE TABLE Customers (ID int PRIMARY KEY, Name nvarchar(50), Surname nvarchar(50))
                                    CREATE TABLE Sellers (ID int PRIMARY KEY, Name nvarchar(50), Surname nvarchar(50))
                                    CREATE TABLE Sale (ID int PRIMARY KEY, SellerID int FOREIGN KEY REFERENCES Sellers(ID), CustomerID int FOREIGN KEY REFERENCES Customers(ID), Price decimal(7,2), Date smalldatetime)";

            DbConnection newconnection = _factory.CreateConnection(_connectionString);
            newconnection.Open();
            if (newconnection.State != ConnectionState.Open)
            {
                Console.WriteLine("Connection Error");
                throw new Exception("Connection Fail");
            }
            using (DbCommand command = _factory.CreateCommand(commandText, newconnection))
            {
                command.ExecuteNonQuery();
            }

        }
        public void DropTables()
        {
            string commandText = @" USE Sales; DROP Table Sale; DROP TABLE Sellers; DROP TABLE Customers;";
                                    
            DbConnection newconnection = _factory.CreateConnection(_connectionString);
            newconnection.Open();
            if (newconnection.State != ConnectionState.Open)
            {
                Console.WriteLine("Connection Error");
                throw new Exception("Connection Fail");
            }
            using (DbCommand command = _factory.CreateCommand(commandText, newconnection))
            {
                command.ExecuteNonQuery();
            }

        }

        public void InsertCustomer(int ID, string Name, string Surname)
        {
            string commandtext = "USE Sales; INSERT INTO Customers VALUES(@id, @name, @surname)";
            using (DbCommand command = _factory.CreateCommand(commandtext, _connection, _transaction))
            {
                command
                    .AddParameter("id", ID)
                    .AddParameter("name", Name)
                    .AddParameter("surname", Surname);
                command.ExecuteNonQuery();
            }
        }

        public void InsertSeller(int ID, string Name, string Surname)
        {
            string commandtext = "USE Sales; INSERT INTO Sellers VALUES(@id, @name, @surname)";
            using (DbCommand command = _factory.CreateCommand(commandtext, _connection, _transaction))
            {
                command
                    .AddParameter("id", ID)
                    .AddParameter("name", Name)
                    .AddParameter("surname", Surname);
                command.ExecuteNonQuery();
            }
        }

        public void InsertSales(int ID, int sellerID, int CustomerID, double Price, DateTime Date)
        {
            string commandtext = "USE Sales; INSERT INTO Sale VALUES(@id, @sellerID, @CustomerID, @price, @date)";
            using (DbCommand command = _factory.CreateCommand(commandtext, _connection, _transaction))
            {
                command
                    .AddParameter("id", ID)
                    .AddParameter("sellerID", sellerID)
                    .AddParameter("customerID", CustomerID)
                    .AddParameter("price", Price)
                    .AddParameter("date", Date);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            string commandtext = "USE Sales; SELECT * FROM Customers";
            using (DbCommand command = _factory.CreateCommand(commandtext,_connection, _transaction))
            {
                using (DbDataReader reader = command.ExecuteReader())
                {
                    List<Customer> customers = new List<Customer>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string name = (string)reader["name"];
                        string surname = (string)reader["surname"];
                        customers.Add(new Customer(id, name, surname));
                    }
                    return customers.ToList<Customer>();
                }
            }
        }
        public IEnumerable<Seller> GetSellers()
        {
            string commandtext = "USE Sales; SELECT * FROM Sellers";
            using (DbCommand command = _factory.CreateCommand(commandtext, _connection, _transaction))
            {
                using (DbDataReader reader = command.ExecuteReader())
                {
                    List<Seller> customers = new List<Seller>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string name = (string)reader["name"];
                        string surname = (string)reader["surname"];
                        customers.Add(new Seller(id, name, surname));
                    }
                    return customers.ToList<Seller>();
                }
            }
        }
        
        public IEnumerable<SalesTable> GetSales()
        {

            string commandtext = "SELECT * FROM Sale";
            using (DbCommand command = _factory.CreateCommand(commandtext, _connection, _transaction)) 
            {
                using (DbDataReader reader = command.ExecuteReader())
                {
                    List<SalesTable> sales = new List<SalesTable>();
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        int sellerid = (int)reader["SellerID"];
                        int customerid = (int)reader["CustomerID"];
                        decimal price = (decimal)reader["Price"];
                        DateTime dateTime = (DateTime)reader["Date"];
                        sales.Add(new SalesTable(id, sellerid, customerid, price, dateTime));
                    }
                    return sales;
                }
            }
        }
        public void Save()
        {
            _transaction.Commit();
            _transaction = _connection.BeginTransaction();
        }
        public void Dispose()
        {
            _transaction.Rollback();
            _connection.Dispose();
        }
    }
}
