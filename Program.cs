using ApiVendasApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using vendasApi.Repositories.produtoRepository;
using vendasApi.Repositories;
using vendasApi.Repositories.unitOfWork;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var ConString = builder.Configuration["ConnectionStrings:APIConnection"];

builder.Services.AddDbContext<ApiVendasContext>(opts=>
opts.UseLazyLoadingProxies().UseMySql(ConString,ServerVersion.AutoDetect(ConString)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opts=>opts.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VendasApi", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
