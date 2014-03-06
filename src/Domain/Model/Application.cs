using System.ComponentModel.DataAnnotations;

namespace Reconfig.Domain.Model
{
    public class Application : AggregateRoot
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AccessKey { get; set; }
        [Required]
        public string Owner { get; set; }
    }
}
