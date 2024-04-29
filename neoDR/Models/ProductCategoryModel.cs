using System.ComponentModel.DataAnnotations;

namespace neoDR.Models
{
    public class ProductCategoryModel
    {
        [Required(ErrorMessage = "Lütfen işlem yapmak istediğiniz ürünü seçiniz")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Lütfen işlemini yaptığınız ürünün kategorisini seçiniz")]
        public int CategoryId { get; set; }
    }
}
