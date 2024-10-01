using LeMat.Domain.DTOs;

namespace LeMat.Domain.Responses;

public record ProductCreateFormInfoResponse(
	List<NameId> Suppliers
);