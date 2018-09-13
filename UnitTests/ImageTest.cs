using System;
using System.Web.Mvc;
using Domain.Abstarct;
using Domain.Entites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;

namespace UnitTests
{
    [TestClass]
    public class ImageTest
    {
        [TestMethod]
        public void Can_retrieve_image_data()
        {
            Book book = new Book
            {
                BookID = 2,
                Author = "Misha",
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new Book{ BookID = 1, Name = "P1"},
                book,
                new Book{ BookID = 3, Name = "P2"}
            });

            BooksController controller = new BooksController(mock.Object);
            ActionResult result = controller.GetImage(2);

            Assert.IsInstanceOfType(result,typeof(FileContentResult));
            Assert.AreEqual(book.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_retrieve_image_with_invalide_ID()
        {
            Book book = new Book
            {
                BookID = 2,
                Author = "Misha",
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new Book{ BookID = 1, Name = "P1"},
                book,
                new Book{ BookID = 3, Name = "P2"}
            });

            BooksController controller = new BooksController(mock.Object);
            ActionResult result = controller.GetImage(22);

            
            Assert.IsNull(result);
        }
    }
}
