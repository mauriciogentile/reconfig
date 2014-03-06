using System;

namespace Reconfig.Configuration
{
    public class ReconfigException : Exception
    {
        public ReconfigException(Exception inner)
            : base("Impossible to load configuration from server", inner)
        {
        }
    }
}
