namespace LegoasService.Core.DAOs
{
    public class MenuAccess : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
        public bool CreateAccess { get; set; }
        public bool ReadAccess { get; set; }
        public bool UpdateAccess { get; set; }
        public bool DeleteAccess { get; set; }
        public virtual Menu? Menu { get; set; }
    }
}
