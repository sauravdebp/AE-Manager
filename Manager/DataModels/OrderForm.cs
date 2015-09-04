using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Data.SqlClient;
using Manager.Utilities;
using System.Data;
using System.IO;

namespace Manager.DataModels
{
    public partial class OrderForm : INPC_impl
    {
        String orderId = null;
        public String OrderId
        {
            get { return orderId; }
            private set
            {
                if(value != orderId)
                {
                    orderId = value;
                    NotifyPropertyChanged("OrderId");
                }
            }
        }

        public List<OrderItem>[] UpdatedOrderItems = new List<OrderItem>[2] { new List<OrderItem>(), new List<OrderItem>() };
        
        ObservableCollection<OrderItem> orderItems = new ObservableCollection<OrderItem>();
        public ObservableCollection<OrderItem> OrderItems
        {
            get { return orderItems; }
            set
            {
                if(value != orderItems)
                {
                    orderItems = value;
                    NotifyPropertyChanged("OrderItems");
                }
            }
        }

        Client client;
        public Client Client
        {
            get { return client; }
            set
            {
                if (value != client)
                {
                    client = value;
                    NotifyPropertyChanged("Client");
                }
            }
        }

        Challan challan;
        public Challan Challan
        {
            get { return challan; }
            set
            {
                if(value != challan)
                {
                    challan = value;
                    NotifyPropertyChanged("Challan");
                }
            }
        }

        DateTime orderDate = DateTime.Now;
        public DateTime OrderDate
        {
            get { return orderDate; }
            set
            {
                if (value != orderDate)
                {
                    orderDate = value;
                    NotifyPropertyChanged("OrderDate");
                }
            }
        }

        bool isApproved = false;
        public bool IsApproved
        {
            get { return isApproved; }
            set
            {
                if(value != isApproved)
                {
                    isApproved = value;
                    NotifyPropertyChanged("IsApproved");
                }
            }
        }

        string pocNo = null;
        public string PocNo
        {
            get { return pocNo; }
            set
            {
                if(value != pocNo)
                {
                    pocNo = value;
                    NotifyPropertyChanged("PocNo");
                }
            }
        }

        string orderHardcopyUrl = null;
        public string OrderHardcopyUrl
        {
            get { return orderHardcopyUrl; }
            set
            {
                if(value != orderHardcopyUrl)
                {
                    orderHardcopyUrl = value;
                    NotifyPropertyChanged("OrderHardcopyUrl");
                }
            }
        }

        int orderTotalAmount = 0;
        public int OrderTotalAmount
        {
            get 
            {
                int total = 0;
                foreach(OrderItem item in OrderItems)
                {
                    total += item.AdjustedRate * item.Qty;
                }
                return total;
            }
            private set
            {
                if(value != orderTotalAmount)
                {
                    orderTotalAmount = value;
                    NotifyPropertyChanged("OrderTotalAmount");
                }
            }
        }

        public OrderForm()
        {
            OrderItems.CollectionChanged += OrderItems_CollectionChanged;
        }

        void OrderItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("OrderTotalAmount");
            if(IsPersist)
            {
                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        foreach (OrderItem item in e.NewItems)
                        {
                            UpdatedOrderItems[0].Add(item);
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        foreach (OrderItem item in e.OldItems)
                        {
                            UpdatedOrderItems[1].Add(item);
                        }
                        break;
                }
            }
        }

        static async Task<List<OrderForm>> RetrieveOrderFormsByCondition(String condition, params SqlParameter[] parameters)
        {
            List<OrderForm> ordersList = new List<OrderForm>();
            string sqlCmdString = "Select * From dbo.order_forms " + condition;
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();
            OrderForm form = null;

            await Task.Run(async () => 
            {
                try
                {
                    sqlCmd.Parameters.AddRange(parameters);
                    myDb.OpenConnection();
                    sqlAdapt.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        form = new OrderForm()
                        {
                            OrderId = (string)row["order_id"],
                            Client = await Client.RetrieveByClientCode((string)row["client_code"]),
                            OrderDate = (DateTime)row["order_date"],
                            IsApproved = (bool)row["order_approved"],
                            PocNo = (string)row["order_poc_no"],
                            OrderHardcopyUrl = (string)row["order_hardcopy_url"],
                            IsPersist = true
                        };
                        await form.RetrieveOrderChallan();
                        ordersList.Add(form);
                    }
                }
                finally
                {
                    sqlAdapt.Dispose();
                    table.Dispose();
                    myDb.CloseConnection();
                }
            });

