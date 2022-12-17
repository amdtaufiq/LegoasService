namespace LegoasService.Core.DTOs
{
    public class OfficerRoleResponse
    {
        public Guid Id { get; set; }
        public Guid OfficerId { get; set; }
        public Guid RoleId { get; set; }
        public virtual RoleResponse Role { get; set; }
    }

    public class CreateOfficerRoleRequest
    {
        public Guid OfficerId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UpdateOfficerRoleRequest
    {
        public Guid? Id { get; set; }
        public Guid OfficerId { get; set; }
        public Guid RoleId { get; set; }
    }
}
