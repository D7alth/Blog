using Blog.Domain.Articles;
using Blog.Domain.Articles.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Domain.Articles;

internal sealed class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("articles");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).HasColumnName("title").HasMaxLength(60);
        builder.Property(e => e.Content).HasColumnName("Content");
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        builder
            .Property(e => e.Tags)
            .HasColumnType("text")
            .HasColumnName("tags")
            .HasConversion(
                t => string.Join(",", t.Select(tagName => tagName.Name)),
                names =>
                    names
                        .Split(',', StringSplitOptions.TrimEntries)
                        .Select(name => Tag.Create(name))
                        .ToList()
            );
    }
}
