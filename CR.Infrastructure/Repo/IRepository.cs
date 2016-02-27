using CR.Infrastructure.Model;
using System.Threading.Tasks;

namespace CR.Infrastructure.Repo
{
    public interface IRepository<T> where T : IDomainEntity
    {
        Task<T> GetById(int Id);
        Task<T> Save(T aggregateRoot);
    }
}
