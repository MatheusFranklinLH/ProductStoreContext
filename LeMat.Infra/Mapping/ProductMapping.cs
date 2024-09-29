using LeMat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeMat.Infra.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product> {
	public void Configure(EntityTypeBuilder<Product> builder) {
		builder.ToTable("products", "lemat");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasColumnName("id");

		builder.Property(x => x.CreatedAt)
			.HasColumnName("created_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(x => x.ModifiedAt)
			.HasColumnName("modified_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(x => x.Name)
			.IsRequired(true)
			.HasColumnType("VARCHAR(100)")
			.HasColumnName("name");

		builder.Property(x => x.SuggestedSellPrice)
			.HasColumnName("suggested_sell_price");

		builder.Property(x => x.MaximumDiscountPercentage)
			.HasColumnName("maximum_discount_percentage");

		builder.Property(x => x.SupplierId)
			.HasColumnName("supplier_id");

		builder.Property(x => x.StockAvailable)
			.HasColumnName("stock_available");

		builder.HasOne(x => x.Supplier)
			.WithMany(s => s.Products)
			.HasForeignKey(x => x.SupplierId);

		builder.Ignore(x => x.Notifications);
	}
}