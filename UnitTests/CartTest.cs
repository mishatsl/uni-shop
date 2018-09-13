using System;
using System.Collections.Generic;
using Domain.Entites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Domain.Abstarct;
using Moq;
using WebUI.Controllers;
using System.Web.Mvc;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Book book1 = new Book { BookID = 1, Name = "Book1"};
            Book book2 = new Book { BookID = 2, Name = "Book2" };

            Cart cart = new Cart();

            cart.AddItem(book1,1);
            cart.AddItem(book2, 1);

            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(results.Count(),2);
            Assert.AreEqual(results[0].Book, book1);
            Assert.AreEqual(results[1].Book, book2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Line()
        {
            Book book1 = new Book { BookID = 1, Name = "Book1" };
            Book book2 = new Book { BookID = 2, Name = "Book2" };

            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1,5);

            List<CartLine> results = cart.Lines.OrderBy(b=>b.Book.BookID).ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            Book book1 = new Book { BookID = 1, Name = "Book1" };
            Book book2 = new Book { BookID = 2, Name = "Book2" };
            Book book3 = new Book { BookID = 3, Name = "Book3" };

            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            cart.AddItem(book3, 2);
            cart.RemoveLine(book2);

            List<CartLine> results = cart.Lines.OrderBy(b => b.Book.BookID).ToList();

            Assert.AreEqual(cart.Lines.Where(c=>c.Book==book2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
            
        }

        [TestMethod]
        public void Can_ComputeTotalValue()
        {
            Book book1 = new Book { BookID = 1, Name = "Book1" ,Price = 2};
            Book book2 = new Book { BookID = 2, Name = "Book2" ,Price = 2};
            Book book3 = new Book { BookID = 3, Name = "Book3" ,Price = 2};

            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            cart.AddItem(book3, 2);
            

            List<CartLine> results = cart.Lines.OrderBy(b => b.Book.BookID).ToList();

            Assert.AreEqual(cart.ComputeTotalValue(), 18);
           

        }

        [TestMethod]
        public void Can_Clear()
        {
            Book book1 = new Book { BookID = 1, Name = "Book1", Price = 2 };
            Book book2 = new Book { BookID = 2, Name = "Book2", Price = 2 };
            Book book3 = new Book { BookID = 3, Name = "Book3", Price = 2 };

            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book1, 5);
            cart.AddItem(book3, 2);

            
            cart.Clear();

            

            Assert.AreEqual(cart.Lines.Count(), 0);


        }

        [TestMethod]
        public void Can_Generete_Specific_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book> {
                new Book { BookID = 1, Name = "Book1" ,Genre="Genre1"},
                new Book { BookID = 2, Name = "Book2" ,Genre="Genre2"},
                new Book { BookID = 3, Name = "Book3" ,Genre="Genre1"},
                new Book { BookID = 4, Name = "Book4" ,Genre="Genre3"},
                new Book { BookID = 5, Name = "Book5" ,Genre="Genre2"},

            }.AsQueryable());

            CartController controller = new CartController(mock.Object,null);

            Cart cart = new Cart();

            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Book.BookID, 1);
        }

        [TestMethod]
        public void Adding_Book_To_Cart()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book> {
                new Book { BookID = 1, Name = "Book1" ,Genre="Genre1"},
                new Book { BookID = 2, Name = "Book2" ,Genre="Genre2"},
                new Book { BookID = 3, Name = "Book3" ,Genre="Genre1"},
                new Book { BookID = 4, Name = "Book4" ,Genre="Genre3"},
                new Book { BookID = 5, Name = "Book5" ,Genre="Genre2"},

            }.AsQueryable());

            CartController controller = new CartController(mock.Object,null);

            Cart cart = new Cart();

            RedirectToRouteResult result = controller.AddToCart(cart, 1, "myUrl");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Content()
        {
            

            CartController target = new CartController(null,null);

            Cart cart = new Cart();

            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart,"myUrl").ViewData.Model;

            Assert.AreSame(result.cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod]
        public void Can_Checkout_Empty_Cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            CartController target = new CartController(null,mock.Object);

            Cart cart = new Cart();

            ShippingDetails shippingDetails = new ShippingDetails();

            ViewResult result = target.Checkout(cart, shippingDetails);

            mock.Verify(m=>m.ProcessOrder(It.IsAny<Cart>(),It.IsAny<ShippingDetails>()),Times.Never());

            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false,result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_invalid_sippingDetails()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            CartController target = new CartController(null, mock.Object);

            Cart cart = new Cart();

            ShippingDetails shippingDetails = new ShippingDetails();

            ViewResult result = target.Checkout(cart, shippingDetails);

            result.ViewData.ModelState.AddModelError("error", "error");

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }
    }
}
