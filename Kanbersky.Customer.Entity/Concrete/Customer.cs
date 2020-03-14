using Kanbersky.Customer.Core.Entity;
using Kanbersky.Customer.Entity.Abstract;
using System.Collections.Generic;

namespace Kanbersky.Customer.Entity.Concrete
{
    public class Customer : BaseEntity,IEntity
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
