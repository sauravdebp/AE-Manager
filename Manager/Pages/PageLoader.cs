using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Manager.Pages
{
    public abstract class PageLoader : Window
    {
        Grid contentArea;
        StackPanel navContentArea;
        StackPanel sideContentArea;

        public delegate void PageLoadedEventHandler(object sender, PageLoadedEventArgs e);
        public event PageLoadedEventHandler PageLoadedEvent;

        public Stack<PageBase> PageBackStack { get; private set; }

        public PageLoader()
        {
            PageBackStack = new Stack<PageBase>();
        }

        public void SetPanels(Grid contentArea, StackPanel navContentArea, StackPanel sideContentArea)
        {
            this.contentArea = contentArea;
            this.navContentArea = navContentArea;
            this.sideContentArea = sideContentArea;
        }

        public PageBase LoadPage(Type pageType)
        {
            PageBase newPage = (PageBase)Activator.CreateInstance(pageType, this);
            LoadPage(newPage);
            return newPage;
        }

        public void LoadPage(PageBase page)
        {
            PageBackStack.Push(page);
            contentArea.Children.Clear();
            navContentArea.Children.Clear();
            sideContentArea.Children.Clear();
            contentArea.Children.Add(page);
            if (page.PageNav != null)
                navContentArea.Children.Add(page.PageNav);
            if (page.PageSide != null)
                sideContentArea.Children.Add(page.PageSide);
            if (PageLoadedEvent != null)
                PageLoadedEvent(this, new PageLoadedEventArgs(page, page.GetType()));
        }

        public PageBase GoBack()
        {
            PageBase prevPage = null;
            if(PageBackStack.Count > 1)
            {
                PageBackStack.Pop();
                prevPage = PageBackStack.Pop();
                if (prevPage != null)
                    LoadPage(prevPage);
            }
            return prevPage;
        }
    }

    public class PageLoadedEventArgs : EventArgs
    {
        public PageBase LoadedPage { get; set; }

        public Type PageType { get; set; }

        public PageLoadedEventArgs(PageBase loadedPage, Type pageType)
        {
            LoadedPage = loadedPage;
            PageType = pageType;
        }
    }
}
