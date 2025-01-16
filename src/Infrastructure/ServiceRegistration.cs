using Blog.Application.Features.Posts.Commands.CreatePost;
using Blog.Domain.Entities.Posts.Repositories;
using Blog.Infrastructure.Configuration;
using Blog.Infrastructure.Configuration.Providers;
using Blog.Infrastructure.Domain.Entities.Posts.Repository;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IDbContextOptionsProvider>(provider => new SqlLiteDbContextProvider(
            configuration.GetConnectionString("DefaultConnection")!
        ));

        services.AddScoped(provider =>
        {
            var dbContextOptionsProvider = provider.GetRequiredService<IDbContextOptionsProvider>();
            return new ApplicationContext(dbContextOptionsProvider);
        });

        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationContext>>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(CreatePostRequestHandler).Assembly)
        );
        services.Decorate(typeof(IRequestHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

        // Configure behaviors (decorators)
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
        // services.AddTransient(
        //     typeof(IPipelineBehavior<,>),
        //     typeof(RequestPostProcessorBehavior<,>)
        // );
        // services.AddTransient(
        //     typeof(IPipelineBehavior<,>),
        //     typeof(RequestExceptionProcessorBehavior<,>)
        // );
        // services.AddTransient(
        //     typeof(IPipelineBehavior<,>),
        //     typeof(RequestExceptionActionProcessorBehavior<,>)
        // );
        // services.AddTransient(
        //     typeof(IPipelineBehavior<,>),
        //     typeof(UnitOfWorkCommandHandlerDecorator<>)
        // );

        return services;
    }
}
