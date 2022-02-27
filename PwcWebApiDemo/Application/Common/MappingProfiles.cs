using AutoMapper;
using PwcWebApiDemo.Domain;

namespace PwcWebApiDemo.Application.Common
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Client, Client>();
        }

    }
}
