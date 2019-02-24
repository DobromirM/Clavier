using System.Threading.Tasks;

namespace CLAVIER.Infrastructure.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> DisptachAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : IQueryResult;
    }
}
