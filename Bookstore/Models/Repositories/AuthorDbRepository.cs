using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class AuthorDbRepository : IBookstoreRepository<Author>
    {

        BookstoreDbContext Db;
        public AuthorDbRepository(BookstoreDbContext _db)
        {
            Db= _db;    
        }
        public void Add(Author entity)
        {
            Db.Authors.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);
            Db.Authors.Remove(author);
            Db.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = Db.Authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return Db.Authors.ToList();
        }

        public void Update(int id, Author newauthor)
        {
            Db.Update(newauthor);
            Db.SaveChanges();
        }
    }
}
