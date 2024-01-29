using StoreProductsContext.Shared.Commands;

namespace StoreProductsContext.Shared.Handlers;

public interface IHandler<T> where T : ICommand {
	ICommandResult Handle(T command);
}