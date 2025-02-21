using System.Text;
using Business.Exceptions;
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
    public async Task<IEnumerable<GameDetails>> Get()
    {
        var games = (await _gameService.GetAllAsync()).Select(x => x.Game);
        return games;
    }

    [HttpGet("{key}")]
    public async Task<GameDetails?> Get(string key)
    {
        var game = (await _gameService.GetByKeyAsync(key)) ?? throw new GameStoreNotFoundException(ErrorMessages.GameNotFound);
        return game.Game;
    }

    [HttpGet("{key}/genres")]
    public async Task<IEnumerable<GenreModel>> GetGenresByGamekey(string key)
    {
        return await _gameService.GetGenresByGamekey(key);
    }

    [HttpGet("{key}/platforms")]
    public async Task<IEnumerable<PlatformModel>> GetPlatformsByGamekey(string key)
    {
        return await _gameService.GetPlatformsByGamekey(key);
    }

    [HttpGet("find/{id}")]
    public async Task<GameDetails?> GetById(int id)
    {
        var game = (await _gameService.GetByIdAsync(id)) ?? throw new GameStoreNotFoundException(ErrorMessages.GameNotFound);
        return game.Game;
    }

    [HttpGet("{key}/file")]
    public async Task<FileContentResult> GetFile(string key)
    {
        var game = (await _gameService.GetByKeyAsync(key)) ?? throw new GameStoreNotFoundException(ErrorMessages.GameNotFound);
        var fileName = $"_{key}.txt";
        var fileContent = $"{game.Game.Name}\n{game.Game.Description}";
        var fileBytes = Encoding.UTF8.GetBytes(fileContent);

        return File(fileBytes, "text/plain", fileName);
    }

    // POST: games/
    [HttpPost]
    public async Task Post([FromBody] GameModel game)
    {
        if (!ModelState.IsValid)
        {
            throw new GameStoreModelStateException("Model is not valid");
        }

        await _gameService.AddAsync(game);
    }

    // PUT: games/
    [HttpPut]
    public async Task Put([FromBody] GameModel game)
    {
        if (!ModelState.IsValid)
        {
            throw new GameStoreModelStateException("Model is not valid");
        }

        await _gameService.UpdateAsync(game);
    }

    // DELETE: games/1
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _gameService.DeleteAsync(id);
    }
}
