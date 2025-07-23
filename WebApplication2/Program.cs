using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Hubs;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
          .WithOrigins("http://localhost:5173", "http://localhost:4173", "http://localhost:3000") // <-- your frontend address
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials(); // <-- REQUIRED for SignalR cross-origin
    });
});

builder.Services.AddResponseCaching();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ApplicationSettingsService>();
builder.Services.AddScoped<TranslationService>();
builder.Services.AddScoped<ModuleService>();
builder.Services.AddScoped<NavigationGroupService>();
builder.Services.AddScoped<PagesService>();
builder.Services.AddScoped<DataSourceService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PagePermissionsService>();
builder.Services.AddScoped<NavigationGroupPermissionsService>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetUsersQueryHandler).Assembly));
builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var jwtConfig = builder.Configuration.GetSection("Jwt");
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
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

app.UseResponseCaching();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors();

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
    }
    else
    {
        await next();
    }
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<PagesHub>("/pageshub");

app.Run();
