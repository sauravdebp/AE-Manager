using Manager.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Manager.Utilities;
using System.ComponentModel;

namespace Manager.UserControls
{
    /// <summary>
    /// Interaction logic for OrderFormUserControl.xaml
    /// </summary>
    public partial class OrderFormUserControl : UserControl, INotifyPropertyChanged
    {
        public static DependencyProperty OrderFormProperty = DependencyProperty.Register("OrderForm", typeof(OrderForm), typeof(OrderFormUserControl));

        public OrderForm OrderForm { get; set; }

        List<StockItem> itemsList;
        public List<StockItem> ItemsList
        {
            get { return itemsList; }
            set
            {
                itemsList = value;
                NotifyPropertyChanged(nameof(ItemsList));
            }
        }

        OrderItem newOrderItem;
        public OrderItem NewOrderItem
        {
            get { return newOrderItem; }
            set
            {
                newOrderItem = value;
                NotifyPropertyChanged(nameof(NewOrderItem));
            }
        }

        public OrderFormUserControl()
        {
            InitializeComponent();
            DataContext = this;
            InitPage();
        }

        async void InitPage()
        {
            ItemsList = await StockItem.RetrieveAllStockItems();
            NewOrderItem = new OrderItem();
        }

        private void but_AddOrderItem_Click(object sender, RoutedEventArgs e)
        {
            if (!NewOrderItem.Validate())
            {
                GlobalAppStatus.AppStatus.StatusMessage("Order item could not be added", GlobalAppStatus.MessageType.WARNING);
                return;
            }
            (GetValue(OrderFormProperty) as OrderForm).OrderItems.Add(new OrderItem()
            {
                Item = NewOrderItem.Item,
                Qty = NewOrderItem.Qty,
                RateAdjustment = NewOrderItem.RateAdjustment
            });
            NewOrderItem.Reset();
            combo_ItemsList.Focus();
        }

        private void combo_ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            text_Qty.Focus();
            text_Qty.Text = null;
        }

        private void text_Qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && text_Qty.Text != null && text_Qty.Text != "0" && text_Qty.Text != "")
            {
                text_Rate.Focus();
                text_Rate.SelectAll();
            }
        }

        private void text_Rate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && text_Rate.Text != null && text_Rate.Text != "")
            {
                but_AddOrderItem.Focus();
            }
        }

        private void combo_ItemsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                combo_ItemsList.IsDropDownOpen = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
