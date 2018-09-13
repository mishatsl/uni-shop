using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Abstarct;
using Domain.Entites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;

namespace UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void Index_contains_all_books()
        {
            List<Book> books = new List<Book> {
                new Book{BookID = 1, Name = "Book1" },
                new Book{BookID = 2, Name = "Book2" },
                new Book{BookID = 3, Name = "Book3" },
                new Book{BookID = 4, Name = "Book4" },
                new Book{BookID = 5, Name = "Book5" },
            };

            Mock <IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(books);

            AdminController controller = new AdminController(mock.Object);

            List<Book> result = ((IEnumerable<Book>)controller.Index().ViewData.Model).ToList();

            Assert.AreEqual(result.Count(),5);
            Assert.AreEqual(result[0].Name, "Book1");
            Assert.AreEqual(result[3].Name, "Book4");
        }

        [TestMethod]
        public void Can_edit_books()
        {
            List<Book> books = new List<Book> {
                new Book{BookID = 1, Name = "Book1" },
                new Book{BookID = 2, Name = "Book2" },
                new Book{BookID = 3, Name = "Book3" },
                new Book{BookID = 4, Name = "Book4" },
                new Book{BookID = 5, Name = "Book5" },
            };

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(books);

            AdminController controller = new AdminController(mock.Object);

            Book result1 = (Book)controller.Edit(2).ViewData.Model;
            Book result2 = (Book)controller.Edit(5).ViewData.Model;

            Assert.AreEqual(result1.Name, "Book2");
            Assert.AreEqual(result2.Name, "Book5");
        }

        [TestMethod]
        public void Can_delete_book()
        {
            List<Book> books = new List<Book> {
                new Book{BookID = 1, Name = "Book1" },
                new Book{BookID = 2, Name = "Book2" },
                new Book{BookID = 3, Name = "Book3" },
                new Book{BookID = 4, Name = "Book4" },
                new Book{BookID = 5, Name = "Book5" },
            };

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(books);

            AdminController controller = new AdminController(mock.Object);

           controller.Delete(1);

            

            mock.Verify(m=>m.DeleteBook(1),Times.Once);
        }

        [TestMethod]
        public void Can_save_books()
        {
            

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            

            AdminController controller = new AdminController(mock.Object);

            Book book = new Book { BookID = 1, Name = "Book1" };

           ActionResult result = controller.Edit(book);

            mock.Verify(m=>m.SaveBook(book));

            Assert.IsNotInstanceOfType(result,typeof(ViewResult));

        }

        [TestMethod]
        public void Can_validate_books()
        {


            Mock<IBookRepository> mock = new Mock<IBookRepository>();


            AdminController controller = new AdminController(mock.Object);

            Book book = new Book { Name = "Book1" };


            controller.ModelState.AddModelError("error", "error");

            ActionResult result = controller.Edit(book);

            mock.Verify(m => m.SaveBook(book),Times.Never());

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
    }
}
