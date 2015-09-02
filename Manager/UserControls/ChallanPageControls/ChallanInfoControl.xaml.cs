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
    /// Interaction logic for ChallanForm.xaml
    /// </summary>
    public partial class ChallanInfoControl : UserControl
    {
        public Thickness Paddding { get { return new Thickness(3); } }

        //public static DependencyProperty OrderFormProperty = DependencyProperty.Register("OrderForm", typeof(OrderForm), typeof(ChallanInfoControl));
        //public OrderForm OrderForm { get; set; }

        public static DependencyProperty OrderChallanProperty = DependencyProperty.Register("OrderChallan", typeof(Challan), typeof(ChallanInfoControl));
        public Challan OrderChallan { get; set; }

        public ChallanInfoControl()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
