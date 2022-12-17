namespace LegoasService.Core.DTOs
{
    public class OfficeResponse
    {
        public Guid Id { get; set; }
        public string? OfficeName { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateOfficeRequest
    {
        public string? OfficeName { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
    }

    public class UpdateOfficeRequest
    {
        public Guid? Id { get; set; }
        public string? OfficeName { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
    }
}
