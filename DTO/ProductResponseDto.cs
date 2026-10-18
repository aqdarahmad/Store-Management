namespace StoreManagment.DTO
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Price { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}
