using AutoMapper;
using Business.Models;
using Data.Entities;

namespace Business
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile() 
		{
			CreateMap<Game, GameModel>().ReverseMap();
			CreateMap<Genre, GenreModel>().ReverseMap();
			CreateMap<Platform, PlatformModel>().ReverseMap();
		}
	}
}
