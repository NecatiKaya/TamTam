using System;
using System.Data;

namespace Caterpillar.Core.Data
{
    public abstract class UnitOfWorkFactory
    {
        public static IUnitOfWork Create(string providerName, string connectionString, bool ownsConnection = false) 
        {
            IUnitOfWork uow = null;

            IDbConnection dbConnection = SqlFactory.CreateDbConnection(providerName = "System.Data.SqlClient", connectionString, false);
            //Providers are listed below. Refer to link https://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.sqldatasource.providername(v=vs.110).aspx
            switch (providerName)
            {
                case "System.Data.SqlClient":
                    uow = new AdoNetUnitOfWork(dbConnection, ownsConnection);
                    break;
                case "System.Data.OleDb":
                    throw new NotSupportedException("Please implement Unit of Work with OleDb Client.");
                case "System.Data.Odbc":
                    throw new NotSupportedException("Please implement Unit of Work with Odbc Client.");
                case "System.Data.OracleClient":
                    throw new NotSupportedException("Please implement Unit of Work with Oracle Client.");
                default:
                    break;
            }
            return uow;
        }

        /// <summary>
        /// Creates new AdoNetUnitOfWork as default Unit of Work class.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="openConnection"></param>
        /// <returns></returns>
        public static IUnitOfWork Create(string connectionString, bool ownsConnection = false)
        {
            IDbConnection dbConnection = SqlFactory.CreateDbConnection("System.Data.SqlClient", connectionString, false);
            IUnitOfWork uow = new AdoNetUnitOfWork(dbConnection, ownsConnection);
            return uow;
        }
    }
}
