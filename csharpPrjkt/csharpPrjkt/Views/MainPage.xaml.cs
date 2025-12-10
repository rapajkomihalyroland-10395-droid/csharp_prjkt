using csharpPrjkt.ViewModels;

namespace csharpPrjkt.Views
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = _viewModel = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadBooksCommand.Execute(null);
        }
    }
}
