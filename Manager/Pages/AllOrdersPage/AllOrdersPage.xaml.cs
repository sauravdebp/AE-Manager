using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using Manager.DataModels;

namespace Manager.Pages
{
    /// <summary>
    /// Interaction logic for AllOrdersPage.xaml
    /// </summary>
    public partial class AllOrdersPage : PageBase, INotifyPropertyChanged
    {
        public ObservableCollection<OrderForm> OrderFormsList { get; set; } = new ObservableCollection<OrderForm>();

        public OrderForm CurrentForm { get; set; }

        public List<KeyValuePair<int, string>> MonthsList = new List<KeyValuePair<int, string>>()
        {
            new KeyValuePair<int, string>(1, "January"),
            new KeyValuePair<int, string>(2, "February"),
            new KeyValuePair<int, string>(3, "March"),
            new KeyValuePair<int, string>(4, "April"),
            new KeyValuePair<int, string>(5, "May"),
            new KeyValuePair<int, string>(6, "June"),
            new KeyValuePair<int, string>(7, "July"),
            new KeyValuePair<int, string>(8, "August"),
            new KeyValuePair<int, string>(9, "September"),
            new KeyValuePair<int, string>(10, "October"),
            new KeyValuePair<int, string>(11, "November"),
            new KeyValuePair<int, string>(12, "December")
        };

        public KeyValuePair<int, string> SelectedMonth { get; set; }

        public int SelectedYear { get; set; }

        public AllOrdersPage(PageLoader pageLoader) : base(pageLoader)
        {
            InitializeComponent();
            DataContext = this;
            PageNav = new AllOrdersPageNav(pageLoader, CurrentForm = new OrderForm());
            SelectedMonth = MonthsList[DateTime.Now.Month - 1];
            SelectedYear = DateTime.Now.Year;
            list_FilterMonths.ItemsSource = MonthsList;
            Button_Click(this, null);
        }

        private async void list_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;
            CurrentForm = e.AddedItems[0] as OrderForm;
            (PageNav as AllOrdersPageNav).OrderForm = CurrentForm;
            await CurrentForm.RetrieveAllOrderItems();
            list_OrderItems.ItemsSource = CurrentForm.OrderItems;
            (PageNav as AllOrdersPageNav).NotifyOrderFormChanged();
        }

        void list_Orders_ClearList()
        {
            CurrentForm = null;
            (PageNav as AllOrdersPageNav).OrderForm = CurrentForm;
            OrderFormsList.Clear();
            (PageNav as AllOrdersPageNav).NotifyOrderFormChanged();
        }

        int GetMaxDays(int month, int year)
        {
            if (month == 2 && IsLeap(year))
                return 29;
            else if (month == 2 && !IsLeap(year))
                return 28;
            if ((month <= 7 && month % 2 != 0) || (month > 7 && month % 2 == 0))
                return 31;
            return 30;
        }

        bool IsLeap(int year)
        {
            if (year % 4 != 0)
                return false;
            else if (year % 100 != 0)
                return true;
            else if (year % 400 != 0)
                return false;
            return true;
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

        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            list_Orders_ClearList();
            int month = SelectedMonth.Key;
            var list = from t in await OrderForm.RetrieveOrderFormsByDateTime(new DateTime(SelectedYear, month, 1), new DateTime(SelectedYear, month, GetMaxDays(month, SelectedYear)))
                       orderby t.OrderDate descending
                       select t;
            OrderFormsList = new ObservableCollection<OrderForm>(list);
            NotifyPropertyChanged("OrderFormsList");
        }

        protected override void OnPageLoaded(object sender, PageLoadedEventArgs e)
        {
            base.OnPageLoaded(sender, e);
            (PageNav as AllOrdersPageNav).NotifyOrderFormChanged();
        }
    }
}
