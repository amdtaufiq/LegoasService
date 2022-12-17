namespace LegoasService.Core.DAOs
{
    public class Menu : BaseEntity
    {
        public string? MenuLabel { get; set; }
        public string? MenuUrl { get; set; }
        public int Ordering { get; set; }
    }
}
