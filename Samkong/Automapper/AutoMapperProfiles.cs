using AutoMapper;
using Samkong.Model;
using Samkong.Model.DTO;

namespace Samkong.AutomapperProfile
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductDto, Product>()
                .ForMember(data=>data.CreateDate,opt=>opt.Ignore())
                .ForMember(data=>data.UpdateDate,opt=>opt.Ignore());
            CreateMap<CustomerDTO,Customer>()
                .ForMember(data=>data.Createdate,opt=>opt.Ignore())
                .ForMember(data=>data.Updatedate,opt=>opt.Ignore());

            CreateMap<EmployeeDTO,Employee>()
                .ForMember(data => data.Createdate, opt => opt.Ignore())
                .ForMember(data => data.Updatedate, opt => opt.Ignore());
            CreateMap<RegisterDTO,Register>();
        }
    }
}
