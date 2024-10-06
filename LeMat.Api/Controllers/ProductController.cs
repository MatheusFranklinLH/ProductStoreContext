using LeMat.Api.Utils;
using LeMat.Domain.Handlers;
using LeMat.Domain.Repositories;
using LeMat.Domain.Requests;
using LeMat.Shared.Requests;
using LeMat.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LeMat.Api.Controllers;

[ApiController]
[Route("v1/products")]
public class ProductController : ControllerBase {

	public ProductController() { }

	[HttpGet]
	public async Task<ActionResult<Response>> GetProducts([FromServices] IProductRepository repository) {
		var response = new Response(await repository.GetAllAsync());
		return ResponseUtils.CreateResponse(response);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<Response>> GetProduct([FromServices] IProductRepository repository, int id) {
		var response = new Response(await repository.GetProductResponseByIdAsync(id));
		return ResponseUtils.CreateResponse(response);
	}

	[HttpPost]
	public async Task<ActionResult<Response>> CreateProduct(
		[FromForm] CreateProductRequest request,
		[FromServices] ProductHandler handler
	) {
		var response = (Response)await handler.Handle(request);
		return ResponseUtils.CreateResponse(response);
	}

	[HttpPut]
	public async Task<ActionResult<Response>> UpdateProduct(
		[FromBody] UpdateProductRequest request,
		[FromServices] ProductHandler handler
	) {
		var response = (Response)await handler.Handle(request);
		return ResponseUtils.CreateResponse(response);
	}

	[HttpDelete]
	public async Task<ActionResult<Response>> DeleteProduct(
		[FromBody] DeleteIdRequest request,
		[FromServices] ProductHandler handler
	) {
		var response = (Response)await handler.Handle(request);
		return ResponseUtils.CreateResponse(response);
	}

	[HttpGet("edit/form")]
	public async Task<ActionResult<Response>> GetProductEditFormInfo([FromServices] IProductRepository repository) {
		var response = new Response(await repository.GetProductEditFormInfoAsync());
		return ResponseUtils.CreateResponse(response);
	}

	[HttpGet("create/form")]
	public async Task<ActionResult<Response>> GetProductCreateFormInfo([FromServices] IProductRepository repository) {
		var response = new Response(await repository.GetProductCreateFormInfoAsync());
		return ResponseUtils.CreateResponse(response);
	}


}
