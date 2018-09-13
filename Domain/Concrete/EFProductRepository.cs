using Domain.Abstarct;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            }
        }

        public Product DeleteProduct(int ProductId)
        {
            Product product = context.Products.Find(ProductId);
            if(product!=null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
                context.Products.Add(product);
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);

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
                dbEntry.ImagesWithResolutions = CopyImagesWithResolutions(product.ImagesWithResolutions);
                dbEntry.Reviews = new List<Review>(product.Reviews);
            }

            context.SaveChanges();
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
