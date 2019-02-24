namespace CLAVIER.Infrastructure.Queries
{
    public class RetrieveCodeBytesQuery : IQuery
    {
        public string Id { get; }

        public RetrieveCodeBytesQuery(string id)
        {
            this.Id = id;
        }
    }
}
