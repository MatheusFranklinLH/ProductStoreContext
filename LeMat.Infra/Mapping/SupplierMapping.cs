using LeMat.Domain.Entities;
using LeMat.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeMat.Infra.Mappings;

public class SupplierMapping : IEntityTypeConfiguration<Supplier> {
	public void Configure(EntityTypeBuilder<Supplier> builder) {
		builder.ToTable("suppliers", "lemat");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasColumnName("id");

		builder.Property(x => x.Name)
			.IsRequired(true)
			.HasColumnType("VARCHAR(100)")
			.HasColumnName("name");

		builder.Property(x => x.CompanyReason)
			.HasColumnType("VARCHAR(255)")
			.HasColumnName("company_reason");

		builder.OwnsOne(x => x.Email, email => {
			email.Property(n => n.Address).HasColumnName("email").IsRequired(true);
			email.Ignore(n => n.Notifications);
		});

		builder.OwnsOne(x => x.Telephone, telephone => {
			telephone.Property(n => n.Number).HasColumnName("telephone").IsRequired(true);
			telephone.Ignore(n => n.Notifications);
		});

		builder.OwnsOne(x => x.Document, document => {
			document.Property(n => n.Number).HasColumnName("document").IsRequired(true);
			document.Property(n => n.Type)
				.HasConversion(
					v => (int)v,
					v => (EDocumentType)v)
				.HasColumnName("document_type")
				.HasDefaultValue(EDocumentType.CPF)
				.IsRequired(true);
			document.Ignore(n => n.Notifications);
		});

		builder.OwnsOne(x => x.Address, address => {
			address.Property(n => n.Street).HasColumnName("street").IsRequired(true);
			address.Property(n => n.Number).HasColumnName("number");
			address.Property(n => n.Neighborhood).HasColumnName("neighborhood");
			address.Property(n => n.City).HasColumnName("city");
			address.Property(n => n.State).HasColumnName("state");
			address.Property(n => n.Country).HasColumnName("country");
			address.Property(n => n.ZipCode).HasColumnName("zip_code");
			address.Ignore(n => n.Notifications);
		});

		builder.Ignore(x => x.Notifications);
	}
}