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
using Manager.DataModels;

namespace Manager.UserControls.ChallanPageControls
{
    /// <summary>
    /// Interaction logic for ChallanHeader.xaml
    /// </summary>
    public partial class ChallanHeader : UserControl
    {
        public static DependencyProperty OrderChallanProperty = DependencyProperty.Register("OrderChallan", typeof(Challan), typeof(ChallanHeader));
        public Challan OrderChallan { get; set; }

        public ChallanHeader()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
