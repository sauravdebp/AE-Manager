using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Utilities;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace Manager.DataModels
{
    public partial class Client : INPC_impl
    {
        string clientCode = null;
        public string ClientCode
        {
            get { return clientCode; }
            set 
            {
                if(value != clientCode)
                {
                    clientCode = value;
                    NotifyPropertyChanged("ClientCode");
                }
            }
        }

        string clientName = null;
        public string ClientName
        {
            get { return clientName; }
            set
            {
                if(value != clientName)
                {
                    clientName = value;
                    NotifyPropertyChanged("ClientName");
                }
            }
        }

        String tinNo = null;
        public String TinNo
        {
            get { return tinNo; }
            set
            {
                if(value != tinNo)
                {
                    tinNo = value;
                    NotifyPropertyChanged("TinNo");
                }
            }
        }

        String mainAddress = null;
        public String MainAddress
        {
            get { return mainAddress; }
            set
            {
                if(value != mainAddress)
                {
                    mainAddress = value;
                    NotifyPropertyChanged("MainAddress");
                }
            }
        }

        String primaryContact = null;
        public String PrimaryContact
        {
            get { return primaryContact; }
            set
            {
                if (value != primaryContact)
                {
                    primaryContact = value;
                    NotifyPropertyChanged("PrimaryContact");
                }
            }
        }

        String primaryContactName = null;
        public String PrimaryContactName
        {
            get { return primaryContactName; }
            set
            {
                if(value != primaryContactName)
                {
                    primaryContactName = value;
                    NotifyPropertyChanged("PrimaryContactName");
                }
            }
        }

        String primaryEmail = null;
        public String PrimaryEmail
        {
            get { return primaryEmail; }
            set
            {
                if (value != primaryEmail)
                {
                    primaryEmail = value;
                    NotifyPropertyChanged("PrimaryEmail");
                }
            }
        }

        public static async Task<List<Client>> RetrieveAllClients()
        {
            List<Client> clientList = new List<Client>();
            string sqlCmdString = "Select * From dbo.clients";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();
            Client client = null;

            await Task.Run(() => 
            {
                try
                {
                    myDb.OpenConnection();
                    sqlAdapt.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        client = new Client()
                        {
                            ClientCode = (string)row["client_code"],
                            ClientName = (string)row["client_name"],
                            TinNo = (string)row["tin_no"],
                            MainAddress = (string)row["main_address"],
                            PrimaryContactName = (string)row["primary_contact_name"],
                            PrimaryContact = (string)row["primary_contact"],
                            PrimaryEmail = (string)row["primary_email"],
                            IsPersist = true
                        };
                        clientList.Add(client);
                    }
                }
                finally
                {
                    sqlAdapt.Dispose();
                    table.Dispose();
                    myDb.CloseConnection();
                }
            });
            return clientList;
        }

        public static async Task<Client> RetrieveByClientCode(string clientCode)
        {
            Client client = new Client();
            string sqlCmdString = "Select * From dbo.clients Where client_code = @client_code";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();

            await Task.Run(() =>
            {
                try
                {
                    myDb.OpenConnection();
                    sqlCmd.Parameters.Add(new SqlParameter("@client_code", clientCode));
                    sqlAdapt.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        client = new Client()
                        {
                            ClientCode = (String)row["client_code"],
                            ClientName = (String)row["client_name"],
                            TinNo = (String)row["tin_no"],
                            MainAddress = (String)row["main_address"],
                            PrimaryContactName = (String)row["primary_contact_name"],
                            PrimaryContact = (String)row["primary_contact"],
                            PrimaryEmail = (String)row["primary_email"],
                            IsPersist = true
                        };
                    }
                }
                finally
                {
                    sqlAdapt.Dispose();
                    table.Dispose();
                    myDb.CloseConnection();
                }
            });
            
            return client;
        }

        public async Task AssignClientCode()
        {
            string sqlCmdString = "Select Max(client_code) as max_client_code From dbo.clients";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            string maxCode;
            int newCodeNumeric;
            ClientCode = "CL";

            await Task.Run(() =>
            {
                try
                {
                    myDb.OpenConnection();
                    maxCode = sqlCmd.ExecuteScalar() as string;
                    if (maxCode != null)
                    {
                        newCodeNumeric = Convert.ToInt16(maxCode.Substring(2)) + 1;
                        for (int i = 0; i < 4 - newCodeNumeric.ToString().Length; i++)
                        {
                            ClientCode += "0";
                        }
                        ClientCode += newCodeNumeric;
                    }
                    else
                    {
                        ClientCode = "CL0001";
                    }
                }
                finally
                {
                    myDb.CloseConnection();
                }
            });

            
        }

        public override bool Validate()
        {
            if (ClientCode == null || ClientName == null || MainAddress == null || PrimaryContact == null || PrimaryEmail == null)
                return false;
            return true;
        }

        public async Task PersistInfo()
        {
            if (!Validate())
                throw new Exception("Client details not valid");
            string sqlCmdString = "Insert Into dbo.clients (client_code, client_name, tin_no, main_address, primary_contact_name, primary_contact, primary_email) " + 
                "Values (@client_code, @client_name, @tin_no, @main_address, @primary_contact_name, @primary_contact, @primary_email)";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            if (ClientCode == null)
                await AssignClientCode();

            await Task.Run(() =>
            {
                try
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@client_code", ClientCode));
                    sqlCmd.Parameters.Add(new SqlParameter("@client_name", ClientName));
                    sqlCmd.Parameters.Add(new SqlParameter("@tin_no", TinNo));
                    sqlCmd.Parameters.Add(new SqlParameter("@main_address", MainAddress));
                    sqlCmd.Parameters.Add(new SqlParameter("@primary_contact", PrimaryContact));
                    sqlCmd.Parameters.Add(new SqlParameter("@primary_contact_name", PrimaryContactName));
                    sqlCmd.Parameters.Add(new SqlParameter("@primary_email", PrimaryEmail));
                    sqlCmd.Transaction = myDb.InitiateTransaction();
                    sqlCmd.ExecuteNonQuery();
                    IsPersist = true;
                }
                catch (IOException ex)
                {
                    GlobalAppStatus.AppStatus.StatusMessage(ex.Message, GlobalAppStatus.MessageType.ERROR);
                    myDb.EndTransaction(true);
                }
                catch (SqlException ex)
                {
                    GlobalAppStatus.AppStatus.StatusMessage(ex.Message, GlobalAppStatus.MessageType.ERROR);
                    myDb.EndTransaction(true);
                }
                finally
                {
                    myDb.EndTransaction();
                }
            });
        }

        public void UpdateInfo()
        {

        }

        public async void Reset()
        {
            await AssignClientCode();
            ClientName = MainAddress = PrimaryContact = PrimaryEmail = "";
            IsPersist = false;
        }
    }
}
