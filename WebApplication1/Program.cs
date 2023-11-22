using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApplication1.Middleware;
using BusinessLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Configure Swagger to accept a static token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter the API key as follows: Bearer YourHardcodedToken",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// register DBContext:
var sqliteDbName = "bookhubproject2.db";

var folder = Environment.SpecialFolder.LocalApplicationData;
var dbPath = Path.Join(Environment.GetFolderPath(folder), sqliteDbName);

var sqliteConnectionString = $"Data Source={dbPath}";

builder.Services.AddDbContextFactory<BookHubDBContext>(options =>
{
    options
       .UseSqlite(sqliteConnectionString)
       ;
    
});

/* Register Services */
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// be careful about the order of the following middleware configuration.
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseMiddleware<StaticTokenAuthenticationMiddleware>();

app.UseMiddleware<FormatTransformMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
