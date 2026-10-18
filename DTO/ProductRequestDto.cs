namespace StoreManagment.DTO
{
    public class ProductRequestDto
    {

       
        public string Name { get; set; } = string.Empty;

        public int Price { get; set; }

        public Guid CategoryId { get; set; }
    }
}
