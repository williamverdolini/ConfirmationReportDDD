using System.Linq;

namespace CR.Infrastructure.Db
{
    public interface IDatabase<T>
    {
        IQueryable<T> DbSet { get; }
    }
}
