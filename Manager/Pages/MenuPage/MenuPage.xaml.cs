namespace Manager.Pages
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : PageBase
    {
        public MenuPage(PageLoader pageLoader) : base(pageLoader)
        {
            InitializeComponent();
            PageNav = new MenuPageNav(pageLoader);
        }
    }
}
