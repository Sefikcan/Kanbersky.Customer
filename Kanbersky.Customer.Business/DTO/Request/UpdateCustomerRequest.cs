﻿namespace Kanbersky.Customer.Business.DTO.Request
{
    public class UpdateCustomerRequest
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
