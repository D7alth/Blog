using Blog.Domain.Posts;
using Blog.Domain.Posts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Domain.Posts;

internal sealed class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).HasColumnName("title").HasMaxLength(60);
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
            )
            .Metadata.SetValueComparer(
                new ValueComparer<List<Tag>>(
                    (a1, a2) => a1!.SequenceEqual(a2!),
                    a =>
                        a.Aggregate(
                            0,
                            (hash, item) => HashCode.Combine(hash, item.Name.GetHashCode())
                        ),
                    a => a.ToList()
                )
            );
    }
}
