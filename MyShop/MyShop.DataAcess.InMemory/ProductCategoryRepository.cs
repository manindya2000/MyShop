using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAcess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productcategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productcategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory p)
        {
            var productCategoriesToUpdate = productCategories.Find(p1 => p1.Id == p.Id);
            if (productCategoriesToUpdate != null)
            {
                productCategoriesToUpdate = p;
            }
            else
            {
                throw new Exception("Product not found!!!");
            }
        }

        public ProductCategory Find(String Id)
        {
            var productCategoriesToFind = productCategories.Find(p => p.Id == Id);
            if (productCategoriesToFind != null)
            {
                return (productCategoriesToFind);
            }
            else
            {
                throw new Exception("Product Category Not Found!!!");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(String Id)
        {
            var productCategoriesToDelete = productCategories.Find(p => p.Id == Id);
            if (productCategoriesToDelete != null)
            {
                productCategories.Remove(productCategoriesToDelete);
            }
            else
            {
                throw new Exception("Product Category Not Found!!!");
            }
        }
    }
}
