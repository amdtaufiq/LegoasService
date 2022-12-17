namespace LegoasService.Core.Interfaces.Services
{
    public interface IPasswordService
    {
        bool Check(string hash, string password);
        string Hash(string password);
    }
}
