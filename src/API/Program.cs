using Blog.AccountContext.Infrastructure;
using Blog.PostContext.Infrastructure;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();

ServiceRegistration.AddInfrastructure(builder.Services, builder.Configuration);
AccountContextModule.AddInfrastructure(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();
app.Run();
