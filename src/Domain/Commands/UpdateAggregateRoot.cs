using Reconfig.Domain.Model;

namespace Reconfig.Domain.Commands
{
    public class UpdateAggregateRoot<TRoot> where TRoot : AggregateRoot
    {
        public UpdateAggregateRoot(TRoot root)
        {
            UpdatedAggregateRoot = root;
        }

        public TRoot UpdatedAggregateRoot { get; private set; }
    }
}
