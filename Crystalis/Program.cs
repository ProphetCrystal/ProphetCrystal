using System.Text;
using Crystalis.Configuration;
using Crystalis.Contexts;
using Crystalis.Models;
using Crystalis.Repositories;
using Crystalis.Repositories.Interfaces;
using Crystalis.Services;
using Crystalis.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
string? connectionString = builder.Configuration.GetConnectionString("CrystalisDatabase");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
//Automapper
builder.Services.AddAutoMapper(typeof(Program));
//Services
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<ITokenService, TokenService>();
//Repositories
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();

//Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSection>();
if(jwtSettings == null) throw new NullReferenceException("JWT settings not found");
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.ValidIssuer,
            ValidAudience = jwtSettings.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
    using (DataContext? context = scope.ServiceProvider.GetService<DataContext>()) {
        // context?.Database.EnsureDeleted();
        context?.Database.EnsureCreated();
        
        context.Roles.Add(new IdentityRole("Admin"));
        context.Roles.Add(new IdentityRole("GameMaster"));
        context.Roles.Add(new IdentityRole("Player"));
        context.SaveChanges();
    }
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();