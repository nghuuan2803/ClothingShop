namespace Domain.Entities
{
    public class Inventory : BaseEntity<int>
    {
        public int VariantId { get; set; }
        public int BranchId { get; set; }
        public int Quantity { get; set; }
        public ProductVariant? Variant { get; set; }
        public ShopBranch? Branch { get; set; }
    }
}
