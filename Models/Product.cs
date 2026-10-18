using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;


        [Required]
        [Range(100,1000)]
        public int Price { get; set; }



        public Guid CategoryId { get; set; }

        // 
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;



        
    }
}
