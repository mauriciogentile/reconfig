using System;

namespace Reconfig.Domain.Model
{
    public class CacheItem : AggregateRoot
    {
        public DateTime ExpirationDate { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
