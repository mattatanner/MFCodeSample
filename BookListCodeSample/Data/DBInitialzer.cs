using BookListCodeSample.Data;

namespace BookListCodeSample.Data;

public class DbInitializer
{
    private readonly DBConnector _db;

    public DbInitializer(DBConnector db)
    {
        _db = db;
    }

    public async Task InitializeAsync()
    {
        await CreateTables();
        await AddBooks();
        await AddFavorites();
    }

    private async Task CreateTables()
    {
        await _db.Execute(@"
            CREATE TABLE IF NOT EXISTS Books (
                bk_id INTEGER PRIMARY KEY AUTOINCREMENT,
                bk_title TEXT NOT NULL,
                bk_author TEXT NOT NULL
            )");

        await _db.Execute(@"
            CREATE TABLE IF NOT EXISTS Favorites (
                fav_id INTEGER PRIMARY KEY AUTOINCREMENT,
                fav_name TEXT NOT NULL,
                fav_bk_id INTEGER NOT NULL,
                fav_created TEXT DEFAULT CURRENT_TIMESTAMP,
                FOREIGN KEY (fav_bk_id) REFERENCES Books(bk_id)
            )");
    }

    //10 books, more can be easily added
    private async Task AddBooks()
    {
        var count = await _db.ExecuteScalar<int>("SELECT COUNT(*) FROM Books");
        if (count > 0) return;

        await _db.Execute(@"
            INSERT INTO Books (bk_title, bk_author) VALUES
            ('To Kill a Mockingbird', 'Harper Lee'),
            ('1984', 'George Orwell'),
            ('Pride and Prejudice', 'Jane Austen'),
            ('The Great Gatsby', 'F. Scott Fitzgerald'),
            ('One Hundred Years of Solitude', 'Gabriel García Márquez'),
            ('The Catcher in the Rye', 'J.D. Salinger'),
            ('Brave New World', 'Aldous Huxley'),
            ('The Lord of the Rings', 'J.R.R. Tolkien'),
            ('Harry Potter and the Sorcerer''s Stone', 'J.K. Rowling'),
            ('The Hitchhiker''s Guide to the Galaxy', 'Douglas Adams')");
    }

    //test data
    private async Task AddFavorites()
    {
        var count = await _db.ExecuteScalar<int>("SELECT COUNT(*) FROM Favorites");
        if (count > 0) return;

        await _db.Execute(@"
            INSERT INTO Favorites (fav_name, fav_bk_id) VALUES
            ('Alice', 1),
            ('Alice', 8),
            ('Alice', 9),
            ('Bob', 1),
            ('Bob', 2),
            ('Bob', 7),
            ('Charlie', 1),
            ('Charlie', 3),
            ('Diana', 2),
            ('Diana', 5),
            ('Diana', 8),
            ('Eve', 4),
            ('Eve', 9),
            ('Frank', 1),
            ('Frank', 10)");
    }
}