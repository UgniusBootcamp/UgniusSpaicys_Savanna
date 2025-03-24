using System.Text;
using System.Text.Json;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SavannaApp.Api.Hubs;
using SavannaApp.Api.Infrastructure;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Business.Services;
using SavannaApp.Business.Services.Web;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Data;
using SavannaApp.Data.Entities.Auth;
using SavannaApp.Data.Helpers.Configuration;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Data.Helpers.Mapper;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Repo;
using SavannaApp.Data.Repositories;
using SavannaApp.Data.Responses;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var corsSettings = builder.Configuration.GetSection("Cors").Get<CorsSettings>();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(corsSettings!.AllowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddDbContext<SavannaDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Azure blob
builder.Services.Configure<AzureBlobServiceOptions>(builder.Configuration.GetSection("AzureBlob"));

//Add JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.Configure<ApiBehaviorOptions>(option =>
{
    option.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values
            .SelectMany(x => x.Errors.Select(e => e.ErrorMessage))
            .ToList();

        var response = ApiResponse.ErrorResponse(WebServiceConstants.ValidationError, errors);

        return new BadRequestObjectResult(response);
    };
});

//Exception
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();

//Services
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IGamesManager, GamesManager>();
builder.Services.AddScoped<AnimalTypeMapper>();
builder.Services.AddScoped<IAnimalCreationService, AnimalCreationService>();
builder.Services.AddScoped<IAnimalFactory, AnimalFactory>();
builder.Services.AddScoped<IAssemblyLoader, AssemblyLoader>();
builder.Services.AddTransient<HunterMovement>();
builder.Services.AddTransient<PrayMovement>();
builder.Services.AddScoped<IAnimalConfigurationService, AnimalConfigurationService>();
builder.Services.AddScoped<IAnimalConfigReader, JsonAnimalConfigurationReader>();
builder.Services.AddScoped<IGameCreationService, GameCreationService>();
builder.Services.AddScoped<IMapManager, MapManager>();
builder.Services.AddScoped<IGameUpdateInformer, GameUpdateInformer>();
builder.Services.AddScoped<DbSeeder>();
builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddScoped<IGameHubService, GameHubService>();
builder.Services.AddScoped<IDtoMapper, DtoMapper>();
builder.Services.AddSignalR();

//Add Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SavannaDbContext>()
    .AddDefaultTokenProviders();

//Add Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

    option.MapInboundClaims = false;
    option.TokenValidationParameters.ValidAudience = jwtSettings!.Audience;
    option.TokenValidationParameters.ValidIssuer = jwtSettings!.Issuer;
    option.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

    option.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var responseJson = JsonSerializer.Serialize(ApiResponse.UnauthorizedResponse(WebServiceConstants.Unauthorized));

            return context.Response.WriteAsync(responseJson);
        },

        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var responseJson = JsonSerializer.Serialize(ApiResponse.ForbiddenResponse(WebServiceConstants.Forbidden));

            return context.Response.WriteAsync(responseJson);
        },
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/game"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

//Seed database
using var scope = app.Services.CreateScope();
if (app.Environment.IsProduction())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SavannaDbContext>();
    dbContext.Database.Migrate();
}

var dbSeeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
await dbSeeder.SeedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.UseWebSockets();
app.MapHub<GameHub>("/game").RequireAuthorization();

app.Run();
