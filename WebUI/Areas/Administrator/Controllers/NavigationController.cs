using Domain.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Areas.Administrator.Models;

namespace WebUI.Areas.Administrator.Controllers
{
    public class NavigationController : Controller
    {
        private IProductRepository repository;

        public NavigationController(IProductRepository repo) {
            repository = repo;
        }
        // GET: Nav
        public PartialViewResult _Sidebar( string Brand = "All", string Category = "All")
        {
            ViewBag.SelectedGenre = Brand;
            ViewBag.SelectedCategory = Category;

            IEnumerable<string> Brands = repository.products.Select(x => x.Brand).Distinct().OrderBy(x => x);
            IEnumerable<string> Сategories = repository.products.Select(x => x.Сategory).Distinct().OrderBy(x=>x);
            
            return PartialView("_Sidebar", new FilterSideBarViewModel { Brands = Brands, Categories = Сategories });
        }
    }
}