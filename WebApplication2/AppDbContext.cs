using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebApplication2.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserClass> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RoleClass> Roles { get; set; }
    public DbSet<PageClass> Pages { get; set; }
    public DbSet<NavigationGroupClass> NavigationGroups { get; set; }
    public DbSet<PagePermissionClass> PagePermissions { get; set; }
    public DbSet<NavigationGroupPermissionsClass> NavigationGroupPermissions { get; set; }
    public DbSet<ModuleClass> Modules { get; set; }
    public DbSet<ApplicationSettingsClass> ApplicationSettings { get; set; }
    public DbSet<LanguageSetting> Languages { get; set; }
    public DbSet<DateTimeFormatSetting> DateTimeFormats { get; set; }
    public DbSet<DecimalSeparatorSetting> DecimalSeparators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<System.Reflection.CustomAttributeData>();
        modelBuilder.HasDefaultSchema("usr");

        modelBuilder.Entity<UserRole>().ToTable("User_Role", "usr");

        modelBuilder.Entity<RoleClass>().ToTable("Role", "role");

        modelBuilder.Entity<RoleClass>()
        .HasOne(p => p.HomePage)
        .WithMany() // or .WithMany(g => g.Pages) if you have that property
        .HasForeignKey(p => p.ID_home_page)
        .IsRequired(false); // optional relationship

        //modelBuilder.Entity<ApplicationSettingsClass>().ToTable("Application_Setting", "utl");
        modelBuilder.Entity<UserClass>().ToTable("User", "usr");
        modelBuilder.Entity<UserClass>()
        .HasOne(p => p.Settings)
        .WithMany()
        .HasForeignKey(p => p.ID_Setting)
        .IsRequired(false);

        modelBuilder.Entity<PageClass>()
            .ToTable("Page", "utl");

        // Configure relationship to Group
        modelBuilder.Entity<PageClass>()
            .HasOne(p => p.Group)
            .WithMany() // or .WithMany(g => g.Pages) if you have that property
            .HasForeignKey(p => p.ID_group)
            .IsRequired(false); // optional relationship

        // Configure relationship to Module (if Module is another navigation property)
        modelBuilder.Entity<PageClass>()
            .HasOne(p => p.Module)
            .WithMany() // or .WithMany(m => m.Pages)
            .HasForeignKey(p => p.ID_module)
            .IsRequired(false); // optional if ID_module is nullable



        modelBuilder.Entity<NavigationGroupClass>().ToTable("Navigation_Group", "utl");
        modelBuilder.Entity<NavigationGroupClass>()
            .HasOne(p => p.ParentGroup)
            .WithMany() // or .WithMany(g => g.Pages) if you have that property
            .HasForeignKey(p => p.ID_parent_group)
            .IsRequired(false); // optional relationship

        modelBuilder.Entity<NavigationGroupPermissionsClass>().ToTable("Role_Group", "role");
        modelBuilder.Entity<NavigationGroupPermissionsClass>()
            .HasOne(p => p.Group)
            .WithMany()
            .HasForeignKey(p => p.ID_navigation_group);
        modelBuilder.Entity<NavigationGroupPermissionsClass>()
            .HasOne(p => p.Role)
            .WithMany()
            .HasForeignKey(p => p.ID_role);

        modelBuilder.Entity<PagePermissionClass>().ToTable("Role_Page", "role");
        modelBuilder.Entity<PagePermissionClass>()
            .HasOne(p => p.Page)
            .WithMany()
            .HasForeignKey(p => p.ID_page);
        modelBuilder.Entity<PagePermissionClass>()
            .HasOne(p => p.Role)
            .WithMany()
            .HasForeignKey(p => p.ID_role);

        modelBuilder.Entity<ModuleClass>().ToTable("Module", "utl");

        modelBuilder.Entity<ApplicationSettingsClass>().ToTable("Application_Setting", "utl");
        modelBuilder.Entity<ApplicationSettingsClass>()
        .HasOne(p => p.Language)
        .WithMany()
        .HasForeignKey(p => p.ID_language)
        .IsRequired(false);


        modelBuilder.Entity<ApplicationSettingsClass>()
        .HasOne(p => p.DateTimeFormat)
        .WithMany()
        .HasForeignKey(p => p.ID_date_time_format)
        .IsRequired(false);

        modelBuilder.Entity<ApplicationSettingsClass>()
        .HasOne(p => p.DecimalSeparator)
        .WithMany()
        .HasForeignKey(p => p.ID_separator)
        .IsRequired(false);

        modelBuilder.Entity<LanguageSetting>().ToTable("Language", "utl");
        modelBuilder.Entity<DateTimeFormatSetting>().ToTable("Date_Time_Format", "utl");
        modelBuilder.Entity<DecimalSeparatorSetting>().ToTable("Decimal_Separator", "utl");
    }
}
