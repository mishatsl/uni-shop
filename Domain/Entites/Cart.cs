using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Product Product, int quantity)
        {
            CartLine line = lineCollection.Where(b => b.Product.ProductID == Product.ProductID).FirstOrDefault();

            if (line == null)
                lineCollection.Add(new CartLine { Product = Product, Quantity = quantity });
            else
                line.Quantity += quantity; 
        }

        public void RemoveLine(Product Product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == Product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e=>e.Product.Price*e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
        
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
