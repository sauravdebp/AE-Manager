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
using Manager.DataModels;

namespace Manager.Pages
{
    /// <summary>
    /// Interaction logic for ChallanPageSide.xaml
    /// </summary>
    public partial class ChallanPageSide : PageBase, INotifyPropertyChanged
    {
        public Challan Challan { get; set; }

        public List<double> VatList { get; set; }

        public List<double> CstList { get; set; }

        public ChallanPageSide(PageLoader pageLoader, Challan challan) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            Challan = challan;
            NotifyPropertyChanged("Challan");
            
        }

        public async void InitPage(PageLoader pageLoader)
        {
            combo_challanTypes.ItemsSource = await ChallanType.RetrieveAllChallanTypes();
            VatList = await Tax.RetriveTaxesByType("VAT");
            CstList = await Tax.RetriveTaxesByType("CST");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
