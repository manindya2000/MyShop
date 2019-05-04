using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAcess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products=cache["products"] as List<Product>;
            if(products==null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product p)
        {
            var productToUpdate = products.Find(p1 => p1.Id == p.Id);
            if(productToUpdate!=null)
            {
                productToUpdate = p;
            }
            else
            {
                throw new Exception("Product not found!!!");
            }
        }

        public Product Find(String Id)
        {
            var productToFind = products.Find(p => p.Id == Id);
            if(productToFind !=null)
            {
                return (productToFind);
            }
            else
            {
                throw new Exception("Product Not Found!!!");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(String Id)
        {
            var productToDelete = products.Find(p => p.Id == Id);
            if(productToDelete !=null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found!!!");
            }
        }
    }
}
