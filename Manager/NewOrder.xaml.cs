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
using System.Windows.Shapes;
using Manager.DataModels;
using System.Diagnostics;
using System.Windows.Markup;
using System.ComponentModel;
using Manager.Utilities;

namespace Manager
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        public OrderForm MyOrder { get; set; }

        public AppStatus AppStatus { get { return GlobalAppStatus.AppStatus; } }

        public Client newClient;
        
        public NewOrder()
        {
            InitializeComponent();
            DataContext = this;
            newClient = new Client();
            newClient.AssignClientCode();
            MyOrder = new OrderForm() { Client = newClient };
            MyOrder.AssignOrderId();
            MyOrder.OrderDate = DateTime.Now;
        }

        private void but_SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyOrder.PersistInfo();
            }
            catch(Exception ex)
            {
                GlobalAppStatus.AppStatus.StatusMessage(ex.Message, GlobalAppStatus.MessageType.ERROR);
            }
        }

        private void but_ResetOrderForm_Click(object sender, RoutedEventArgs e)
        {
            MyOrder.Reset();
        }

        private void but_ApproveOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyOrder.ApproveOrder();
            }
            catch(Exception ex)
            {
                GlobalAppStatus.AppStatus.StatusMessage(ex.Message, GlobalAppStatus.MessageType.ERROR);
            }
        }

        private void but_GenerateChallan_Click(object sender, RoutedEventArgs e)
        {
            //challan.Visibility = System.Windows.Visibility.Visible;
            //newOrderForm.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void but_Print_Click(object sender, RoutedEventArgs e)
        {
            //PrintDialog printDialog = new PrintDialog();
            //printDialog.ShowDialog();
            //challan.FontSize = 8;

            //printDialog.PrintVisual(challan, "Challan");
            //challan.FontSize = this.FontSize;
        }

        //private void but_SelectClient_Click(object sender, RoutedEventArgs e)
        //{
        //    SelectClient window = new SelectClient(this);
        //    isNewClient = false;
        //    window.ShowDialog();
        //}
    }

    public class VisibilityConverter : IValueConverter, IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Boolean cond = (Boolean)value;
            String param = parameter as String;
            Visibility visibility = Visibility.Visible;
            if(cond && param == "OnBoolFalse")
            {
                visibility = System.Windows.Visibility.Collapsed;
            }
            else if(!cond && param == "OnBoolTrue")
            {
                visibility = System.Windows.Visibility.Collapsed;
            }
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
