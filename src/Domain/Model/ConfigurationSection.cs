using System.Collections.Generic;

namespace Reconfig.Domain.Model
{
    public class ConfigurationSection
    {
        public string Name { get; set; }
        public IEnumerable<ConfigurationEntry> Settings { get; set; }
    }
}
