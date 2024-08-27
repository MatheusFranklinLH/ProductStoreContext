using Microsoft.AspNetCore.Mvc;
using StoreProductsContext.Domain.Commands;
using StoreProductsContext.Domain.Entities;
using StoreProductsContext.Domain.Handlers;
using StoreProductsContext.Domain.Repositories;
using StoreProductsContext.Shared.Commands;

namespace StoreProductsContext.Api.Controllers;

[ApiController]
[Route("v1/supplier")]
public class SupplierController : ControllerBase {

	public SupplierController() { }

	[HttpGet]
	public IEnumerable<Supplier> Get([FromServices] ISupplierRepository repository) {
		return repository.GetAll();
	}

	[HttpPost]
	public GenericCommandResult Create(
		[FromBody] CreateSupplierCommand command,
		[FromServices] SupplierHandler handler
	) {
		return (GenericCommandResult)handler.Handle(command);
	}
}
