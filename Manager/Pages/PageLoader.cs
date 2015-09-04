using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Manager.Pages
{
    public abstract class PageLoader : Window
    {
        Grid contentArea;
        StackPanel navContentArea;
        StackPanel sideContentArea;

        public delegate void PageActivatedEventHandler(object sender, PageLoadedEventArgs e);
        public event PageActivatedEventHandler PageActivatedEvent;
        public event PageActivatedEventHandler PageBackEvent;

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

        public void LoadPage(PageBase page)
        {
            if (PageBackStack.Count >= 1)
            {
                PageBackStack.Peek().RemovePageLoadedHandler();
            }
            PageBackStack.Push(page);
            page.AddPageLoadedHandler();

            contentArea.Children.Clear();
            navContentArea.Children.Clear();
            sideContentArea.Children.Clear();
            contentArea.Children.Add(page);
            if (page.PageNav != null)
                navContentArea.Children.Add(page.PageNav);
            if (page.PageSide != null)
                sideContentArea.Children.Add(page.PageSide);
            if (PageActivatedEvent != null)
                PageActivatedEvent(this, new PageLoadedEventArgs(page, page.GetType()));
        }

        public PageBase GoBack()
        {
            PageBase prevPage = null;
            if(PageBackStack.Count > 1)
            {
                PageBackStack.Pop().RemovePageLoadedHandler();
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
