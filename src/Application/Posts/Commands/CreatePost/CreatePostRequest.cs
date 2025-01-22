using MediatR;

namespace Blog.Application.Posts.Commands.CreatePost;

public sealed record CreatePostRequest(string Title, string Content) : IRequest;
