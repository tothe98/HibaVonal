using HibaVonal.DataContext;
using Microsoft.EntityFrameworkCore;
using HibaVonal.Services.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using HibaVonal.Services.Security;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//�n adtam hozz� eddig: builder.Services.AddControllers(); volt csak
//Melyik megold�s? Az egyik megoldja az �sszes recurs�v lek�rdez�st, a m�sik, hogy az entityben [JsonIgnore] haszn�lata

/*builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});*/

// Swagger
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


byte[] JWTKey = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
string? Audience = builder.Configuration["JWT:Audience"];
string? Issuer = builder.Configuration["JWT:Issuer"];
builder.Services.AddScoped<TokenHandlerService>(sp => new TokenHandlerService(sp.GetRequiredService<SQL>(), JWTKey, Issuer, Audience));


// Database connection
builder.Services.AddDbContext<SQL>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

// Local services
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddDbContext<SQL>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IDormitoryService, DormitoryService>();
builder.Services.AddScoped<IErrorTypeService, ErrorTypeService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IErrorLogService, ErrorLogService>();


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

/*
 Elnevez�s              ID

 Admin                  1
 MaintenanceManager     2
 MaintenanceWorker      3
 User                   4
 */


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

// Allow frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HibavonalApp API v1"));

}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
