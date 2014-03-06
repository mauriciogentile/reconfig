using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reconfig.Configuration
{
    enum Environment
    {
        Development,
        Testing,
        Staging,
        Production
    }

    class Configuration
    {
        public Configuration()
        {
            Sections = new List<ConfigurationSection>();
        }

        public string ApplicationId { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Environment Environment { get; set; }
        public string Url { get; set; }
        public List<ConfigurationSection> Sections { get; set; }
    }
}
