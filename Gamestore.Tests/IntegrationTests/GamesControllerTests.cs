using Business.Models;
using Data.Entities;
using Newtonsoft.Json;

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

		[TearDown]
		public void TearDown()
		{
			_factory.Dispose();
			_client.Dispose();
		}
	}
}
