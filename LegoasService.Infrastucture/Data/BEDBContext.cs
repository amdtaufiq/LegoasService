using LegoasService.Core.DAOs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegoasService.Infrastucture.Data
{
    public class BEDBContext : DbContext
    {
        public BEDBContext(DbContextOptions<BEDBContext> options) : base(options)
        {

        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<MenuAccess> MenuAccesses { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<OfficerRole> OfficerRoles { get; set; }
        public DbSet<OfficerOffice> OfficerOffices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CreatedAt indentities
            foreach (var entity in modelBuilder.Model.GetEntityTypes()
                .Where(x =>
                    x.ClrType.GetProperties().Any(y =>
                        y.CustomAttributes.Any(z =>
                            z.AttributeType == typeof(DatabaseGeneratedAttribute)))))
            {
                foreach (var property in entity.ClrType.GetProperties()
                    .Where(x =>
                        x.PropertyType == typeof(DateTime) && x.CustomAttributes.Any(y =>
                            y.AttributeType == typeof(DatabaseGeneratedAttribute))))
                {
                    modelBuilder
                        .Entity(entity.ClrType)
                        .Property(property.Name)
                        .HasDefaultValueSql("GETUTCDATE()");
                }
            }

            // IsDelete identities
            foreach (var entity in modelBuilder.Model.GetEntityTypes()
                .Where(t =>
                    t.ClrType.GetProperties()
                        .Any(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(DatabaseGeneratedAttribute)))))
            {
                foreach (var property in entity.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(bool?) && p.CustomAttributes.Any(a => a.AttributeType == typeof(DatabaseGeneratedAttribute))))
                {
                    modelBuilder
                        .Entity(entity.ClrType)
                        .Property(property.Name)
                        .HasDefaultValue(false);
                }
            }
        }
    }
}
