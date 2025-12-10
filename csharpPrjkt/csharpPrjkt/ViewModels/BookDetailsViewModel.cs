using System.Windows.Input;
using csharpPrjkt.Models;
using csharpPrjkt.Services;

namespace csharpPrjkt.ViewModels
{
    [QueryProperty(nameof(Book), "Book")]
    public class BookDetailsViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        
        private Book _book;
        public Book Book
        {
            get => _book;
            set
            {
                _book = value;
                OnPropertyChanged();
            }
        }

        public BookDetailsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Book = new Book();
        }

        public ICommand SaveCommand => new Command(async () => await SaveBookAsync());
        public ICommand DeleteCommand => new Command(async () => await DeleteBookAsync());
        public ICommand CancelCommand => new Command(async () => await Shell.Current.GoToAsync(".."));

        private async Task SaveBookAsync()
        {
            if (string.IsNullOrWhiteSpace(Book.Title) || string.IsNullOrWhiteSpace(Book.Author))
            {
                await Shell.Current.DisplayAlert("Hiba", "Kérem töltse ki a Könyv címét és szerzőjét!", "OK");
                return;
            }

            if (Book.Id != 0)
            {
                bool answer = await Shell.Current.DisplayAlert("Megerősítés", "Biztosan módosítani szeretné az adatokat?", "Igen", "Nem");
                if (!answer) return;
            }

            await _databaseService.SaveBookAsync(Book);
            await Shell.Current.GoToAsync("..");
        }

        private async Task DeleteBookAsync()
        {
            if (Book.Id == 0) return;

            bool answer = await Shell.Current.DisplayAlert("Megerősítés", "Biztosan törölni szeretné ezt a könyvet?", "Igen", "Nem");
            if (answer)
            {
                await _databaseService.DeleteBookAsync(Book);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
