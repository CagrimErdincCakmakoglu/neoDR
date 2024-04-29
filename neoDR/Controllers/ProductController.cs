using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neoDR.Data.Context;
using neoDR.Data.Entities;
using neoDR.Models;
using System.Reflection.Metadata;

namespace neoDR.Controllers
{
    //Yönetim paneline yalnızca Admin rolünün ulaşabilmesi için yetkilendirme, kısıtlama yaptırdım.
    [Authorize(Roles = "Admin")]


    public class ProductController : Controller
    {

        ProjectContext db = new ProjectContext();

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> productList = db.Products.ToList();

            return View(productList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            db.Products.Add(product);
            int result = db.SaveChanges();

            if (result > 0)
            {
                TempData["Message"] = "Ürün kayıt edilmiştir!";
            }
            else
            {
                TempData["Message"] = "Ürün kayıt işlemi başarısızdır!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Categories = db.Categories.ToList();
            Product product = db.Products.Find(id);

            return View(product);

        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            db.Products.Update(product);
            int result = db.SaveChanges();

            if (result > 0)
            {
                TempData["Message"] = "Ürün güncellenmiştir!";
            }
            else
            {
                TempData["Message"] = "Ürün güncelleme işlemi başarısızdır!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            db.Products.Remove(product);
            int result = db.SaveChanges();

            if (result > 0)
            {
                TempData["Message"] = "Ürün silinmiştir!";
            }
            else
            {
                TempData["Message"] = "Ürün silme işlemi başarısızdır!";
            }

            return RedirectToAction("Index");
        }
        
    }
}
