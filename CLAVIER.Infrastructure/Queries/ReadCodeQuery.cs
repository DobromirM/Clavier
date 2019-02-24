using CLAVIER.Infrastructure.Commands;

namespace CLAVIER.Infrastructure.Queries
{
    public class ReadCodeQuery : IQuery
    {
        public string Id { get; }

        public ReadCodeQuery(string id)
        {
            this.Id = id;
        }
    }
}
