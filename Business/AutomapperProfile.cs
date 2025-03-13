using AutoMapper;
using Business.Models;
using Data.Entities;

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
				.ForMember(to => to.UnitsInStock, from => from.MapFrom(x => x.Game.UnitsInStock))
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
		}
	}
}
