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
using Manager.Pages;
using Manager.Utilities;

namespace Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : PageLoader
    {
        public AppStatus AppStatus { get { return GlobalAppStatus.AppStatus; } }

        public MainWindow()
        {
            InitializeComponent();
            SetPanels(ContentArea, NavContentArea, SideContentArea);
            DataContext = this;
            PageLoadedEvent += MainWindow_PageLoadedEvent;
            LoadPage(typeof(MenuPage));
        }

        void MainWindow_PageLoadedEvent(object sender, PageLoadedEventArgs e)
        {
            
        }

        private void but_GoBack_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }
    }
}
