using Manager.DataModels;
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
    /// Interaction logic for NewOrderPageSide.xaml
    /// </summary>
    public partial class NewOrderPageSide : PageBase, INotifyPropertyChanged
    {
        OrderForm myOrder;
        public OrderForm MyOrder
        {
            get { return myOrder; }
            set
            {
                myOrder = value;
                NotifyPropertyChanged("MyOrder");
            }
        }

        public NewOrderPageSide(OrderForm orderForm, PageLoader pageLoader) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            MyOrder = orderForm;
            PageSide = null;
            PageNav = null;
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
