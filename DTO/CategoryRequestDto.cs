using System.ComponentModel.DataAnnotations;

namespace StoreManagment.DTO
{
    public class CategoryRequestDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
