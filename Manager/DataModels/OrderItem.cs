using Manager.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Manager.DataModels
{
    public class OrderItem : INPC_impl
    {
        StockItem item;
        public StockItem Item
        {
            get { return item; }
            set
            {
                if(value != item)
                {
                    item = value;
                    if(value != null)
                        AdjustedRate = item.ItemRate + RateAdjustment;
                    NotifyPropertyChanged("Item");
                }
            }
        }

        int qty = 0;
        public int Qty
        {
            get { return qty; }
            set
            {
                if(value != qty && !IsPersist)
                {
                    qty = value;
                    NotifyPropertyChanged("Qty");
                }
            }
        }

        int rateAdjustment;
        public int RateAdjustment
        {
            get { return rateAdjustment; }
            set
            {
                if(value != rateAdjustment)
                {
                    rateAdjustment = value;
                    NotifyPropertyChanged("RateAdjustment");
                    NotifyPropertyChanged("AdjustedRate");
                }
            }
        }

        public int AdjustedRate
        {
            get 
            { 
                if(Item != null)
                    return Item.ItemRate + RateAdjustment;
                return 0;
            }
            set
            {
                if(Item != null)
                {
                    RateAdjustment = value - Item.ItemRate;
                }
                NotifyPropertyChanged("AdjustedRate");
            }
        }

        public int TotalAmount
        {
            get { return AdjustedRate * Qty; }
        }

        public void Reset()
        {
            Item = null;
            Qty = 0;
            RateAdjustment = 0;
        }

        public override bool Validate()
        {
            if (Item == null || Qty == 0)
                return false;
            return true;
        }

        public static async Task<List<OrderItem>> RetrieveItemsByOrderId(string orderId)
        {
            List<OrderItem> orderItemsList = new List<OrderItem>();
            string sqlCmdString = "Select * From dbo.order_items Where order_id = @order_id";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            //SqlDataReader reader = null;
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();
            OrderItem orderItem;

            await Task.Run(async () =>
            {
                try
                {
                    myDb.OpenConnection();
                    sqlCmd.Parameters.Add(new SqlParameter("@order_id", orderId));
                    sqlAdapt.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        orderItem = new OrderItem()
                        {
                            Qty = Convert.ToInt16(row["item_qty"]),
                            RateAdjustment = Convert.ToInt32(row["rate_adjustment"]),
                            IsPersist = true
                        };
                        orderItem.Item = await StockItem.RetrieveBySkuCode((string)row["item_sku_code"]);
                        orderItemsList.Add(orderItem);
                    }
                }
                finally
                {
                    sqlAdapt.Dispose();
                    table.Dispose();
                    myDb.CloseConnection();
                }
            });
            
            return orderItemsList;
        }

        public async Task PersistInfo(OrderForm order, int itemNo)
        {
            if (Qty > Item.StockQty)
                throw new Exception("Not enough stock");
            string sqlCmdString = "Insert Into dbo.order_items Values (@order_id, @item_no, @item_sku_code, @item_qty, @rate_adjustment)";
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, Database.SharedConnection);

            await Task.Run(() =>
            {
                try
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@order_id", order.OrderId));
                    sqlCmd.Parameters.Add(new SqlParameter("@item_no", itemNo));
                    sqlCmd.Parameters.Add(new SqlParameter("@item_sku_code", Item.ItemSkuCode));
                    sqlCmd.Parameters.Add(new SqlParameter("@item_qty", Qty));
                    sqlCmd.Parameters.Add(new SqlParameter("@rate_adjustment", RateAdjustment));
                    sqlCmd.Transaction = Database.InitiateSharedTransaction(this);
                    sqlCmd.ExecuteNonQuery();
                    IsPersist = true;
                }
                catch(Exception)
                {
                    Database.EndSharedTransaction(this, true);
                }
                finally
                {
                    Database.EndSharedTransaction(this);
                }
            });
        }
    }
}
