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


        public void CreateDB(string dbname)
        {
            DbConnection newconnection = _factory.CreateConnection(_connectionString);
            newconnection.Open();
            if (newconnection.State != ConnectionState.Open)
            {
                Console.WriteLine("Connection Error");
                throw new Exception("Connection Fail");
            }

            string commandText = "USE MASTER; CREATE DATABASE @DatabaseName;";
            DbCommand command = _factory.CreateCommand(commandText,newconnection);
            command.AddParameter("DatabaseName", dbname);
            command.ExecuteNonQuery();

        }
        public void Dispose()
        {
            _transaction.Rollback();
            _connection.Dispose();
        }
    }
}
