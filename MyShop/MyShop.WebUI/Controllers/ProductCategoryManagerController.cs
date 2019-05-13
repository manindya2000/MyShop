using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAcess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;
        public ProductCategoryManagerController(IRepository<ProductCategory> productCategory)
        {
            context = productCategory;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> products = context.Collection().ToList();
            return View(products);
        }
        [HttpGet]
        public ActionResult CreateProductCategory()
        {
            ProductCategory p = new ProductCategory();
            return View(p);
        }
        [HttpPost]
        public ActionResult CreateProductCategory(ProductCategory p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                context.Insert(p);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult EditProductCategory(string Id)
        {
            var productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult EditProductCategory(Product p, String Id)
        {
            var productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategoryToEdit);
                }
                else
                {
                    productCategoryToEdit.Category = p.Category;
                    context.Commit();
                    return RedirectToAction("Index");
                }

            }
        }

        [HttpGet]
        public ActionResult DeleteProductCategory(string Id)
        {
            var productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("DeleteProductCategory")]
        public ActionResult DeleteProductCategory(Product p, string Id)
        {
            var productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}