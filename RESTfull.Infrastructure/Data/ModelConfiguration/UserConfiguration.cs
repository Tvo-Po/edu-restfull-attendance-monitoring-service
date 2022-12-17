using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RESTfull.Domain.Model;

namespace RESTfull.Infrastructure.Data.ModelConfiguration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(user => user.FIO)
                .HasMaxLength(255);
        builder
            .Property(user => user.Email)
                .HasMaxLength(127);
    }
}
