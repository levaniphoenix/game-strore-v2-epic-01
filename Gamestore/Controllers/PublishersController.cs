using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.PublisherModel;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class PublishersController(IPublisherService publisherService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<PublisherDetails>>> Get()
	{
		return Ok((await publisherService.GetAllAsync()).Select(p => p.Publisher));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<PublisherDetails?>> Get(Guid id)
	{
		var publisher = await publisherService.GetByIdAsync(id);
		if (publisher is null)
		{
			return NotFound("publisher not found");
		}

		return Ok(publisher.Publisher);
	}

	[HttpGet("{companyName}/games")]
	public async Task<ActionResult<IEnumerable<PublisherDetails>>> GetGamesByPublisher(string companyName)
	{
		return Ok((await publisherService.GetGamesByPublisherNameAsync(companyName)).Select(g => g.Game));
	}

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

	[HttpDelete]
	public async Task<ActionResult> Delete(Guid id)
	{
		await publisherService.DeleteAsync(id);
		return Ok();
	}
}
