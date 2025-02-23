using AutoMapper;
using speezs.DataAccess.Models;
using speezs.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Configurations
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<CreateUserRequest, User>();
			CreateMap<UpdateUserRequest, User>()
				.ForMember(src => src.UserId, opt => opt.Ignore())
				.ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))
				.ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => src.PhoneNumber != null))
				.ForMember(dest => dest.FullName, opt => opt.Condition(src => src.FullName != null))
				.ForMember(dest => dest.ProfileImageUrl, opt => opt.Condition(src => src.ProfileImageUrl != null));
			
		}
	}
}
