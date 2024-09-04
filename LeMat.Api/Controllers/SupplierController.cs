using LeMat.Domain.Entities;
using LeMat.Domain.Handlers;
using LeMat.Domain.Repositories;
using LeMat.Domain.Requests;
using LeMat.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LeMat.Api.Controllers;

[ApiController]
[Route("v1/supplier")]
public class SupplierController : ControllerBase {

	public SupplierController() { }

	[HttpGet]
	public async Task<IResponse> Get([FromServices] ISupplierRepository repository, int? id) {
		if (id is null)
			return new Response(await repository.GetAllAsync());
		return new Response(await repository.GetByIdAsync(id.Value));
	}

	[HttpPost]
	public async Task<IResponse> Create(
		[FromBody] CreateSupplierRequest Request,
		[FromServices] SupplierHandler handler
	) {
		return (Response)await handler.Handle(Request);
	}

	[HttpPut]
	public async Task<IResponse> Update(
		[FromBody] UpdateSupplierRequest Request,
		[FromServices] SupplierHandler handler
	) {
		return (Response)await handler.Handle(Request);
	}

	[HttpDelete]
	public async Task<IResponse> Delete(
		[FromBody] DeleteIdRequest Request,
		[FromServices] SupplierHandler handler
	) {
		return (Response)await handler.Handle(Request);
	}
}
