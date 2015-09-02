using Manager.DataModels;
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
    /// Interaction logic for ChallanControl.xaml
    /// </summary>
    public partial class ChallanPage : PageBase, INotifyPropertyChanged
    {
        //OrderForm orderForm;
        public OrderForm OrderForm 
        {
            get { return OrderChallan.OrderForm; }
            set
            {
                OrderChallan.OrderForm = value;
                NotifyPropertyChanged("OrderForm");
            }
        }

        Challan orderChallan;
        public Challan OrderChallan 
        {
            get { return orderChallan; }
            set
            {
                orderChallan = value;
                NotifyPropertyChanged("OrderChallan");
                if(PageNav != null)
                    (PageNav as ChallanPageNav).OrderChallan = value;
            }
        }

        public ChallanPage(PageLoader pageLoader, OrderForm orderForm) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            OrderChallan = new Challan();
            OrderForm = orderForm;
            PageNav = new ChallanPageNav(pageLoader, OrderChallan, this);
            PageSide = new ChallanPageSide(pageLoader, OrderChallan);
            InitPage(pageLoader);
        }

        public ChallanPage(PageLoader pageLoader, Challan orderChallan) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            OrderChallan = orderChallan;
            PageNav = new ChallanPageNav(pageLoader, OrderChallan, this);
            PageSide = new ChallanPageSide(pageLoader, OrderChallan);
        }

        async void InitPage(PageLoader pageLoader)
        {
            await OrderChallan.AssignChallanBookSerialNo();
        }

        void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
