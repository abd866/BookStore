using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Bookstore.Models.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {

        IList<Book> books;

        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book{Id=1,
                         Title="CS",
                         Description="no des",
                         ImgUrl = "Screenshot (12).png",
                         Author=new Author{  Id=1}  },

                new Book{Id=2,
                    Title="html",
                    Description="no",
                    ImgUrl = "Screenshot (15).png",
                    Author=new Author{Id=2 } },
                
                new Book {Id=3,
                    Title="python",
                    Description="nothing",
                    ImgUrl = "Screenshot (18).png",
                    Author=new Author{ Id=2} },
            };

                
                     
        }
        public void Add(Book entity)
        {
            entity.Id =books.Max(b=>b.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book =Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book =books.SingleOrDefault(b => b.Id == id);
            return book;
            
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id,Book newbook)
        {
            var book= Find(id);
            book.Title = newbook.Title;
            book.Description = newbook.Description;
            book.Author = newbook.Author;
            book.ImgUrl = newbook.ImgUrl;
        }
    }
}
