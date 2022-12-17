namespace LegoasService.Core.DTOs
{
    public class MenuResponse
    {
        public Guid Id { get; set; }
        public string? MenuLabel { get; set; }
        public string? MenuUrl { get; set; }
        public int Ordering { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateMenuRequest
    {
        public string? MenuLabel { get; set; }
        public string? MenuUrl { get; set; }
        public int Ordering { get; set; }
    }

    public class UpdateMenuRequest
    {
        public Guid? Id { get; set; }
        public string? MenuLabel { get; set; }
        public string? MenuUrl { get; set; }
        public int Ordering { get; set; }
    }
}
