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

        public ViewResult Store(ProductFilterViewModel productFilterViewModel, int page = 1, int pageSize = 9)
        {
            ProductListViewModel model;
            if (productFilterViewModel == null || 
                (productFilterViewModel.Categories == null &&
                productFilterViewModel.Brands == null &&
                productFilterViewModel.PriceMin == 0 &&
                productFilterViewModel.PriceMax == 0))
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
                model = new ProductListViewModel
                {
                    Products = productRepository.products.Where(p=>
                    (productFilterViewModel.Categories != null &&
                    productFilterViewModel.Categories.Select(c=>c.Category).Contains(p.Сategory)) && 
                    (productFilterViewModel.Brands != null && productFilterViewModel.Brands.Select(b=>b.Brand).Contains(p.Brand)) && 
                    (p.Price>= productFilterViewModel.PriceMin && p.Price<= productFilterViewModel.PriceMax)).
                    
                    //Where(p => p.Brand == null ||(ProductFilterViewModel.Brands.Select(b=>b.Brand).Contains(p.Brand))&&
                    //(p.Сategory == null || (productRepository.products.Select(c=>c.Сategory).Contains(p.Сategory))&&
                    //( (p.Price>=ProductFilterViewModel.PriceMax && p.Price<= ProductFilterViewModel.PriceMax)))
                    //).
                    
                    OrderBy(p => p.ProductID).Skip((page - 1) * pageSize).Take(pageSize),
                    pagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = productRepository.products.Count()
                    },
                    productFilterViewModel = new ProductFilterViewModel()
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
            IEnumerable<Product> products = productRepository.products.Where(p => p.New == true).OrderBy(p => p.ProductID).Skip((int)(TotalProductsInSlide - 1)).Take(2);
            return PartialView("_NewProduct", products);
            //.OrderBy(book => book.BookID).Skip((page - 1) * pageSize).Take(pageSize),
        }


        public ActionResult TopSelling(int? TotalProductsInSlide)
        {
            int countOfProducts = 4;
            // ProductSlideViewModel productSlideViewModel = 
            if (TotalProductsInSlide == null)
                TotalProductsInSlide = countOfProducts;
            IEnumerable<Product> products = productRepository.products.Where(p => p.New == true).OrderBy(p => p.ProductID).Skip((int)(TotalProductsInSlide - 1)).Take(2);
            return PartialView("_TopSelling", products);
            //.OrderBy(book => book.BookID).Skip((page - 1) * pageSize).Take(pageSize),
        }

        public ActionResult Collections()
        {
            IEnumerable<string> categories = productRepository.products.Select(p => p.Сategory).Distinct().Take(3);
            List<Product> products  = new List<Product>();
            foreach(var category in categories)
            {
                products.Add(productRepository.products.Where(p => p.Сategory == category).FirstOrDefault());
            }
            return PartialView("_Collections", products);
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
            Product product = productRepository.products.Where(m => m.ProductID == ProductID).FirstOrDefault();
            if (product != null)
            {
                Image image;
                if (imageId!=-1)
                    image = product.ImagesWithResolutions.Where(i => i.ImagesWithResolutionsID == imageId).FirstOrDefault().Images.Where(i => i.width == width && i.height == height).FirstOrDefault();
                else
                    image = product.ImagesWithResolutions.FirstOrDefault().Images.FirstOrDefault();
                return File(image.ImageData, image.ImageMimeType);
            }
            else
            {
                return null;
            }
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