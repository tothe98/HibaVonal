using HibaVonal.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HibaVonal.Services.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//én adtam hozzá eddig: builder.Services.AddControllers(); volt csak
//Melyik megoldás? Az egyik megoldja az összes recursív lekérdezést, a másik, hogy az entityben [JsonIgnore] használata

/*builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});*/

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetpincerApp API", Version = "v1" });
});

// Database Connection
builder.Services.AddDbContext<SQL>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

// Local services
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IDormitoryService, DormitoryService>();
builder.Services.AddScoped<IErrorTypeService, ErrorTypeService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRoomService, RoomService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HibavonalApp API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
