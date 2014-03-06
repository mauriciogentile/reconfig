using System;

namespace Reconfig.Domain.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(Type type, object id)
            : base(string.Format(Texts.EntityNotFound, type.Name, id))
        {
        }
    }
}
