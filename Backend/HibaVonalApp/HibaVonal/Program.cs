using HibaVonal.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HibaVonal.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetpincerApp API", Version = "v1" });
});
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SQL>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IDormitoryService, DormitoryService>();
builder.Services.AddScoped<IErrorTypeService, ErrorTypeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HibavonalApp API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
