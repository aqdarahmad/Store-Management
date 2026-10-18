using System.ComponentModel.DataAnnotations;

namespace StoreManagment.DTO
{
    public class ProductRequestDto
    {
        [Required]

        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(100,1000)]
        public int Price { get; set; }

        public Guid CategoryId { get; set; }
    }
}
