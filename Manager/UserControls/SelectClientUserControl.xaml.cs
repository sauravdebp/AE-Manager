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

namespace Manager.UserControls
{
    /// <summary>
    /// Interaction logic for SelectClientUserControl.xaml
    /// </summary>
    public partial class SelectClientUserControl : UserControl
    {
        List<Client> clientList;
        public List<Client> ClientList { get { return clientList; } }

        public static DependencyProperty OrderFormProperty = DependencyProperty.Register("OrderForm", typeof(OrderForm), typeof(SelectClientUserControl));

        public OrderForm OrderForm { get; set; }

        public SelectClientUserControl()
        {
            InitializeComponent();
            DataContext = this;
            InitPage();
        }

        async void InitPage()
        {
            clientList = await Client.RetrieveAllClients();
            list_ClientList.ItemsSource = ClientList;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (GetValue(OrderFormProperty) as OrderForm).Client = e.AddedItems[0] as Client;
            Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
