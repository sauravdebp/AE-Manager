using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Utilities;

namespace Manager.DataModels
{
    public class ChallanType : INPC_impl
    {
        public override bool Validate()
        {
            throw new NotImplementedException();
        }

        String typeName;
        public String TypeName
        {
            get { return typeName; }
            set
            {
                if(value != typeName)
                {
                    typeName = value;
                    NotifyPropertyChanged("TypeName");
                }
            }
        }

        public static async Task<List<ChallanType>> RetrieveAllChallanTypes()
        {
            List<ChallanType> typeList = new List<ChallanType>();
            string sqlCmdString = "Select * From dbo.challan_types";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();
            ChallanType challanType = null;

            await Task.Run(() =>
            {
                try
                {
                    myDb.OpenConnection();
                    sqlAdapt.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        challanType = new ChallanType()
                        {
                            TypeName = (String)row["type_name"],
                            IsPersist = true
                        };
                        typeList.Add(challanType);
                    }
                }
                finally
                {
                    sqlAdapt.Dispose();
                    table.Dispose();
                    myDb.CloseConnection();
                }
            });
            
            return typeList;
        }
    }
}
