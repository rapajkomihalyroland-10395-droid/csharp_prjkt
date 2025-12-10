using System.Collections.ObjectModel;
using System.Windows.Input;
using csharpPrjkt.Models;
using csharpPrjkt.Services;

namespace csharpPrjkt.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<Book> Books { get; } = new();

        public MainViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public ICommand LoadBooksCommand => new Command(async () => await LoadBooksAsync());
        
        public ICommand AddBookCommand => new Command(async () => await AddBookAsync());
        
        public ICommand BookTappedCommand => new Command<Book>(async (book) => await OnBookTapped(book));

        private async Task LoadBooksAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var books = await _databaseService.GetBooksAsync();
                Books.Clear();
                foreach (var book in books)
                {
                    Books.Add(book);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddBookAsync()
        {
            // Navigáció a részletező oldalra
            await Shell.Current.GoToAsync("BookDetailsPage");
        }

        private async Task OnBookTapped(Book book)
        {
            if (book == null) return;
            
            var navigationParameter = new Dictionary<string, object>
            {
                { "Book", book }
            };
            await Shell.Current.GoToAsync("BookDetailsPage", navigationParameter);
        }
    }
}
