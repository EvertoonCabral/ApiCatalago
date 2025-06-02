using ApiCatalago.Context;
using ApiCatalago.Filters;
using ApiCatalago.Logging;
using ApiCatalago.Repositories;
using APICatalogo.Logging;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Carrega a connection string antes de usar
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registra o AppDbContext ANTES do builder.Build()
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseLazyLoadingProxies().UseSqlServer(connectionString);
});

builder.Services.AddControllers(options =>
{ 
    options.Filters.Add(typeof(ApiExceptionFilter));
}).
AddJsonOptions(options => options.JsonSerializerOptions
    .ReferenceHandler = ReferenceHandler.IgnoreCycles)
.AddNewtonsoftJson();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler(options =>
{
    options.ExceptionHandler = async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new
        {
            Status = context.Response.StatusCode,
            Message = "Ocorreu um erro interno no servidor"
        });
    };
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddEntityFrameworkStores<AppDbContext>().
    AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfig
{
    LogLevel = LogLevel.Information
}));

var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
