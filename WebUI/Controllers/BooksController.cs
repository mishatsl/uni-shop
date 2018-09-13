using Domain.Abstarct;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository repository;
        public int pageSize = 4;

        public BooksController(IBookRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(string genre, int page = 1)
        {
            BookListViewModel model = new BookListViewModel
            {
                Books = repository.Books.Where(b => genre == null || b.Genre == genre).OrderBy(book => book.BookID).Skip((page - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = genre == null ? repository.Books.Count() : repository.Books.Where(g => g.Genre == genre).Count()
                },
                CurrentGenre = genre
        };
            
            return View(model);
        }

        public FileContentResult GetImage(int BookID)
        {
            Book book = repository.Books.FirstOrDefault(m => m.BookID == BookID);
            if (book != null)
            {
                return File(book.ImageData, book.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}