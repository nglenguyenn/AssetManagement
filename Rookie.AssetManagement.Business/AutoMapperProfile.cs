using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.DataAccessor.Entities;


namespace Rookie.AssetManagement.Business
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            FromDataAccessorLayer();
            FromPresentationLayer();
        }

        private void FromPresentationLayer()
        {
            
        }

        private void FromDataAccessorLayer()
        {
            CreateMap<User, UserCreateDto>().ForMember(x =>x.TimeOffset, opt => opt.Ignore()).ReverseMap();
            CreateMap<User, UserEditDto>().ForMember(x => x.TimeOffset, opt => opt.Ignore()).ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Asset, AssetCreateDto>().ReverseMap();
            CreateMap<Asset, AssetResponseDto>().ReverseMap();
        }
    }
}
