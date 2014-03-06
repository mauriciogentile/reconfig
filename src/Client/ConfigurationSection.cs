using System.Collections.Generic;

namespace Reconfig.Configuration
{
    class ConfigurationSection
    {
        public string Name { get; set; }
        public List<ConfigurationEntry> Settings { get; set; }
    }
}
