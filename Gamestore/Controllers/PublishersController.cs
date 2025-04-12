using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.PublisherModel;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class PublishersController(IPublisherService publisherService) : ControllerBase
{
	[AllowAnonymous]
	[HttpGet]
	public async Task<ActionResult<IEnumerable<PublisherDetails>>> Get()
	{
		return Ok((await publisherService.GetAllAsync()).Select(p => p.Publisher));
	}

	[AllowAnonymous]
	[HttpGet("{companyName}")]
	public async Task<ActionResult<PublisherDetails?>> Get(string companyName)
	{
		var publisher = await publisherService.GetByNameAsync(companyName);
		if (publisher is null)
		{
			return NotFound("publisher not found");
		}

		return Ok(publisher.Publisher);
	}

	[AllowAnonymous]
	[HttpGet("{companyName}/games")]
	public async Task<ActionResult<IEnumerable<PublisherDetails>>> GetGamesByPublisher(string companyName)
	{
		return Ok((await publisherService.GetGamesByPublisherNameAsync(companyName)).Select(g => g.Game));
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpPost]
	public async Task<ActionResult> Post([FromBody] PublisherModel publisher)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await publisherService.AddAsync(publisher);
		return Ok();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpPut]
	public async Task<ActionResult> Put([FromBody] PublisherModel publisher)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await publisherService.UpdateAsync(publisher);
		return Ok();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await publisherService.DeleteAsync(id);
		return Ok();
	}
}
