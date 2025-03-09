using HibaVonal.DataContext;
using Microsoft.EntityFrameworkCore;
using HibaVonal.Services.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//én adtam hozzá eddig: builder.Services.AddControllers(); volt csak
//Melyik megoldás? Az egyik megoldja az összes recursív lekérdezést, a másik, hogy az entityben [JsonIgnore] használata 

/*builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetpincerApp API", Version = "v1" });
});*/
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});
builder.Services.AddDbContext<SQL>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));
byte[] JWTKey = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
string? Audience = builder.Configuration["JWT:Audience"];
string? Issuer = builder.Configuration["JWT:Issuer"];
builder.Services.AddScoped<TokenHandlerService>(sp => new TokenHandlerService(sp.GetRequiredService<SQL>(), JWTKey, Issuer, Audience));
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IDormitoryService, DormitoryService>();
builder.Services.AddScoped<IErrorTypeService, ErrorTypeService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(JWTKey)
        };
    });

builder.Services.AddAuthorization(a =>
{
    a.AddPolicy("Admin", b =>
    {
        b.RequireClaim(ClaimTypes.Role, "1");
    });
    a.AddPolicy("MaintenanceManager", b =>
    {
        b.RequireClaim(ClaimTypes.Role, "2", "1");
    });
    a.AddPolicy("MaintenanceWorker", b =>
    {
        b.RequireClaim(ClaimTypes.Role, "3", "2", "1");
    });
    a.AddPolicy("User", b =>
    {
        b.RequireClaim(ClaimTypes.Role, "4", "3", "2", "1");
    });
});



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
