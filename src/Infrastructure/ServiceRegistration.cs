using Blog.Application.Posts.Commands.CreatePost;
using Blog.Application.Posts.Services;
using Blog.Domain.Posts.Repositories;
using Blog.Infrastructure.Application.Posts.Services;
using Blog.Infrastructure.Configuration;
using Blog.Infrastructure.Configuration.Providers;
using Blog.Infrastructure.Domain.Entities.Posts.Repository;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IDbContextOptionsProvider>(provider => new SqlServerDbContextProvider(
            configuration.GetConnectionString("DefaultConnection")!
        ));
        services.AddScoped(provider =>
        {
            var dbContextOptionsProvider = provider.GetRequiredService<IDbContextOptionsProvider>();
            return new ApplicationContext(dbContextOptionsProvider);
        });
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ITextProcessor, TextProcessor>();
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationContext>>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(CreatePostRequestHandler).Assembly)
        );
        services.Decorate(typeof(IRequestHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
    }
}
