using Kanbersky.Customer.Core.Entity;
using Kanbersky.Customer.Entity.Abstract;

namespace Kanbersky.Customer.Entity.Concrete
{
    public class Address : BaseEntity,IEntity
    {
        public string Name { get; set; }

        public string FullAddress { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
