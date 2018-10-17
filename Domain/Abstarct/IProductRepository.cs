using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstarct
{
    public interface IProductRepository
    {
        IEnumerable<Product> products { get; }
        Task SaveProduct(Product product);
        Task<Product> DeleteProduct(int ProductId);
        Task<ImagesWithResolutions> DeleteImagesWithResolutions(int ProductID, int ImageID);
        Task<ImagesWithResolutions> UpdateImagesWithResolutions(int ProductID, int ImageID, ImagesWithResolutions imagesWithResolutions);
        Task SaveImagesWithResolutions(int ProductID, ImagesWithResolutions imagesWithResolutions);
    }
}
