using Manager.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for InventoryPageNav.xaml
    /// </summary>
    public partial class InventoryPageNav : PageBase
    {
        ObservableCollection<StockItem> itemsList;

        public InventoryPageNav(PageLoader pageLoader, ObservableCollection<StockItem> listItems) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            itemsList = listItems;
        }

        private void but_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (itemsList == null)
                return;
            foreach(StockItem item in itemsList)
            {
                item.PersistInfo();
            }
        }
    }
}
