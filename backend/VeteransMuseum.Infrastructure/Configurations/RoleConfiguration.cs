using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Role = VeteransMuseum.Domain.Users.Role;

namespace VeteransMuseum.Infrastructure.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
 
        builder.HasKey(role => role.Id);
 
        builder.HasMany(role => role.Users)
            .WithMany(user => user.Roles);
 
        builder.HasData(Role.Admin);
    }
}