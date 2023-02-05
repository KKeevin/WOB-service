namespace iSpan_final_service.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int ClassId { get; set; }
        public int BrandId { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public string? Describe { get; set; }
        public decimal Discount { get; set; }
        public int Stock { get; set; }
        public bool Validity { get; set; }
    }
}
