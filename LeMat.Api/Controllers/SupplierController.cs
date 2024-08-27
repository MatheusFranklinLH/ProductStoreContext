using LeMat.Domain.Commands;
using LeMat.Domain.Entities;
using LeMat.Domain.Handlers;
using LeMat.Domain.Repositories;
using LeMat.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LeMat.Api.Controllers;

[ApiController]
[Route("v1/supplier")]
public class SupplierController : ControllerBase {

	public SupplierController() { }

	[HttpGet]
	public async Task<CommandResult> Get([FromServices] ISupplierRepository repository) {
		List<Supplier> suppliers = await repository.GetAllAsync();
		return new CommandResult(suppliers);
	}

	[HttpPost]
	public async Task<CommandResult> Create(
		[FromBody] CreateSupplierCommand command,
		[FromServices] SupplierHandler handler
	) {
		return (CommandResult)await handler.Handle(command);
	}
}
