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
		}
	}
}
