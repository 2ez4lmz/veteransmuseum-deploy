using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeteransMuseum.Domain.Shared;
using VeteransMuseum.Domain.Veterans;

namespace VeteransMuseum.Infrastructure.Configurations;

public sealed class VeteranConfiguration : IEntityTypeConfiguration<Veteran>
{
    public void Configure(EntityTypeBuilder<Veteran> builder)
    {
        builder.ToTable("veterans");

        builder.HasKey(veteran => veteran.Id);

        builder.Property(veteran => veteran.FirstName)
            .HasMaxLength(200)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value));

        builder.Property(veteran => veteran.LastName)
            .HasMaxLength(200)
            .HasConversion(lastName => lastName.Value, value => new LastName(value));

        builder.Property(veteran => veteran.MiddleName)
            .HasMaxLength(200)
            .HasConversion(middleName => middleName.Value, value => new MiddleName(value));
        
        builder.Property(veteran => veteran.Biography)
            .HasMaxLength(5000)
            .HasConversion(biography => biography.Value, value => new Biography(value));
        
        builder.Property(veteran => veteran.Rank)
            .HasMaxLength(200)
            .HasConversion(rank => rank.Value, value => new Rank(value));
        
        builder.Property(veteran => veteran.Awards)
            .HasMaxLength(500)
            .HasConversion(awards => awards.Value, value => new Award(value));
        
        builder.Property(veteran => veteran.MilitaryUnit)
            .HasMaxLength(200)
            .HasConversion(militaryUnit => militaryUnit.Value, value => new MilitaryUnit(value));
        
        builder.Property(veteran => veteran.MilitaryUnit)
            .HasMaxLength(200)
            .HasConversion(militaryUnit => militaryUnit.Value, value => new MilitaryUnit(value));
        
        builder.Property(veteran => veteran.Battles)
            .HasMaxLength(2000)
            .HasConversion(battle => battle.Value, value => new Battle(value));
        
        builder.Property(veteran => veteran.ImageUrl)
            .HasMaxLength(1000)
            .HasConversion(imageUrl => imageUrl.Value, value => new ImageUrl(value));
    }
}