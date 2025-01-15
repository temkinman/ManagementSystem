using BuildingBlocks.Behaviors;
using BuildingBlocks.Middlewares;
using Catalog.Api.Mapping;
using Catalog.Application;
using Catalog.Application.Catalogs.Commands.CreateProduct;
using Catalog.Infrastructure;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>(ServiceLifetime.Transient);

builder.Services.AddAutoMapper(typeof(CatalogRequestProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
