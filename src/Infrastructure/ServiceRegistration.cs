using Blog.Application.Articles.Commands.CreateArticle;
using Blog.Application.Articles.Services;
using Blog.Domain.Articles.Repositories;
using Blog.Infrastructure.Application.Articles;
using Blog.Infrastructure.Configuration;
using Blog.Infrastructure.Configuration.Providers;
using Blog.Infrastructure.Domain.Articles.Repository;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.UnitOfWork;
using FluentValidation;
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
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ITextProcessor, TextProcessor>();
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationContext>>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(CreateArticleCommandHandler).Assembly)
        );
        services.AddValidatorsFromAssembly(typeof(CreateArticleCommand).Assembly);
        services.Decorate(typeof(IRequestHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
    }
}
