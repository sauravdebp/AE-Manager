using System.Diagnostics;
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
        }

        public void AddPageLoadedHandler()
        {
            PageLoader.PageActivatedEvent += OnPageLoaded;
        }

        public void RemovePageLoadedHandler()
        {
            PageLoader.PageActivatedEvent -= OnPageLoaded;
        }

        protected virtual void OnPageLoaded(object sender, PageLoadedEventArgs e) { }
    }
}
