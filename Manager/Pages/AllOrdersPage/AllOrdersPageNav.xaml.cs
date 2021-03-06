﻿using Manager.DataModels;
using Manager.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Manager.Pages
{
    /// <summary>
    /// Interaction logic for AllOrdersPageNav.xaml
    /// </summary>
    public partial class AllOrdersPageNav : PageBase, INotifyPropertyChanged
    {
        public OrderForm OrderForm { get; set; }

        public AllOrdersPageNav(PageLoader pageLoader, OrderForm orderForm) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            OrderForm = orderForm;
        }

        public void NotifyOrderFormChanged()
        {
            NotifyPropertyChanged("OrderForm");
        }

        private void but_ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            NewOrderPage page = (NewOrderPage)PageLoader.LoadPage(typeof(NewOrderPage));
            page.MyOrder = OrderForm;
        }

        private void but_GenerateChallan_Click(object sender, RoutedEventArgs e)
        {
            ChallanPage page = PageLoader.LoadPage(typeof(ChallanPage)) as ChallanPage;
            page.OrderForm = OrderForm;
        }

        private async void but_ViewChallan_Click(object sender, RoutedEventArgs e)
        {
            ChallanPage page = (ChallanPage)PageLoader.LoadPage(typeof(ChallanPage));
            page.OrderChallan = await OrderForm.RetrieveOrderChallan();
            page.OrderChallan.OrderForm = OrderForm;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
