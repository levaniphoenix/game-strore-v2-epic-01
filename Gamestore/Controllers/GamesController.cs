using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class GamesController(IGameService gameService) : ControllerBase
{
    private readonly IGameService _gameService = gameService;

    [HttpGet]
    public async Task<IEnumerable<GameModel>> Get()
    {
        return await _gameService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<GameModel?> Get(int id)
    {
        return await _gameService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task Post([FromBody] GameModel game)
    {
        await _gameService.AddAsync(game);
    }

    [HttpPut]
    public async Task Put([FromBody] GameModel game)
    {
        await _gameService.UpdateAsync(game);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _gameService.DeleteAsync(id);
    }
}
