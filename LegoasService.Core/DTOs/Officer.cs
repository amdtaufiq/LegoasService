namespace LegoasService.Core.DTOs
{
    public class OfficerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<OfficerRoleResponse> OfficerRoles { get; set; }
        public virtual ICollection<OfficerOfficeResponse> OfficerOffices { get; set; }
    }

    public class CreateOfficerRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<CreateOfficerRoleRequest> OfficerRoles { get; set; }
        public virtual ICollection<CreateOfficerOfficeRequest> OfficerOffices { get; set; }
    }

    public class UpdateOfficerRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<UpdateOfficerRoleRequest> OfficerRoles { get; set; }
        public virtual ICollection<UpdateOfficerOfficeRequest> OfficerOffices { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdatePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
