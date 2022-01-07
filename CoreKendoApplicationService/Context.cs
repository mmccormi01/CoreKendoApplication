using System;
using System.DirectoryServices.AccountManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CoreKendoApplicationService.EntityModels;
using System.IO;


namespace CoreKendoApplicationService
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                    .AddJsonFile("appsettings.json", false)
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("AppDatabaseConnection"));
            }
        }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<ResourceClass> ResourceClasses { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<DesignationStatus> DesignationStatuses { get; set; }
        public DbSet<ParentDistrict> ParentDistricts { get; set; }
        public DbSet<ParentSensitivityZone> ParentSensitivityZone { get; set; }
    }
}
