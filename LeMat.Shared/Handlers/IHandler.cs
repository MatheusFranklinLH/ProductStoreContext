using LeMat.Shared.Commands;

namespace LeMat.Shared.Handlers;

public interface IHandler<T> where T : ICommand {
	Task<ICommandResult> Handle(T command);
}