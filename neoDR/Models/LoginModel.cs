using System.ComponentModel.DataAnnotations;

namespace neoDR.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Kısmı Boş Geçilemez!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre Kısmı Boş Geçilemez !")]
        [MinLength(8, ErrorMessage = "Şifreyi eksik giriş yaptınız, minumum '8' karakter içermelidir!")]

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
