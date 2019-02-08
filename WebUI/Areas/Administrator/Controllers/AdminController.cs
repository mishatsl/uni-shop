using Domain.Abstarct;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebUI.Areas.Administrator.Models;

namespace WebUI.Areas.Administrator.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IProductRepository productRepository;
        int count = 4;
        int pageSize = 9;
        public AdminController(IProductRepository repository)
        {
            productRepository = repository;
        }
        // GET: Home
        public ActionResult Index(int page = 1, string brand = null, string category = null)
        {
            ProductViewModel productViewModel;
            if (brand != null)
            {
                productViewModel = new ProductViewModel
                {
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = productRepository.products.Count()
                    },
                    Products = productRepository.products.Where(p => p.Brand == brand).Skip((page - 1) * pageSize).Take(pageSize)
                };
            }
            else if(category != null)
            {
                productViewModel = new ProductViewModel
                {
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = productRepository.products.Count()
                    },
                    Products = productRepository.products.Where(p => p.Сategory == category).Skip((page - 1) * pageSize).Take(pageSize)
                };
            }
            else
            {
                productViewModel = new ProductViewModel
                {
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = productRepository.products.Count()
                    },
                    Products = productRepository.products.Skip((page - 1) * pageSize).Take(pageSize)
                };
            }
            

            return View(productViewModel);
        }

        public ViewResult Edit(int ProductID)
        {
            Product product = productRepository.products.FirstOrDefault(m => m.ProductID == ProductID);
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/temp/")), System.IO.File.Delete);
            List<State> states = new List<State>();
            for (int i = 0; i < 12; i++)
                states.Add(State.doNothing);
            return View(new UploadProductViewModel { States = states, Product = product });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UploadProductViewModel UploadProductViewModel)
        {
            if (ModelState.IsValid)
            {
                ImagesWithResolutions ImagesWithResolutions = new ImagesWithResolutions();

                string[] Files = Directory.GetFiles(Server.MapPath("~/temp/"));
                List<State> LStates = UploadProductViewModel.States.ToList();
                Product currentProduct = productRepository.products.FirstOrDefault(p => p.ProductID == UploadProductViewModel.Product.ProductID);
                if(currentProduct == null)
                {
                    await productRepository.SaveProduct(UploadProductViewModel.Product);
                    currentProduct = productRepository.products.LastOrDefault();
                }
                List<ImagesWithResolutions> LImagesWithResolutions = currentProduct.ImagesWithResolutions.ToList();
                int CountFiles = 0;
                
                for (int i = 0; i < LStates.Count(); i++)
                {
                    switch (LStates[i])
                    {
                        case State.add:
                            if (Files.Length > 0 && CountFiles < Files.Length)
                            {
                                deleteTempImg(Files[CountFiles]);
                                await productRepository.SaveImagesWithResolutions(currentProduct.ProductID, MakeImages(Files[CountFiles++]));
                            }

                            break;

                        case State.delete:
                            await productRepository.DeleteImagesWithResolutions(currentProduct.ProductID, currentProduct.ImagesWithResolutions.ElementAt(i).ImagesWithResolutionsID);
                            LStates.RemoveAt(i);
                            --i;
                            break;

                        case State.update:
                            deleteTempImg(Files[CountFiles]);
                            await productRepository.UpdateImagesWithResolutions(currentProduct.ProductID, currentProduct.ImagesWithResolutions.ElementAt(i).ImagesWithResolutionsID, MakeImages(Files[CountFiles++]));
                            break;
                        case State.saved:
                        case State.doNothing:
                        default: break;
                    }
                }



                UploadProductViewModel.Product.ImagesWithResolutions = currentProduct.ImagesWithResolutions;

             

                await productRepository.SaveProduct(UploadProductViewModel.Product);



                string filePath = Server.MapPath("~/temp/");
                Array.ForEach(Directory.GetFiles(filePath), System.IO.File.Delete);
                UploadProductViewModel.Product = productRepository.products.FirstOrDefault(p => p.ProductID == currentProduct.ProductID);
                if(UploadProductViewModel.Product == null)
                {
                    UploadProductViewModel.Product = productRepository.products.OrderBy(p => p.ProductID).LastOrDefault();
                }
                TempData["message"] = string.Format("Changes in the product \"{0}\" was change!", UploadProductViewModel.Product.Name);

            }


            return View(UploadProductViewModel);
        }


        void deleteTempImg(string fileName = "All")
        {
            string filePath = Server.MapPath("~/temp/");
            string[] files = Directory.GetFiles(filePath);
            if (fileName != "All")
            {
               string[] delFiles =  files.Where(f => (f.Split('\\').Last().Contains(fileName.Split('_')[0] + "_")) && (!f.Contains(fileName))).ToArray();
                Array.ForEach(delFiles, System.IO.File.Delete);
            }
            else
            { 
                Array.ForEach(Directory.GetFiles(filePath), System.IO.File.Delete);
            }
        }

        private ImagesWithResolutions MakeImages(string fileName)
        {
            byte[] image;
            //foreach (FileInfo file in Files)
            ImagesWithResolutions ImagesWithResolutions = new ImagesWithResolutions();
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    image = reader.ReadBytes((int)stream.Length);
                }
            }
            if (image != null)
            {

                System.Drawing.Image tempImage = byteArrayToImage(image);

               
                ImagesWithResolutions.Images.Add(new Domain.Entites.Image
                {
                    alt = fileName,
                    ImageData = this.imageToByteArray(tempImage),
                    ImageMimeType = "image/png",
                    width = 600,
                    height = 600
                });
                ImagesWithResolutions.Images.Add(new Domain.Entites.Image
                {
                    alt = fileName,
                    ImageData = this.imageToByteArray(tempImage),
                    ImageMimeType = "image/png",
                    width = 263,
                    height = 263

                });
                ImagesWithResolutions.Images.Add(new Domain.Entites.Image
                {
                    alt = fileName,
                    ImageData = this.imageToByteArray(tempImage),
                    ImageMimeType = "image/png",
                    width = 60,
                    height = 60
                });

            }


            return ImagesWithResolutions;
        }



        [HttpPost]
        public ActionResult UploadTemp(string id)
        {
            string fileName = null;
            ImageUploadViewModel imageUploadViewModel = null;

            try
            {

                foreach (string file in Request.Files)
                {
                   
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        using (var stream = fileContent.InputStream)
                        {
                            // and optionally write the file to disk
                            fileName = fileContent.FileName;//Path.GetExtension(file);
                            string filePath = Server.MapPath("~/temp/");
                            if ((System.IO.File.Exists(filePath + fileName)))
                            {
                                System.IO.File.Delete(filePath + fileName);
                            }
                            ViewBag.fileName = fileName;
                            var path = Path.Combine(Server.MapPath("~/temp/"), fileName);
                            using (var fileStream = System.IO.File.Create(path))
                            {
                                stream.CopyTo(fileStream);
                            }
                            imageUploadViewModel = new ImageUploadViewModel
                            {
                                Id = Convert.ToInt32(id),
                                Name = fileName
                            };

                            deleteTempImg(fileName);
                        }
                    }
                }

            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            //if (id == "0")
            //    return PartialView("_UploadMainImageTemp", imageUploadViewModel);
            //else
            //    return PartialView("_UploadImageTemp", imageUploadViewModel);
            return Content(fileName);
        }


        [HttpPost]
        public ActionResult DeleteTemp(string fileName)
        {
            deleteTempImg(fileName);
            if ((System.IO.File.Exists(Server.MapPath("~/temp/") + fileName)))
            {
                System.IO.File.Delete(Server.MapPath("~/temp/") + fileName);
                return Json("Ok");
            }
            return Json("File doesn't exist!"); ;
        }

     
        [HttpPost]
        public async Task<ActionResult> Delete(int ProductID)
        {

            Product deletedProduct = await productRepository.DeleteProduct(ProductID);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Книга \"{0}\" была удалена!", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Informations(string param = null)
        {
            Information information = null;
            if (param != null)
            {
               information = productRepository.information.FirstOrDefault(i => i.Head == param);
            }
            return View(information);
        }

        [HttpPost]
        public ActionResult Informations(Domain.Entites.Information information = null)
        {
            if(information != null)
            {
                productRepository.UpdateInformation(information);
            }
            return View(information);
        }

        public ViewResult Create()
        {
            ViewBag.Title = "Add";
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/temp/")), System.IO.File.Delete);
            return View("Edit", new UploadProductViewModel { Product = new Product()});
        }

        public ViewResult HotDeal()
        {
            HotDeal hotDeal = productRepository.HotDeal;
            if (hotDeal == null)
                hotDeal = new HotDeal();
            return View(hotDeal);
        }

        [HttpPost]
        public ViewResult HotDeal(HotDeal hot)
        {
            if(hot != null)
            {
                productRepository.UpdateHotDeal(hot);
            }            
            return View(hot);
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                arr = ms.ToArray();
            }
            return arr;
        }

        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image returnImage;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                returnImage = System.Drawing.Image.FromStream(ms);
            }
            return returnImage;
        }

        public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
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
                    ImagesWithResolutions imagesWithResolutions =  product.ImagesWithResolutions.FirstOrDefault(i => i.ImagesWithResolutionsID == imageId);
                    image = imagesWithResolutions.Images.FirstOrDefault(i => i.width == width && i.height == height);
                    //.Images.FirstOrDefault(i => i.width == width && i.height == height);
                   // image = null;
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
    }
}