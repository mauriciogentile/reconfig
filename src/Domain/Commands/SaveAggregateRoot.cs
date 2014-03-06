using Reconfig.Domain.Model;

namespace Reconfig.Domain.Commands
{
    public class SaveAggregateRoot<TRoot> where TRoot : AggregateRoot
    {
        public SaveAggregateRoot(TRoot root)
        {
            NewAggregateRoot = root;
        }

        public TRoot NewAggregateRoot { get; private set; }
    }
}
