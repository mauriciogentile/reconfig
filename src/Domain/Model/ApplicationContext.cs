using System.Security.Principal;

namespace Reconfig.Domain.Model
{
    public class ApplicationContext
    {
        public Application CurrentApplication { get; set; }
        public IIdentity User { get; set; }
    }
}
