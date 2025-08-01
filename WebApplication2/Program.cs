using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication2.Buissniss.User.Queries.GetUsersQuery;
using WebApplication2.Hubs;
using WebApplication2.Models;
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
builder.Services.AddScoped<DataSourceService>();
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Users.Any())
    {
        db.Users.Add(new UserClass
        {
            Username = "admin",
            Email = "admin@example.com",
            Is_System = true,
            Password = PasswordHelper.HashPassword("admin123")
        });
        db.SaveChanges();
    }

    if (!db.Languages.Any())
    {
        db.Languages.Add(new LanguageSetting
        {
            Value = "en-GB",
            Display_Value = "s:english",
        });

        db.Languages.Add(new LanguageSetting
        {
            Value = "sl",
            Display_Value = "s:slovenian",
        });

        db.SaveChanges();
    }

    if (!db.DateTimeFormats.Any())
    {
        db.DateTimeFormats.Add(new DateTimeFormatSetting
        {
            Value = "d.MM.yyyy HH:mm:ss",
            Display_Value = "d.MM.yyyy HH:mm:ss",
        });

        db.DateTimeFormats.Add(new DateTimeFormatSetting
        {
            Value = "dd.MM.yyyy HH:mm:ss",
            Display_Value = "dd.MM.yyyy HH:mm:ss",
        });

        db.DateTimeFormats.Add(new DateTimeFormatSetting
        {
            Value = "d/MM/yyyy HH:mm:ss",
            Display_Value = "d/MM/yyyy HH:mm:ss",
        });

        db.DateTimeFormats.Add(new DateTimeFormatSetting
        {
            Value = "dd/MM/yyyy HH:mm:ss",
            Display_Value = "dd/MM/yyyy HH:mm:ss",
        });

        db.DateTimeFormats.Add(new DateTimeFormatSetting
        {
            Value = "MM/dd/yyyy HH:mm:ss",
            Display_Value = "MM/dd/yyyy HH:mm:ss",
        });

        db.DateTimeFormats.Add(new DateTimeFormatSetting
        {
            Value = "ddMMM.yyyy HH:mm:ss",
            Display_Value = "ddMMMyyyy HH:mm:ss",
        });
        db.SaveChanges();
    }

    if (!db.DecimalSeparators.Any())
    {
        db.DecimalSeparators.Add(new DecimalSeparatorSetting
        {
            Value = ".",
            Display_Value = "s:dot",
        });

        db.DecimalSeparators.Add(new DecimalSeparatorSetting
        {
            Value = ",",
            Display_Value = "s:comma",
        });

        db.SaveChanges();
    }

    if (!db.Modules.Any())
    {
        db.Modules.Add(new ModuleClass
        {
            Name = "user-management",
            Module_Path = "/user-management/remoteEntry.js",
        });

        db.Modules.Add(new ModuleClass
        {
            Name = "role-management",
            Module_Path = "/role-management/remoteEntry.js",
        });

        db.Modules.Add(new ModuleClass
        {
            Name = "utl",
            Module_Path = "/utl/remoteEntry.js",
        });

        db.SaveChanges();
    }

    if (!db.NavigationGroups.Any())
    {
        db.NavigationGroups.Add(new NavigationGroupClass
        {
            Name = "s:applicationManagement",
            Icon = "applicationManagement.svg"
        });

        db.NavigationGroups.Add(new NavigationGroupClass
        {
            Name = "s:usersAndRoles",
            Icon = "usersAndRoles.svg",
            ID_parent_group = 1
        });

        db.NavigationGroups.Add(new NavigationGroupClass
        {
            Name = "s:configuration",
            Icon = "usersAndRoles.svg"
        });

        db.NavigationGroups.Add(new NavigationGroupClass
        {
            Name = "s:general",
            Icon = "usersAndRoles.svg",
            ID_parent_group = 3
        });

        db.SaveChanges();
    }

    if (!db.Pages.Any())
    {
        db.Pages.Add(new PageClass
        {
            Name = "s:usersManagement",
            Icon = "usersManagement.svg",
            Path = "/userManagement/users",
            Component_Name = "./UsersManagement",
            ID_module = 1,
            ID_group = 2
        });

        db.Pages.Add(new PageClass
        {
            Name = "s:rolesManagement",
            Icon = "rolesManagement.svg",
            Path = "/rolesManagement/users",
            Component_Name = "./RolesManagement",
            ID_module = 2,
            ID_group = 2
        });

        db.Pages.Add(new PageClass
        {
            Name = "s:mySettings",
            Icon = "userProfile.svg",
            Path = "/profile",
            Component_Name = "./Profile",
            ID_module = 1
        });

        db.Pages.Add(new PageClass
        {
            Name = "s:applicationSettings",
            Icon = "applicationManagement.svg",
            Path = "/applicationSettings",
            Component_Name = "ApplicationSettings",
            ID_group = 1
        });

        db.Pages.Add(new PageClass
        {
            Name = "s:utility",
            Icon = "applicationManagement.svg",
            Path = "/configuration/utility",
            Component_Name = "./Utility",
            ID_group = 4,
            ID_module = 3
        });

        db.SaveChanges();
    }

    if (!db.ApplicationSettings.Any())
    {
        db.ApplicationSettings.Add(new ApplicationSettingsClass
        {
            ID_date_time_format = 1,
            ID_separator = 1,
            ID_language = 1,
            System = true,
            Use_Strong_Password = true
        });

        db.SaveChanges();
    }

    if (!db.DataSources.Any())
    {
        db.DataSources.Add(new DataSourceClass
        {
            Name = "utlEnumSets",
            Path = "/utl/enumsets",
            Method = "GET",
        });

        db.DataSources.Add(new DataSourceClass
        {
            Name = "utlEnumValues",
            Path = "/utl/enumvalues",
            Method = "GET",
        });

        db.DataSources.Add(new DataSourceClass
        {
            Name = "utlEnumSetsEdit",
            Path = "/utl/enumsets/edit",
            Method = "GET",
        });

        db.DataSources.Add(new DataSourceClass
        {
            Name = "utlEnumValuesEdit",
            Path = "/utl/enumvalues/edit",
            Method = "GET",
        });

        db.DataSources.Add(new DataSourceClass
        {
            Name = "utlModuleActions",
            Path = "/utl/module/actions",
            Method = "GET",
        });

        db.DataSources.Add(new DataSourceClass
        {
            Name = "utlModuleActionForms",
            Path = "/utl/module/forms",
            Method = "POST",
        });

        db.DataSources.Add(new DataSourceClass
        {
            Name = "utlEnumGroupsProd",
            Path = "/utl/enumgroups/prod",
            Method = "GET",
        });

        db.DataSources.Add(new DataSourceClass
        {
            Name = "utlRecordStatus",
            Path = "/utl/recordstatus",
            Method = "GET",
        });

        db.DataSources.Add(new DataSourceClass
        {
            Name = "userModuleActionForms",
            Path = "/users/forms",
            Method = "POST",
        });

        db.DataSources.Add(new DataSourceClass
        {
            Name = "coreModuleActionForms",
            Path = "/utl/forms",
            Method = "POST",
        });

        db.SaveChanges();
    }

}

app.Run();
