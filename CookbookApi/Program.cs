using System.Reflection;
using AutoMapper;
using CookbookApi.Data;
using Microsoft.OpenApi.Models;
using CookbookApi.Dtos;
using CookbookApi.Interfaces;
using CookbookApi.Models;
using CookbookApi.Repositories;
using CookbookApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEntityFrameworkSqlite().AddDbContext<CookbookDbContext>();

var mapperConfig = new MapperConfiguration(config =>
{
    config.CreateMap<Recipe, RecipeDto>();
    config.CreateMap<RecipeDto, Recipe>();
    config.CreateMap<Ingredient, IngredientDto>();
    config.CreateMap<IngredientDto, Ingredient>();
});
IMapper mapper = new Mapper(mapperConfig);
builder.Services.AddSingleton(mapper);

// Added as singleton so that recipe list is shared between http requests.
builder.Services.AddSingleton<RecipeListDatabase>();
builder.Services.AddSingleton<IRepositoryList<Recipe>, RecipeListRepositoryList>();
builder.Services.AddSingleton<IRecipeListService, RecipeListService>();

builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddTransient<IRepository<Recipe>, RecipeRepository>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Cookbook API",
        Description = "A sample ASP.NET Core Web API to create a collection of delicious recipes",
        Contact = new OpenApiContact()
        {
            Name = "Siva Karthik Narisetty",
            Email = "siva.narisetty@gmail.com",
            Url = new Uri("https://github.com/karthiksiva555")
        },
        Version = "v1"
    });
    
    // Generate xml docs for Swagger documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();