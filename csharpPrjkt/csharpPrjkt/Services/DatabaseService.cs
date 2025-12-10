using SQLite;
using csharpPrjkt.Models;

namespace csharpPrjkt.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
        }

        async Task Init()
        {
            if (_database is not null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Books.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<Book>();
            
            if (await _database.Table<Book>().CountAsync() == 0)
            {
                 await _database.InsertAsync(new Book { Title = "Egri csillagok", Author = "Gárdonyi Géza", Genre = "Regény", PublishedYear = 1901 });
                 await _database.InsertAsync(new Book { Title = "A Pál utcai fiúk", Author = "Molnár Ferenc", Genre = "Ifjúsági regény", PublishedYear = 1906 });
            }
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            await Init();
            return await _database.Table<Book>().ToListAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            await Init();
            return await _database.Table<Book>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveBookAsync(Book item)
        {
            await Init();
            if (item.Id != 0)
                return await _database.UpdateAsync(item);
            else
                return await _database.InsertAsync(item);
        }

        public async Task<int> DeleteBookAsync(Book item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
    }
}
