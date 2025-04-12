using AutoMapper;
using Common.Helpers;
using Business.Models;
using Business.Models.Auth;
using Business.Models.Northwind;
using Data.Entities;
using Northwind.Data.Entities;
using static Business.Models.CommentModel;
using static Business.Models.Northwind.ProductModel;
using Order = Data.Entities.Order;
using OrderDetailsModel = Business.Models.OrderDetailsModel;
using OrderModel = Business.Models.OrderModel;

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
				.ForMember(to => to.IsDeleted, from => from.MapFrom(x => x.Game.IsDeleted))
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

			CreateMap<User, UserRegistrationModel>()
				.ForMember(to => to.Email, from => from.MapFrom(x => x.Email))
				.ForMember(to => to.LastName, from => from.MapFrom(x => x.LastName))
				.ForMember(to => to.FirstName, from => from.MapFrom(x => x.FirstName));

			CreateMap<UserRegistrationModel, User>()
				.ForMember(to => to.Email, from => from.MapFrom(x => x.Email))
				.ForMember(to => to.LastName, from => from.MapFrom(x => x.LastName))
				.ForMember(to => to.FirstName, from => from.MapFrom(x => x.FirstName))
				.ForMember(to => to.PasswordHash, from => from.MapFrom(x => PasswordHasher.HashPassword(x.Password)));

			CreateMap<User, UserModel>()
				.ForPath(to => to.User.Id, from => from.MapFrom(x => x.Id))
				.ForPath(to => to.User.Email, from => from.MapFrom(x => x.Email))
				.ForPath(to => to.User.FirstName, from => from.MapFrom(x => x.FirstName))
				.ForPath(to => to.User.LastName, from => from.MapFrom(x => x.LastName))
				.ForPath(to => to.User.BannedUntil, from => from.MapFrom(x => x.BannedUntil))
				.ForPath(to => to.User.IsBanned, from => from.MapFrom(x => x.IsBanned))
				.ForPath(to => to.User.PasswordHash, from => from.MapFrom(x => x.PasswordHash))
				.ReverseMap();

			CreateMap<Role, RoleModel>()
				.ForPath(to => to.Role.Id, from => from.MapFrom(x => x.Id))
				.ForPath(to => to.Role.Name, from => from.MapFrom(x => x.Name))
				.ForMember(to => to.Permissions, from => from.MapFrom(x => x.Description.Split(",", StringSplitOptions.None)));

			CreateMap<RoleModel, Role>()
				.ForMember(to => to.Id, from => from.MapFrom(x => x.Role.Id))
				.ForMember(to => to.Name, from => from.MapFrom(x => x.Role.Name))
				.ForMember(to => to.Description, from => from.MapFrom(x => string.Join(",", x.Permissions.Where(v => v != null))));


			//mapping for Northwind
			CreateMap<Category,CategoryModel>().ReverseMap();
			CreateMap<Customer, CustomerModel>().ReverseMap();
			CreateMap<Employee, EmployeeModel>().ReverseMap();
			CreateMap<EmployeeTerritory, EmployeeTerritoryModel>().ReverseMap();
			CreateMap<OrderDetails, Models.Northwind.OrderDetailsModel>().ReverseMap();
			CreateMap<Northwind.Data.Entities.Order,Models.Northwind.OrderModel>().ReverseMap();
			CreateMap<Region, RegionModel>().ReverseMap();
			CreateMap<Shipper, ShipperModel>().ReverseMap();
			CreateMap<Supplier, SupplierModel>().ReverseMap();
			CreateMap<Territory, TerritoryModel>().ReverseMap();

			CreateMap<Product, ProductDetails>().ReverseMap();
			CreateMap<Product, ProductModel>()
				.ForMember(to => to.Product, from => from.MapFrom(x => x))
				.ReverseMap();
		}
	}
}
