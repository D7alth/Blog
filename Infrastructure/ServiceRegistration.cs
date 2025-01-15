using Blog.Domain.Entities.Posts.Repositories;
using Blog.Infrastructure.Domain.Entities.Posts;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.UnitOfWork;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationContext>>();
     
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        //    typeof(CreatePostCommandHandler).Assembly
        //));

        // Configure behaviors (decorators)
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionActionProcessorBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkCommandHandlerDecorator<>));

        return services;
    }
}