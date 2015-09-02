using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Manager.Pages
{
    public class PageBase : UserControl
    {
        public PageLoader PageLoader { get; set; }

        public PageBase PageSide { get; set; }

        public PageBase PageNav { get; set; }

        public PageBase(PageLoader pageLoader)
        {
            PageLoader = pageLoader;
            PageLoader.PageLoadedEvent += OnPageLoaded;
        }

        protected virtual void OnPageLoaded(object sender, PageLoadedEventArgs e)
        {
            
        }
    }
}
