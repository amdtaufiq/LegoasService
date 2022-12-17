namespace LegoasService.Core.DAOs
{
    public class Role : BaseEntity
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<MenuAccess>? MenuAccesses { get; set; }
    }
}
