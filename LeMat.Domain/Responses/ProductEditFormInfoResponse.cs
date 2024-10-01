using LeMat.Domain.DTOs;

namespace LeMat.Domain.Responses;

public record ProductEditFormInfoResponse(
	List<NameId> Products,
	List<NameId> Suppliers
);