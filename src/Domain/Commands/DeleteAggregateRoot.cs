namespace Reconfig.Domain.Commands
{
    public class DeleteAggregateRoot<TRoot>
    {
        public DeleteAggregateRoot(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
