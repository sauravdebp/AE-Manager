using System;
using System.ComponentModel;
using System.Windows;
using Manager.DataModels;

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
            PageLoader.LoadPage(new NewOrderPage(PageLoader, OrderForm));
        }

        private void but_GenerateChallan_Click(object sender, RoutedEventArgs e)
        {
            PageLoader.LoadPage(new ChallanPage(PageLoader, OrderForm));
        }

        private async void but_ViewChallan_Click(object sender, RoutedEventArgs e)
        {
            PageLoader.LoadPage(new ChallanPage(PageLoader, await OrderForm.RetrieveOrderChallan()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
