using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using static Business.Models.Northwind.ProductModel;

namespace Gamestore.Controllers.Northwind;

[Route("Northwind/[controller]")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ProductDetails>>> Get()
	{
		var products = (await productService.GetAllAsync()).Select(x => x.Product);
		return Ok(products);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ProductModel?>> Get(string id)
	{
		if (!ObjectId.TryParse(id, out var _))
		{
			return BadRequest("Invalid ID format");
		}

		var product = await productService.GetByIdAsync(id);
		if (product is null)
		{
			return NotFound("Product not found");
		}

		return Ok(product);
	}

	[HttpPost]
	public async Task<ActionResult> Post([FromBody] ProductModel product)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await productService.AddAsync(product);
		return Ok();
	}

	[HttpPut]
	public async Task<ActionResult> Put([FromBody] ProductModel product)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await productService.UpdateAsync(product);
		return Ok();
	}
}
