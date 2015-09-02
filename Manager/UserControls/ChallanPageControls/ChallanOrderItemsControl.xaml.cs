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

namespace Manager.UserControls.ChallanPageControls
{
    /// <summary>
    /// Interaction logic for ChallanOrderItemsContro.xaml
    /// </summary>
    public partial class ChallanOrderItemsControl : UserControl
    {
        public static DependencyProperty OrderFormProperty = DependencyProperty.Register("OrderForm", typeof(OrderForm), typeof(ChallanOrderItemsControl));
        public OrderForm OrderForm { get; set; }

        public ChallanOrderItemsControl()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
