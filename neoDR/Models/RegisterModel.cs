using System.ComponentModel.DataAnnotations;

namespace neoDR.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Soyadı Boş Geçilemez!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Doğum Tarihi Boş Geçilemez!")]
        public DateTime BirthDay { get; set; }

        [EmailAddress(ErrorMessage = "E-Mail Formatında Giriniz!")]
        [Required(ErrorMessage = "E-Mail Boş Geçilemez!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Boş Geçilemez !")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*_.-]).{8,12}$", ErrorMessage = "Güvenli Parola Seçiniz, 8-12 Karakter Arası Giriş Yapmalısınız!")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Parolar Eşleşmiyor!")]
        [Required(ErrorMessage = "Password Boş Geçilemez!")]
        public string PasswordAgain { get; set; }
    }
}
