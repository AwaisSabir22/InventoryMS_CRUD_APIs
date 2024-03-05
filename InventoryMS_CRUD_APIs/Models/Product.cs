namespace InventoryMS_CRUD_APIs.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ?ProductName { get; set; }

        public string ?ProductDescription { get; set; }

        //foreign key property
        public int CategoryId { get; set; }

        //navigation property
        public Category ?Category { get; set; }
    }
}
