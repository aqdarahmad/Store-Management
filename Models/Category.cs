using System.ComponentModel.DataAnnotations;

namespace StoreManagment.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;



        public List<Product> Products { get; set; } =new ();


       

    }
}
