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
using System.Windows.Shapes;
using Manager.DataModels;
using System.Diagnostics;

namespace Manager
{
    /// <summary>
    /// Interaction logic for SelectClient.xaml
    /// </summary>
    public partial class SelectClient : Window
    {
        //List<Client> clientList;
        //public List<Client> ClientList { get { return clientList; } }

        public SelectClient()
        {
            InitializeComponent();
            DataContext = this;
            //clientList = Client.RetrieveAllClients();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Client selectedClient = e.AddedItems[0] as Client;
            //this.Close();
        }
    }
}
