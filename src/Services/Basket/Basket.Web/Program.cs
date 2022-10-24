using AutoMapper;
using Basket.Repo;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OLopatskyi.ErrorsHandler;
using OLopatskyi.ErrorsHandler.MapperProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
builder.Services.AddTransient<IBasketRepository, BasketRepository>();
builder.Services.AddSingleton<ExceptionHandlerMiddleware>();

//Configure fluent validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//Configure AutoMapper
builder.Services.AddSingleton(provider =>
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddMaps(typeof(ErrorMapperProfile).Assembly);
        cfg.ConstructServicesUsing(type => ActivatorUtilities.CreateInstance(provider, type));
    });
    config.AssertConfigurationIsValid();
    return config.CreateMapper();
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var errors = new InvalidModelStateHandler(context).Errors;
        return new BadRequestObjectResult(errors);
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
