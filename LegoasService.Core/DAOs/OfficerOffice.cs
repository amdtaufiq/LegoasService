namespace LegoasService.Core.DAOs
{
    public class OfficerOffice : BaseEntity
    {
        public Guid OfficerId { get; set; }
        public Guid OfficeId { get; set; }
        public virtual Office? Office { get; set; }
    }
}
