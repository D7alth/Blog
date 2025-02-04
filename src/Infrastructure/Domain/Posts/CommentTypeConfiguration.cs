using Blog.Domain.Posts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Domain.Posts;

internal sealed class CommentTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.PostId).HasColumnName("post_id");
        builder.Property(e => e.Content).HasColumnName("content");
        builder
            .HasMany<Comment>(e => e.Comments)
            .WithOne()
            .HasForeignKey("ParentCommentId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
