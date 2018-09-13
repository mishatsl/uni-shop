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

        public AdminController(IProductRepository repository)
        {
            productRepository = repository;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View(productRepository.products);
        }

        public ViewResult Edit(int ProductID)
        {
            Product product = productRepository.products.FirstOrDefault(m => m.ProductID == ProductID);
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/temp/")), System.IO.File.Delete);
            List<State> states = new List<State>();
            for (int i = 0; i < 12; i++)
                states.Add(State.doNothing);
            return View(new UploadProductViewModel { States = states, Product = product});
        }

        [HttpPost]
        public ActionResult Edit(UploadProductViewModel UploadProductViewModel)
        {
            if (ModelState.IsValid)
            {
                ImagesWithResolutions ImagesWithResolutions = new ImagesWithResolutions();
                //DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/temp/"));//Assuming Test is your Folder
                //FileInfo[] Files = d.GetFiles("*.png"); 
                //byte[] image;
                //foreach (FileInfo file in Files)
                string[] Files = Directory.GetFiles(Server.MapPath("~/temp/"));
                //foreach (string fileName in Files)
                //{
                //    using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                //    {
                //        using (var reader = new BinaryReader(stream))
                //        {
                //            image = reader.ReadBytes((int)stream.Length);
                //        }
                //    }
                //    if (image != null)
                //    {


                //        System.Drawing.Image tempImage = byteArrayToImage(image);

                //        //ImagesWithResolutions = new ImagesWithResolutions();
                //        ImagesWithResolutions = MakeImages(ImagesWithResolutions);
                        
                        
                //    }
                //}

                int FileIndex = 0;
                Product currentProduct = productRepository.products.FirstOrDefault(p => p.ProductID == UploadProductViewModel.Product.ProductID);
                List<ImagesWithResolutions> LImagesWithResolutions = currentProduct.ImagesWithResolutions.ToList();

                for ( int i = 0; i < UploadProductViewModel.States.Count(); i++)
                {
                    switch(UploadProductViewModel.States.ElementAt(i))
                    {
                        case State.add:
                            LImagesWithResolutions.Add(MakeImages(Files[FileIndex++]));
                            break;

                        case State.delete:
                            foreach (var Image in LImagesWithResolutions[i].Images.ToList())
                                LImagesWithResolutions[i].Images.Remove(Image);
                            LImagesWithResolutions
                                 .Remove(currentProduct.ImagesWithResolutions.ElementAt(i));
                            break;

                        case State.update:
                                LImagesWithResolutions[i] = MakeImages(Files[FileIndex++]);
                            break;
                        case State.saved:
                        case State.doNothing:
                        default: break;
                    }
                }

                UploadProductViewModel.Product.ImagesWithResolutions = LImagesWithResolutions;
                productRepository.SaveProduct(UploadProductViewModel.Product);

                string filePath = Server.MapPath("~/temp/");
                Array.ForEach(Directory.GetFiles(filePath), System.IO.File.Delete);

                TempData["message"] = string.Format("Changes in the product \"{0}\" was change!", UploadProductViewModel.Product.Name);
               // return RedirectToAction("Index");
            }
            //else
                return View(UploadProductViewModel);
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

                    //ImagesWithResolutions = new ImagesWithResolutions();

                    ImagesWithResolutions.Images.Add(new Domain.Entites.Image
                    {
                        alt = "",
                        ImageData = this.imageToByteArray(ResizeImage(tempImage, 600, 600)),
                        ImageMimeType = "image/png",
                        width = 600,
                        height = 600
                    });
                    ImagesWithResolutions.Images.Add(new Domain.Entites.Image
                    {
                        alt = "",
                        ImageData = this.imageToByteArray(ResizeImage(tempImage, 263, 263)),
                        ImageMimeType = "image/png",
                        width = 263,
                        height = 263

                    });
                    ImagesWithResolutions.Images.Add(new Domain.Entites.Image
                    {
                        alt = "",
                        ImageData = this.imageToByteArray(ResizeImage(tempImage, 60, 60)),
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
                        var stream = fileContent.InputStream;
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
                    }
                }

            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            //if(id == "0")
            //    return PartialView("_UploadMainImageTemp", imageUploadViewModel);
            //else
            //    return PartialView("_UploadImageTemp", imageUploadViewModel);
            return Content(fileName);
        }


        [HttpPost]
        public ActionResult DeleteTemp(string fileName)
        {
            if ((System.IO.File.Exists(Server.MapPath("~/temp/") + fileName)))
            {
                System.IO.File.Delete(Server.MapPath("~/temp/") + fileName);
                return Json("Ok");
            }
            return Json("File doesn't exist!"); ;
        }

        //[HttpPost]
        //public ActionResult Temp(HttpPostedFileBase image)
        //{
        //    System.Drawing.Image tempImage = byteArrayToImage(new byte[image.ContentLength]);

            //    tempImage.Save(,"~/temp/temp_image.png");
            //    return View(productRepository.products);
            //}

        [HttpPost]
        public ActionResult Delete(int ProductID)
        {

            Product deletedProduct = productRepository.DeleteProduct(ProductID);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Книга \"{0}\" была удалена!", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }



        public ViewResult Create(Product Product)
        {
            ViewBag.Title = "Add";
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/temp/")), System.IO.File.Delete);
            return View("Edit", new Product());
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
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
                if (imageId != -1)
                    image = product.ImagesWithResolutions.Where(i => i.ImagesWithResolutionsID == imageId).FirstOrDefault().Images.Where(i => i.width == width && i.height == height).OrderBy(i => i.ImageID).ElementAt(imageId);
                else
                    image = product.ImagesWithResolutions.FirstOrDefault().Images.FirstOrDefault();
                return File(image.ImageData, image.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}