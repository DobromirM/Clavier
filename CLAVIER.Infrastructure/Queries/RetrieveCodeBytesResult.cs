namespace CLAVIER.Infrastructure.Queries
{
    public class RetrieveCodeBytesResult : IQueryResult
    {
        public byte[] Data { get; set; }

        public RetrieveCodeBytesResult(byte[] data)
        {
            this.Data = data;
        }
    }
}
