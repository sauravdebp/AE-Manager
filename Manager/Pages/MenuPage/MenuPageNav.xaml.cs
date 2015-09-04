using System.Windows;

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
            PageLoader.LoadPage(new NewOrderPage(PageLoader));
        }

        private void but_AllOrders_Click(object sender, RoutedEventArgs e)
        {
            PageLoader.LoadPage(new AllOrdersPage(PageLoader));
        }

        private void but_Inventory_Click(object sender, RoutedEventArgs e)
        {
            PageLoader.LoadPage(new InventoryPage(PageLoader));
        }
    }
}
