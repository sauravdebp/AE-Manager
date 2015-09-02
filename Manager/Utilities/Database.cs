using System;
using System.Data.SqlClient;
using System.Threading;

namespace Manager.Utilities
{
    public class Database
    {
        public const string connectionString = "Data Source=SAURAV-PC\\SQLEXPRESS;Initial Catalog=aquatek_engineers;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        static SqlConnection sharedConnection = new SqlConnection(connectionString);
        public static SqlConnection SharedConnection { get { return sharedConnection; } }

        public static SqlTransaction SharedTransaction { get; private set; }

        static object initialSource;

        static bool mustRollback = false;

        SqlConnection connection = new SqlConnection(connectionString);
        public SqlConnection Connection
        {
            get { return connection; }
        }

        public SqlTransaction Transaction { get; private set; }

        public void OpenConnection()
        { 
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
        }

        public void CloseConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
                Connection.Close();
        }

        public SqlTransaction InitiateTransaction()
        {
            if(Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
                Transaction = Connection.BeginTransaction();
            }
            return Transaction;
        }

        public void EndTransaction(bool rollback = false)
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                if (rollback)
                    Transaction.Rollback();
                else
                    Transaction.Commit();
                Connection.Close();
            }
        }

        public static SqlTransaction InitiateSharedTransaction(object source)
        {
            Monitor.Enter(SharedConnection);
            try
            {
                if(SharedConnection.State != System.Data.ConnectionState.Open)
                {
                    SharedConnection.Open();
                    SharedTransaction = SharedConnection.BeginTransaction();
                    initialSource = source;
                }
            }
            finally
            {
                Monitor.Exit(SharedConnection);
            }
            return SharedTransaction;
        }

        public static void EndSharedTransaction(object source, bool rollback = false)
        {
            Monitor.Enter(SharedConnection);
            try
            {
                if(SharedConnection.State != System.Data.ConnectionState.Closed && source == initialSource)
                {
                    if (mustRollback || rollback)
                        SharedTransaction.Rollback();
                    else
                        SharedTransaction.Commit();
                    SharedConnection.Close();
                }
                else if(source != initialSource)
                {
                    mustRollback = rollback;
                }
            }
            finally
            {
                Monitor.Exit(SharedConnection);
            }
        }
    }
}
