using System.ComponentModel.DataAnnotations.Schema;

namespace neoDR.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Author {  get; set; }
        public string? Publisher { get; set; }
        public int? Pages { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set;}

        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
