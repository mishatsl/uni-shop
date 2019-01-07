using Domain.Abstarct;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Product> products
        {
            get
            {
                 return context.Products.Include(p => p.ImagesWithResolutions.Select(i => i.Images));
                //return context.Entry(products).Collection("ImagesWithResolutions").Load();
            }
        }

        public IEnumerable<Information> information
        {
            get
            {
                return context.Informations;
            }
        }

        public async Task UpdateInformation(Information info)
        {
            Information information = await context.Informations.FirstOrDefaultAsync(i => i.Id == info.Id);
            if(information != null)
            {
                information.HTMLContent = info.HTMLContent;
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveImagesWithResolutions(int ProductID, ImagesWithResolutions imagesWithResolutions)
        {
            // Product product = context.Products.Include(i => i.ImagesWithResolutions).FirstOrDefault( p => p.ProductID == ProductID);
            Product product = context.Products.Include(p => p.ImagesWithResolutions.Select(i => i.Images)).FirstOrDefault();

            

            if (product != null && product.ImagesWithResolutions.Count() < 12 )
            {
                context.Products.Find(ProductID).ImagesWithResolutions.Add(imagesWithResolutions);
                await context.SaveChangesAsync();
            }

            
        }

        public async Task<ImagesWithResolutions> DeleteImagesWithResolutions(int ProductID, int ImageID)
        {
            Product product = context.Products.FirstOrDefault(p => p.ProductID == ProductID);
            ImagesWithResolutions imagesWithResolutions = product.ImagesWithResolutions.FirstOrDefault(i => i.ImagesWithResolutionsID == ImageID);
            if (imagesWithResolutions != null) { 
            context.Entry(product).Collection("ImagesWithResolutions").Load();
            context.Entry(imagesWithResolutions).Collection("Images").Load();
            foreach(var image in imagesWithResolutions.Images.ToList())
            {
                    context.Images.Remove(image);
            }
            await context.SaveChangesAsync();
            context.ImagesWithResolutions.Remove(imagesWithResolutions);
            await context.SaveChangesAsync();
            }
            return imagesWithResolutions;
        }

        public async Task<ImagesWithResolutions> UpdateImagesWithResolutions(int ProductID, int ImageID, ImagesWithResolutions updatedImagesWithResolutions)
        { 
            Product product = context.Products.FirstOrDefault(p => p.ProductID == ProductID);
            ImagesWithResolutions imagesWithResolutions = product.ImagesWithResolutions.FirstOrDefault(i => i.ImagesWithResolutionsID == ImageID);
            if (imagesWithResolutions != null)
            {
                context.Entry(product).Collection("ImagesWithResolutions").Load();
                context.Entry(imagesWithResolutions).Collection("Images").Load();
                for (int i = 0; i<imagesWithResolutions.Images.Count(); i++)
                {
                   await UpdateImage(imagesWithResolutions.Images.ElementAt(i), updatedImagesWithResolutions.Images.ElementAt(i));
                }
                await context.SaveChangesAsync();
            }
            return imagesWithResolutions;
        }

        public async Task UpdateImage(Image image, Image uploadedImage)
        {
            Image DbEntry =  context.Images.Find(image.ImageID);
            if(DbEntry != null)
            {
                DbEntry.ImageData = uploadedImage.ImageData;
                DbEntry.height = uploadedImage.height;
                DbEntry.width = uploadedImage.width;
                DbEntry.alt = uploadedImage.alt;
                DbEntry.ImageMimeType = uploadedImage.ImageMimeType;
            }
           await context.SaveChangesAsync();
        }

        public async Task<Product> DeleteProduct(int ProductId)
        {
            Product product = context.Products.Find(ProductId);
            if(product!=null)
            {
                context.Products.Remove(product);
               await context.SaveChangesAsync();
            }
            return product;
        }

        public async Task SaveProduct(Product product)
        {
            if (product.ProductID == 0)
                context.Products.Add(product);
            else
            {
                Product dbEntry = await context.Products.FindAsync(product.ProductID);

                dbEntry.Name = product.Name;
                dbEntry.InStock = product.InStock;
                dbEntry.Price = product.Price;
                dbEntry.PreviousPrice = product.PreviousPrice;
                dbEntry.Сategory = product.Сategory;
                dbEntry.Brand = product.Brand;
                dbEntry.Description = product.Description;
                dbEntry.Details = product.Details;
                dbEntry.New = product.New;
                dbEntry.TopSelling = product.TopSelling;
                //dbEntry.ImagesWithResolutions = new List<ImagesWithResolutions>(product.ImagesWithResolutions); //CopyImagesWithResolutions(product.ImagesWithResolutions);
                //dbEntry.Reviews = new List<Review>(product.Reviews);
            }

            await context.SaveChangesAsync();
        }



        public ICollection<ImagesWithResolutions> CopyImagesWithResolutions(ICollection<ImagesWithResolutions> imagesWithResolutions)
        {
            List<ImagesWithResolutions> images = new List<ImagesWithResolutions>(imagesWithResolutions);

            for(int i = 0; i < imagesWithResolutions.Count(); i++)
            {
                imagesWithResolutions.ElementAt(i).Images = new List<Image>(imagesWithResolutions.ElementAt(i).Images);
            }
            return imagesWithResolutions;
        }
    }
}
