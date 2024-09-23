using LeMat.Domain.Entities;
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
	public async Task<IResponse> Get([FromServices] IProductRepository repository, int? id) {
		if (id is null)
			return new Response(await repository.GetAllAsync());
		return new Response(await repository.GetByIdAsync(id.Value));
	}

	[HttpPost]
	public async Task<IResponse> Create(
		[FromBody] CreateProductRequest request,
		[FromServices] ProductHandler handler
	) {
		return (Response)await handler.Handle(request);
	}

	[HttpPut]
	public async Task<IResponse> Update(
		[FromBody] UpdateProductRequest request,
		[FromServices] ProductHandler handler
	) {
		return (Response)await handler.Handle(request);
	}

	[HttpDelete]
	public async Task<IResponse> Delete(
		[FromBody] DeleteIdRequest request,
		[FromServices] ProductHandler handler
	) {
		return (Response)await handler.Handle(request);
	}
}
