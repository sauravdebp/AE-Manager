using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Manager.Utilities;

namespace Manager.DataModels
{
    public class StockItem : INPC_impl
    {
        String itemSkuCode;
        public String ItemSkuCode
        {
            get { return itemSkuCode; }
            set
            {
                if(value !=  itemSkuCode)
                {
                    itemSkuCode = value;
                    NotifyPropertyChanged("ItemSkuCode");
                }
            }
        }

        String itemName;
        public String ItemName
        {
            get { return itemName; }
            set
            {
                if(value != itemName)
                {
                    itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        String itemDescription;
        public String ItemDescription
        {
            get { return itemDescription; }
            set
            {
                if (value != itemDescription)
                {
                    itemDescription = value;
                    NotifyPropertyChanged("ItemDescription");
                }
            }
        }

        int itemRate;
        public int ItemRate
        {
            get { return itemRate; }
            set
            {
                if (value != itemRate)
                {
                    itemRate = value;
                    NotifyPropertyChanged("ItemRate");
                }
            }
        }

        int stockQty;
        public int StockQty
        {
            get { return stockQty; }
            set
            {
                if (value != stockQty)
                {
                    stockQty = value;
                    NotifyPropertyChanged("StockQty");
                }
            }
        }

        public async Task<int> RetrieveLatestStock()
        {
            string sqlCmdString = "Select stock_qty From dbo.stock_items Where item_sku_code = @item_sku_code";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);

            await Task.Run(() =>
            {
                try
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@item_sku_code", ItemSkuCode));
                    myDb.OpenConnection();
                    StockQty = (int)sqlCmd.ExecuteScalar();
                }
                finally
                {
                    myDb.CloseConnection();
                }
            });
            
            return StockQty;
        }

        public async void UpdateStockQty()
        {
            string sqlCmdString = "Update dbo.stock_items Set stock_qty = @stock_qty Where item_sku_code = @item_sku_code";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);

            await Task.Run(() =>
            {
                try
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@item_sku_code", ItemSkuCode));
                    sqlCmd.Parameters.Add(new SqlParameter("@stock_qty", StockQty));
                    sqlCmd.Transaction = myDb.InitiateTransaction();
                    sqlCmd.ExecuteNonQuery();
                    IsPersist = true;
                }
                catch(Exception ex)
                {
                    myDb.EndTransaction(true);
                    throw ex;
                }
                finally
                {
                    myDb.EndTransaction();
                }
            });
        }

        public static async Task<List<StockItem>> RetrieveAllStockItems()
        {
            List<StockItem> itemList = new List<StockItem>();
            string sqlCmdString = "Select * From dbo.stock_items";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();
            StockItem item = null;

            await Task.Run(() => 
            {
                try
                {
                    myDb.OpenConnection();
                    sqlAdapt.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        item = new StockItem()
                        {
                            ItemSkuCode = row["item_sku_code"] as String,
                            ItemRate = Convert.ToInt32(row["item_rate"]),
                            StockQty = Convert.ToInt32(row["stock_qty"]),
                            ItemName = row["item_name"] as String,
                            ItemDescription = row["item_description"] as String,
                            IsPersist = true
                        };
                        itemList.Add(item);
                    }
                }
                finally
                {
                    sqlAdapt.Dispose();
                    table.Dispose();
                    myDb.CloseConnection();
                }
            });
            
            return itemList;
        }

        public static async Task<StockItem> RetrieveBySkuCode(String skuCode)
        {
            StockItem stockItem = new StockItem();
            string sqlCmdString = "Select * From dbo.stock_items Where item_sku_code = @item_sku_code";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();

            await Task.Run(() =>
            {
                try
                {
                    myDb.OpenConnection();
                    sqlCmd.Parameters.Add(new SqlParameter("@item_sku_code", skuCode));
                    sqlAdapt.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        stockItem = new StockItem()
                        {
                            ItemSkuCode = (string)row["item_sku_code"],
                            ItemRate = Convert.ToInt32(row["item_rate"]),
                            StockQty = Convert.ToInt32(row["stock_qty"]),
                            ItemName = (string)row["item_name"],
                            //ItemDescription = (String)(reader["item_description"] == null ? "" : reader["item_description"]),
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
            
            return stockItem;
        }

        public async void PersistInfo()
        {
            if (!Validate())
                throw new Exception("Stock Item details not valid");
            if (IsPersist && IsUpdated)
            {
                await UpdateInfo();
                return;
            }
            else if (IsPersist && !IsUpdated)
                return;
            string sqlCmdString = "Insert Into dbo.stock_items (item_sku_code, item_rate, stock_qty, item_name, item_description) " +
                "Values (@item_sku_code, @item_rate, @stock_qty, @item_name, @item_description)";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);

            await Task.Run(() =>
            {
                try
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@item_sku_code", ItemSkuCode));
                    sqlCmd.Parameters.Add(new SqlParameter("@item_rate", ItemRate));
                    sqlCmd.Parameters.Add(new SqlParameter("@stock_qty", StockQty));
                    sqlCmd.Parameters.Add(new SqlParameter("@item_name", ItemName));
                    sqlCmd.Parameters.Add(new SqlParameter("@item_description", (ItemDescription == null) ? "" : ItemDescription));
                    sqlCmd.Transaction = myDb.InitiateTransaction();
                    sqlCmd.ExecuteNonQuery();
                    IsPersist = true;
                }
                catch(Exception ex)
                {
                    myDb.EndTransaction(true);
                    throw ex;
                }
                finally
                {
                    myDb.EndTransaction();
                }
            });
            
        }

        async Task UpdateInfo()
        {
            if (!IsPersist || !IsUpdated)
                return;
            string sqlCmdString = "Update dbo.stock_items " +
                "Set item_rate = @item_rate, stock_qty = @stock_qty, item_name = @item_name, item_description = @item_description " +
                "Where item_sku_code = @item_sku_code";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);

            await Task.Run(() =>
            {
                try
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@item_rate", ItemRate));
                    sqlCmd.Parameters.Add(new SqlParameter("@stock_qty", StockQty));
                    sqlCmd.Parameters.Add(new SqlParameter("@item_name", ItemName));
                    sqlCmd.Parameters.Add(new SqlParameter("@item_description", (ItemDescription == null) ? "" : ItemDescription));
                    sqlCmd.Parameters.Add(new SqlParameter("@item_sku_code", ItemSkuCode));
                    sqlCmd.Transaction = myDb.InitiateTransaction();
                    sqlCmd.ExecuteNonQuery();
                    IsUpdated = false;
                }
                catch(Exception ex)
                {
                    myDb.EndTransaction(true);
                    throw ex;
                }
                finally
                {
                    myDb.EndTransaction();
                }
            });
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
