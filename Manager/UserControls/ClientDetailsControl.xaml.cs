using System.Windows;
using System.Windows.Controls;
using Manager.DataModels;

namespace Manager.UserControls
{
    /// <summary>
    /// Interaction logic for ClientDetailsControl.xaml
    /// </summary>
    public partial class ClientDetailsControl : UserControl
    {
        public static DependencyProperty OrderFormProperty = DependencyProperty.Register("OrderForm", typeof(OrderForm), typeof(ClientDetailsControl));
        public OrderForm OrderForm { get; set; }

        public ClientDetailsControl()
        {
            InitializeComponent();
            DataContext = this;
            OrderForm = GetValue(OrderFormProperty) as OrderForm;
        }

        private void but_SelectClient_Click(object sender, RoutedEventArgs e)
        {
            selectClientControl.Visibility = Visibility.Visible;
        }
    }
}
