using LeMat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeMat.Infra.Mappings;

public class ImageMapping : IEntityTypeConfiguration<Image> {
	public void Configure(EntityTypeBuilder<Image> builder) {
		builder.ToTable("images", "lemat");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasColumnName("id");

		builder.Property(x => x.CreatedAt)
			.HasColumnName("created_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(x => x.ModifiedAt)
			.HasColumnName("modified_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(x => x.ImagePath)
			.IsRequired(true)
			.HasColumnType("VARCHAR(60)")
			.HasColumnName("imagePath");

		builder.Property(x => x.ProductId)
			.HasColumnName("product_id");

		builder.Property(x => x.ImagePath)
			.HasColumnType("VARCHAR(100)")
			.HasColumnName("image_path");

		builder.Ignore(x => x.Notifications);
	}
}