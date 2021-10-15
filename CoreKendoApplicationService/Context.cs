using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using CoreKendoApplicationService.EntityModels;


namespace CoreKendoApplicationService
{
    internal class Context : DbContext
    {
        public Context() : base()
        {
        }

        // Uncomment below to read connection string from appsetting.json file.
        // Reading the file is cached and not expensive plus this is done though
        // dependency injection the preferred method for .Net Core framework.

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
        //            .AddJsonFile("appsettings.json", false)
        //            .Build();

        //        optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnectionName"));
        //    }
        //}

        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
    }
}
