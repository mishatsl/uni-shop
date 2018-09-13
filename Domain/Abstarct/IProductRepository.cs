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
        void SaveProduct(Product product);
        Product DeleteProduct(int ProductId);
    }
}
