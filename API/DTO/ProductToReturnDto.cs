namespace API.DTO
{
    public class ProductToReturnDto
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        // Product has relationship 
        public string ProductType { get; set; }


        public string ProductBrand { get; set; }

    }
}