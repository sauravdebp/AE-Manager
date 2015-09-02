using System;
using System.ComponentModel;
using System.Threading;
using Manager.DataModels;

namespace Manager.Pages
{
    /// <summary>
    /// Interaction logic for NewOrderPage.xaml
    /// </summary>
    public partial class NewOrderPage : PageBase, INotifyPropertyChanged
    {
        OrderForm myOrder;
        public OrderForm MyOrder 
        { 
            get { return myOrder; } 
            set 
            {
                myOrder = value; 
                NotifyPropertyChanged("MyOrder");
                if(PageNav != null)
                    (PageNav as NewOrderPageNav).MyOrder = value;
                if(PageSide != null)
                    (PageSide as NewOrderPageSide).MyOrder = value;
            }
        }

        public OrderForm sample;

        public Client newClient;

        public NewOrderPage(PageLoader pageLoader) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            MyOrder = new OrderForm() { Client = new Client() };
            PageSide = new NewOrderPageSide(MyOrder, pageLoader);
            PageNav = new NewOrderPageNav(MyOrder, pageLoader);
            InitPage(pageLoader);
        }

        public NewOrderPage(PageLoader pageLoader, OrderForm orderForm) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            MyOrder = orderForm;
            PageSide = new NewOrderPageSide(MyOrder, pageLoader);
            PageNav = new NewOrderPageNav(MyOrder, pageLoader);
        }

        async void InitPage(PageLoader pageLoader)
        {
            await MyOrder.Client.AssignClientCode();
            await MyOrder.AssignOrderId();
            MyOrder.OrderDate = DateTime.Now;
        }

        protected override void OnPageLoaded(object sender, PageLoadedEventArgs e)
        {
            base.OnPageLoaded(sender, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
