
namespace BookInspector.Data.Context
{
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public interface IBookInspectorContext
    {
        DbSet<Author> Author { get; set; }
        DbSet<Book> Book { get; set; }
        DbSet<BookByAuthor> BookByAuthor { get; set; }
        DbSet<BookByCategory> BookByCategory { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<FavoriteBook> FavoriteBook { get; set; }
        DbSet<Publisher> Publisher { get; set; }
        DbSet<RatingForBookByUser> RatingByBook { get; set; }
        DbSet<User> User { get; set; }
    }
}
