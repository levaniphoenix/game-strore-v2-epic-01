using System.Text;
using Business.Models;
using Data.Data;
using FluentAssertions;
using Newtonsoft.Json;

namespace Gamestore.Tests.IntegrationTests
{
	public class GamesControllerTests
	{
		private HttpClient _client;

		private CustomWebApplicationFactory _factory;

		private const string BaseUrl = "/games";

		[OneTimeSetUp]
		public async Task Init()
		{
			_factory = new CustomWebApplicationFactory();
			await _factory.StartContainerAsync();
			_client = _factory.CreateClient();
		}

		//[Ignore("ignore until integration tests can be run on CI")]
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

		[Ignore("ignore until fix json serialize/deserialize")]
		[Test]
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
			actual.Should().NotBeNull();
		}

		[OneTimeTearDown]
		public async Task TearDown()
		{
			await _factory.StopContainerAsync();
			await _factory.DisposeAsync();
			_client.Dispose();
		}
	}
}
