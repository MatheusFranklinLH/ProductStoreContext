using LeMat.Domain.Handlers;
using LeMat.Domain.Repositories;
using LeMat.Domain.Requests;
using LeMat.Shared.Requests;
using LeMat.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LeMat.Api.Controllers;

[ApiController]
[Route("v1/product")]
public class ProductController : ControllerBase {

	public ProductController() { }

	[HttpGet]
	public async Task<IActionResult> Get([FromServices] IProductRepository repository, int? id) {
		if (id is null) {
			var idResponse = new Response(await repository.GetAllAsync());
			if (idResponse.IsSuccess) return Ok(idResponse);
			return BadRequest(idResponse);
		}
		var response = new Response(await repository.GetByIdAsync(id.Value));
		if (response.IsSuccess) return Ok(response);
		return BadRequest(response);
	}

	[HttpPost]
	public async Task<IActionResult> Create(
		[FromBody] CreateProductRequest request,
		[FromServices] ProductHandler handler
	) {
		var response = (Response)await handler.Handle(request);
		if (response.IsSuccess) return Ok(response);
		return BadRequest(response);
	}

	[HttpPut]
	public async Task<IActionResult> Update(
		[FromBody] UpdateProductRequest request,
		[FromServices] ProductHandler handler
	) {
		var response = (Response)await handler.Handle(request);
		if (response.IsSuccess) return Ok(response);
		return BadRequest(response);
	}

	[HttpDelete]
	public async Task<IActionResult> Delete(
		[FromBody] DeleteIdRequest request,
		[FromServices] ProductHandler handler
	) {
		var response = (Response)await handler.Handle(request);
		if (response.IsSuccess) return Ok(response);
		return BadRequest(response);
	}

	[HttpPost("images")]
	public async Task<IActionResult> UploadImages(
		[FromForm] UpdateProductImagesRequest request,
		[FromServices] ProductHandler handler
	) {
		var response = (Response)await handler.Handle(request);
		if (response.IsSuccess) return Ok(response);
		return BadRequest(response);
	}

}
