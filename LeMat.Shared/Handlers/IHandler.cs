using LeMat.Shared.Requests;
using LeMat.Shared.Responses;

namespace LeMat.Shared.Handlers;

public interface IHandler<T> where T : IRequest {
	Task<IResponse> Handle(T request);
}