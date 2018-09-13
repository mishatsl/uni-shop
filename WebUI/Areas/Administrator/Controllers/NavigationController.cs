using Domain.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Administrator.Controllers
{
    public class NavigationController : Controller
    {
        private IProductRepository repository;

        public NavigationController(IProductRepository repo) {
            repository = repo;
        }
        // GET: Nav
        public PartialViewResult _Sidebar( string Brand = "All")
        {
            ViewBag.SelectedGenre = Brand;

            IEnumerable<string> Сategories = repository.products.Select(x => x.Сategory).Distinct().OrderBy(x=>x);
            
            return PartialView("_Sidebar", Сategories);
        }
    }
}