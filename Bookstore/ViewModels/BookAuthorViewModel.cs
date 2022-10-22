
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookstore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Bookstore.ViewModels
{
    public class BookAuthorViewModel
    {

        public int BookId { get; set; }

        [Required]
        [StringLength(20,MinimumLength =5)]
        public string Title { get; set; }

        [Required]
        [StringLength(120, MinimumLength =10 )]
        public string Description { get; set; }
        public int AuthorId { get; set; }

        public List<Author> Authors { get; set; }

        public IFormFile File { get; set; }
        public string ImgUrl { get; set; }
    }
}
