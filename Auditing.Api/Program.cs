using Auditing.Infrastructure.Persistence;
using Auditing.Infrastructure.Repositories;
using Auditing.Application.Services;
using Auditing.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Conexión a SQL Server
builder.Services.AddDbContext<AuditingDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IFindingRepository, FindingRepository>();

// Servicios de aplicación
builder.Services.AddScoped<AuditAppService>();
builder.Services.AddScoped<OwnerAppService>();
builder.Services.AddScoped<FindingAppService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt => opt.AddPolicy("WebPolicy", p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


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
