using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Manager.Utilities;

namespace Manager.DataModels
{
    public partial class Challan : INPC_impl
    {
        const int challansPerBook = 1000;

        OrderForm orderForm;
        public OrderForm OrderForm
        {
            get { return orderForm; }
            set
            {
                if(value != orderForm)
                {
                    orderForm = value;
                    NotifyPropertyChanged("OrderForm");
                }
            }
        }

        ChallanType challanType;
        public ChallanType ChallanType
        {
            get { return challanType; }
            set
            {
                if(value != challanType)
                {
                    challanType = value;
                    NotifyPropertyChanged("ChallanType");
                }
            }
        }

        int bookNo;
        public int BookNo
        {
            get { return bookNo; }
            set
            {
                if(value != bookNo)
                {
                    bookNo = value;
                    NotifyPropertyChanged("BookNo");
                }
            }
        }

        int challanNo;
        public int ChallanNo
        {
            get { return challanNo; }
            set
            {
                if(value != challanNo)
                {
                    challanNo = value;
                    NotifyPropertyChanged("ChallanNo");
                }
            }
        }

        DateTime challanDate = DateTime.Now;
        public DateTime ChallanDate
        {
            get { return challanDate; }
            set
            {
                if(value != challanDate)
                {
                    challanDate = value;
                    NotifyPropertyChanged("ChallanDate");
                }
            }
        }

        int freightCharge;
        public int FreightCharge
        {
            get { return freightCharge; }
            set
            {
                if(value != freightCharge)
                {
                    freightCharge = value;
                    NotifyPropertyChanged("FreightCharge");
                    NotifyPropertyChanged("GrandTotal");
                    NotifyPropertyChanged("PendingAmount");
                }
            }
        }

        string vehicleNo;
        public string VehicleNo
        {
            get { return vehicleNo; }
            set
            {
                if(value != vehicleNo)
                {
                    vehicleNo = value;
                    NotifyPropertyChanged("VehicleNo");
                }
            }
        }

        string driverMobile;
        public string DriverMobile
        {
            get { return driverMobile; }
            set
            {
                if (value != driverMobile)
                {
                    driverMobile = value;
                    NotifyPropertyChanged("DriverMobile");
                }
            }
        }

        String consignmentNoteNo;
        public String ConsignmentNoteNo
        {
            get { return consignmentNoteNo; }
            set
            {
                if (value != consignmentNoteNo)
                {
                    consignmentNoteNo = value;
                    NotifyPropertyChanged("ConsignmentNoteNo");
                }
            }
        }

        String transporterName;
        public String TransporterName
        {
            get { return transporterName; }
            set
            {
                if (value != transporterName)
                {
                    transporterName = value;
                    NotifyPropertyChanged("TransporterName");
                }
            }
        }

        String specialNote;
        public String SpecialNote
        {
            get { return specialNote; }
            set
            {
                if(value != specialNote)
                {
                    specialNote = value;
                    NotifyPropertyChanged("SpecialNote");
                }
            }
        }

        double vat;
        public double Vat
        {
            get { return vat; }
            set
            {
                if(value != vat)
                {
                    vat = value;
                    NotifyPropertyChanged("Vat");
                    NotifyPropertyChanged("SubTotal");
                    NotifyPropertyChanged("GrandTotal");
                    NotifyPropertyChanged("PendingAmount");
                }
            }
        }

        double cst;
        public double Cst
        {
            get { return cst; }
            set
            {
                if (value != cst)
                {
                    cst = value;
                    NotifyPropertyChanged("Cst");
                    NotifyPropertyChanged("SubTotal");
                    NotifyPropertyChanged("GrandTotal");
                    NotifyPropertyChanged("PendingAmount");
                }
            }
        }

        public double SubTotal
        {
            get { return OrderForm.OrderTotalAmount + (vat / 100) * OrderForm.OrderTotalAmount + (cst / 100) * OrderForm.OrderTotalAmount; }
        }

        public int GrandTotal
        {
            get { return Convert.ToInt32(SubTotal + FreightCharge); }
        }

        int receivedAmount;
        public int ReceivedAmount
        {
            get { return receivedAmount; }
            set
            {
                if (value != receivedAmount)
                {
                    receivedAmount = value;
                    NotifyPropertyChanged("ReceivedAmount");
                    NotifyPropertyChanged("PendingAmount");
                }
            }
        }

        public int PendingAmount
        {
            get { return GrandTotal - ReceivedAmount; }
        }

        public override bool Validate()
        {

            return true;
        }

