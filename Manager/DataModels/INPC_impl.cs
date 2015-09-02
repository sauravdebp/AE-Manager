using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.DataModels
{
    public abstract class INPC_impl : INotifyPropertyChanged
    {
        bool isPersist = false;
        public bool IsPersist
        {
            get { return isPersist; }
            protected set
            {
                if (value != isPersist)
                {
                    isPersist = value;
                    NotifyPropertyChanged("IsPersist");
                }
            }
        }

        bool isUpdated = false;
        public bool IsUpdated
        {
            get { return isUpdated; }
            protected set
            {
                if(value != isUpdated)
                {
                    isUpdated = value;
                    NotifyPropertyChanged("IsUpdated");
                }
            }
        }

        public abstract bool Validate();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                IsUpdated = true;
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //public const string connectionString = "Data Source=SAURAV-PC\\SQLEXPRESS;Initial Catalog=aquatek_engineers;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        //protected SqlConnection dbConnection = new SqlConnection(connectionString);

        //SqlTransaction transaction;

        //protected void InitiateTransaction()
        //{
        //    transaction = dbConnection.BeginTransaction();
        //}

        //protected void CommitTransaction()
        //{
        //    transaction.Commit();
        //    dbConnection.Close();
        //}

        //protected void RollbackTransaction()
        //{
        //    transaction.Rollback();
        //    dbConnection.Close();
        //}
    }
}
