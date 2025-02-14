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
		}
	}
}
