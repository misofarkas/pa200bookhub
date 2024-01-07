using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApplication1.Middleware;
using BusinessLayer.Services;
using BusinessLayer.Services.Author;
using Mapster;
using DataAccessLayer.Models;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;
using BusinessLayer.DTOs;

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
        .UseSqlite(
            sqliteConnectionString,
            x => x.MigrationsAssembly("DAL.SQLite.Migrations")
        );
});

/* Register Services */
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService,  AuthorService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

TypeAdapterConfig<Author, AuthorDTO>.NewConfig().Map(dest => dest.Books, src => src.AuthorBooks.Select(ab => ab.Book.Adapt<BookDTO>()));
TypeAdapterConfig<Book, BookDTO>.NewConfig().Map(dest => dest.Authors, src => src.AuthorBooks.Select(ab => ab.Author.Adapt<AuthorWithoutBooksDTO>()))
                                            .Map(dest => dest.Genres, src => src.GenreBooks.Select(gb => gb.Genre.Adapt<GenreDTO>()));
TypeAdapterConfig<Customer, CustomerDTO>.NewConfig().Map(dest => dest.Username, src => src.Username);


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
