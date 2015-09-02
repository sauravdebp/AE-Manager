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

namespace Manager.Pages
{
    /// <summary>
    /// Interaction logic for MenuPageNav.xaml
    /// </summary>
    public partial class MenuPageNav : PageBase
    {
        public MenuPageNav(PageLoader pageLoader) : base(pageLoader)
        {
            InitializeComponent();
        }

        private void but_NewOrder_Click(object sender, RoutedEventArgs e)
        {
            PageLoader.LoadPage(typeof(NewOrderPage));
        }

        private void but_AllOrders_Click(object sender, RoutedEventArgs e)
        {
            PageLoader.LoadPage(typeof(AllOrdersPage));
        }

        private void but_Inventory_Click(object sender, RoutedEventArgs e)
        {
            PageLoader.LoadPage(typeof(InventoryPage));
        }
    }
}
