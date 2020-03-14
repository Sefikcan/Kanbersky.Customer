using AutoMapper;
using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.DTO.Response;

namespace Kanbersky.Customer.Business.Mappings.AutoMapper
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Entity.Concrete.Customer, GetAllCustomerQueryResponse>().ReverseMap();
            CreateMap<Entity.Concrete.Customer, GetCustomerByIdQueryResponse>().ReverseMap();

            CreateMap<Entity.Concrete.Customer, CreateCustomerRequest>().ReverseMap();
            CreateMap<Entity.Concrete.Customer, CreateCustomerResponse>().ReverseMap();

            CreateMap<Entity.Concrete.Customer, UpdateCustomerRequest>().ReverseMap();
            CreateMap<Entity.Concrete.Customer, UpdateCustomerResponse>().ReverseMap();
        }
    }
}
