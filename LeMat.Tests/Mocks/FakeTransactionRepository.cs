using LeMat.Domain.Entities;
using LeMat.Domain.Enums;
using LeMat.Domain.Repositories;
using LeMat.Domain.ValueObjects;

namespace LeMat.Tests.Mocks;

public class FakeTransactionRepository : ITransactionRepository {
	public Task BeginTransactionAsync() {
		return Task.Delay(50);
	}

	public Task CommitAsync() {
		return Task.Delay(50);
	}

	public void Dispose() {
		GC.SuppressFinalize(this);
	}

	public Task RollbackAsync() {
		return Task.Delay(50);
	}
}