using Microsoft.AspNetCore.Mvc;
using LeMat.Domain.Commands;
using LeMat.Domain.Entities;
using LeMat.Domain.Handlers;
using LeMat.Domain.Repositories;
using LeMat.Shared.Commands;

namespace LeMat.Api.Controllers;

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
