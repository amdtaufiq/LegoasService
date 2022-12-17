namespace LegoasService.Core.DTOs
{
    public class MenuAccessResponse
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
        public bool CreateAccess { get; set; }
        public bool ReadAccess { get; set; }
        public bool UpdateAccess { get; set; }
        public bool DeleteAccess { get; set; }
        public virtual MenuResponse Menu { get; set; }
    }

    public class CreateMenuAccessRequest
    {
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
        public bool CreateAccess { get; set; }
        public bool ReadAccess { get; set; }
        public bool UpdateAccess { get; set; }
        public bool DeleteAccess { get; set; }
    }

    public class UpdateMenuAccessRequest
    {
        public Guid? Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
        public bool CreateAccess { get; set; }
        public bool ReadAccess { get; set; }
        public bool UpdateAccess { get; set; }
        public bool DeleteAccess { get; set; }
    }
}
