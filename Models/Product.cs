using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public int Price { get; set; }



        public Guid CategoryId { get; set; }

        // 
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;



        
    }
}
