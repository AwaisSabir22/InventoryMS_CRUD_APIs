namespace InventoryMS_CRUD_APIs.Models
{
    public class Category
    {

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        //Create a navigation property to represent the relationship between the Category and Product entities.
        public ICollection<Product> ?Products { get; set; }

    }
}
