using csharpPrjkt.Views;

namespace csharpPrjkt
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("BookDetailsPage", typeof(BookDetailsPage));
        }
    }
}
