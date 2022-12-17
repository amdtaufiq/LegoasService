namespace LegoasService.Core.DTOs
{
    public class OfficerOfficeResponse
    {
        public Guid Id { get; set; }
        public Guid OfficerId { get; set; }
        public Guid OfficeId { get; set; }
        public virtual OfficeResponse Office { get; set; }
    }

    public class CreateOfficerOfficeRequest
    {
        public Guid OfficerId { get; set; }
        public Guid OfficeId { get; set; }
    }

    public class UpdateOfficerOfficeRequest
    {
        public Guid? Id { get; set; }
        public Guid OfficerId { get; set; }
        public Guid OfficeId { get; set; }
    }
}
