using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using neoDR.Data.Context;
using neoDR.Data.Entities;
using neoDR.Models;
using System.Security;
using System.Security.Claims;

namespace neoDR.Controllers
{
    public class AccountController : Controller
    {
        ProjectContext db = new ProjectContext();

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User();
                    user.Email = model.Email;
                    user.Name = model.UserName;
                    user.Password = model.Password;
                    user.Surname = model.Surname;
                    user.BirthDate = model.BirthDay;
                    user.isVIPMember = false;

                    if (user.isVIPMember != false)
                    {
                        Role.RoleType userLevel = Role.RoleType.VIP;
                    }

                    db.Users.Add(user);
                    db.SaveChanges();

                    ViewBag.Message = "Kayıt başarılıdır, sisteme giriş yapabilirsiniz";

                }
                catch (Exception)
                {
                    ModelState.AddModelError("Email", "Giriş yaptığınız e-posta adresinden kayıt mevcuttur!");
                    ModelState.AddModelError("UserName", "Giriş yaptığınız kullanıcı isminden kayıt mevcuttur!");
                }

            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Include(x => x.Roles).FirstOrDefault(y => y.Name == model.UserName);
                if (user != null)
                {
                    model.Password = user.Password;
                    if (user.Password == model.Password)
                    {
                        List<Claim> claims = new List<Claim>();

                        var claim1 = new Claim(ClaimTypes.Name, user.Name);
                        var claim2 = new Claim(ClaimTypes.Email, user.Email);

                        claims.Add(claim1);
                        claims.Add(claim2);

                        foreach (var role in user.Roles)
                        {
                            var claim3 = new Claim(ClaimTypes.Role, role.Name);

                            claims.Add(claim3);
                        }

                        var identity = new ClaimsIdentity(claims, "6161");

                        var claimPrinciple = new ClaimsPrincipal(identity);

                        var authProps = new AuthenticationProperties();

                        authProps.IsPersistent = model.RememberMe;

                        if (model.RememberMe)
                        {
                            authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30);
                        }

                        HttpContext.SignInAsync(claimPrinciple, authProps);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Kullanıcı adı doğru girilmedi");
                        ModelState.AddModelError("Password", "Parola Doğru Değil");
                        return View();
                    }
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            List<User> userList = db.Users.ToList();

            return View(userList);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(User user)
        {
            db.Users.Add(user);
            int result = db.SaveChanges();

            if (result > 0)
            {
                TempData["Message"] = "Üye kayıt edilmiştir!";
            }
            else
            {
                TempData["Message"] = "Üye kayıt işlemi başarısızdır!";
            }

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            User user = db.Users.Find(id);

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(User user)
        {
            db.Users.Update(user);
            int result = db.SaveChanges();

            if (result > 0)
            {
                TempData["Message"] = "Kullanıcı bilgileri güncellenmiştir!";
            }
            else
            {
                TempData["Message"] = "Kullanıcı bilgileri güncelleme işlemi başarısızdır!";
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            User user = db.Users.Find(id);

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(User user)
        {
            db.Users.Remove(user);
            int result = db.SaveChanges();

            if (result > 0)
            {
                TempData["Message"] = "Üyelik silinmiştir!";
            }
            else
            {
                TempData["Message"] = "Üyelik silme işlemi başarısızdır!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync("6161");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult BeVIPMember()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult BeVIPMember(int id)
        {
            //VIP üyelik satın alımı için gerekli işlemleri yapabilmem (ödeme işlemleri vs. ) için araştırmalarımda API kullanmam gerektiğini gördüğümden bu kısmı atladım.

            try
            {
                //Database'den kullanıcıyı buldum. ID zaten tekli olduğundan dolayı, null'da dönerse else kısmına düşsün diye FIND yöntemini kullandım.
                var user = db.Users.Find(id);

                if (user != null)
                {
                    //Eğer kullanıcı bulunduysa ve API kısmında ödeme başarılı olursa bu IF içine girerdi ve propertylerden gelen özelliği true'ya çeker.
                    user.isVIPMember = true;
                    db.SaveChanges();

                    return Ok(new { Status = "success", Message = "Üyelik güncelleme başarılı." });
                } 
                else
                {
                    return NotFound(new { Status = "error", Message = "Kullanıcı bulunamadı." });
                }
            }
            catch (Exception ex)
            {
                string customErrorMessage = "Ödeme işlemi sırasında bir hata oluştu.";
                //Ödeme işleminde bir hata olsaydı direkt olarak buraya düşecek.
                return BadRequest(new { Status = "error", Message = customErrorMessage });
            }

            return View();
        }
    }
}
