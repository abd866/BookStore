using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models.Repositories
{
    public class BookDbRepository:IBookstoreRepository<Book>
    {
        BookstoreDbContext Db;

        public BookDbRepository(BookstoreDbContext _db)
        {
            Db = _db;
        }
        public void Add(Book entity)
        {
            Db.Books.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            Db.Books.Remove(book);
            Db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = Db.Books.SingleOrDefault(b => b.Id == id);
            System.Console.WriteLine(book.Author);

            return book;

        }

        public IList<Book> List()
        {
            return Db.Books.Include(a=>a.Author).ToList();
        }

        public void Update(int id, Book newbook)
        {
            Db.Update(newbook);
            Db.SaveChanges();
        }
    }
}
