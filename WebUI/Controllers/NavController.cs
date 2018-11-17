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

        
        public PartialViewResult _ProductFilterArea(ProductFilterViewModel productFilterViewModel = null, string param = null)
        {
            if(param != null)
            {
                ViewBag.param = param;
            }
            if (productFilterViewModel == null || productFilterViewModel.Categories == null || productFilterViewModel.Brands == null)
            {
                productFilterViewModel = new ProductFilterViewModel();

                List<string> LBrands = productRepository.products.Select(p => p.Brand).Distinct().ToList();
                List<string> LCategories = productRepository.products.Select(p => p.Сategory).Distinct().ToList();
                List<FilterOfBrand> filterOfBrands = new List<FilterOfBrand>();
                List<FilterOfCategory> filterOfCategory = new List<FilterOfCategory>();
                for (int i = 0; i < LBrands.Count(); i++)
                {
                    filterOfBrands.Add(
                        new FilterOfBrand
                        {
                            Brand = LBrands[i],
                            CountOfProducts = productRepository.products.Where(p => p.Brand == LBrands[i]).Count(),
                            IsChecked = false
                        }
                        );
                }
                for (int i = 0; i < LCategories.Count(); i++)
                {
                    filterOfCategory.Add(
                        new FilterOfCategory
                        {
                            Category = LCategories[i],
                            CountOfProducts = productRepository.products.Where(p => p.Сategory == LCategories[i]).Count(),
                            IsChecked = false
                        }
                        );
                }
                int pMax = productFilterViewModel.PriceMax;
                int pMin = productFilterViewModel.PriceMin;
                productFilterViewModel = new ProductFilterViewModel
                {
                    Brands = filterOfBrands,
                    Categories = filterOfCategory,
                    PriceMax = pMax,
                    PriceMin = pMin
                };
            }
            else
            {
                ProductFilterViewModel productFilterViewModel_2 = new ProductFilterViewModel();

                List<string> LBrands = productRepository.products.Select(p => p.Brand).Distinct().ToList();
                List<string> LCategories = productRepository.products.Select(p => p.Сategory).Distinct().ToList();
                List<FilterOfBrand> filterOfBrands = new List<FilterOfBrand>();
                List<FilterOfCategory> filterOfCategory = new List<FilterOfCategory>();
                for (int i = 0; i < LBrands.Count(); i++)
                {
                    filterOfBrands.Add(
                        new FilterOfBrand
                        {
                            Brand = productFilterViewModel.Brands[i].Brand ?? LBrands[i],
                            CountOfProducts = productRepository.products.Select(p => p.Brand).Count(),
                            IsChecked = productFilterViewModel.Brands[i].IsChecked
                        }
                        );
                }
                for (int i = 0; i < LCategories.Count(); i++)
                {
                    filterOfCategory.Add(
                        new FilterOfCategory
                        {
                            Category = productFilterViewModel.Categories[i].Category ?? LCategories[i],
                            CountOfProducts = productRepository.products.Select(p => p.Сategory).Count(),
                            IsChecked = productFilterViewModel.Categories[i].IsChecked
                        }
                        );
                }
                int pMax = productFilterViewModel.PriceMax;
                int pMin = productFilterViewModel.PriceMin;
                productFilterViewModel_2 = new ProductFilterViewModel
                {
                    Brands = filterOfBrands,
                    Categories = filterOfCategory,
                    PriceMax = pMax,
                    PriceMin = pMin
                };
                productFilterViewModel = productFilterViewModel_2;
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

            public PartialViewResult _BreadCrumb(string Category = null)
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