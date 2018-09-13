using Domain.Abstarct;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IBookRepository bookRepository;

        public AdminController(IBookRepository repo)
        {
            bookRepository = repo;
        }

        // GET: Admin
        public ViewResult Index()
        {
            return View(bookRepository.Books);
        }

        public ViewResult Edit(int BookID)
        {
            Book book = bookRepository.Books.FirstOrDefault(m => m.BookID == BookID);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book,HttpPostedFileBase image = null)
        {
            if(ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                bookRepository.SaveBook(book);
                TempData["message"] = string.Format("Измения в книге \"{0}\" были сделаны!",book.Name);
                return RedirectToAction("Index");
            }
            else
                return View(book);
        }

        [HttpPost]
        public ActionResult Delete(int BookID)
        {
           
            Book deletedBook = bookRepository.DeleteBook(BookID);
            if (deletedBook!=null)
            {              
                TempData["message"] = string.Format("Книга \"{0}\" была удалена!", deletedBook.Name);              
            }
            return RedirectToAction("Index");
        }


        
        public ViewResult Create(Book book)
        {         
                return View("Edit",new Book());
        }
    }
}