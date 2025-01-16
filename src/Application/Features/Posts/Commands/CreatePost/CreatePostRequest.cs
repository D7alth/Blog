using MediatR;

namespace Blog.Application.Features.Posts.Commands.CreatePost;

public sealed record CreatePostRequest(string Title, string Content) : IRequest;
