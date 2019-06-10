using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAcess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public ProductManagerController(IRepository<Product> product, IRepository<ProductCategory> productCategory)
        {
            context = product;
            productCategories = productCategory;
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
            ProductManagerViewModel productViewModel = new ProductManagerViewModel();
            productViewModel.Product = new Product();
            productViewModel.ProductCategories = productCategories.Collection();
            return View(productViewModel);
            //Product p = new Product();
            //return View(p);
        }
        [HttpPost]
        public ActionResult CreateProduct(ProductManagerViewModel p,HttpPostedFile file)
        {
            
            if (!ModelState.IsValid)
            {
                return View(p.Product);
            }
            else
            {
                if(file!=null)
                {
                    p.Product.Image = p.Product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//"));
                }
                context.Insert(p.Product);
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
                ProductManagerViewModel productViewModel = new ProductManagerViewModel();
                productViewModel.Product = product;
                productViewModel.ProductCategories = productCategories.Collection();
                return View(productViewModel);
            }
        }
        [HttpPost]
        public ActionResult EditProduct(ProductManagerViewModel e, String Id, HttpPostedFile file)
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
                    if (file != null)
                    {
                        e.Product.Image = e.Product.Id + Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath("//Content//ProductImages//"));
                    }
                    productToEdit.Category = e.Product.Category;
                    productToEdit.Description = e.Product.Description;
                    productToEdit.Image = e.Product.Image;
                    productToEdit.Price = e.Product.Price;
                    productToEdit.Name = e.Product.Name;
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