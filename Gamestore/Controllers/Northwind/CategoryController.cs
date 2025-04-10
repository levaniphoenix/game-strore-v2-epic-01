using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Gamestore.Controllers.Northwind;

[Route("Northwind/[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CategoryModel>>> Get()
	{
		var categories = await categoryService.GetAllAsync();
		return Ok(categories);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<CategoryModel?>> Get(string id)
	{
		if (!ObjectId.TryParse(id, out var _))
		{
			return BadRequest("Invalid ID format");
		}

		var category = await categoryService.GetByIdAsync(id);
		if (category is null)
		{
			return NotFound("Category not found");
		}

		return Ok(category);
	}

	[HttpPost]
	public async Task<ActionResult> Post([FromBody] CategoryModel category)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await categoryService.AddAsync(category);
		return Ok();
	}

	[HttpPut]
	public async Task<ActionResult> Put([FromBody] CategoryModel category)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await categoryService.UpdateAsync(category);
		return Ok();
	}
}
