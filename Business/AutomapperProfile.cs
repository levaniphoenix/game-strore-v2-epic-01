using AutoMapper;
using Business.Models;
using Data.Entities;
using static Business.Models.CommentModel;

namespace Business
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<GameModel, Game>()
				.ForMember(to => to.Id, from => from.MapFrom(x => x.Game.Id))
				.ForMember(to => to.Name, from => from.MapFrom(x => x.Game.Name))
				.ForMember(to => to.Key, from => from.MapFrom(x => x.Game.Key))
				.ForMember(to => to.Description, from => from.MapFrom(x => x.Game.Description))
				.ForMember(to => to.Price, from => from.MapFrom(x => x.Game.Price))
				.ForMember(to => to.Discount, from => from.MapFrom(x => x.Game.Discount))
				.ForMember(to => to.UnitInStock, from => from.MapFrom(x => x.Game.UnitInStock))
				.ForMember(to => to.PublisherId, from => from.MapFrom(x => x.PublisherId))
				.ReverseMap();

			CreateMap<GenreModel, Genre>()
				.ForMember(to => to.Id, from => from.MapFrom(x => x.Genre.Id))
				.ForMember(to => to.Name, from => from.MapFrom(x => x.Genre.Name))
				.ForMember(to => to.ParentGenreId, from => from.MapFrom(x => x.Genre.ParentGenreId))
				.ReverseMap();

			CreateMap<PlatformModel, Platform>()
				.ForMember(to => to.Id, from => from.MapFrom(x => x.Platform.Id))
				.ForMember(to => to.Type, from => from.MapFrom(x => x.Platform.Type))
				.ReverseMap();

			CreateMap<PublisherModel, Publisher>()
				.ForMember(to => to.Id, from => from.MapFrom(x => x.Publisher.Id))
				.ForMember(to => to.CompanyName, from => from.MapFrom(x => x.Publisher.CompanyName))
				.ForMember(to => to.HomePage, from => from.MapFrom(x => x.Publisher.HomePage))
				.ForMember(to => to.Description, from => from.MapFrom(x => x.Publisher.Description))
				.ReverseMap();

			CreateMap<OrderModel, Order>().ReverseMap();

			CreateMap<OrderDetailsModel, OrderGame>().ReverseMap();

			CreateMap<CommentDetails, Comment>()
				.ForMember(to => to.Id, from => from.MapFrom(x => x.Id))
				.ForMember(to => to.Name, from => from.MapFrom(x => x.Name))
				.ForMember(to => to.Body, from => from.MapFrom(x => x.Body))
				.ForMember(to => to.Replies, from => from.MapFrom(x => x.ChildComments));

			CreateMap<Comment, CommentDetails>()
				.ForMember(to => to.Id, from => from.MapFrom(x => x.Id))
				.ForMember(to => to.Name, from => from.MapFrom(x => x.Name))
				.ForMember(to => to.Body, from => from.MapFrom(x => x.DisplayContent))
				.ForMember(to => to.ChildComments, from => from.MapFrom(x => x.Replies));

			CreateMap<CommentModel, Comment>()
				.ForMember(to => to.Id, from => from.MapFrom(x => x.Comment.Id))
				.ForMember(to => to.GameId, from => from.MapFrom(x => x.GameId))
				.ForMember(to => to.Name, from => from.MapFrom(x => x.Comment.Name))
				.ForMember(to => to.Body, from => from.MapFrom(x => x.Comment.Body))
				.ForMember(to => to.Replies, from => from.MapFrom(x => x.Comment.ChildComments))
				.ForMember(to => to.ParentId, from => from.MapFrom(x => x.ParentId));

			CreateMap<Comment, CommentModel>()
				.ForMember(to => to.GameId, from => from.MapFrom(x => x.GameId))
				.ForPath(to => to.Comment.Id, from => from.MapFrom(x => x.Id))
				.ForPath(to => to.Comment.Name, from => from.MapFrom(x => x.Name))
				.ForPath(to => to.Comment.Body, from => from.MapFrom(x => x.DisplayContent))
				.ForPath(to => to.Comment.ChildComments, from => from.MapFrom(x => x.Replies))
				.ForMember(to => to.ParentId, from => from.MapFrom(x => x.ParentId));
		}
	}
}
