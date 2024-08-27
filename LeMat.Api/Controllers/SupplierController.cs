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
	public async Task<ICommandResult> Get([FromServices] ISupplierRepository repository, int? id) {
		if (id is null)
			return new CommandResult(await repository.GetAllAsync());
		return new CommandResult(await repository.GetByIdAsync(id.Value));
	}

	[HttpPost]
	public async Task<ICommandResult> Create(
		[FromBody] CreateSupplierCommand command,
		[FromServices] SupplierHandler handler
	) {
		return (CommandResult)await handler.Handle(command);
	}

	[HttpPut]
	public async Task<ICommandResult> Update(
		[FromBody] UpdateSupplierCommand command,
		[FromServices] SupplierHandler handler
	) {
		return (CommandResult)await handler.Handle(command);
	}

	[HttpDelete]
	public async Task<ICommandResult> Delete(
		[FromBody] DeleteIdCommand command,
		[FromServices] SupplierHandler handler
	) {
		return (CommandResult)await handler.Handle(command);
	}
}
