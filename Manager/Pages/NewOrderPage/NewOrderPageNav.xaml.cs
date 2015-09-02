using System;
using System.ComponentModel;
using System.Windows;
using Manager.DataModels;
using Manager.Utilities;

namespace Manager.Pages
{
    /// <summary>
    /// Interaction logic for NewOrderNav.xaml
    /// </summary>
    public partial class NewOrderPageNav : PageBase, INotifyPropertyChanged
    {
        OrderForm myOrder;
        public OrderForm MyOrder
        {
            get { return myOrder; }
            set
            {
                myOrder = value;
                NotifyPropertyChanged(nameof(MyOrder));
            }
        }
        
        public NewOrderPageNav(OrderForm orderForm, PageLoader pageLoader) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            MyOrder = orderForm;
        }

        protected override void OnPageLoaded(object sender, PageLoadedEventArgs e)
        {
            base.OnPageLoaded(sender, e);
            NotifyPropertyChanged(nameof(MyOrder));
        }

        private async void but_SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobalAppStatus.AppStatus.StatusMessage("Saving Order...", GlobalAppStatus.MessageType.INFO);
                await MyOrder.PersistInfo();
                GlobalAppStatus.AppStatus.StatusMessage("Order Saved", GlobalAppStatus.MessageType.INFO);
            }
            catch (Exception ex)
            {
                GlobalAppStatus.AppStatus.StatusMessage(ex.Message, GlobalAppStatus.MessageType.WARNING);
            }
            finally
            {
                NotifyPropertyChanged("MyOrder");
            }
        }

        private void but_ResetOrderForm_Click(object sender, RoutedEventArgs e)
        {
            MyOrder.Reset();
            NotifyPropertyChanged("MyOrder");
        }

        private void but_ApproveOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyOrder.ApproveOrder();
            }
            catch (Exception ex)
            {
                GlobalAppStatus.AppStatus.StatusMessage(ex.Message, GlobalAppStatus.MessageType.ERROR);
            }
            finally
            {
                NotifyPropertyChanged("MyOrder");
            }
        }

        private void but_GenerateChallan_Click(object sender, RoutedEventArgs e)
        {
            PageLoader.LoadPage(new ChallanPage(PageLoader, MyOrder));
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

        private async void but_ViewChallan_Click(object sender, RoutedEventArgs e)
        {
            PageLoader.LoadPage(new ChallanPage(PageLoader, await MyOrder.RetrieveOrderChallan()));
        }
    }
}
