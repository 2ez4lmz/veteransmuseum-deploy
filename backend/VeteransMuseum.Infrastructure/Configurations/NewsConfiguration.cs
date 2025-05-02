using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeteransMuseum.Domain.News;
using VeteransMuseum.Domain.Shared;

namespace VeteransMuseum.Infrastructure.Configurations;

public sealed class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.ToTable("news");

        builder.HasKey(news => news.Id);

        builder.Property(news => news.Title)
            .IsRequired()
            .HasMaxLength(200)
            .HasConversion(title => title.Value, value => new Title(value));
        
        builder.Property(news => news.Content)
            .HasMaxLength(5000)
            .HasConversion(content => content.Value, value => new Content(value));

        builder.Property(news => news.ImageUrl)
            .HasMaxLength(1000)
            .HasConversion(imageUrl => imageUrl.Value, value => new ImageUrl(value));
    }
}