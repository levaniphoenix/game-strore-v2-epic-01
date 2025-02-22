using Business.Models;
using Data.Data;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;

namespace Gamestore.Tests.IntegrationTests
{
	public class GamesControllerTests
	{
		private HttpClient _client;

		private CustomWebApplicationFactory _factory;

		private const string BaseUrl = "/games";

		[SetUp]
		public void Init()
		{
			_factory = new CustomWebApplicationFactory();
			_client = _factory.CreateClient();
		}

		[Ignore("ignore until integration tests can be run on CI")]
		[Test]
		public async Task GamesControllerGetAllReturnsAllFromDb()
		{
			var response = await _client.GetAsync(BaseUrl);
			response.EnsureSuccessStatusCode();
			var stingResponse = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<List<GameModel>>(stingResponse);

			actual.Should().NotBeNullOrEmpty();
			actual.Count.Should().Be(DBSeeder.Games.Length);
		}

		//[Test]
		public async Task GameControllerAddsGameToDB()
		{
			var game = new GameModel
			{
				Game = new GameDetails
				{
					Name = "TestGame",
					Description = "TestDescription",
				},
			};
			var json = JsonConvert.SerializeObject(game);
			var data = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync(BaseUrl, data);
			response.EnsureSuccessStatusCode();
			var stingResponse = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<GameModel>(stingResponse);
		}

		[TearDown]
		public void TearDown()
		{
			_factory.Dispose();
			_client.Dispose();
		}
	}
}
