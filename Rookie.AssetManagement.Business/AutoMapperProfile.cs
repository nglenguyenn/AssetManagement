using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
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
            CreateMap<Asset, AssetEditDto>().ReverseMap();
            CreateMap<Asset, AssetDto>().ReverseMap(); 
	        CreateMap<Asset, AssetDetailDto>().ReverseMap();
            CreateMap<Assignment, AssetHistoryDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.AssignedDate, opt => opt.MapFrom(s => s.AssignedDate))
                .ForMember(x => x.AssignedTo, opt => opt.MapFrom(s => s.AssignTo))
                .ForMember(x => x.AssignedBy, opt => opt.MapFrom(s => s.AssignBy))
                .ForMember(x => x.ReturnedDate, opt => opt.MapFrom(s => s.ReturnRequest.ReturnedDate))
                .ReverseMap();
            CreateMap<Assignment, AssignmentDto>()
                .ForMember(x => x.AssetCode, opt => opt.MapFrom(s => s.Asset.AssetCode))
                .ForMember(x => x.AssetName, opt => opt.MapFrom(s => s.Asset.AssetName))
                .ForMember(x => x.AssignedBy, opt => opt.MapFrom(s => s.AssignBy.UserName))
                .ForMember(x => x.AssignedTo, opt => opt.MapFrom(s => s.AssignTo.UserName))
                .ReverseMap();
            CreateMap<Assignment, AssignmentCreateDto>().ReverseMap();

            CreateMap<Assignment, AssignmentDetailDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.AssetCode, opt => opt.MapFrom(s => s.Asset.AssetCode))
                .ForMember(x => x.AssetName, opt => opt.MapFrom(s => s.Asset.AssetName))
                .ForMember(x => x.Specification, opt => opt.MapFrom(s => s.Asset.Specification))
                .ForMember(x => x.AssignedDate, opt => opt.MapFrom(s => s.AssignedDate))
                .ForMember(x => x.AssignedTo, opt => opt.MapFrom(s => s.AssignTo))
                .ForMember(x => x.AssignedBy, opt => opt.MapFrom(s => s.AssignBy))
                .ReverseMap();
        }
    }
}
