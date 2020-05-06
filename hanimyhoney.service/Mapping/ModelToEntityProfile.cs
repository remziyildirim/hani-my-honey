using AutoMapper;
using hanimyhoney.Domain.Dto;
using hanimyhoney.Domain.Entity;


namespace hanimyhoney.Service.Mapping
{
	public class ModelToEntityProfile : Profile
	{
		public ModelToEntityProfile()
		{
			CreateMap<User, UserDto>();

			// Example Conditional Mapping
			// CreateMap<User, UserDto>()  
			//         .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.CurrentCity));
		}
	}

	public class EntityToModelProfile : Profile
	{
		public EntityToModelProfile()
		{
			CreateMap<UserDto, User>();
		}
	}

}