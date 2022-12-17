using LegoasService.Core.DAOs;

namespace LegoasService.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(Guid id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
