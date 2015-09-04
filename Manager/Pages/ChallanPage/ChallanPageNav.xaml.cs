using Manager.DataModels;
using Manager.Utilities;
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

namespace Manager.Pages
{
    /// <summary>
    /// Interaction logic for ChallanPageNav.xaml
    /// </summary>
    public partial class ChallanPageNav : PageBase, INotifyPropertyChanged
    {
        ChallanPage challanPageRef;

        public Challan OrderChallan { get; set; }

        public ChallanPageNav(PageLoader pageLoader, Challan orderChallan, ChallanPage pageRef) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            OrderChallan = orderChallan;
            challanPageRef = pageRef;
        }

        private async void but_SaveChallan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await OrderChallan.PersistInfo();
            }
            catch (Exception ex)
            {
                GlobalAppStatus.AppStatus.StatusMessage(ex.Message, GlobalAppStatus.MessageType.ERROR);
            }
            finally
            {
                NotifyPropertyChanged("OrderChallan");
                (challanPageRef.PageSide as ChallanPageSide).NotifyChallanPropertyChanged();
            }
        }

        private void but_PrintChallan_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            FlowDocument flowDoc = challanPageRef.doc;
            flowDoc.PageHeight = 11.7 * 96;
            flowDoc.PageWidth = 8.5 * 96;
            flowDoc.ColumnWidth = printDialog.PrintableAreaWidth;
            flowDoc.PagePadding = new Thickness(50);
            if (printDialog.ShowDialog() == true)
            {
                IDocumentPaginatorSource dps = flowDoc;
                printDialog.PrintDocument(dps.DocumentPaginator, "doc");
            }
            
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
    }
}
