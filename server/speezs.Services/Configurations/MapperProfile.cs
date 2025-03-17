using AutoMapper;
using speezs.DataAccess.Models;
using speezs.Services.Models.Look;
using speezs.Services.Models.MakeupProduct;
using speezs.Services.Models.Review;
using speezs.Services.Models.SubscriptionTier;
using speezs.Services.Models.Transfer;
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
			ConfigureUser();
			ConfigureMakeupProduct();	
			ConfigureLook();
			ConfigureTransfer();
			ConfigureSubscriptionTier();
			ConfigureReview();
		}

		public void ConfigureUser()
		{
			CreateMap<CreateUserRequest, User>();
			CreateMap<UpdateUserRequest, User>()
				.ForMember(src => src.UserId, opt => opt.Ignore())
				.ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null))
				.ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => src.PhoneNumber != null))
				.ForMember(dest => dest.FullName, opt => opt.Condition(src => src.FullName != null))
				.ForMember(dest => dest.ProfileImageUrl, opt => opt.Condition(src => src.ProfileImageUrl != null));
		}

		public void ConfigureMakeupProduct()
		{
			CreateMap<CreateMakeupProductRequest, MakeupProduct>();
			CreateMap<UpdateMakeupProductRequest, MakeupProduct>()
				.ForMember(src => src.ProductId, opt => opt.Ignore())
				.ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
				.ForMember(dest => dest.Brand, opt => opt.Condition(src => src.Brand != null))
				.ForMember(dest => dest.Category, opt => opt.Condition(src => src.Category != null))
				.ForMember(dest => dest.ColorCode, opt => opt.Condition(src => src.ColorCode != null))
				.ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
				.ForMember(dest => dest.ImageUrl, opt => opt.Condition(src => src.ImageUrl != null))
				.ForMember(dest => dest.IsVerified, opt => opt.Condition(src => src.IsVerified != null));
		}

		public void ConfigureLook()
		{
			CreateMap<CreateLookRequest, Look>();
			CreateMap<UpdateLookRequest, Look>()
			.ForMember(dest => dest.LookId, opt => opt.Ignore()) // Assume LookId is not updated in the request
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
			.ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic))
			.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
			.ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.ThumbnailUrl))
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Only map if source member is not null
		}

		public void ConfigureTransfer()
		{
			CreateMap<CreateTransferRequest, Transfer>();
		}

		public void ConfigureSubscriptionTier()
		{
			CreateMap<CreateSubscriptionTierRequest, SubscriptionTier>();
			CreateMap<UpdateSubscriptionTierRequest, SubscriptionTier>()
			.ForMember(dest => dest.TierId, opt => opt.Ignore()) // TierId should not be updated from request
			.ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
			.ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
			.ForMember(dest => dest.Price, opt => opt.Condition(src => src.Price != null))
			.ForMember(dest => dest.DurationDays, opt => opt.Condition(src => src.DurationDays != null))
			.ForMember(dest => dest.MaxTransfers, opt => opt.Condition(src => src.MaxTransfers != null))
			.ForMember(dest => dest.MaxCollections, opt => opt.Condition(src => src.MaxCollections != null))
			.ForMember(dest => dest.AllowsCommercialUse, opt => opt.Condition(src => src.AllowsCommercialUse != null))
			.ForMember(dest => dest.PriorityProcessing, opt => opt.Condition(src => src.PriorityProcessing != null))
			.ForMember(dest => dest.IsActive, opt => opt.Condition(src => src.IsActive != null))
			.ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
			.ForMember(dest => dest.DateCreated, opt => opt.Ignore())
			.ForMember(dest => dest.DateModified, opt => opt.Ignore())
			.ForMember(dest => dest.DateDeleted, opt => opt.Ignore())
			.ForMember(dest => dest.Usersubscriptions, opt => opt.Ignore());
		}

		public void ConfigureReview()
		{	
			CreateMap<CreateReviewRequest, Review>()
				.ForMember(dest => dest.LookId, opt => opt.MapFrom(src => src.LookId))
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
				.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
				.ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
				.ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow))
				.ForMember(dest => dest.DateModified, opt => opt.MapFrom(src => DateTime.UtcNow))
				.ForMember(dest => dest.HelpfulVotes, opt => opt.Ignore())
				.ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
				.ForMember(dest => dest.DateDeleted, opt => opt.Ignore())
				.ForMember(dest => dest.Look, opt => opt.Ignore())
				.ForMember(dest => dest.User, opt => opt.Ignore());
			CreateMap<UpdateReviewRequest, Review>()
				.ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.ReviewId))
				.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
				.ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
				.ForMember(dest => dest.DateModified, opt => opt.MapFrom(src => DateTime.UtcNow))
				.ForMember(dest => dest.LookId, opt => opt.Ignore())
				.ForMember(dest => dest.UserId, opt => opt.Ignore())
				.ForMember(dest => dest.HelpfulVotes, opt => opt.Ignore())
				.ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
				.ForMember(dest => dest.DateCreated, opt => opt.Ignore())
				.ForMember(dest => dest.DateDeleted, opt => opt.Ignore())
				.ForMember(dest => dest.Look, opt => opt.Ignore())
				.ForMember(dest => dest.User, opt => opt.Ignore());
		}
	}
}
