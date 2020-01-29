using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDepart
{
    public static class DbFactoryExtensions
    {
        public static DbConnection CreateConnection(this DbProviderFactory factory, string connectionString)
        {
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        public static DbCommand CreateCommand(this DbProviderFactory factory,string commandText, DbConnection connection, DbTransaction transaction = null)
        {
            DbCommand command = factory.CreateCommand();
            command.CommandText = commandText;
            command.Connection = connection;
            command.Transaction = transaction;
            return command;
        }

        public static DbCommand AddParameter(this DbCommand command, string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
            return command;
        }
    }
}
