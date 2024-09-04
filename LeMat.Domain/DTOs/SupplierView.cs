using LeMat.Domain.ValueObjects;

namespace LeMat.Domain.DTOs;

public record SupplierView(int Id, string Name, string CompanyReason, Telephone telephone, Email email, Address address, Document document);

