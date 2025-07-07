

using Blog.PostContext.Application.Articles.Commands.CreateArticle;
using Blog.PostContext.Application.Articles.Services;
using Blog.PostContext.Domain.Articles.Repositories;
using Blog.PostContext.Infrastructure.Application.Articles;
using Blog.PostContext.Infrastructure.Configuration;
using Blog.PostContext.Infrastructure.Configuration.Providers;
using Blog.PostContext.Infrastructure.Domain.Articles.Repository;
using Blog.PostContext.Infrastructure.Persistence;
using Blog.PostContext.Infrastructure.UnitOfWork;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.PostContext.Infrastructure;

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
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(CreateArticleCommandHandler).Assembly)
        );
        services.AddValidatorsFromAssembly(typeof(CreateArticleCommand).Assembly);
        services.Decorate(typeof(IRequestHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
    }
}
