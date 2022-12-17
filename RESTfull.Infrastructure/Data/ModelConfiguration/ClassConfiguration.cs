using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTfull.Domain.Model;

namespace RESTfull.Infrastructure.Data.ModelConfiguration;
public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder
            .Property(class_ => class_.Subject)
                .HasMaxLength(127);
        builder
            .Property(class_ => class_.Group)
                .HasMaxLength(15);
        builder
            .Property(class_ => class_.Start)
                .HasColumnType("datetime2");
        builder
            .HasIndex(class_ => new { class_.TeacherId, class_.Start })
                .IsUnique();
        builder
            .HasIndex(class_ => new { class_.Group, class_.Start })
                .IsUnique();
    }
}
