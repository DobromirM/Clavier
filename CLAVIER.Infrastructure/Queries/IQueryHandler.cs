using System.Threading.Tasks;

namespace CLAVIER.Infrastructure.Queries
{
    public interface IQueryHandler<in TQuery, out TResult>
        where TQuery : IQuery
        where TResult : IQueryResult
    {
        TResult RetrieveAsync(TQuery query);
    }
}
