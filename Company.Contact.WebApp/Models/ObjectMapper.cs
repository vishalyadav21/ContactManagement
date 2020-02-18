using AutoMapper;
using System.Collections.Generic;

namespace Company.Contact.WebApp.Models
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Contacts.DomainEntities.Contact, ContactModel>();
            CreateMap<ContactModel, Contacts.DomainEntities.Contact>();
        }
    }
}
