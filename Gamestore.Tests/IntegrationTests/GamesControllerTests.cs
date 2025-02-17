using Business.Models;
using Data.Entities;
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

		[Test]
		public async Task GamesControllerGetAllReturnsAllFromDb()
		{
			var response = await _client.GetAsync(BaseUrl);
			response.EnsureSuccessStatusCode();
			var stingResponse = await response.Content.ReadAsStringAsync();
			var actual = JsonConvert.DeserializeObject<List<GameModel>>(stingResponse);
		}

		//[Test]
		public async Task GameControllerAddsGameToDB()
		{
			var game = new GameModel
			{
				Name = "TestGame",
				Description = "TestDescription",
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
