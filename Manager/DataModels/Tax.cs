using Manager.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Manager.DataModels
{
    public class Tax : INPC_impl
    {
        public static async Task<List<double>> RetriveTaxesByType(string taxName)
        {
            List<double> taxList = new List<double>();
            string sqlCmdString = "Select tax_amount From dbo.taxes Where tax_name=@tax_name";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();
            double tax;

            await Task.Run(() =>
            {
                try
                {
                    myDb.OpenConnection();
                    sqlCmd.Parameters.Add(new SqlParameter("@tax_name", taxName));
                    sqlAdapt.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        tax = Convert.ToDouble(row["tax_amount"]);
                        taxList.Add(tax);
                    }
                }
                finally
                {
                    sqlAdapt.Dispose();
                    table.Dispose();
                    myDb.CloseConnection();
                }
            });
            
            return taxList;
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
