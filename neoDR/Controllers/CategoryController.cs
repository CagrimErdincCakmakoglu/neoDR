using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neoDR.Data.Context;
using neoDR.Data.Entities;

namespace neoDR.Controllers
{
    //Yönetim paneline yalnızca Admin rolünün ulaşabilmesi için yetkilendirme, kısıtlama yaptırdım.
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        ProjectContext db = new ProjectContext();

        [HttpGet]
        public IActionResult Index()
        {
            List<Category> categoryList = db.Categories.ToList();

            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            db.Categories.Add(category);
            int result = db.SaveChanges();

            if (result > 0)
            {
                TempData["Message"] = "Kategori kayıt edilmiştir!";
            }
            else
            {
                TempData["Message"] = "Kategori kayıt işlemi başarısızdır!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Category category = db.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            db.Categories.Update(category);
            int result = db.SaveChanges();

            if (result > 0)
            {
                TempData["Message"] = "Kategori güncellenmiştir!";
            }
            else
            {
                TempData["Message"] = "Kategori güncelleme işlemi başarısızdır!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            db.Categories.Remove(category);
            int result = db.SaveChanges();

            if (result > 0)
            {
                TempData["Message"] = "Kategori silinmiştir!";
            }
            else
            {
                TempData["Message"] = "Kategori silme işlemi başarısızdır!";
            }

            return RedirectToAction("Index");
        }
    }
}
