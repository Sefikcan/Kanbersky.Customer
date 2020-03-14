using System.ComponentModel.DataAnnotations;

namespace Kanbersky.Customer.Entity.Abstract
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
