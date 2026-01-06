using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi;
using GarageClickerBackend.Data;
using GarageClickerBackend.Services;
using GarageClickerBackend.Services.Implementation;
using GarageClickerBackend.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key");
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

builder.Services.AddAuthorization(); 


builder.Services.AddDbContext<GarageClickerDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "GarageClickerBackend", Version = "v1" });
}); 
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<GarageService>();
builder.Services.AddTransient<IPlayerItemsService, PlayerItemService>();


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("FrontendPolicy");
app.UseAuthentication();    

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();