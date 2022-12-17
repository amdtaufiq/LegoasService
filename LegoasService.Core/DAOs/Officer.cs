namespace LegoasService.Core.DAOs
{
    public class Officer : BaseEntity
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public virtual ICollection<OfficerRole>? OfficerRoles { get; set; }
        public virtual ICollection<OfficerOffice>? OfficerOffices { get; set; }
    }
}
