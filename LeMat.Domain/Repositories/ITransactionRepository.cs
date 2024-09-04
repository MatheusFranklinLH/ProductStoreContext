using LeMat.Domain.Entities;
using LeMat.Domain.ValueObjects;

namespace LeMat.Domain.Repositories;

public interface ITransactionRepository : IDisposable {
	Task BeginTransactionAsync();
	Task CommitAsync();
	Task RollbackAsync();
}