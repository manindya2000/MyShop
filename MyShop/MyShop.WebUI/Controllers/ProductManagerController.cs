using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAcess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            Product p = new Product();
            return View(p);
        }
        [HttpPost]
        public ActionResult CreateProduct(Product p)
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
        public ActionResult EditProduct(string Id)
        {
            var product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult EditProduct(Product p, String Id)
        {
            var productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productToEdit);
                }
                else
                {
                    productToEdit.Category = p.Category;
                    productToEdit.Description = p.Description;
                    productToEdit.Image = p.Image;
                    productToEdit.Price = p.Price;
                    productToEdit.Name = p.Name;
                    context.Commit();
                    return RedirectToAction("Index");
                }

            }
        }

        [HttpGet]
        public ActionResult DeleteProduct(string Id)
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
        [ActionName("DeleteProduct")]
        public ActionResult DeleteProduct(Product p, string Id)
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