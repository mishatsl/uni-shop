using Domain.Abstarct;
using Domain.Concrete;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository productRepository;
        int count = 4;
        int pageSize = 9;

        public ProductController(IProductRepository repository)
        {
            productRepository = repository;
        }
        // GET: Home
        public ActionResult Index()
        {
           var p =  productRepository.products.ElementAt(0);
            return View(productRepository.products);
        }

        //public PartialViewResult ProductSlideList(string criterion,int countOfProducts)
        //{
        //    ProductSlideListModel model = null;
        //    switch (criterion)
        //    {
        //        case "New":
        //            model =  new ProductSlideListModel
        //            {
        //                Products = productRepository.products.Where(m => m.New == true).Skip((countOfProducts - 1) * countOfProducts).Take(countOfProducts),
        //                countOfProducts = countOfProducts
        //            };
        //            return PartialView("NewProduct", model);
        //        case"InStock":
        //            model = new ProductSlideListModel
        //            {
        //                Products = productRepository.products.Where(m => m.InStock == true).Skip((countOfProducts - 1) * countOfProducts).Take(countOfProducts),
        //                countOfProducts = countOfProducts
        //            };
        //            return PartialView("InStockProduct", model);
        //        default: break;
        //    }
        //    return null;
        //}


        public ViewResult Search(string searchParam = null, string category = null, int page = 1, int pageSize = 9)
        {
            ViewBag.active = null;
            ProductListViewModel model;
            IEnumerable<Product> products = new List<Product>();
            if(searchParam != null)
            {
                searchParam = searchParam.ToLower();

                if (category != null && category != "null")
                {
                    products = productRepository.products.Where(p => (p.Name.ToLower().Split(' ').FirstOrDefault(s => s.StartsWith(searchParam)) != null || (p.Name.ToLower() == searchParam )) && p.Сategory == category).OrderBy(p => p.ProductID).Skip((page - 1) * pageSize).Take(pageSize);
                    if (products.Count() <= 0)
                    {
                        products = productRepository.products.Where(p => (p.Brand.ToLower().Split(' ').FirstOrDefault(s => s.StartsWith(searchParam)) != null || (p.Brand.ToLower() == searchParam )) && p.Сategory == category).OrderBy(p => p.ProductID).Skip((page - 1) * pageSize).Take(pageSize);
                    }
                }
                else
                {

                    products = productRepository.products.Where(p => (p.Name.ToLower().Split(' ').FirstOrDefault(s => s.StartsWith(searchParam)) != null) || (p.Name.ToLower() == searchParam)).OrderBy(p => p.ProductID).Skip((page - 1) * pageSize).Take(pageSize);
                    if (products.Count() <= 0)
                    {
                        products = productRepository.products.Where(p => (p.Brand.ToLower().Split(' ').FirstOrDefault(s => s.StartsWith(searchParam)) != null) ||( p.Brand.ToLower() == searchParam)).OrderBy(p => p.ProductID).Skip((page - 1) * pageSize).Take(pageSize);
                    }
                    if (products.Count() <= 0)
                    {
                        products = productRepository.products.Where(p => (p.Сategory.ToLower().Split(' ').FirstOrDefault(s => s.StartsWith(searchParam)) != null) || (p.Сategory.ToLower() == searchParam)).OrderBy(p => p.ProductID).Skip((page - 1) * pageSize).Take(pageSize);
                    }
                }
                              
            }
            model = new ProductListViewModel
            {
                Products = products,
                pagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = products.Count()
                },
                productFilterViewModel = new ProductFilterViewModel()
            };
            return View("Store", model);
        }

            public ViewResult Store(ProductFilterViewModel productFilterViewModel = null, string param = null, int page = 1, int pageSize = 9)
        {
            ProductListViewModel model; 

            //if (productFilterViewModel == null || 
            //    (productFilterViewModel.Categories == null &&
            //    productFilterViewModel.Brands == null &&
            //    productFilterViewModel.PriceMin == 0 &&
            //    productFilterViewModel.PriceMax == 0))
            if(param == null)
            {
                model = new ProductListViewModel
                {
                    Products = productRepository.products.OrderBy(p => p.ProductID).Skip((page - 1) * pageSize).Take(pageSize),
                    pagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = productRepository.products.Count()
                    },
                    productFilterViewModel = new ProductFilterViewModel()
                };
            }
            else
            {
                int PriceMin, PriceMax;
                string[] arr_param = param.Split('/');
                IEnumerable<Product> Products = new List<Product>();
                List<Product> Products_sort_category = new List<Product>();
                List<Product> Products_sort_brand = new List<Product>();

                IEnumerable<string> filter_categories = productRepository.products.Select(p => p.Сategory).Distinct().ToList().Intersect(arr_param);

                //filter_categories = productRepository.products.Select(p => p.Сategory).Distinct().ToList();

                //filter_categories = filter_categories.Intersect(param);

                if (filter_categories.Count() > 0)
                {
                    
                    foreach (string category in  filter_categories)
                    {
                        Products_sort_category.AddRange(productRepository.products.Where(p => p.Сategory == category));
                    }
                }

                Products = Products_sort_category;

                IEnumerable<string> filter_brands = productRepository.products.Select(p => p.Brand).Distinct().ToList().Intersect(arr_param);

                if (filter_brands.Count() > 0)
                {
                    
                    foreach (string brand in filter_brands)
                    {
                        Products_sort_brand.AddRange(productRepository.products.Where(p => p.Brand == brand));
                    }
                    if(Products_sort_category.Count() > 0)
                    {
                        Products = Products_sort_brand.Intersect(Products_sort_category);
                    }
                    else
                    {
                        Products = Products_sort_brand;
                    }
                }
                
                if(param.Contains("PriceMin") && param.Contains("PriceMax"))
                {
                    string Str_PriceMin = arr_param.FirstOrDefault(a => a.Contains("PriceMin")).Replace("PriceMin","");
                    PriceMin = Convert.ToInt32(Str_PriceMin);

                    string Str_PriceMax = arr_param.FirstOrDefault(a => a.Contains("PriceMax")).Replace("PriceMax", "");
                    PriceMax = Convert.ToInt32(Str_PriceMax);
                    if(Products.Count() > 0)
                    {
                        Products = Products.Where(p => p.Price >= PriceMin && p.Price <= PriceMax);
                    }
                    else
                    {
                        Products = productRepository.products.Where(p => p.Price >= PriceMin && p.Price <= PriceMax);
                    }
                    
                }


                model = new ProductListViewModel
                {
                    Products = Products.
                    //productRepository.products.Where(p=>
                    //(productFilterViewModel.Categories != null &&
                    //productFilterViewModel.Categories.Select(c => c.Category).Contains(p.Сategory)) &&
                    //(productFilterViewModel.Brands != null && productFilterViewModel.Brands.Select(b => b.Brand).Contains(p.Brand)) &&
                    //(p.Price >= productFilterViewModel.PriceMin && p.Price <= productFilterViewModel.PriceMax)).

                    //Where(p => p.Brand == null ||(ProductFilterViewModel.Brands.Select(b=>b.Brand).Contains(p.Brand))&&
                    //(p.Сategory == null || (productRepository.products.Select(c=>c.Сategory).Contains(p.Сategory))&&
                    //( (p.Price>=ProductFilterViewModel.PriceMax && p.Price<= ProductFilterViewModel.PriceMax)))
                    //).
                    //productFilterViewModel.Categories.Select(viewCategory => viewCategory.Category).Where(category => category == p.Сategory)).
                    OrderBy(p => p.ProductID).Skip((page - 1) * pageSize).Take(pageSize),
                    pagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = Products.Count()
                    }
                  //  productFilterViewModel = productFilterViewModel
                    
                };
}
           

            return View(model);
        }

        public ViewResult Product(int ProductID)
        {
            Product product = productRepository.products.Where(m => m.ProductID == ProductID).FirstOrDefault();

            //Avarage_rating(product);
            if (product == null)
                return null;
            return View(product);
        }

        
        public PartialViewResult ReviewsArea(Review review)
        {
            int? ProductID = Convert.ToInt32(HttpContext.Request.QueryString["ProductID"]);
            if (ProductID==null)
                return null;
            Product product = productRepository.products.Where(m => m.ProductID == ProductID).FirstOrDefault();
            Avarage_rating(product);
            if (review == null)
                return null;
            if (product == null)
                return null;
            product.Reviews.Add(review);
            productRepository.SaveProduct(product);
            return PartialView();
        }

        public ActionResult NewProduct(int? TotalProductsInSlide)
        {
            int countOfProducts = 4;
            // ProductSlideViewModel productSlideViewModel = 
            if (TotalProductsInSlide == null)
                TotalProductsInSlide = countOfProducts;
            IEnumerable<Product> products = productRepository.products.Where(p => p.New == true).OrderBy(p => p.ProductID);
            return PartialView("~/Views/Product/Sections/_NewProduct.cshtml", products);
            //.OrderBy(book => book.BookID).Skip((page - 1) * pageSize).Take(pageSize),
        }


        public ActionResult TopSelling(int? TotalProductsInSlide)
        {
            int countOfProducts = 4;
            // ProductSlideViewModel productSlideViewModel = 
            if (TotalProductsInSlide == null)
                TotalProductsInSlide = countOfProducts;
            IEnumerable<Product> products = productRepository.products.Where(p => p.TopSelling == true).OrderBy(p => p.ProductID);
            return PartialView("~/Views/Product/Sections/_TopSelling.cshtml", products);
            //.OrderBy(book => book.BookID).Skip((page - 1) * pageSize).Take(pageSize),
        }

        public ActionResult Collections()
        {
            //IEnumerable<string> categories = productRepository.products.Select(p => p.Сategory).Distinct().Take(3);
            //List<Product> products  = new List<Product>();
            //foreach(var category in categories)
            //{
            //    products.Add(productRepository.products.Where(p => p.Сategory == category).FirstOrDefault());
            //}

            IEnumerable<Product> products = productRepository.products.GroupBy(p => p.Сategory).Select(p => p.FirstOrDefault());
            return PartialView("~/Views/Product/Sections/_Collections.cshtml", products);
            //.OrderBy(book => book.BookID).Skip((page - 1) * pageSize).Take(pageSize),
        }

        void Avarage_rating(Product product)
        {
            if (Session["Previous_reviewa_count"] == null || Convert.ToUInt32(Session["Previous_reviewa_count"]) != product.Reviews.Count())
            {
                Session["Previous_reviewa_count"] = product.Reviews.Count();
                float Avarage_rating = product.Reviews.Sum(m => m.Rating) / product.Reviews.Count();
                Avarage_rating = (Avarage_rating * 10F + 0.5F) / 10F;
                string str_Avarage_rating = Avarage_rating - Math.Floor(Avarage_rating) == 0 ? String.Format("{0}", (int)Avarage_rating) : String.Format("{0}", Avarage_rating);
                Session["Avarage_rating"] = str_Avarage_rating;
            }
        }

        public FileContentResult GetImage(int ProductID, int width, int height, int imageId = -1)
        {
            Product product = productRepository.products.FirstOrDefault(m => m.ProductID == ProductID);
            if (product != null)
            {
                Domain.Entites.Image image;
                if (imageId == -1)
                {
                    image = product.ImagesWithResolutions.FirstOrDefault().Images.FirstOrDefault(i => i.width == width && i.height == height);
                }
                else if (imageId < 0)
                {
                    return null;
                }
                else// if (imageId < product.ImagesWithResolutions.Count())
                {
                    image = product.ImagesWithResolutions.FirstOrDefault(i => i.ImagesWithResolutionsID == imageId).Images.FirstOrDefault(i => i.width == width && i.height == height);
                }
                //else
                //{
                //    image = null;
                //}
                if (image != null)
                    return File(image.ImageData, image.ImageMimeType);
            }
            return null;
        }
        //public FileContentResult GetAdditionalImage(int ProductID,int index)
        //{
        //    Image image = productRepository.products.FirstOrDefault(m => m.ProductID == ProductID).ImagesWithResolutions.ElementAt(index + 1).FirstOrDefault().Images.ElementAt(index+1);
        //    if (image != null)
        //    {
        //        return File(image.ImageData, image.ImageMimeType);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}