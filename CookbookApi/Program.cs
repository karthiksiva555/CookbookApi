using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
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