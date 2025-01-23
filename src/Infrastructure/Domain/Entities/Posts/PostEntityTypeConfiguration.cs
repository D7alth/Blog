using Blog.Domain.Posts;
using Blog.Domain.Posts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Blog.Infrastructure.Domain.Entities.Posts;

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
                t => JsonConvert.SerializeObject(t.Select(tag => tag.Name).ToList()),
                t =>
                    JsonConvert
                        .DeserializeObject<List<Tag>>(t)!
                        .Select(tag => Tag.Create(tag.Name))
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
