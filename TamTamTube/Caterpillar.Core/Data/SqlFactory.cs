using Caterpillar.Core.Helpers;
using System;
using System.Configuration;
using System.Data.Common;

namespace Caterpillar.Core.Data
{
    public static class SqlFactory
    {
        /// <summary>
        /// Given a provider name and connection string, create the DbProviderFactory and DbConnection. 
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DbConnection CreateDbConnection(string providerName, string connectionString, bool openConnection = false)
        {
            //http://www.codeproject.com/Articles/55890/Don-t-hard-code-your-DataProviders

            ParameterHelper.CheckForNull(providerName, "providerName");
            ParameterHelper.CheckForEmpty(providerName, "providerName");

            ParameterHelper.CheckForNull(connectionString, "connectionString");
            ParameterHelper.CheckForEmpty(connectionString, "connectionString");

            DbConnection connection = null;
            object supportMultipleResultSets = false;
            // Create the DbProviderFactory and DbConnection. 
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            
            //DbConnectionStringBuilder  connectionStringBuilder = factory.CreateConnectionStringBuilder();
            //if (connectionStringBuilder != null)
            //{
            //    if (connectionStringBuilder.TryGetValue("MultipleActiveResultSets", out supportMultipleResultSets))
            //    {
            //        if (Convert.ToBoolean(supportMultipleResultSets))
            //        {
            //            connectionStringBuilder.
            //        }
            //    }
            //}

            connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            if (openConnection)
            {
                connection.Open();
            }
            // Return the connection. 
            return connection;
        }

        /// <summary>
        /// Creates DbConection from connection string key in configuration file
        /// </summary>
        /// <param name="connectionStringNameInConfig"></param>
        /// <returns></returns>
        public static DbConnection CreateDbConnection(string connectionStringNameInConfig, bool openConnection = false)
        {
            ParameterHelper.CheckForNull(connectionStringNameInConfig, "connectionStringNameInConfig");
            ParameterHelper.CheckForEmpty(connectionStringNameInConfig, "connectionStringNameInConfig");

            ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings[connectionStringNameInConfig];
            string connectionString = connectionSettings.ConnectionString;
            string providerName = connectionSettings.ProviderName;
            return CreateDbConnection(providerName, connectionString, openConnection);
        }

        public static DbConnection CreateDbConnection(DbDataProvider provider, string connectionString, bool openConnection = false)
        {
            switch (provider)
            {
                case DbDataProvider.MsSql:
                    return CreateDbConnection("System.Data.SqlClient", connectionString, openConnection);
                case DbDataProvider.Oracle:
                    throw new NotSupportedException("Please implement Unit of Work with Oracle Client.");
                case DbDataProvider.Odbc:
                    throw new NotSupportedException("Please implement Unit of Work with Odbc Client.");
                case DbDataProvider.OleDb:
                    throw new NotSupportedException("Please implement Unit of Work with OleDb Client.");
                default:
                    throw new NotSupportedException("Not supported data provider.");
            }
        }
    }
}
