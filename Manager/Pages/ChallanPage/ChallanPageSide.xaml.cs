using System.Collections.Generic;
using System.ComponentModel;
using Manager.DataModels;

namespace Manager.Pages
{
    /// <summary>
    /// Interaction logic for ChallanPageSide.xaml
    /// </summary>
    public partial class ChallanPageSide : PageBase, INotifyPropertyChanged
    {
        public Challan Challan { get; set; }

        List<double> vatList;
        public List<double> VatList
        {
            get { return vatList; }
            set
            {
                vatList = value;
                NotifyPropertyChanged(nameof(VatList));
            }
        }

        List<double> cstList;
        public List<double> CstList
        {
            get { return cstList; }
            set
            {
                cstList = value;
                NotifyPropertyChanged(nameof(CstList));
            }
        }

        public ChallanPageSide(PageLoader pageLoader, Challan challan) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            Challan = challan;
            NotifyPropertyChanged("Challan");
            InitPage();
        }

        public async void InitPage()
        {
            combo_challanTypes.ItemsSource = await ChallanType.RetrieveAllChallanTypes();
            VatList = await Tax.RetriveTaxesByType("VAT");
            CstList = await Tax.RetriveTaxesByType("CST");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void NotifyChallanPropertyChanged()
        {
            NotifyPropertyChanged(nameof(Challan));
        }
    }
}
