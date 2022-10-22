using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class AuthorRepository : IBookstoreRepository<Author>
    {

        IList<Author> authors;

        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author{Id = 1, FullName="mohamed gamal"},
                new Author {Id =2 ,FullName = "mohamed samy"},
            
            };
        }

        public void Add(Author entity)
        {
            entity.Id = authors.Max(b => b.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.Id==id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int id, Author newauthor)
        {
            var author = Find(id);
            author.FullName = newauthor.FullName;
        }
    }
}