            return ordersList;
        }

        public static async Task<List<OrderForm>> RetrieveAllOrderForms()
        {
            return await RetrieveOrderFormsByCondition("");
        }

        public static async Task<OrderForm> RetrieveOrderFormByOrderId(string orderId)
        {
            OrderForm form;
            form = (await RetrieveOrderFormsByCondition("Where order_id = @order_id", new SqlParameter("@order_id", orderId)))[0];
            return form;
        }

        public static async Task<List<OrderForm>> RetrieveOrderFormsByDateTime(DateTime from, DateTime to)
        {
            return await RetrieveOrderFormsByCondition("Where order_date > @from And order_date < @to", 
                new SqlParameter("@from", from),
                new SqlParameter("@to", to)
                );
        }

        public async Task<ObservableCollection<OrderItem>> RetrieveAllOrderItems()
        {
            if(IsPersist)
            {
                OrderItems = new ObservableCollection<OrderItem>(await OrderItem.RetrieveItemsByOrderId(OrderId));
            }            

            return OrderItems;
        }

        public async Task AssignOrderId()
        {
            string sqlCmdString = "Select Max(order_id) as max_order_id From dbo.order_forms";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            string maxCode;
            int newCodeNumeric;
            OrderId = "ORD";

            await Task.Run(() =>
            {
                try
                {
                    myDb.OpenConnection();
                    maxCode = sqlCmd.ExecuteScalar() as string;
                    if (maxCode != null)
                    {
                        newCodeNumeric = Convert.ToInt16(maxCode.Substring(3)) + 1;
                        for (int i = 0; i < 7 - newCodeNumeric.ToString().Length; i++)
                        {
                            OrderId += "0";
                        }
                        OrderId += newCodeNumeric;
                    }
                    else
                    {
                        OrderId = "ORD0000001";
                    }
                }
                finally
                {
                    myDb.CloseConnection();
                }
            });
        }

        public async Task<string> AssignPocNo()
        {
            string sqlCmdString = "Select Max(order_poc_no) as max_poc_no From dbo.order_forms";
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, new SqlConnection(Database.connectionString));
            string maxPoc;
            int newPocNo;
            PocNo = "POC";

            await Task.Run(() =>
            {
                try
                {
                    sqlCmd.Connection.Open();
                    maxPoc = sqlCmd.ExecuteScalar() as String;
                    if (maxPoc != "")
                    {
                        newPocNo = Convert.ToInt16(maxPoc.Substring(3)) + 1;
                        for (int i = 0; i < 7 - newPocNo.ToString().Length; i++)
                        {
                            PocNo += "0";
                        }
                        PocNo += newPocNo;
                    }
                    else
                    {
                        PocNo = "POC0000001";
                    }
                }
                finally
                {
                    sqlCmd.Connection.Close();
                }
            });
            
            return PocNo;
        }

        public override bool Validate()
        {
            if(OrderItems.Count == 0 || Client == null)
            {
                return false;
            }
            return true;
        }

        public async Task PersistInfo()
        {
            if (!Validate())
                throw new Exception("Order form details not valid");
            if(!Client.IsPersist)
            {
                await Client.PersistInfo();
                NotifyPropertyChanged("Client");
            }
            string sqlCmdString = "Insert Into dbo.order_forms Values (@order_id, @client_code, @order_date, @order_approved, @order_poc_no, @order_hardcopy_url)";
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, Database.SharedConnection);
            if (OrderId == null)
                await AssignOrderId();

            await Task.Run(async () =>
            {
                try
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@order_id", OrderId));
                    sqlCmd.Parameters.Add(new SqlParameter("@client_code", Client.ClientCode));
                    sqlCmd.Parameters.Add(new SqlParameter("@order_date", DateTime.Now));
                    sqlCmd.Parameters.Add(new SqlParameter("@order_approved", IsApproved));
                    sqlCmd.Parameters.Add(new SqlParameter("@order_poc_no", (PocNo == null) ? "" : PocNo));
                    sqlCmd.Parameters.Add(new SqlParameter("@order_hardcopy_url", (OrderHardcopyUrl == null) ? "" : OrderHardcopyUrl));
                    sqlCmd.Transaction = Database.InitiateSharedTransaction(this);
                    sqlCmd.ExecuteNonQuery();
                    int itemNo = 1;
                    foreach (OrderItem item in OrderItems)
                    {
                        await item.PersistInfo(this, itemNo++);
                    }
                    IsPersist = true;
                }
                catch (Exception ex)
                {
                    Database.EndSharedTransaction(this, true);
                    throw ex;
                }
                finally
                {
                    Database.EndSharedTransaction(this);
                }
            });
        }
        
        public async Task UpdateInfo()
        {
            string sqlCmdString = "Update dbo.order_forms " + 
                "Set order_approved = @order_approved, order_poc_no = @order_poc_no, order_hardcopy_url = @order_hardcopy_url " +
                "Where order_id = @order_id";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            if (OrderId == null)
                await AssignOrderId();

            await Task.Run(() =>
            {
                try
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@order_id", OrderId));
                    sqlCmd.Parameters.Add(new SqlParameter("@order_approved", IsApproved));
                    sqlCmd.Parameters.Add(new SqlParameter("@order_poc_no", (PocNo == null) ? "" : PocNo));
                    sqlCmd.Parameters.Add(new SqlParameter("@order_hardcopy_url", (OrderHardcopyUrl == null) ? "" : OrderHardcopyUrl));
                    sqlCmd.Transaction = myDb.InitiateTransaction();
                    sqlCmd.ExecuteNonQuery();
                    IsPersist = true;
                }
                catch (Exception ex)
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

        void UpdateOrderItems()
        {

        }

        public async void Reset()
        {
            await AssignOrderId();
            OrderItems.Clear();
            if (Client != null && !Client.IsPersist)
                Client.Reset();
            OrderDate = DateTime.Now;
            IsApproved = false;
            PocNo = "";
            OrderHardcopyUrl = "";
            OrderTotalAmount = 0;
            IsPersist = false;
        }

        async Task UpdateOrderItemsStock()
        {
            foreach(OrderItem item in OrderItems)
            {
                if (item.Qty > await item.Item.RetrieveLatestStock())
                    throw (new Exception("Not enough stock"));
                else
                {
                    item.Item.StockQty -= item.Qty;
                    item.Item.UpdateStockQty();
                }
            }
        }

        public async Task ApproveOrder()
        {
            if (!Validate())
                throw new Exception("Cannot approve order");
            try
            {
                await UpdateOrderItemsStock();
                await AssignPocNo();
                IsApproved = true;
                if(IsPersist)
                {
                    await UpdateInfo();
                }
            }
            catch(Exception ex)
            {
                GlobalAppStatus.AppStatus.StatusMessage(ex.Message, GlobalAppStatus.MessageType.ERROR);
            }
        }

        public async Task<Challan> RetrieveOrderChallan()
        {
            if(Challan == null)
                return (Challan = await Challan.RetrieveChallanByOrder(this));
            return Challan;
        }

        public void NotifyAllPropertyChanged()
        {
            NotifyPropertyChanged("OrderId");
            NotifyPropertyChanged("OrderItems");
            NotifyPropertyChanged("Client");
            NotifyPropertyChanged("OrderDate");
            NotifyPropertyChanged("IsApproved");
            NotifyPropertyChanged("PocNo");
            NotifyPropertyChanged("OrderHardcopyUrl");
            NotifyPropertyChanged("PaymentRcvd");
            NotifyPropertyChanged("OrderTotalAmount");
        }
    }
}
