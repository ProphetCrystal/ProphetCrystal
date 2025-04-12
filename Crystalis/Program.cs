using System.Reflection;
using System.Text;
using Crystalis.Authorization;
using Crystalis.Configuration;
using Crystalis.Contexts;
using Crystalis.Models;
using Crystalis.Repositories;
using Crystalis.Repositories.Interfaces;
using Crystalis.Services;
using Crystalis.Services.Interfaces;
using Crystalis.Sieves;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Sieve.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<IWorldService, WorldService>();
//Repositories
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IWorldRepository, WorldRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
//Autovalidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    // Disable the built-in .NET model (data annotations) validation.
    configuration.DisableBuiltInModelValidation = true;

    // Enable validation for parameters bound from `BindingSource.Path` binding sources.
    configuration.EnablePathBindingSourceAutomaticValidation = true;

    // Only validate controllers decorated with the `FluentValidationAutoValidation` attribute.
    configuration.ValidationStrategy = ValidationStrategy.All;
});
//Misc
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
//Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
JwtSection? jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSection>();
if (jwtSettings != null)
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminAccess", policy =>
        policy.Requirements.Add(new RoleHierarchyRequirement("Admin")));

    options.AddPolicy("GameMasterAccess", policy =>
        policy.Requirements.Add(new RoleHierarchyRequirement("GameMaster")));

    options.AddPolicy("PlayerAccess", policy =>
        policy.Requirements.Add(new RoleHierarchyRequirement("Player")));
});
builder.Services.AddSingleton<IAuthorizationHandler, RoleHierarchyHandler>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
    using (DataContext? context = scope.ServiceProvider.GetService<DataContext>())
    {
        context?.Database.EnsureDeletedAsync();
        context?.Database.EnsureCreatedAsync();

        context?.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
        context?.Roles.Add(new IdentityRole { Name = "GameMaster", NormalizedName = "GAMEMASTER" });
        context?.Roles.Add(new IdentityRole { Name = "Player", NormalizedName = "PLAYER" });

        await context?.SaveChangesAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();