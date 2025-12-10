using csharpPrjkt.ViewModels;

namespace csharpPrjkt.Views
{
    public partial class BookDetailsPage : ContentPage
    {
        public BookDetailsPage(BookDetailsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
