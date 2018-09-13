using Domain.Abstarct;
using Domain.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IBookRepository repository;
        private IProductRepository productRepository;

        public NavController(IProductRepository repo)
        {
            productRepository = repo;
        }

        public PartialViewResult _ProductFilterArea(ProductFilterViewModel productFilterViewModel = null)
        {
            if (productFilterViewModel == null || productFilterViewModel.Categories == null || productFilterViewModel.Brands == null)
            {
                List<string> LBrands = productRepository.products.Select(p => p.Brand).Distinct().ToList();
                List<string> LCategories = productRepository.products.Select(p => p.Сategory).Distinct().ToList();
                List<FilterOfBrand> filterOfBrands = new List<FilterOfBrand>();
                List<FilterOfCategory> filterOfCategory = new List<FilterOfCategory>();
                foreach (string brand in LBrands)
                {
                    filterOfBrands.Add(
                        new FilterOfBrand
                        {
                            Brand = brand,
                            CountOfProducts = productRepository.products.Select(p => p.Brand).Count(),
                            IsChecked = false
                        }
                        );
                }
                foreach (string category in LCategories)
                {
                    filterOfCategory.Add(
                        new FilterOfCategory
                        {
                            Category = category,
                            CountOfProducts = productRepository.products.Select(p => p.Сategory).Count(),
                            IsChecked = false
                        }
                        );
                }
                productFilterViewModel = new ProductFilterViewModel
                {
                    Brands = filterOfBrands,
                    Categories = filterOfCategory,
                    PriceMax = 10000,
                    PriceMin = 1
                };
            }

            return PartialView(productFilterViewModel);


        }
        // GET: Nav
        //public PartialViewResult Menu( string genre = null,bool horizontalNav = false)
        //{
        //    ViewBag.SelectedGenre = genre;

        //    IEnumerable<string> genres = repository.Books.Select(x => x.Genre).Distinct().OrderBy(x=>x);

        //    return PartialView("FlexMenu", genres);
        //}

        public PartialViewResult BreadCrumb(string Category = null)
        {
            ViewBag.SelectedCategory = Category;
            IEnumerable<string> RouteCategory = Request.QueryString.AllKeys;

            return PartialView("_BreadCrumb", RouteCategory);
        }

        public PartialViewResult Menu(string Category = null)
        {
            ViewBag.SelectedCategory = Category;
            IEnumerable<string> Categories = productRepository.products.Select(c => c.Сategory).Distinct().OrderBy(x => x);

            return PartialView("_Menu", Categories);
        }

        public PartialViewResult Header(Cart cart, string Category = null)
        {
            //    ViewBag.SelectedCategory = Category;
            HeaderViewModel headerViewModel = new HeaderViewModel
            {
                cart = cart,
                Categories = productRepository.products.Select(c => c.Сategory).Distinct().OrderBy(x => x)
            };
            return PartialView("_Header", headerViewModel);
        }

        public ActionResult AutocompleteSearch(string term, string Category = null)
        {


            if (Category == null)
            {
                var models = productRepository.products.Where(p => p.Name.Contains(term)).Select(p => new { value = p.Name }).
                Distinct();
                return Json(models, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var models = productRepository.products.Where(p => p.Name.Contains(term) && p.Сategory == Category).Select(p => new { value = p.Name }).
                Distinct();
                return Json(models, JsonRequestBehavior.AllowGet);
            }


        }
    }
}