        public async Task AssignChallanBookSerialNo()
        {
            string sqlCmdString = "Select Top 1 book_no, max(sl_no) as max_sl_no From dbo.challans Group By book_no Order By book_no Desc";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();
            BookNo = 1;
            ChallanNo = 0;

            await Task.Run(() =>
            {
                try
                {
                    myDb.OpenConnection();
                    sqlAdapt.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        BookNo = (int)row["book_no"];
                        ChallanNo = (int)row["max_sl_no"];
                    }
                    if (ChallanNo >= challansPerBook)
                    {
                        ChallanNo = 1;
                        BookNo++;
                    }
                    else
                        ChallanNo++;
                }
                finally
                {
                    sqlAdapt.Dispose();
                    table.Dispose();
                    myDb.CloseConnection();
                }
            });
        }

        public async Task PersistInfo()
        {
            if (!Validate())
                throw new Exception("Client details not valid");
            string sqlCmdString = "Insert Into dbo.challans (book_no, sl_no, challan_date, challan_type, order_id, freight_charge, vehicle_no, driver_mobile, consignment_note_no, transporter_name, vat, cst, special_note, received_amount) " +
                "Values (@book_no, @sl_no, @challan_date, @challan_type, @order_id, @freight_charge, @vehicle_no, @driver_mobile, @consignment_note_no, @transporter_name, @vat, @cst, @special_note, @received_amount)";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);

            await Task.Run(() =>
            {
                try
                {
                    sqlCmd.Parameters.Add(new SqlParameter("@book_no", BookNo));
                    sqlCmd.Parameters.Add(new SqlParameter("@sl_no", ChallanNo));
                    sqlCmd.Parameters.Add(new SqlParameter("@challan_date", ChallanDate));
                    sqlCmd.Parameters.Add(new SqlParameter("@challan_type", ChallanType.TypeName));
                    sqlCmd.Parameters.Add(new SqlParameter("@order_id", OrderForm.OrderId));
                    sqlCmd.Parameters.Add(new SqlParameter("@freight_charge", FreightCharge));
                    sqlCmd.Parameters.Add(new SqlParameter("@vehicle_no", VehicleNo));
                    sqlCmd.Parameters.Add(new SqlParameter("@driver_mobile", DriverMobile));
                    sqlCmd.Parameters.Add(new SqlParameter("@consignment_note_no", ConsignmentNoteNo));
                    sqlCmd.Parameters.Add(new SqlParameter("@transporter_name", TransporterName));
                    sqlCmd.Parameters.Add(new SqlParameter("@vat", Vat));
                    sqlCmd.Parameters.Add(new SqlParameter("@cst", Cst));
                    sqlCmd.Parameters.Add(new SqlParameter("@special_note", SpecialNote));
                    sqlCmd.Parameters.Add(new SqlParameter("@received_amount", ReceivedAmount));
                    sqlCmd.Transaction = myDb.InitiateTransaction();
                    sqlCmd.ExecuteNonQuery();
                    IsPersist = true;
                    OrderForm.Challan = this;
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

        public static async Task<Challan> RetrieveChallanByOrder(OrderForm orderForm)
        {
            Challan challan = null;
            string sqlCmdString = "Select * From dbo.challans Where order_id = @order_id";
            Database myDb = new Database();
            SqlCommand sqlCmd = new SqlCommand(sqlCmdString, myDb.Connection);
            SqlDataAdapter dataAdapt = new SqlDataAdapter(sqlCmd);
            DataTable table = new DataTable();

            await Task.Run(() =>
            {
                try
                {
                    myDb.OpenConnection();
                    sqlCmd.Parameters.Add(new SqlParameter("@order_id", orderForm.OrderId));
                    dataAdapt.Fill(table);

                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        challan = new Challan()
                        {
                            BookNo = Convert.ToInt32(row["book_no"]),
                            ChallanNo = Convert.ToInt32(row["sl_no"]),
                            ChallanDate = (DateTime)row["challan_date"],
                            ChallanType = new ChallanType() { TypeName = (string)row["challan_type"] },
                            OrderForm = orderForm,
                            FreightCharge = Convert.ToInt32(row["freight_charge"]),
                            VehicleNo = (string)row["vehicle_no"],
                            DriverMobile = (string)row["driver_mobile"],
                            ConsignmentNoteNo = (string)row["consignment_note_no"],
                            TransporterName = (string)row["transporter_name"],
                            Vat = Convert.ToDouble(row["vat"]),
                            Cst = Convert.ToDouble(row["cst"]),
                            SpecialNote = row["special_note"] == DBNull.Value ? "" : (string)row["special_note"],
                            ReceivedAmount = Convert.ToInt32(row["received_amount"]),
                            IsPersist = true
                        };
                    }
                }
                finally
                {
                    dataAdapt.Dispose();
                    table.Dispose();
                    myDb.CloseConnection();
                }
            });
            
            return challan;
        }
    }
}
