namespace Shared.Categories
{
    public class AddCategoryReq
    {
        public required string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
