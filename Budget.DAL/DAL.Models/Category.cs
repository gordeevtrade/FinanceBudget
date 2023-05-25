namespace FamilyBudjetAPI
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryTypeId { get; set; }

        // [JsonIgnore]
        public CategoryType? TransactionType { get; set; }
    }
}