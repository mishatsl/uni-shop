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
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo,IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }
        // GET: Cart
        public ViewResult Index(Cart cart,string returnUrl)
        {
            
            return View(new CartIndexViewModel { cart = cart, ReturnUrl = returnUrl});
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public RedirectToRouteResult AddToCart(Cart cart,int ProductID, string returnUrl)
        {
            Product product = repository.products.Where(b => b.ProductID == ProductID).FirstOrDefault();

            if (product != null)
            {
                cart.AddItem(product,1);
            }

            return RedirectToAction("Index",new { returnUrl });
        }

        public ActionResult RemoveFromCart(Cart cart, int ProductID, string returnUrl = null)
        {
            Product product = repository.products.Where(b => b.ProductID == ProductID).FirstOrDefault();

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            if (Request.IsAjaxRequest())
                return PartialView("_DropDown",cart);
            else
             return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Checkout(Cart cart)
        {
            return View(new CheckoutViewModel { cart = cart,shippingDetails = new ShippingDetails() } );
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            { ModelState.AddModelError(" ", "Извините корзина пуста!"); }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
                return View(new CheckoutViewModel { cart = cart, shippingDetails = new ShippingDetails() });
        }
    }
}