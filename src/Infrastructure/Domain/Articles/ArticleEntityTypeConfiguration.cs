using Blog.Domain.Articles;
using Blog.Domain.Articles.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Domain.Articles;

internal sealed class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("articles");
        builder.HasKey(e => e.Id);
        builder
            .HasMany(a => a.Tags)
            .WithMany(t => t.Articles)
            .UsingEntity(
                "ArticleTags",
                l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagId"),
                r => r.HasOne(typeof(Article)).WithMany().HasForeignKey("ArticleId"),
                j =>
                {
                    j.HasKey("ArticleId", "TagId");
                    j.ToTable("article_tags");
                }
            );
        builder.Property(e => e.Title).HasColumnName("title").HasMaxLength(60);
        builder.Property(e => e.Content).HasColumnName("Content");
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
    }
}
