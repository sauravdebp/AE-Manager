using Manager.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : PageBase, INotifyPropertyChanged
    {
        public ObservableCollection<StockItem> StockItems = new ObservableCollection<StockItem>();
        public List<StockItem> NewItems = new List<StockItem>();
        
        public StockItem SelectedItem { get; set; }

        public StockItem newItem = new StockItem();
        public StockItem NewItem 
        {
            get { return newItem; }
            set
            {
                newItem = value;
                NotifyPropertyChanged("NewItem");
            }
        }
        
        public int NewStock { set; get; }

        public InventoryPage(PageLoader pageLoader) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            PageNav = new InventoryPageNav(pageLoader, StockItems);
            InitPage();
        }

        async void InitPage()
        {
            List<StockItem> stockItems = await StockItem.RetrieveAllStockItems();
            foreach (StockItem item in stockItems)
                StockItems.Add(item);
            list_StockItems.ItemsSource = StockItems;
        }

        private void list_StockItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewStock = 0;
            NotifyPropertyChanged("NewStock");
            SelectedItem = e.AddedItems[0] as StockItem;
            NotifyPropertyChanged("SelectedItem");
        }

        private void but_EnterQty_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem.StockQty += NewStock;
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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                SelectedItem.StockQty += Convert.ToInt32((sender as TextBox).Text);
        }

        private void but_AddItem_Click(object sender, RoutedEventArgs e)
        {
            StockItem item = new StockItem()
            {
                ItemName = NewItem.ItemName,
                ItemSkuCode = NewItem.ItemSkuCode,
                ItemRate = NewItem.ItemRate,
                ItemDescription = NewItem.ItemDescription,
                StockQty = NewItem.StockQty
            };
            StockItems.Add(item);
            NewItems.Add(item);
            NewItem = new StockItem();
        }
    }
}
