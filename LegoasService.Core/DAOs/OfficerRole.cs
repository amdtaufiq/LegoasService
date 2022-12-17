namespace LegoasService.Core.DAOs
{
    public class OfficerRole : BaseEntity
    {
        public Guid OfficerId { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
